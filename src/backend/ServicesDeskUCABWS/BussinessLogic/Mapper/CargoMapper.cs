using AutoMapper;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Entity;

namespace ServicesDeskUCABWS.BussinessLogic.Mapper
{
    public class CargoMapper : Profile
    {
         public CargoMapper()
        {
            CreateMap<Cargo, CargoDTO>();
        }
    }
}