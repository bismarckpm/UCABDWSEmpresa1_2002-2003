using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCAB.DTO
{
    public class GrupoDTO
    {

        public int id { get; set; }
        [Required(ErrorMessage = "Ingrese un Grupo")]
        public string nombre { get; set; }
        public int departamentoid { get; set; }
    }
}