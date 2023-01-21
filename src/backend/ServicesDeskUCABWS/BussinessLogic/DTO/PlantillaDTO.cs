using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCABWS.BussinessLogic.DTO
{

    public class PlantillaDTO
    {
        public int id { get; set; }

        public string? Titulo { get; set; }


        public string? Cuerpo { get; set; }


        public int EstadoId { get; set; }
    }

    public class PlantillaDTOCreate
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 128, ErrorMessage = "El campo {0} no debe de tener más de {1} carácteres")]
        public string? Titulo { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 255, ErrorMessage = "El campo {0} no debe de tener más de {1} carácteres")]
        public string? Cuerpo { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El campo {0} debe ser mayor a 0")]
        public int EstadoId { get; set; }

    }

}