using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Entity;

namespace ServicesDeskUCABWS.BussinessLogic.Mapper
{
    public class FlujoAprobacionMapper : Profile
    {
        public FlujoAprobacionMapper()
        {
            CreateMap<FlujoAprobacion, FlujoAprobacionDTO>().ReverseMap();
        }
    }
}
