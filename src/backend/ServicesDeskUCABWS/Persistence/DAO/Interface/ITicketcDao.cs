using ServicesDeskUCABWS.Persistence.Entity;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface
{
    public interface ITicketDao
    {
        ICollection<Ticket> GetTikects();

        Usuario GetTicket(int id);

        bool Save();
    }
}
