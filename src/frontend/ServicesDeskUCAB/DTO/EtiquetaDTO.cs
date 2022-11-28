using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCAB.DTO
{
    public class EtiquetaDTO
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Ingrese una Etiqueta")]
        [StringLength(50)]
        public string? nombre { get; set; }

        public string? descripcion { get; set; }
    }
}