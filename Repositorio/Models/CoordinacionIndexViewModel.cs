using System.Collections.Generic;
using Repositorio.Models;

namespace Repositorio.Models.ViewModels
{
    public class CoordinacionIndexViewModel
    {
        // Perfil
        public string ProfileName { get; set; }
        public string ProfileCorreo { get; set; }
        public string Cargo { get; set; }

        // Filtros
        public string Search { get; set; }
        public string SelectedMateria { get; set; }
        public string SelectedSemestre { get; set; }
        public string SelectedTipo { get; set; }
        public string SelectedAutor { get; set; }

        // Paginación
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }

        // Datos
        public List<string> Materias { get; set; } = new();
        public List<string> Tipos { get; set; } = new();
        public List<string> Semestres { get; set; } = new();
        public List<RecursoResumenDto> Recursos { get; set; } = new();
    }
}
