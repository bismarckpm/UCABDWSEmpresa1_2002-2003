using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCAB.DTO
{
    public class PlantillaDTO
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Ingrese un Titulo para la Plantilla")]
        [StringLength(128)]
        public string titulo { get; set; }

        [Required(ErrorMessage = "Ingrese un cuerpo para la Plantilla")]
        [StringLength(128)]
        public string cuerpo { get; set; }

        public int estadoId { get; set; }
    }
}