using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCAB.DTO
{
    public class CategoriaDTO
    {
        public int id { get; set; }
        [Required(ErrorMessage ="Ingrese una categoria")]
        public string nombre { get; set; }
    }
}
