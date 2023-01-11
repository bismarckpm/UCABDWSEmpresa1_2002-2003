using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ServicesDeskUCABWS.Persistence.Entity
{
    [Index(nameof(email), IsUnique = true)]
    public abstract class  Usuario 
    {
        public int id {get; set;}
        public byte[] passwordHash {get; set;} = new byte[32];
        public byte[] passwordSalt {get; set;} = new byte[32];
        public string? VerificationToken {get; set;}
        public DateTime? VerifiedAt{get;set;}
         public string? PasswordResetToken {get; set;}
         public DateTime? ResetTokenExpires{get;set;}
        public string? email {get; set;}
        public string? nombre {get; set;}

        [InverseProperty("asginadoa")]
        public ICollection<Ticket> ticketsasignados { get; set; }
        [InverseProperty("creadopor")]
        public ICollection<Ticket> ticketscreados { get; set; }
   
        public virtual ICollection<FlujoAprobacion> Flujo { get; set; }
        public virtual Cargo? cargo { get; set; }
        public virtual Grupo? Grupo {get;set;}

        public string? Discriminator;
        
    }   
}