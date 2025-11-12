using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repositorio.Models
{
    public class Metrica
    {
        [Key]
        public int Id_Metrica { get; set; }

        [Required, MaxLength(50)]
        public string Tipo_Evento { get; set; }

        public DateTime Fecha_Evento { get; set; } = DateTime.Now;

        [Required]
        [ForeignKey("Recurso")]
        public int Id_Recurso { get; set; }
        public Recurso Recurso { get; set; }
    }
}
