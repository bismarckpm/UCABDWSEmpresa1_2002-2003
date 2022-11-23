namespace ServicesDeskUCABWS.Persistence.Entity
{
    public class Ticket
    {
        public int id { get; set; }

        public string? nombre { get; set; }

        public string? fecha { get; set; }

        public string descripcion { get; set; }

        public Usuario? creadopor {get; set;}
        public Usuario? asginadoa { get; set; }
        public Prioridad? prioridad { get; set; }

        public Ticket? delegacion { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<FlujoAprobacion> Flujo { get; set; }

        private Estado? status;

        public Estado Status {get => status!; set => status = value;}
    }
}