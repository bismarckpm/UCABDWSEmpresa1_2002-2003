using ServicesDeskUCABWS.Persistence.Entity;

namespace ServicesDeskUCABWS.BussinessLogic.DTO
{
    public class TicketDTO
    {
       
        public int Id { get; set; }
        public string? nombre { get; set; }
        public string? fecha { get; set; }
        public string? descripcion { get; set; }
        public Empleado? asignadoa { get; set; }
        public Prioridad? prioridad { get; set; }
        public Estado? estado { get; set; }


    }
}
