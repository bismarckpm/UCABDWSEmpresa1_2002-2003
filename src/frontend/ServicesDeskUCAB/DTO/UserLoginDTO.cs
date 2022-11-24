using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCAB.DTO
{
    public class UserLoginDTO
    {

        [Required(ErrorMessage = "Introduzca un email")]
        [EmailAddress(ErrorMessage = "Introduzca un correo válido")]
        public string? Email {get; set;}
        [Required(ErrorMessage = "Introduzca la contraseña")]
        public string? Password {get; set;}
    }
    
}