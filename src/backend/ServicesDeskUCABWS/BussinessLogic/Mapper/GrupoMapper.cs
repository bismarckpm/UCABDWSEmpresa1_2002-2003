using AutoMapper;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Entity;

namespace ServicesDeskUCABWS.BussinessLogic.Mapper
{
    public class GrupoMapper : Profile
    {
        public GrupoMapper()
        {
            CreateMap<Grupo, GrupoDTO>().ReverseMap();
        }
    }
}

/*using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;


namespace ServicesDeskUCABWS.BussinessLogic.Mapper
{
    public class GrupoMapper
    {
        public static GrupoDTO EnityToDto(Grupo grupo)
        {
            return new GrupoDTO()
            {
                id = grupo.id,
                nombre = grupo.nombre,  
               
            };
        }
        public static Grupo DtoToEntity(GrupoDTO dto)
        {
            return new Grupo()
            {
                id = dto.id,
                nombre = dto.nombre,
                
            };
        }

    }
}
*/