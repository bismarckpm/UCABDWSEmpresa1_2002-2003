using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCABWS.BussinessLogic.DTO
{
    public class TickectCreateDTO
    {
        
        public string? nombre { get; set; }
        public DateTime? fecha { get; set; }
        public string? descripcion { get; set; }

        public int? creadopor { get; init; }
        public int? categoriaid { get; init; }
        public int? Departamentoid { get; init; }


    }
}