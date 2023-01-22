using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Entity;
using AutoMapper;

namespace ServicesDeskUCABWS.BussinessLogic.Mapper;

public class ModeloParaleloMapper : Profile
{
    public ModeloParaleloMapper()
    {   
        CreateMap<ModeloParalelo, ModeloParaleloDTO>();
        CreateMap<ModeloParaleloDTO, ModeloParalelo>(); 
        CreateMap<ModeloParaleloCreateDTO, ModeloParalelo>(); 
        CreateMap<ModeloParalelo, ModeloParaleloCreateDTO>();   
    }
}