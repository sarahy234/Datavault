using Repositorio.Models.ViewModels;
using System;
using System.Collections.Generic;

namespace Repositorio.Models
{
    public class GestorDetailsViewModel
    {
        public RecursoResumenDto Recurso { get; set; }
        public List<Metadato> Metadatos { get; set; } = new();
        public List<Version> Versiones { get; set; } = new();
        public List<Validacion> Validacion { get; set; } = new();

        // campo para el formulario de revisión
        public string Comentario { get; set; }
    }
}
