using AutoMapper;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Entity;

namespace ServicesDeskUCABWS.BussinessLogic.Mapper
{
    public class CargoMapper : Profile
    {
         public CargoMapper()
        {
            CreateMap<Cargo, CargoDTO>().ReverseMap();
            /*CreateMap<CargoDTO, Cargo>()
            .ForMember(dest => dest.tipocargo.id, opt => opt.MapFrom(src => src.TipoCargoId))
            .ForMember(dest => dest.nombre, opt => opt.MapFrom(src => src.Nombre))
            .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id));*/
        }
    }
}