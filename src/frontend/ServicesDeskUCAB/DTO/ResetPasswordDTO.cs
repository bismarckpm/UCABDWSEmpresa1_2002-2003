using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCAB.DTO
{
    public class ResetPasswordDTO
    {
        [Required]
        public string token {get; set;} = string.Empty;

        [Required(ErrorMessage = "Introduzca la contraseña")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener minimo 8 caracteres")]
        public string Password {get; set;} = string.Empty;

        [Required(ErrorMessage = "Repita la contraseña")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        public string confirmationpassword {get; set;} =string.Empty;
    }
    }
