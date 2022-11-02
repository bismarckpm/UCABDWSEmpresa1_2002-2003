using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCABWS.BussinessLogic.DTO
{
    public class EtiquetaDTO
    {


        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 120, ErrorMessage = "El campo {0} no debe de tener más de {1} carácteres")]
        public string? Nombre { get; set; }
        [StringLength(maximumLength: 255, ErrorMessage = "El campo {0} no debe de tener más de {1} carácteres")]
        public string? Descripcion { get; set; }
    }
}