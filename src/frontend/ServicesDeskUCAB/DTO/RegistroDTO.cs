using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCAB.DTO
{
    public class RegistroDTO
    {
         [Required,EmailAddress]
        public string? Email {get; set;}
        [Required,MinLength(8)]
        public string? Password {get; set;}
        [Required,Compare("Password")]
         public string? confirmationpassword {get; set;}
    }
}