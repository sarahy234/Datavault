using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repositorio.Models
{
    public class Version
    {
        [Key]
        public int Id_Version { get; set; }

        [Range(1, int.MaxValue)]
        public int Numero_Version { get; set; }

        [Required, MaxLength(50)]
        public string Autor { get; set; }

        public DateTime Fecha { get; set; }

        [Required]
        [ForeignKey("Recurso")]
        public int Id_Recurso { get; set; }
        public Recurso Recurso { get; set; }

        [MaxLength(500)]
        public string Ruta_Archivo { get; set; }

    }
}
