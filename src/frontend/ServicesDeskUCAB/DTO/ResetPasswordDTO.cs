using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCAB.DTO
{
    public class ResetPasswordDTO
    {
        [Required]
        public string token {get; set;} = string.Empty;

        [Required(ErrorMessage = "Introduzca la contrase�a")]
        [MinLength(8, ErrorMessage = "La contrase�a debe tener minimo 8 caracteres")]
        public string Password {get; set;} = string.Empty;

        [Required(ErrorMessage = "Repita la contrase�a")]
        [Compare("Password", ErrorMessage = "Las contrase�as no coinciden")]
        public string confirmationpassword {get; set;} =string.Empty;
    }
    }
