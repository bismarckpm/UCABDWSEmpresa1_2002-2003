using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCABWS.BussinessLogic.DTO
{
    public class RegistroDTO
    {
        
        
        public string Email {get; set;}
        
        public string Password {get; set;}
        
        public string confirmationpassword {get; set;}
        public string nombre {get; set;}
        public int cargoid {get; set;} = 0;
      
        public int GrupoId {get; set;}
        
        public int tipousuario {get; set;}
    }
}