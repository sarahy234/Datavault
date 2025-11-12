using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace Repositorio.Models
{
    public class Usuario
    {
        [Key]
        public int Id_Usuario { get; set; }

        [Required, MaxLength(100)]
        public string Nombre { get; set; }

        [Required, EmailAddress, MaxLength(256)]
        public string Correo { get; set; }

        [Required, MinLength(8), MaxLength(50)]
        public string Contrasena { get; set; }

        [Required, MaxLength(50)]
        public string Rol { get; set; }

        public bool Activo { get; set; } = true;
        // ✅ Campos nuevos para control de seguridad:
        public int IntentosFallidos { get; set; } = 0;

        public DateTime? BloqueadoHasta { get; set; } = null;

        // Relaciones
        public Docente Docente { get; set; }
        public Estudiante Estudiante { get; set; }
        public Gestor Gestor { get; set; }
        public Coordinacion Coordinacion { get; set; }
        public TI TI { get; set; }
        public ICollection<Notificacion> Notificaciones { get; set; }
    }
}
