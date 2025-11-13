using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace Repositorio.Models
{
    public class Gestor
    {
        [Key]
        public int Id_Gestor { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public int Id_Usuario { get; set; }
        public Usuario Usuario { get; set; }

        [Required, MaxLength(100)]
        public string Area_Responsable { get; set; }

        // Relaciones
        public ICollection<Validacion> Validacion { get; set; }
    }
}
