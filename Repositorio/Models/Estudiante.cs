using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repositorio.Models
{
    public class Estudiante
    {
        [Key]
        public int Id_Estudiante { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public int Id_Usuario { get; set; }
        public Usuario Usuario { get; set; }

        [Required, MaxLength(50)]
        public string Semestre { get; set; }

        [Required, MaxLength(50)]
        public string Carrera { get; set; }
    }
}
