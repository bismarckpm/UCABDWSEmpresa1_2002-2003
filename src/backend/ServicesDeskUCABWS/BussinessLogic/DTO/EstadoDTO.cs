using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCABWS.BussinessLogic.DTO
{

    public class EstadoDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El campo {0} debe ser mayor a 0")]
        public int id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(128, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres", MinimumLength = 3)]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El campo {0} debe ser mayor a 0")]
        public int EtiquetaId { get; set; }

    }

    public class EstadoCreateDTO
    {

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(128, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres", MinimumLength = 3)]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El campo {0} debe ser mayor a 0")]
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