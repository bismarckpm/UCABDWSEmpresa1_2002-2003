using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;


namespace ServicesDeskUCABWS.BussinessLogic.Mapper
{
    public class GrupoMapper
    {
        public static GrupoDTO EntityToDto(Grupo grupo)
        {
            return new GrupoDTO()
            {
                id = grupo.id,
                nombre = grupo.nombre,  
                departamentoid = grupo.departamentoid
            };
        }
        public static Grupo DtoToEntity(GrupoDTO dto)
        {
            return new Grupo()
            {
                id = dto.id,
                nombre = dto.nombre,
                departamentoid = dto.departamentoid
            };
        }

    }
}
