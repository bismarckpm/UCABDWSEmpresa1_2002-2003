using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCAB.DTO
{
    public class PlantillaDTO
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Ingrese un Titulo para la Plantilla")]
        public string titulo { get; set; }


        public string cuerpo { get; set; }


        public string tipo { get; set; }
    }
}