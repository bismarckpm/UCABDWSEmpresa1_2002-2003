using AutoMapper;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Entity;

namespace ServicesDeskUCABWS.BussinessLogic.Mapper
{
    public class TicketMapper : Profile
    {
        public TicketMapper()
        {
            CreateMap<TicketDTO, Ticket>();
            CreateMap<Ticket, TicketDTO>();

        }
        
    }
}
