using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCAB.Models
{
    public class PrioridadModel
    {
        [Required(ErrorMessage = "Introduzca una prioridad")]
        public string? nombre { get; set; }
    }
}
