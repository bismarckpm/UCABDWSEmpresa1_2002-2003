using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCAB.DTO
{
    public class RegistroDTO
    {
        [Required(ErrorMessage = "Introduzca un email")]
        [EmailAddress(ErrorMessage = "Introduzca un correo válido")]
        public string? Email {get; set;}

        [Required(ErrorMessage = "Introduzca la contraseña")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener minimo 8 caracteres")]
        public string? Password {get; set;}

        [Required(ErrorMessage = "Repita la contraseña")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        public string? confirmationpassword {get; set;}
    }
}