using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using AutoMapper;

namespace ServicesDeskUCABWS.BussinessLogic.Mapper
{
    public class EstadoMapper : Profile
    {
        public EstadoMapper()
        {
            CreateMap<EstadoEtiquetaDTO, Estado>();
            CreateMap<Estado, EstadoEtiquetaDTO>();
            CreateMap<Estado, EstadoDTO>();
            CreateMap<EstadoDTO, Estado>();
        }

    }
}