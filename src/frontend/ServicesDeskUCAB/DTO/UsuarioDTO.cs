using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCAB.DTO
{
    public class UsuarioDTO
    {
        public string? id { get; set; }
        [Required(ErrorMessage = "Introduzca un email")]
        [EmailAddress(ErrorMessage = "Introduzca un correo v�lido")]
        public string? Email {get; set;}
        public string? Discriminator { get; set; }
    }
}