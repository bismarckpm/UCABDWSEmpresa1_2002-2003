using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCAB.DTO
{
    public class DepartamentoDTO
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Ingrese un Departamento")]
        public string? nombre { get; set; }
    }
}
