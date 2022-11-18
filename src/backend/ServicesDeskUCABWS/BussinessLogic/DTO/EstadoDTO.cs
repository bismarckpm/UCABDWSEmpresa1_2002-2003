using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCABWS.BussinessLogic.DTO
{

    public class EstadoDTO
    {
        public int id { get; set; }
        public string? Nombre { get; set; }

        public int EtiquetaId { get; set; }

    }



    public class EstadoEtiquetaDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 120, ErrorMessage = "El campo {0} no debe de tener más de {1} carácteres")]
        public string? Nombre { get; set; }
    }

    public class EstadoEtiquetaUpdateDTO
    {
        public int id { get; set; }
        public int New_EtiquetaId { get; set; }
    }
}