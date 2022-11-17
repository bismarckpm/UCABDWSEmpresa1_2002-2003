using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCAB.DTO
{
    public class EtiquetaDTO
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Ingrese un Tipo de Cargo")]
        [StringLength(50)]
        public string? nombre { get; set; }

        public string? descripcion { get; set; }
    }
}