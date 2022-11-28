using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;

namespace ServicesDeskUCABWS.BussinessLogic.Mapper
{
    public class TipoCargoMapper
    {
            public static TipoCargoDTO EntityToDTO(TipoCargo tipo)
            {
                return new TipoCargoDTO()
                {
                    Id = tipo.id,
                    Nombre = tipo.nombre
                };
            }

            public static TipoCargo DtoToEntity(TipoCargoDTO dto)
            {
                return new TipoCargo()
                {
                    id = dto.Id,
                    nombre = dto.Nombre
                };
            }
    }
}