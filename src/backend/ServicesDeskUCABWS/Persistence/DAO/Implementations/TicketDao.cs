using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.BussinessLogic.Mapper;
using ServicesDeskUCABWS.Exceptions;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Database;
using ServicesDeskUCABWS.Persistence.Entity;

namespace ServicesDeskUCABWS.Persistence.DAO.Implementations
{
    public class TicketDao : ITicketDao
    {
        private readonly IMigrationDbContext _context;
        public TicketDao(MigrationDbContext context)
        {
            _context = context;

        }

        public bool AgregarTicketDAO(Ticket ticket, int creadopor, int asignadaa, int prioridad, int estatud)
        {
        
            ticket.creadopor = _context.Usuario.Where(c => c.id == creadopor).FirstOrDefault();
            ticket.asginadoa = _context.Usuario.Where(c => c.id == asignadaa).FirstOrDefault();
            ticket.prioridad = _context.Prioridades.Where(c => c.id == prioridad).FirstOrDefault();
            ticket.Estado = _context.Estados.Where(c => c.id == estatud).FirstOrDefault();
           
                  
              
            _context.Tickets.Add(ticket);
            return Save();
            
        }

         public bool Update(Ticket ticket, int asignadoaid, int prioridadid, int Estadoid)
        {
            ticket.asginadoa = _context.Usuario.Where(c => c.id == asignadoaid).FirstOrDefault();
            ticket.prioridad = _context.Prioridades.Where(c => c.id == prioridadid).FirstOrDefault();
            ticket.Estado = _context.Estados.Where(c => c.id == Estadoid).FirstOrDefault();
            _context.Tickets.Update(ticket);
            return Save();
        }


        public ICollection<TicketCDTO> GetTickets()
        {
            var q = (from tk in _context.Tickets
                     join us in _context.Usuario on tk.creadopor equals us
                     join us2 in _context.Usuario on tk.asginadoa equals us2
                     join e in _context.Estados on tk.Estado equals e
                     join p in _context.Prioridades on tk.prioridad equals p
                     select new TicketCDTO()
                     {
                        nombre = tk.nombre,
                        asginadoa = us2.email,
                        creadopor = us.email,
                        descripcion = tk.descripcion,
                        fecha = tk.fecha,
                        estado = e.nombre,
                        prioridad = p.nombre
                      }).ToList();
            return q;
        }
        public TicketCDTO GetTicket(int ticketid){
            var q = (from tk in _context.Tickets
                     join us in _context.Usuario on tk.creadopor equals us
                     join us2 in _context.Usuario on tk.asginadoa equals us2
                     join e in _context.Estados on tk.Estado equals e
                     join p in _context.Prioridades on tk.prioridad equals p
                     where tk.id == ticketid
                     select new TicketCDTO()
                     {
                        nombre = tk.nombre,
                        asginadoa = us2.email,
                        creadopor = us.email,
                        descripcion = tk.descripcion,
                        fecha = tk.fecha,
                        estado = e.nombre,
                        prioridad = p.nombre
                      }).FirstOrDefault();
            return q;
        }

        public bool Save()
        {
            var saved = _context.DbContext.SaveChanges();
            return saved > 0 ? true : false;
        }


    }
}
