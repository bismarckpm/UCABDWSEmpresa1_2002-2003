namespace ServicesDeskUCABWS.Persistence.Entity
{
    public class Plantilla
    {
        public int id { get; set; }

        public string? operacion { get; set; }

        public bool titulo { get; set; }

        public bool fecha { get; set; }

        public bool descripcion { get; set; }

        public bool asignadoa { get; set; }

        //Fk de la tabla Tickets
        public int TicketId { get; set; }
        public Ticket? Ticket { get; set; }
        // public string? titulo { get; set; }

        // public string? cuerpo { get; set; }

        // public string? tipo { get; set; }


        // public List<Notification>? notifications { get; set; }



    }
}