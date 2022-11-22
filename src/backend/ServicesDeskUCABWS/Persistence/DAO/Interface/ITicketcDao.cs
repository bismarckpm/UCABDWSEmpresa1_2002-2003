using ServicesDeskUCABWS.Persistence.Entity;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface
{
    public interface ITicketDao
    {
        ICollection<Ticket> GetTickets();

        Usuario GetTicket(int id);

        bool Save();
    }
}
