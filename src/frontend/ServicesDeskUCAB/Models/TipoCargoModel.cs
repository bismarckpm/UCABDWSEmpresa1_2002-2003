using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCAB.Models
{
    public class TipoCargoModel
    {
        public int id {get; set;}
        [Required(ErrorMessage = "Introduzca un Tipo de Cargo")]
        public string? nombre {get; set;}
        
    }
}