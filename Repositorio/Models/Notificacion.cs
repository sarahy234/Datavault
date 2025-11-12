using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repositorio.Models
{
    public class Notificacion
    {
        [Key]
        public int Id_Notificacion { get; set; }

        [Required, MaxLength(50)]
        public string Tipo { get; set; }

        [Required, MaxLength(255)]
        public string Mensaje { get; set; }

        public DateTime Fecha_Envio { get; set; } = DateTime.Now;

        public bool Leido { get; set; } = false;

        [Required]
        [ForeignKey("Usuario")]
        public int Id_Usuario { get; set; }
        public Usuario Usuario { get; set; }

        [ForeignKey("Recurso")]
        public int? Id_Recurso { get; set; }
        public Recurso Recurso { get; set; }
    }
}
