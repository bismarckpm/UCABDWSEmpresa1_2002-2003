using System.ComponentModel.DataAnnotations.Schema;

namespace ServicesDeskUCABWS.Persistence.Entity
{
    public abstract class  Usuario 
    {
        public int id {get; set;}
        public string? username {get; set;}

        public byte[] passwordHash {get; set;} = new byte[32];
        public byte[] passwordSalt {get; set;} = new byte[32];
        public string? VerificationToken {get; set;}
        public DateTime? VerifiedAt{get;set;}
         public string? PasswordResetToken {get; set;}
         public DateTime? ResetTokenExpires{get;set;}
        public string? email {get; set;}

     
        public virtual Cargo? cargo { get; set; }
    }   
}