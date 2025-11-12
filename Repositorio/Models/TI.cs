using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repositorio.Models
{
    public class TI
    {
        [Key]
        public int Id_TI { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public int Id_Usuario { get; set; }
        public Usuario Usuario { get; set; }

        [Required, MaxLength(100)]
        public string Especialidad { get; set; }
    }
}
