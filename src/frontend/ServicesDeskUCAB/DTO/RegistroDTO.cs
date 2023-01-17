using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCAB.DTO
{
    public class RegistroDTO
    {
        [Required(ErrorMessage = "Introduzca un email")]
        [EmailAddress(ErrorMessage = "Introduzca un correo v�lido")]
        public string? Email {get; set;}

        [Required(ErrorMessage = "Introduzca la contrase�a")]
        [MinLength(8, ErrorMessage = "La contrase�a debe tener minimo 8 caracteres")]
        public string? Password {get; set;}

        [Required(ErrorMessage = "Repita la contrase�a")]
        [Compare("Password", ErrorMessage = "Las contrase�as no coinciden")]
        public string? confirmationpassword {get; set;}
        [Required(ErrorMessage = "Introduzca su nombre")]
        public string nombre {get; set;}
        public int cargoid {get; set;} = 0;
        
        public int GrupoId {get; set;}= 0;
       
        public int tipousuario {get; set;}= 3;
        
    }
}