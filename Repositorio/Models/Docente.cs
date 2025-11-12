using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Repositorio.Models
{
    public class Docente
    {
        [Key]
        public int Id_Docente { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public int Id_Usuario { get; set; }
        public Usuario Usuario { get; set; }

        [MaxLength(100)]
        public string Departamento { get; set; }

        // Relaciones
        public ICollection<Recurso> Recursos { get; set; }
    }
}
