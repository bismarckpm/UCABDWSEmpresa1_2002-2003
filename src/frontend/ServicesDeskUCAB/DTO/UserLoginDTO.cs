using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCAB.DTO
{
    public class UserLoginDTO
    {

        [Required(ErrorMessage = "Introduzca un email")]
        [EmailAddress(ErrorMessage = "Introduzca un correo v�lido")]
        public string? Email {get; set;}
        [Required(ErrorMessage = "Introduzca la contrase�a")]
        public string? Password {get; set;}
    }
    
}