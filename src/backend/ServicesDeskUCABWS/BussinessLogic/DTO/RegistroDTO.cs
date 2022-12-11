using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCABWS.BussinessLogic.DTO
{
    public class RegistroDTO
    {
        
        [Required,EmailAddress]
        public string Email {get; set;}
        [Required,MinLength(8)]
        public string Password {get; set;}
        [Required,Compare("Password")]
        public string confirmationpassword {get; set;}
        public string nombre {get; set;}
        public int cargoid {get; set;} = 0;
        [Required]
        public int GrupoId {get; set;}
        [Required]
        public int tipousuario {get; set;}
    }
}