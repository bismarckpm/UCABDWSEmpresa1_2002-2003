using ServicesDeskUCABWS.Persistence.Entity;

namespace ServicesDeskUCABWS.BussinessLogic.DTO
{
    public class CargoDTO
    {
        public int Id {get; set;}
        public string? Nombre {get; set;}
        public int TipoCargoId { get; set; }
    }
}