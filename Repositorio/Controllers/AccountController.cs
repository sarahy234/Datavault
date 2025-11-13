using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositorio.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

namespace Repositorio.Controllers
{
    public class AccountController : Controller
    {
        private readonly RepositorioContext _context;
        private readonly PasswordHasher<Usuario> _passwordHasher;

        public AccountController(RepositorioContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Usuario>();
        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var fieldErrors = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            // Normalizar / limpiar espacios
            model.Correo = (model.Correo ?? "").Trim();
            model.Nombre = (model.Nombre ?? "").Trim();
            model.Rol = (model.Rol ?? "").Trim();

            // Validacion básicas (aparecerán debajo de cada input)
            if (string.IsNullOrWhiteSpace(model.Correo))
                fieldErrors["Correo"] = "El correo es obligatorio.";

            if (string.IsNullOrWhiteSpace(model.Contrasena))
                fieldErrors["Contrasena"] = "La contraseña es obligatoria.";

            if (string.IsNullOrWhiteSpace(model.Rol))
                fieldErrors["Rol"] = "Selecciona un rol.";

            if (fieldErrors.Count > 0)
            {
                ViewBag.FieldErrors = fieldErrors;
                return View(model);
            }

            // Buscar usuario por correo (incluimos Estudiante para poder añadir claims de carrera/semestre)
            var usuario = await _context.Usuario
                .Include(u => u.Estudiante) // carga navegación Estudiante si existe
                .SingleOrDefaultAsync(u => u.Correo.ToLower() == model.Correo.ToLower());

            if (usuario == null)
            {
                fieldErrors["Correo"] = "No existe una cuenta con ese correo.";
                ViewBag.FieldErrors = fieldErrors;
                return View(model);
            }

            // --- validar que el NOMBRE ingresado coincida con el nombre de la cuenta ---
            // Normalizamos y comparamos case-insensitive
            var nombreCuenta = (usuario.Nombre ?? "").Trim();
            var nombreIngresado = (model.Nombre ?? "").Trim();

            if (!string.Equals(nombreCuenta, nombreIngresado, StringComparison.OrdinalIgnoreCase))
            {
                fieldErrors["Nombre"] = "El nombre no coincide con la cuenta asociada a ese correo.";
                ViewBag.FieldErrors = fieldErrors;
                return View(model);
            }

            // Verificar que el rol seleccionado coincida con el rol de la cuenta
            if (!string.Equals(usuario.Rol ?? "", model.Rol ?? "", StringComparison.OrdinalIgnoreCase))
            {
                fieldErrors["Rol"] = "El rol seleccionado no coincide con la cuenta.";
                ViewBag.FieldErrors = fieldErrors;
                return View(model);
            }

            // Revisar bloqueo
            if (usuario.BloqueadoHasta.HasValue && usuario.BloqueadoHasta.Value > DateTime.Now)
            {
                var remaining = usuario.BloqueadoHasta.Value - DateTime.Now;
                ViewBag.LockRemainingSeconds = (int)Math.Ceiling(remaining.TotalSeconds);
                ViewBag.LockMessage = $"Cuenta bloqueada por seguridad. Intenta de nuevo en {remaining.Minutes:D2}:{remaining.Seconds:D2}.";
                return View(model);
            }

            // Verificar contraseña
            var verifyResult = _passwordHasher.VerifyHashedPassword(usuario, usuario.Contrasena, model.Contrasena);

            if (verifyResult == PasswordVerificationResult.Failed)
            {
                // Contraseña incorrecta -> incrementar contador
                usuario.IntentosFallidos++;

                if (usuario.IntentosFallidos >= 5)
                {
                    // Bloquear por 5 minutos
                    usuario.BloqueadoHasta = DateTime.Now.AddMinutes(5);
                    usuario.IntentosFallidos = 0;
                    await _context.SaveChangesAsync();

                    var remaining = usuario.BloqueadoHasta.Value - DateTime.Now;
                    ViewBag.LockRemainingSeconds = (int)Math.Ceiling(remaining.TotalSeconds);
                    ViewBag.LockMessage = "La cuenta fue bloqueada por 5 minutos debido a múltiples intentos fallidos.";
                    fieldErrors["Contrasena"] = "Contraseña incorrecta. La cuenta ha sido bloqueada por 5 minutos.";
                    ViewBag.FieldErrors = fieldErrors;
                    return View(model);
                }
                else
                {
                    await _context.SaveChangesAsync();
                    int restantes = 5 - usuario.IntentosFallidos;
                    fieldErrors["Contrasena"] = $"Contraseña incorrecta. Intentos restantes: {restantes}.";
                    ViewBag.FieldErrors = fieldErrors;
                    return View(model);
                }
            }

            // Contraseña correcta -> resetear contadores y loguear
            usuario.IntentosFallidos = 0;
            usuario.BloqueadoHasta = null;
            await _context.SaveChangesAsync();

            // Crear claims y firmar cookie (añadimos claims de Estudiante si existen)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Email, usuario.Correo),
                new Claim(ClaimTypes.Role, usuario.Rol),
                new Claim("Id_Usuario", usuario.Id_Usuario.ToString())
            };

            if (usuario.Estudiante != null)
            {
                if (!string.IsNullOrWhiteSpace(usuario.Estudiante.Carrera))
                    claims.Add(new Claim("Carrera", usuario.Estudiante.Carrera));
                if (!string.IsNullOrWhiteSpace(usuario.Estudiante.Semestre))
                    claims.Add(new Claim("Semestre", usuario.Estudiante.Semestre));
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
                });

            // Redirección según rol
            return usuario.Rol switch
            {
                "Estudiante" => RedirectToAction("Index", "Estudiante"),
                "Docente" => RedirectToAction("Index", "Docente"),
                "Gestor" => RedirectToAction("Index", "Gestor"),
                "Coordinacion" => RedirectToAction("Index", "Coordinacion"),
                "TI" => RedirectToAction("Index", "TI"),
                _ => RedirectToAction("Index", "Home")
            };
        }

        // GET: /Account/Signup
        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }


        // POST: /Account/Signup
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signup(
            [FromForm] string suName,
            [FromForm] string suEmail,
            [FromForm] string suPassword,
            [FromForm] string suConfirm,
            [FromForm] string suRole,
            [FromForm] bool suTerms
        )
        {
            var fieldErrors = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            // Normalizar y quitar espacios (adelante/atrás y en medio)
            suName = (suName ?? "").Replace(" ", "");
            suEmail = (suEmail ?? "").Replace(" ", "");

            ViewData["suName"] = suName;
            ViewData["suEmail"] = suEmail;
            ViewData["suRole"] = suRole ?? "";
            ViewData["suTerms"] = suTerms ? "true" : "false";

            // ---------- Validacion DEL NOMBRE (NUEVAS) ----------
            if (string.IsNullOrWhiteSpace(suName))
            {
                fieldErrors["su-name"] = "El nombre es obligatorio.";
            }
            else if (suName.Length < 5)
            {
                fieldErrors["su-name"] = "El nombre debe tener al menos 5 caracteres.";
            }
            else if (suName.Length > 30)
            {
                fieldErrors["su-name"] = "El nombre no puede tener más de 30 caracteres.";
            }
            else
            {
                var namePattern = @"^[A-Za-zÁÉÍÓÚáéíóúÑñ]+$"; 
                if (!System.Text.RegularExpressions.Regex.IsMatch(suName, namePattern))
                {
                    fieldErrors["su-name"] = "El nombre solo puede contener letras (sin números ni símbolos).";
                }
            }

            // Validacion OBLIGATORIAS (resto)
            if (string.IsNullOrWhiteSpace(suEmail))
                fieldErrors["su-email"] = "El correo institucional es obligatorio.";

            if (string.IsNullOrEmpty(suRole))
                fieldErrors["su-role"] = "Selecciona un rol.";

            if (!suTerms)
                fieldErrors["su-terms"] = "Debes aceptar los términos y condiciones.";

            if (string.IsNullOrEmpty(suPassword))
                fieldErrors["su-password"] = "La contraseña es obligatoria.";
            else if (suPassword != suConfirm)
                fieldErrors["su-confirm"] = "Las contraseñas no coinciden.";

            // Validar formato correo institucional
            var emailPattern = @"^[A-Za-z0-9._%+\-]+@est\.univalle\.edu$";
            if (!string.IsNullOrWhiteSpace(suEmail))
            {
                var emailParts = suEmail.Split('@');
                if (emailParts.Length > 0)
                {
                    var usernamePart = emailParts[0];
                    if (usernamePart.Length < 4)
                    {
                        fieldErrors["su-email"] = "El inicio del correo debe tener al menos 4 caracteres.";
                    }
                    else if (usernamePart.Length > 30)
                    {
                        fieldErrors["su-email"] = "El inicio del correo no puede tener más de 30 caracteres.";
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(suEmail) &&
                !System.Text.RegularExpressions.Regex.IsMatch(suEmail, emailPattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                fieldErrors["su-email"] = "Correo inválido. Debe terminar en @est.univalle.edu";

            // Validar contraseña: mínimo 8, mayúscula, minúscula, número y símbolo
            var pwdPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$";
            if (!string.IsNullOrWhiteSpace(suPassword) && !System.Text.RegularExpressions.Regex.IsMatch(suPassword, pwdPattern))
                fieldErrors["su-password"] = "La contraseña debe tener 8+ caracteres, incluir mayúscula, minúscula, número y símbolo.";
            if (!string.IsNullOrWhiteSpace(suPassword) && suPassword.Length > 25)
            {
                fieldErrors["su-password"] = "La contraseña no puede tener más de 25 caracteres.";
            }

            // SI HAY ERRORES, ENVIARLOS A LA VISTA
            if (fieldErrors.Count > 0)
            {
                ViewBag.FieldErrors = fieldErrors;
                return View();
            }

            // Verificar que no exista usuario con ese correo
            if (await _context.Usuario.AnyAsync(u => u.Correo.ToLower() == suEmail.ToLower()))
            {
                fieldErrors["su-email"] = "Ya existe una cuenta con este correo.";
                ViewBag.FieldErrors = fieldErrors;
                return View();
            }

            // Si rol = Estudiante -> crear cuenta directamente
            if (string.Equals(suRole, "Estudiante", StringComparison.OrdinalIgnoreCase))
            {
                var nuevoUsuario = new Usuario
                {
                    Nombre = suName,
                    Correo = suEmail,
                    Rol = suRole,
                    Activo = true,
                    IntentosFallidos = 0,
                    BloqueadoHasta = null
                };

                var passwordHasher = new PasswordHasher<Usuario>();
                nuevoUsuario.Contrasena = passwordHasher.HashPassword(nuevoUsuario, suPassword);

                _context.Usuario.Add(nuevoUsuario);
                await _context.SaveChangesAsync();

                _context.Estudiante.Add(new Estudiante
                {
                    Id_Usuario = nuevoUsuario.Id_Usuario,
                    Semestre = "1",
                    Carrera = "Ingeniería de Sistemas"
                });
                await _context.SaveChangesAsync();

                ViewBag.Exito = "¡Registro exitoso! Tu cuenta de estudiante ha sido creada correctamente. Te redigiremos al Login";
                ViewBag.FieldErrors = null;
                return View();
            }
            else
            {
                // Roles distintos a estudiante -> enviar solicitud
                ViewBag.Info = "Tu solicitud de creación de cuenta ha sido enviada. Comunicate con el Gestor.";
                ViewBag.FieldErrors = null;
                return View();
            }
        }

        // Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
