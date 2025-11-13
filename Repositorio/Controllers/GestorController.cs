using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositorio.Models;
using Repositorio.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// arriba del archivo, con los otros using
using Version = Repositorio.Models.Version;


namespace Repositorio.Controllers
{
    [Authorize(Roles = "Gestor")]
    public class GestorController : Controller
    {
        private readonly RepositorioContext _context;

        public GestorController(RepositorioContext context)
        {
            _context = context;
        }

        // GET: /Gestor
        public async Task<IActionResult> Index(
            string search = "",
            string materia = "",
            string semestre = "",
            string tipo = "",
            string autor = "",
            int page = 1,
            int pageSize = 10)
        {
            var vm = new GestorIndexViewModel
            {
                Search = search,
                SelectedMateria = materia,
                SelectedSemestre = semestre,
                SelectedTipo = tipo,
                SelectedAutor = autor,
                Page = page,
                PageSize = pageSize,
                ProfileName = User?.Identity?.Name ?? "Gestor",
                ProfileCorreo = User?.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Email)?.Value ?? "No disponible"
            };

            // Query: sólo recursos pendientes (cola de revisión)
            var q = _context.Recurso
                        .Include(r => r.Metadatos)
                        .Include(r => r.Docente)
                            .ThenInclude(d => d.Usuario)
                        .Where(r => r.Estado == "Pendiente")
                        .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                q = q.Where(r =>
                    r.Titulo.Contains(search) ||
                    (r.Descripcion ?? "").Contains(search) ||
                    r.Autor.Contains(search) ||
                    r.Metadatos.Any(m => m.Palabras_Clave.Contains(search))
                );

            if (!string.IsNullOrWhiteSpace(materia))
                q = q.Where(r => r.Metadatos.Any(m => m.Materia == materia));

            if (!string.IsNullOrWhiteSpace(semestre))
                q = q.Where(r => r.Semestre == semestre);

            if (!string.IsNullOrWhiteSpace(tipo))
                q = q.Where(r => r.Metadatos.Any(m => m.Tipo_Recurso == tipo));

            if (!string.IsNullOrWhiteSpace(autor))
                q = q.Where(r => r.Autor.Contains(autor));

            vm.TotalItems = await q.CountAsync();

            var recursos = await q
                .OrderByDescending(r => r.Fecha_Subida)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            vm.Recursos = recursos.Select(r =>
            {
                var md = r.Metadatos.FirstOrDefault();
                return new RecursoResumenDto
                {
                    Id_Recurso = r.Id_Recurso,
                    Titulo = r.Titulo,
                    Descripcion = r.Descripcion,
                    Autor = r.Autor,
                    Semestre = r.Semestre,
                    Estado = r.Estado,
                    Fecha_Subida = r.Fecha_Subida,
                    MateriaPrincipal = md?.Materia ?? "Sin materia",
                    TipoRecurso = md?.Tipo_Recurso ?? "N/D",
                    PalabrasClave = md?.Palabras_Clave ?? ""
                };
            }).ToList();

            vm.Materias = await _context.Metadato.Select(m => m.Materia).Distinct().OrderBy(x => x).ToListAsync();
            vm.Tipos = await _context.Metadato.Select(m => m.Tipo_Recurso).Distinct().OrderBy(x => x).ToListAsync();
            vm.Semestres = await _context.Recurso.Where(r => r.Semestre != null).Select(r => r.Semestre).Distinct().OrderBy(x => x).ToListAsync();

            return View(vm);
        }

        // GET: /Gestor/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var recurso = await _context.Recurso
                .Include(r => r.Metadatos)
                .Include(r => r.Versiones)
                .Include(r => r.Validacion)
                .Include(r => r.Docente)
                    .ThenInclude(d => d.Usuario)
                .FirstOrDefaultAsync(r => r.Id_Recurso == id);

            if (recurso == null) return NotFound();

            var vm = new GestorDetailsViewModel
            {
                Recurso = new RecursoResumenDto
                {
                    Id_Recurso = recurso.Id_Recurso,
                    Titulo = recurso.Titulo,
                    Descripcion = recurso.Descripcion,
                    Autor = recurso.Autor,
                    Semestre = recurso.Semestre,
                    Estado = recurso.Estado,
                    Fecha_Subida = recurso.Fecha_Subida,
                    MateriaPrincipal = recurso.Metadatos.FirstOrDefault()?.Materia ?? "Sin materia",
                    TipoRecurso = recurso.Metadatos.FirstOrDefault()?.Tipo_Recurso ?? "N/D",
                    PalabrasClave = recurso.Metadatos.FirstOrDefault()?.Palabras_Clave ?? ""
                },
                Metadatos = recurso.Metadatos?.ToList() ?? new List<Metadato>(),
                Versiones = recurso.Versiones?.ToList() ?? new List<Version>(),
                Validacion = recurso.Validacion?.ToList() ?? new List<Validacion>()
            };

            return View(vm);
        }

        // POST: /Gestor/Aprobar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Aprobar(int id, string comentario)
        {
            var recurso = await _context.Recurso.Include(r => r.Docente).ThenInclude(d => d.Usuario).FirstOrDefaultAsync(r => r.Id_Recurso == id);
            if (recurso == null) return NotFound();

            // 1) Cambia estado a Publicado (según tu requisito)
            recurso.Estado = "Publicado";

            // 2) Guardar validacion
            var idGestor = await GetCurrentGestorId();
            var validacion = new Validacion
            {
                Resultado = "Aprobado",
                Comentario = string.IsNullOrWhiteSpace(comentario) ? "Aprobado por gestor" : comentario,
                Fecha = DateTime.Now,
                Id_Recurso = recurso.Id_Recurso,
                Id_Gestor = idGestor
            };
            _context.Validacion.Add(validacion);

            // 3) Notificar al autor (docente -> usuario)
            await NotifyAuthor(recurso, $"Tu recurso \"{recurso.Titulo}\" ha sido aprobado y publicado.");

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Recurso aprobado y publicado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        // POST: /Gestor/Rechazar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Rechazar(int id, string comentario)
        {
            var recurso = await _context.Recurso.Include(r => r.Docente).ThenInclude(d => d.Usuario).FirstOrDefaultAsync(r => r.Id_Recurso == id);
            if (recurso == null) return NotFound();

            // Dejamos en Pendiente (para que docente corrija)
            recurso.Estado = "Pendiente";

            var idGestor = await GetCurrentGestorId();
            var validacion = new Validacion
            {
                Resultado = "Rechazado",
                Comentario = string.IsNullOrWhiteSpace(comentario) ? "Rechazado por gestor" : comentario,
                Fecha = DateTime.Now,
                Id_Recurso = recurso.Id_Recurso,
                Id_Gestor = idGestor
            };
            _context.Validacion.Add(validacion);

            await NotifyAuthor(recurso, $"Tu recurso \"{recurso.Titulo}\" fue revisado: {validacion.Comentario}");

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Recurso marcado como pendiente con observaciones.";
            return RedirectToAction(nameof(Index));
        }

        // POST: /Gestor/SolicitarCorreccion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SolicitarCorreccion(int id, string comentario)
        {
            var recurso = await _context.Recurso.Include(r => r.Docente).ThenInclude(d => d.Usuario).FirstOrDefaultAsync(r => r.Id_Recurso == id);
            if (recurso == null) return NotFound();

            recurso.Estado = "Pendiente";

            var idGestor = await GetCurrentGestorId();
            var validacion = new Validacion
            {
                Resultado = "Pendiente",
                Comentario = string.IsNullOrWhiteSpace(comentario) ? "Se requieren correcciones." : comentario,
                Fecha = DateTime.Now,
                Id_Recurso = recurso.Id_Recurso,
                Id_Gestor = idGestor
            };
            _context.Validacion.Add(validacion);

            await NotifyAuthor(recurso, $"Tu recurso \"{recurso.Titulo}\" requiere correcciones: {validacion.Comentario}");

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Solicitud de corrección enviada al autor."; 
            return RedirectToAction(nameof(Index));
        }

        // -----------------------
        // Helpers
        // -----------------------
        private async Task<int> GetCurrentGestorId()
        {
            var idClaim = User?.Claims.FirstOrDefault(c => c.Type == "Id_Usuario")?.Value;
            if (!string.IsNullOrEmpty(idClaim) && int.TryParse(idClaim, out int idUsuario))
            {
                var gestor = await _context.Gestor.AsNoTracking().FirstOrDefaultAsync(g => g.Id_Usuario == idUsuario);
                if (gestor != null) return gestor.Id_Gestor;
            }
            return 0;
        }

        private async Task NotifyAuthor(Recurso recurso, string mensaje)
        {
            try
            {
                int? idAutorUsuario = null;
                if (recurso.Docente != null && recurso.Docente.Id_Usuario > 0)
                    idAutorUsuario = recurso.Docente.Id_Usuario;
                else
                {
                    var doc = await _context.Docente.Include(d => d.Usuario).FirstOrDefaultAsync(d => d.Id_Docente == recurso.Id_Docente);
                    if (doc != null) idAutorUsuario = doc.Id_Usuario;
                }

                if (idAutorUsuario.HasValue)
                {
                    var not = new Notificacion
                    {
                        Tipo = "Revision",
                        Mensaje = mensaje,
                        Fecha_Envio = DateTime.Now,
                        Leido = false,
                        Id_Usuario = idAutorUsuario.Value,
                        Id_Recurso = recurso.Id_Recurso
                    };
                    _context.Notificacion.Add(not);
                }
            }
            catch
            {
                // No hacemos nada; no queremos volver el flujo dependiente de notificaciones
            }
        }

        // Llamar para notificar a todos los gestores (p. ej. cuando docente solicita revisión)
        private async Task NotifyAllGestores(string tipo, string mensaje, int? idRecurso = null)
        {
            var gestores = await _context.Gestor.Include(g => g.Usuario).ToListAsync();
            foreach (var g in gestores)
            {
                var not = new Notificacion
                {
                    Tipo = tipo,
                    Mensaje = mensaje,
                    Fecha_Envio = DateTime.Now,
                    Leido = false,
                    Id_Usuario = g.Id_Usuario,
                    Id_Recurso = idRecurso
                };
                _context.Notificacion.Add(not);
            }
            await _context.SaveChangesAsync();
        }
    }
}
