using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCAB.DTO
{
    public class UsuarioDTO
    {
        [Required(ErrorMessage = "Introduzca un email")]
        [EmailAddress(ErrorMessage = "Introduzca un correo válido")]
        public string? Email {get; set;}
   
    }
}