using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace Repositorio.Models
{
    public class Recurso
    {
        [Key]
        public int Id_Recurso { get; set; }

        [Required, MaxLength(150)]
        public string Titulo { get; set; }

        [MaxLength(255)]
        public string Descripcion { get; set; }

        [Required, MaxLength(50)]
        public string Autor { get; set; }

        [MaxLength(20)]
        public string Semestre { get; set; }

        [Range(0, 1048576)]
        public int? Tamano { get; set; }

        public DateTime Fecha_Subida { get; set; } = DateTime.Now;

        [MaxLength(50)]
        public string Estado { get; set; } = "Pendiente";

        [Required]
        [ForeignKey("Docente")]
        public int Id_Docente { get; set; }
        public Docente Docente { get; set; }

        // Relaciones
        public ICollection<Version> Versiones { get; set; }
        public ICollection<Metadato> Metadatos { get; set; }
        public ICollection<Validacion> Validacion { get; set; }
        public ICollection<Notificacion> Notificaciones { get; set; }
        public ICollection<Metrica> Metricas { get; set; }
    }
}
