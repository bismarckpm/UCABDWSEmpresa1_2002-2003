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
