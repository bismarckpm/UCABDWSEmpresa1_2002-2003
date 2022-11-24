using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Entity;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface
{
    public interface ITicketDao
    {
        ICollection<Ticket> GetTickets();

        Usuario GetTicket(int id);

        bool Save();
        object AgregarTicketDAO(Ticket ticket);
        object ConsultarTicketDAO();
        TicketDTO ModificarTicketDAO(Ticket ticket);
        TicketDTO EliminarTicketDAO(int id);
    }
}
