using System.ComponentModel.DataAnnotations.Schema;

namespace ServicesDeskUCABWS.Persistence.Entity
{
    public abstract class  Usuario 
    {
        public int id {get; set;}
        public string? username {get; set;}

        public string? password {get; set;}

        public string? email {get; set;}

     
        public virtual Cargo cargo { get; set; }
    }   
}