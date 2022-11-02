namespace ServicesDeskUCABWS.Persistence.Entity
{
    public class Ticket
    {
        public  int id {get; set;}

        public string? nombre {get; set;}

        public string? fecha { get; set;}
        public Usuario? usuario {get; set;}
        public Prioridad? prioridad { get; set; }

        private Estado? status;

        public Estado Status {get => status!; set => status = value;}
    }
}