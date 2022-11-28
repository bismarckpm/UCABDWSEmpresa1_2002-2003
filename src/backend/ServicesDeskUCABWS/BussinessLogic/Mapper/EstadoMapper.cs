using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using AutoMapper;

namespace ServicesDeskUCABWS.BussinessLogic.Mapper
{
    public class EstadoMapper : Profile
    {
        public EstadoMapper()
        {
            CreateMap<Estado, EstadoDTO>();
            CreateMap<EstadoDTO, Estado>();
            CreateMap<EstadoCreateDTO, Estado>();
        }

    }
}