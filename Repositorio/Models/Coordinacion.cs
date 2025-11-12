using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repositorio.Models
{
    public class Coordinacion
    {
        [Key]
        public int Id_Coordinacion { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public int Id_Usuario { get; set; }
        public Usuario Usuario { get; set; }

        [Required, MaxLength(100)]
        public string Cargo { get; set; }
    }
}
