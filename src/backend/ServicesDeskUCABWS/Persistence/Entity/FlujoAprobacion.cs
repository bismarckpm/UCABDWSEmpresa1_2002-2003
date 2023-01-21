using MimeKit.Cryptography;

namespace ServicesDeskUCABWS.Persistence.Entity
{
    public class FlujoAprobacion
    {
        public int id { get; set; }
        public int ticketid { get; set; }
        public virtual Ticket Ticket{get;set;}

        public int modeloid{get;set; } 
        public virtual ModeloAprobacion ModeloAprobacion { get;set;} 

        public int empleadoid{get;set;}
        public virtual Empleado Empleado{get;set;}    
        public int estatus {get;set;}

    }

   
}