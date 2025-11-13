using System.Collections.Generic;
using Repositorio.Models.ViewModels;

namespace Repositorio.Models
{
    public class GestorIndexViewModel
    {
        // Perfil
        public string ProfileName { get; set; } = "Gestor";
        public string ProfileCorreo { get; set; } = "No disponible";
        public string AreaResponsable { get; set; } = "Gestión";

        // Filtros / búsqueda / paginación
        public string Search { get; set; }
        public string SelectedMateria { get; set; }
        public string SelectedSemestre { get; set; }
        public string SelectedTipo { get; set; }
        public string SelectedAutor { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalItems { get; set; }

        // Datos
        public List<string> Materias { get; set; } = new();
        public List<string> Tipos { get; set; } = new();
        public List<string> Semestres { get; set; } = new();
        public List<RecursoResumenDto> Recursos { get; set; } = new();
    }
}
