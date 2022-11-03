using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCABWS.BussinessLogic.DTO
{

    public class PlantillaDTO
    {
        public int id { get; set; }

        public string? Titulo { get; set; }


        public string? Cuerpo { get; set; }


        public string? Tipo { get; set; }

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
        [StringLength(maximumLength: 128, ErrorMessage = "El campo {0} no debe de tener más de {1} carácteres")]
        public string? Tipo { get; set; }

    }

}