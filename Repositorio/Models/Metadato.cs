using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repositorio.Models
{
    public class Metadato
    {
        [Key]
        public int Id_Metadato { get; set; }

        [Required, MaxLength(50)]
        public string Titulo { get; set; }

        [Required, MaxLength(80)]
        public string Materia { get; set; }

        [Required, MaxLength(100)]
        public string Palabras_Clave { get; set; }

        [Required, MaxLength(50)]
        public string Tipo_Recurso { get; set; }

        [Required]
        [ForeignKey("Recurso")]
        public int Id_Recurso { get; set; }
        public Recurso Recurso { get; set; }
    }
}
