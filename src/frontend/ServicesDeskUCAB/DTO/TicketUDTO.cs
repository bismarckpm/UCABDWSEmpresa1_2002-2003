namespace ServicesDeskUCAB.DTO
{
    public class TicketUDTO
    {
        public int Id { get; set; }
        public string? nombre { get; set; }
        public DateTime? fecha { get; set; }
        public string? descripcion { get; set; }

    }
}