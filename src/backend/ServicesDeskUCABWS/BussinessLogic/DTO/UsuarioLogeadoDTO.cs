using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCABWS.BussinessLogic.DTO
{
    public class UsuarioLogeadoDTO
    {
        public int? id {get; set;}
        public string? nombre {get; set;}
        public int? departamento {get; set;}
        public string? email {get;set;}
        public int? grupo {get;set;}
         public byte[] passwordHash {get; set;} = new byte[32];
        public byte[] passwordSalt {get; set;} = new byte[32];
        public string? VerificationToken {get; set;}
        public DateTime? VerifiedAt{get;set;}
         public string? PasswordResetToken {get; set;}
         public DateTime? ResetTokenExpires{get;set;}
        
    }

}