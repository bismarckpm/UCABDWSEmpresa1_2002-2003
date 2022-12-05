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
        private readonly IEmailDao _emailRepository;

        public TicketDao(IMigrationDbContext context, IEmailDao emailDao)
        {
            _context = context;
            _emailRepository = emailDao;

        }

        public bool AgregarTicketDAO(Ticket ticket, int creadopor, int grupoid, int categoriaid)
        {
        
            ticket.creadopor = _context.Usuario.Where(c => c.id == creadopor).FirstOrDefault();
            ticket.categoria = _context.Categorias.Where(c => c.id == categoriaid).FirstOrDefault();
            ticket.grupo = _context.Grupo.Where(c => c.id == grupoid).FirstOrDefault();
            ticket.Estado = _context.Estados.Where(c => c.nombre == "En espera").FirstOrDefault();
            _context.Tickets.Add(ticket);
            return Save();
            
        }

         public bool Update(Ticket ticket, int asignadoaid, int prioridadid, int Estadoid, int categoriaid)
        {
            ticket.asginadoa = _context.Usuario.Where(c => c.id == asignadoaid).FirstOrDefault();
            ticket.prioridad = _context.Prioridades.Where(c => c.id == prioridadid).FirstOrDefault();
            ticket.Estado = _context.Estados.Where(c => c.id == Estadoid).FirstOrDefault();
            ticket.categoria = _context.Categorias.Where(c => c.id == categoriaid).FirstOrDefault();
            _context.Tickets.Update(ticket);
            _context.Tickets.Add(ticket);
            return Save();
        }
         public bool AsignarTicket(AsignarTicketDTO asignarTicket){
            var ticket = _context.Tickets.Where(c => c.id == asignarTicket.ticketid).FirstOrDefault();
            ticket.asginadoa =_context.Usuario.Where(c => c.id == asignarTicket.asginadoa).FirstOrDefault();
            ticket.prioridad =_context.Prioridades.Where(c => c.id == asignarTicket.prioridadid).FirstOrDefault();
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
                     join ca in _context.Categorias on tk.categoria equals ca
                     select new TicketCDTO()
                     {
                        id = tk.id,
                        nombre = tk.nombre,
                        idasignad = us2.id,
                        asginadoa = us2.email,
                        creadopor = us.email,
                        descripcion = tk.descripcion,
                        fecha = tk.fecha,
                        idestado = e.id,
                        estado = e.nombre,
                        idprioridad =p.id,
                        prioridad = p.nombre,
                        idcategoria = ca.id,
                        categoria = ca.nombre
                        
                      }).ToList();
            return q;
        }
        public TicketCDTO GetTicket(int ticketid){
            var q = (from tk in _context.Tickets
                     join us in _context.Usuario on tk.creadopor equals us
                     join us2 in _context.Usuario on tk.asginadoa equals us2 
                     join e in _context.Estados on tk.Estado equals e
                     join p in _context.Prioridades on tk.prioridad equals p
                     join ca in _context.Categorias on tk.categoria equals ca
                     where tk.id == ticketid
                     select new TicketCDTO()
                     {
                        id = tk.id,
                        nombre = tk.nombre,
                        idasignad = us2.id,
                        asginadoa = us2.email,
                        creadopor = us.email,
                        descripcion = tk.descripcion,
                        fecha = tk.fecha,
                        idestado = e.id,
                        estado = e.nombre,
                        idprioridad =p.id,
                        prioridad = p.nombre,
                        idcategoria = ca.id,
                        categoria = ca.nombre
                      }).FirstOrDefault();
            return q;
        }

        public ICollection<TicketCDTO> GetTicketporusuarioasignado(int usuarioasignado){
            var usu =_context.Usuario.Where(c => c.id == usuarioasignado).FirstOrDefault();
            var q = (from tk in _context.Tickets
                     join us in _context.Usuario on tk.creadopor equals us
                     join us2 in _context.Usuario on tk.asginadoa equals us2
                     join e in _context.Estados on tk.Estado equals e
                     join p in _context.Prioridades on tk.prioridad equals p
                     join ca in _context.Categorias on tk.categoria equals ca
                     where tk.asginadoa == usu
                     select new TicketCDTO()
                     {
                        nombre = tk.nombre,
                        asginadoa = us2.email,
                        creadopor = us.email,
                        descripcion = tk.descripcion,
                        fecha = tk.fecha,
                        estado = e.nombre,
                        prioridad = p.nombre,
                        categoria = ca.nombre
                      }).ToList();
            return q;
        }

         public ICollection<TicketCDTO> GetTicketporestado(int estado){
            var usu =_context.Estados.Where(c => c.id == estado).FirstOrDefault();
            var q = (from tk in _context.Tickets
                     join us in _context.Usuario on tk.creadopor equals us
                     join us2 in _context.Usuario on tk.asginadoa equals us2
                     join e in _context.Estados on tk.Estado equals e
                     join p in _context.Prioridades on tk.prioridad equals p
                     join ca in _context.Categorias on tk.categoria equals ca
                     where tk.Estado == usu
                     select new TicketCDTO()
                     {
                        nombre = tk.nombre,
                        asginadoa = us2.email,
                        creadopor = us.email,
                        descripcion = tk.descripcion,
                        fecha = tk.fecha,
                        estado = e.nombre,
                        prioridad = p.nombre,
                        categoria = ca.nombre
                      }).ToList();
            return q;
        }

         public ICollection<TicketCDTO> GetTicketsPorDepartamento(int departamentoid){
            var usu =_context.Departamentos.Where(c => c.id == departamentoid).FirstOrDefault();
            var q = (from tk in _context.Tickets
                     join us in _context.Usuario on tk.creadopor equals us
                     join us2 in _context.Usuario on tk.asginadoa equals us2
                     join e in _context.Estados on tk.Estado equals e
                     join p in _context.Prioridades on tk.prioridad equals p
                     join ca in _context.Categorias on tk.categoria equals ca
                     where us2.Grupo.departamento == usu
                     select new TicketCDTO()
                     {
                        nombre = tk.nombre,
                        asginadoa = us2.email,
                        creadopor = us.email,
                        descripcion = tk.descripcion,
                        fecha = tk.fecha,
                        estado = e.nombre,
                        prioridad = p.nombre,
                        categoria = ca.nombre
                      }).ToList();
            return q;
        }

         public ICollection<TicketCDTO> GetTicketsPorCategoria(int categoriaid){
            var usu =_context.Categorias.Where(c => c.id == categoriaid).FirstOrDefault();
            var q = (from tk in _context.Tickets
                     join us in _context.Usuario on tk.creadopor equals us
                     join us2 in _context.Usuario on tk.asginadoa equals us2
                     join e in _context.Estados on tk.Estado equals e
                     join p in _context.Prioridades on tk.prioridad equals p
                     join ca in _context.Categorias on tk.categoria equals ca
                     where tk.categoria == usu
                     select new TicketCDTO()
                     {
                        nombre = tk.nombre,
                        asginadoa = us2.email,
                        creadopor = us.email,
                        descripcion = tk.descripcion,
                        fecha = tk.fecha,
                        estado = e.nombre,
                        prioridad = p.nombre,
                        categoria = ca.nombre
                      }).ToList();
            return q;
        }

        public bool Save()
        {
            var saved = _context.DbContext.SaveChanges();
            return saved > 0 ? true : false;
        }


    }
}
