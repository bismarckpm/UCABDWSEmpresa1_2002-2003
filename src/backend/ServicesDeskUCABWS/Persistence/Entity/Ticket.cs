using System;
namespace ServicesDeskUCABWS.Persistence.Entity
{
    public class Ticket
    {
        public int id { get; set; }

        public string? nombre { get; set; }

        public DateTime? fecha { get; set; }

        public string descripcion { get; set; }

        public Usuario? creadopor {get; set;}
        public Usuario? asginadoa { get; set; }
        public Prioridad? prioridad { get; set; }
        
        public FlujoAprobacion? FlujoAprobacion {get; set;}
        public Categoria? categoria {get;set;}
      
        public Estado? Estado { get; set; }

    }
}