using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCABWS.BussinessLogic.DTO
{
    public class AdministratorDTO
    {
        
        [Required,EmailAddress]
        public string? Email {get; set;}
        [Required,MinLength(8)]
        public string? Password {get; set;}
        [Required,Compare("Password")]
         public string? confirmationpassword {get; set;}
    }
}