using ServicesDeskUCABWS.BussinessLogic.DTO;
using AutoMapper;
using ServicesDeskUCABWS.Persistence.Entity;

namespace ServicesDeskUCABWS.BussinessLogic.Mapper
{
    public class ModeloJerarquicoMapper: Profile
    {

        public ModeloJerarquicoMapper()
        {
            CreateMap<ModeloJerarquicoCreateDTO, ModeloJerarquico>();
            CreateMap<ModeloJerarquico, ModeloJerarquicoCreateDTO>();
            CreateMap<ModeloJerarquico, ModeloJerarquicoDTO>();
            CreateMap<ModeloJerarquicoDTO, ModeloJerarquico>();
        
        }

    }
}