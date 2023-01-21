using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCAB.DTO
{
    public class TicketDTO
    {

        [Required(ErrorMessage = "Introduzca un nombre")]
        public string? nombre { get; set; }
        public DateTime? fecha { get; set; }
        [Required(ErrorMessage = "Introduzca un Descripcion")]
        public string? descripcion { get; set; }
        public int? creadopor { get; set; }
        [Required(ErrorMessage = "Elija una Categoria")]
        public int? categoriaid { get; set; }
        [Required(ErrorMessage = "Elija un departamento")]
        public int? Departamentoid { get; set; }
    }
}
