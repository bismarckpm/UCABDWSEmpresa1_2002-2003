namespace ServicesDeskUCABWS.BussinessLogic.DTO
{
    public class TicketCDTO
    {
         public string? nombre { get; set; }
        public DateTime? fecha { get; set; }
        public string? descripcion { get; set; }
         public string? creadopor {get; set;}
        public string? asginadoa { get; set; }
        public string? prioridad { get; set; }

        public string? estado {get;set;}
        public string? categoria {get;set;}
    }
}