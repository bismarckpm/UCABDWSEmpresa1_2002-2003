using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Database;
using ServicesDeskUCABWS.Persistence.Entity;

namespace ServicesDeskUCABWS.Persistence.DAO.Implementations
{
    public class TicketDao : ITicketcDao
    {
        private readonly IMigrationDbContext _context;
        public TicketDao(MigrationDbContext context)
        {
            _context = context;

        }
        public Usuario GetTicket(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Ticket> GetTikects()
        {
            return _context.Tickets.OrderBy(p => p.id).ToList();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }
    }
}
