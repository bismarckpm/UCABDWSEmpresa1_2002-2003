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
        public TicketDao(MigrationDbContext context, IEmailDao emailDao)
        {
            _context = context;
            _emailRepository = emailDao;

        }

        public bool AgregarTicketDAO(Ticket ticket, int creadopor, int asignadaa, int prioridad, int estatud, int categoriaid)
        {
        
            ticket.creadopor = _context.Usuario.Where(c => c.id == creadopor).FirstOrDefault();
            ticket.asginadoa = _context.Usuario.Where(c => c.id == asignadaa).FirstOrDefault();
            ticket.prioridad = _context.Prioridades.Where(c => c.id == prioridad).FirstOrDefault();
            ticket.categoria = _context.Categorias.Where(c => c.id == categoriaid).FirstOrDefault();
           
            ticket.Estado = _context.Estados.Where(c => c.id == estatud).FirstOrDefault();
            var email = new EmailDTO();
            email.para = ticket.asginadoa.email;
            email.Cuerpo ="Descripcion del ticket: " + ticket.descripcion;
            email.asunto = "Ticket " + ticket.nombre + " Creado con exito asignado a " + ticket.asginadoa.email;
            _emailRepository.SendEmail(email);
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
             var email = new EmailDTO();
            email.para = ticket.asginadoa.email;
            email.Cuerpo ="Descripcion del ticket: " + ticket.descripcion;
            email.asunto = "Ticket " + ticket.nombre + " Ha sido actualizado ";
            _emailRepository.SendEmail(email);
            _context.Tickets.Add(ticket);
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
                        nombre = tk.nombre,
                        asginadoa = us2.email,
                        creadopor = us.email,
                        descripcion = tk.descripcion,
                        fecha = tk.fecha,
                        estado = e.nombre,
                        prioridad = p.nombre,
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
                     where us2.Departamento == usu
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
