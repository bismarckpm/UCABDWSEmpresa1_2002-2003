using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCAB.DTO
{
    public class DelegarDTO
    {
        [Required(ErrorMessage = "Ticket es requerido")]
        public int? idticket { get; init; }
        [Required(ErrorMessage = "Asignado a es requerido")]
        public int? idAsignadoa { get; init; }
       
    }
}