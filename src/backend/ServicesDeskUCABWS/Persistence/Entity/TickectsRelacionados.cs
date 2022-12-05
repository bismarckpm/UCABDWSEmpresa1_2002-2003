namespace ServicesDeskUCABWS.Persistence.Entity
{
    public class TickectsRelacionados
    {
         public int Id { get; set; }
         public int? Ticketid { get; set; } 
         public virtual Ticket? ticket { get; set; }

        public int? TicketRelacionadoid { get; set; } 
        public virtual Ticket? TicketRelacion { get; set; }
    }
}