using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositorio.Models;
using Repositorio.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Repositorio.Controllers
    {
        public class CoordinacionController : Controller
        {
            private readonly RepositorioContext _context;

            public CoordinacionController(RepositorioContext context)
            {
                _context = context;
            }

            // ==============================
            // INDEX: Lista de recursos
            // ==============================
            public async Task<IActionResult> Index(
                string search = "",
                string materia = "",
                string semestre = "",
                string tipo = "",
                string autor = "",
                int page = 1,
                int pageSize = 10)
            {
                var vm = new CoordinacionIndexViewModel
                {
                    Search = search,
                    SelectedMateria = materia,
                    SelectedSemestre = semestre,
                    SelectedTipo = tipo,
                    SelectedAutor = autor,
                    Page = page,
                    PageSize = pageSize,
                    ProfileName = User?.Identity?.Name ?? "Coordinador",
                    ProfileCorreo = "No disponible",
                    Cargo = "Coordinador Académico"
                };

                // Obtener Id_Usuario desde claims (si está logueado)
                var idClaim = User?.Claims.FirstOrDefault(c => c.Type == "Id_Usuario")?.Value;
                if (!string.IsNullOrEmpty(idClaim) && int.TryParse(idClaim, out int idUsuario))
                {
                    var usuario = await _context.Usuario
                        .AsNoTracking()
                        .FirstOrDefaultAsync(u => u.Id_Usuario == idUsuario);

                    if (usuario != null)
                    {
                        vm.ProfileCorreo = usuario.Correo ?? vm.ProfileCorreo;
                        vm.ProfileName = !string.IsNullOrEmpty(usuario.Nombre) ? usuario.Nombre : vm.ProfileName;
                    }

                    var coord = await _context.Coordinacion
                        .AsNoTracking()
                        .FirstOrDefaultAsync(c => c.Id_Usuario == idUsuario);
                    if (coord != null)
                    {
                        vm.Cargo = coord.Cargo;
                    }
                }

                // Consulta base de recursos
                var query = _context.Recurso
                    .Include(r => r.Metadatos)
                    .Include(r => r.Docente)
                    .AsQueryable();

                // Filtros
                if (!string.IsNullOrWhiteSpace(search))
                    query = query.Where(r =>
                        r.Titulo.Contains(search) ||
                        (r.Descripcion ?? "").Contains(search) ||
                        r.Autor.Contains(search) ||
                        r.Metadatos.Any(m => m.Palabras_Clave.Contains(search))
                    );

                if (!string.IsNullOrWhiteSpace(materia))
                    query = query.Where(r => r.Metadatos.Any(m => m.Materia == materia));

                if (!string.IsNullOrWhiteSpace(semestre))
                    query = query.Where(r => r.Semestre == semestre);

                if (!string.IsNullOrWhiteSpace(tipo))
                    query = query.Where(r => r.Metadatos.Any(m => m.Tipo_Recurso == tipo));

                if (!string.IsNullOrWhiteSpace(autor))
                    query = query.Where(r => r.Autor.Contains(autor));

                // Obtener datos
                vm.TotalItems = await query.CountAsync();

                var recursos = await query
                    .OrderByDescending(r => r.Fecha_Subida)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                // Mapear a DTO
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

                // Datos para filtros
                vm.Materias = await _context.Metadato.Select(m => m.Materia).Distinct().OrderBy(x => x).ToListAsync();
                vm.Tipos = await _context.Metadato.Select(m => m.Tipo_Recurso).Distinct().OrderBy(x => x).ToListAsync();
                vm.Semestres = await _context.Recurso
                    .Where(r => r.Semestre != null)
                    .Select(r => r.Semestre)
                    .Distinct()
                    .OrderBy(x => x)
                    .ToListAsync();

                return View(vm);
            }

            // ==============================
            // EDITAR RECURSO (GET)
            // ==============================
            [HttpGet]
            public async Task<IActionResult> Editar(int id)
            {
                var recurso = await _context.Recurso
                    .Include(r => r.Metadatos)
                    .FirstOrDefaultAsync(r => r.Id_Recurso == id);

                if (recurso == null)
                    return NotFound();

                return View(recurso);
            }

            // ==============================
            // EDITAR RECURSO (POST)
            // ==============================
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Editar(Recurso recurso)
            {
                if (!ModelState.IsValid)
                    return View(recurso);

                try
                {
                    _context.Update(recurso);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Recurso actualizado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    TempData["ErrorMessage"] = "Error al actualizar el recurso.";
                    return View(recurso);
                }
            }

            // ==============================
            // CAMBIAR ESTADO (AJAX o POST)
            // ==============================
            [HttpPost]
            public async Task<IActionResult> CambiarEstado(int id, string nuevoEstado)
            {
                var recurso = await _context.Recurso.FindAsync(id);
                if (recurso == null)
                    return NotFound();

                recurso.Estado = nuevoEstado;
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
        }
    }
