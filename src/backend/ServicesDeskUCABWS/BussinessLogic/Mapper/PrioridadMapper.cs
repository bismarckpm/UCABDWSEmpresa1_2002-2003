using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;

namespace ServicesDeskUCABWS.BussinessLogic.Mapper
{
    public class PrioridadMapper
    {
        public static PrioridadDTO EntityToDto(Prioridad priori)
        {
            return new PrioridadDTO()
            {
                Id = priori.id,
                Nombre = priori.nombre
            };
        }

        public static Prioridad DtoToEntity(PrioridadDTO dto)
        {
            return new Prioridad()
            {
                id = dto.Id,
                nombre = dto.Nombre
            };
        }
    }
}