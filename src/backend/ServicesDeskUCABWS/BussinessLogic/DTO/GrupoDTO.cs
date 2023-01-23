using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCABWS.BussinessLogic.DTO
{

    public class GrupoDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El campo {0} debe ser mayor a 0")]
        public int id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(128, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres", MinimumLength = 3)]
        public string? nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El campo {0} debe ser mayor a 0")]
        public int departamentoid { get; set; }

    }

    public class GrupoCreateDTO
    {

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(128, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres", MinimumLength = 3)]
        public string? nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El campo {0} debe ser mayor a 0")]
        public int departamentoid { get; set; }

    }


    public class GrupoResponseDTO
    {
        public int id { get; set; }

        public string? nombre { get; set; }

        public int departamentoid { get; set; }
    }




}
