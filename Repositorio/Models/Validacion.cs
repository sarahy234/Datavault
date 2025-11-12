using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repositorio.Models
{
    public class Validacion
    {
        [Key]
        public int Id_Validacion { get; set; }

        [Required, MaxLength(50)]
        public string Resultado { get; set; }

        public string Comentario { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;

        [Required]
        [ForeignKey("Recurso")]
        public int Id_Recurso { get; set; }
        public Recurso Recurso { get; set; }

        [Required]
        [ForeignKey("Gestor")]
        public int Id_Gestor { get; set; }
        public Gestor Gestor { get; set; }
    }
}
