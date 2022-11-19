using ServicesDeskUCABWS.Persistence.Entity;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface
{
    public interface ITicketcDao
    {
        ICollection<Ticket> GetTikects();

        Usuario GetTicket(int id);

        bool Save();
    }
}
