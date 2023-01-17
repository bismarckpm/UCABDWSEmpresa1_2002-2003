using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCABWS.BussinessLogic.DTO
{
    public class CategoriaDTO
    {
        public int Id {get; set;}
        [Required(ErrorMessage = "Nombre es requerido")]
        public string? Nombre {get; set;}
        

        // public FlujoAprobacion FlujoAprobacion {get;set;}
    }
}