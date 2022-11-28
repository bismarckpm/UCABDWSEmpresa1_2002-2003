using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCAB.DTO
{
    public class GrupoDTO
    {
        public int Id { get; set; }
        [Required (ErrorMessage ="Ingrese uun grupo")]
        public string? nombre { get; set; }
        public int departamentoid { get; set; }

    }
}
