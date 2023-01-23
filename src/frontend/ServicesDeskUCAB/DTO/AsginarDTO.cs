using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCAB.DTO
{
    public class AsginarDTO
    {
        [Required(ErrorMessage = "Ticket es requerido")]
        public int? ticketid { get; init; }
        [Required(ErrorMessage = "Asignado a es requerido")]
        public int? asginadoa { get; init; }
        [Required(ErrorMessage = "Prioridad es requerido")]
        public int? prioridadid { get; init; }
    }
}