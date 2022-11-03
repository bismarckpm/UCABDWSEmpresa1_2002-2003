using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using AutoMapper;

namespace ServicesDeskUCABWS.BussinessLogic.Mapper
{
    public class PlantillaMapper : Profile
    {
        public PlantillaMapper()
        {
            CreateMap<Plantilla, PlantillaDTO>();
            CreateMap<PlantillaDTO, Plantilla>();
            CreateMap<PlantillaDTOCreate, Plantilla>();
        }

    }
}