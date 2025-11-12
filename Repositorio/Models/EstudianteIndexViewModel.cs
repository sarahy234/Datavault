using System;
using System.Collections.Generic;

namespace Repositorio.Models.ViewModels
{
    public class RecursoResumenDto
    {
        public int Id_Recurso { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Autor { get; set; }
        public string Semestre { get; set; }
        public string Estado { get; set; }
        public DateTime Fecha_Subida { get; set; }
        public string MateriaPrincipal { get; set; } 
        public string TipoRecurso { get; set; }
        public string PalabrasClave { get; set; }
    }

    public class EstudianteIndexViewModel
    {
        public List<RecursoResumenDto> Recursos { get; set; } = new List<RecursoResumenDto>();

        // filtros disponibles
        public List<string> Materias { get; set; } = new List<string>();
        public List<string> Semestres { get; set; } = new List<string>();
        public List<string> Tipos { get; set; } = new List<string>();

        // selección de filtro + búsqueda + paginación
        public string Search { get; set; }
        public string SelectedMateria { get; set; }
        public string SelectedSemestre { get; set; }
        public string SelectedTipo { get; set; }
        public string SelectedAutor { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalItems { get; set; }

        public string ProfileName { get; set; } = "Usuario";
        public string ProfileCorreo { get; set; } = "No disponible";
        public string ProfileCarrera { get; set; } = "N/D";
        public string ProfileSemestre { get; set; } = "N/D";
    }
}
