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

        public static Ticket DtoToEntity(TicketDTO dto)
        {
            return new Ticket()
            {
                id = dto.Id,
                nombre = dto.nombre
            };
        }
        public static TicketDTO EntityToDto(Ticket ticket)
        {
            return new TicketDTO()
            {
                Id = ticket.id,
                nombre = ticket.nombre
            };
        }

    }
}
