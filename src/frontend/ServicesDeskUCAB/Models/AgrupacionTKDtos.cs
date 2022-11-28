
using ServicesDeskUCAB.DTO;

namespace ServicesDeskUCAB.Models
{
    public class AgrupacionTKDtos
    {
        public TicketDTO tk { get; set; }
        public List<PrioridadDTO> listaPrioridades { get; set; }

        public int? creadopor { get; set; }
        public int? asginadoa { get; set; }
        public int? prioridad { get; set; }
        public int? estado { get; set; }
        public int? categoria { get; set; }

    }
}