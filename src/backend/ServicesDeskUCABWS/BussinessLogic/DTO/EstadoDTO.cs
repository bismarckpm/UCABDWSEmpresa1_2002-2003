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




}