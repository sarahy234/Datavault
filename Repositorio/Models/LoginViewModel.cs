using System.ComponentModel.DataAnnotations;

namespace Repositorio.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El nombre es requerido.")]
        [MinLength(5, ErrorMessage = "El nombre debe tener al menos 5 caracteres.")]
        [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Correo institucional es requerido.")]
        [EmailAddress(ErrorMessage = "Correo inválido.")]
        [RegularExpression(@"^[^\s@]+@est\.univalle\.edu$", ErrorMessage = "Formato: usuario@est.univalle.edu")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida.")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        [MaxLength(25, ErrorMessage = "La contraseña no puede exceder 25 caracteres.")]
        public string Contrasena { get; set; }

        [Required(ErrorMessage = "Selecciona un rol.")]
        public string Rol { get; set; }
    }
}
