using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositorio.Models;
using Repositorio.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Repositorio.Controllers
{
    public class EstudianteController : Controller
    {
        private readonly RepositorioContext _context;

        public EstudianteController(RepositorioContext context)
        {
            _context = context;
        }

        // GET: /Estudiante/Index
        public async Task<IActionResult> Index(
            string search = "",
            string materia = "",
            string semestre = "",
            string tipo = "",
            string autor = "",
            int page = 1,
            int pageSize = 10)
        {
            try
            {
                // --- 0) Crear VM y rellenar con valores por defecto (evita nulls en la vista) ---
                var vm = new EstudianteIndexViewModel
                {
                    Search = search,
                    SelectedMateria = materia,
                    SelectedSemestre = semestre,
                    SelectedTipo = tipo,
                    SelectedAutor = autor,
                    Page = page,
                    PageSize = pageSize,
                    TotalItems = 0,
                    ProfileName = User?.Identity?.Name ?? "Usuario",
                    ProfileCorreo = "No disponible",
                    ProfileCarrera = "N/D",
                    ProfileSemestre = "N/D"
                };

                // --- 1) Intentar obtener Id_Usuario desde las claims (esto depende de que tu AccountController la agregue) ---
                var idClaim = User?.Claims.FirstOrDefault(c => c.Type == "Id_Usuario")?.Value;
                if (!string.IsNullOrEmpty(idClaim) && int.TryParse(idClaim, out int idUsuario))
                {
                    // Obtener fila Usuario
                    var usuario = await _context.Usuario
                        .AsNoTracking()
                        .FirstOrDefaultAsync(u => u.Id_Usuario == idUsuario);

                    if (usuario != null)
                    {
                        vm.ProfileCorreo = usuario.Correo ?? vm.ProfileCorreo;
                        // preferir nombre de la base si existe
                        vm.ProfileName = !string.IsNullOrEmpty(usuario.Nombre) ? usuario.Nombre : vm.ProfileName;
                    }

                    // Obtener fila Estudiante (si existe)
                    var estudiante = await _context.Estudiante
                        .AsNoTracking()
                        .FirstOrDefaultAsync(e => e.Id_Usuario == idUsuario);

                    if (estudiante != null)
                    {
                        vm.ProfileCarrera = estudiante.Carrera ?? vm.ProfileCarrera;
                        vm.ProfileSemestre = estudiante.Semestre ?? vm.ProfileSemestre;
                    }
                }
                // (si no hay claim, la vista usará los valores por defecto ya puestos)

                // --- 2) Construir la consulta de recursos (filtros / paginación) ---
                var baseQuery = _context.Recurso
                    .Include(r => r.Metadatos)
                    .Include(r => r.Docente)
                    .AsQueryable();

                // Solo recursos publicados/aprobados
                baseQuery = baseQuery.Where(r => r.Estado == "Publicado" || r.Estado == "Aprobado");

                // Aplicar filtros de búsqueda
                if (!string.IsNullOrWhiteSpace(search))
                {
                    var s = search.Trim();
                    baseQuery = baseQuery.Where(r =>
                        r.Titulo.Contains(s) ||
                        (r.Descripcion != null && r.Descripcion.Contains(s)) ||
                        r.Metadatos.Any(m => m.Palabras_Clave.Contains(s)) ||
                        r.Autor.Contains(s)
                    );
                }

                if (!string.IsNullOrWhiteSpace(materia))
                {
                    var m = materia.Trim();
                    baseQuery = baseQuery.Where(r => r.Metadatos.Any(md => md.Materia == m));
                }

                if (!string.IsNullOrWhiteSpace(semestre))
                {
                    var sem = semestre.Trim();
                    baseQuery = baseQuery.Where(r => r.Semestre != null && r.Semestre == sem);
                }

                if (!string.IsNullOrWhiteSpace(tipo))
                {
                    var t = tipo.Trim();
                    baseQuery = baseQuery.Where(r => r.Metadatos.Any(md => md.Tipo_Recurso == t));
                }

                if (!string.IsNullOrWhiteSpace(autor))
                {
                    var a = autor.Trim();
                    baseQuery = baseQuery.Where(r => r.Autor.Contains(a));
                }

                // Listas para filtros (distintas)
                var materiasList = await _context.Metadato.Select(m => m.Materia).Distinct().OrderBy(x => x).ToListAsync();
                var tiposList = await _context.Metadato.Select(m => m.Tipo_Recurso).Distinct().OrderBy(x => x).ToListAsync();
                var semestresList = await _context.Recurso
                    .Where(r => r.Semestre != null)
                    .Select(r => r.Semestre)
                    .Distinct()
                    .OrderBy(x => x)
                    .ToListAsync();

                // Paginación y resultados
                var totalItems = await baseQuery.CountAsync();
                var recursos = await baseQuery
                    .OrderByDescending(r => r.Fecha_Subida)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                // Proyección segura a DTO
                var recursosDto = recursos.Select(r =>
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
                        MateriaPrincipal = md?.Materia ?? "Sin asignar",
                        TipoRecurso = md?.Tipo_Recurso ?? "Sin asignar",
                        PalabrasClave = md?.Palabras_Clave ?? ""
                    };
                }).ToList();

                // --- 3) Rellenar VM con los datos de filtros/recursos ---
                vm.Recursos = recursosDto;
                vm.Materias = materiasList;
                vm.Tipos = tiposList;
                vm.Semestres = semestresList;
                vm.TotalItems = totalItems;

                return View(vm);
            }
            catch
            {
                // En caso de error devolvemos una VM vacía (no romper la vista)
                var emptyVm = new EstudianteIndexViewModel
                {
                    Recursos = new List<RecursoResumenDto>(),
                    Materias = new List<string>(),
                    Tipos = new List<string>(),
                    Semestres = new List<string>(),
                    Page = 1,
                    PageSize = pageSize,
                    TotalItems = 0,
                    ProfileName = User?.Identity?.Name ?? "Usuario",
                    ProfileCorreo = "No disponible",
                    ProfileCarrera = "N/D",
                    ProfileSemestre = "N/D"
                };
                return View(emptyVm);
            }
        }
    }
}
