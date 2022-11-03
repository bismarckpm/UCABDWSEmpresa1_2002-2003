using AutoMapper;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;

namespace ServicesDeskUCABWS.BussinessLogic.Mapper
{
    public class EtiquetaMapper : Profile
    {

        public EtiquetaMapper()
        {
            CreateMap<EtiquetaDTO, Etiqueta>();
            CreateMap<Etiqueta, EtiquetaDTO>();
            CreateMap<EtiquetaDTOCreate, Etiqueta>();
        }

    }
}