using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCAB.DTO
{
    public class PrioridadDTO
    {
        public int id { get; set; }
        [Required(ErrorMessage ="Ingrese una prioridad")]
        public string nombre { get; set; }
    }
}
