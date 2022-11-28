using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCAB.DTO
{
    public class CargoDTO
    {

        public int id { get; set; }
        [Required(ErrorMessage = "Ingrese un Cargo")]
        public string? nombre { get; set; }
        public int tipoCargoId { get; set; }
    }
}