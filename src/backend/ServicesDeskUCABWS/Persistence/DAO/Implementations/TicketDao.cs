using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.BussinessLogic.Mapper;
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

        public TicketDTO AgregarTicketDAO(Ticket Ticket)
        {
            try
            {
                _context.Tickets.Add(Ticket);
                _context.DbContext.SaveChanges();

                var data = _context.Tickets.Where(a => a.id == Ticket.id)
                .Select(
                        a => new TicketDTO
                        {
                            Id = a.id,
                            nombre = a.nombre,
                            fecha = a.fecha,
                            descripcion = a.descripcion,
                            asignadoa = (Empleado)a.asginadoa,
                            prioridad = (Prioridad)a.prioridad

                        }
                    ); ;

                return data.First();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception("Transaccion Fallida: Ticket no creado", ex);
            }
        }

        public object ConsultarTicketDAO()
        {
            try
            {
                var ticket = _context.Tickets.Where(
                p => p.id == p.id).First(); //aca algo raro no debe ser p.id REVISAR
                return TicketMapper.EntityToDto(ticket); ;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex.InnerException!;
            }
        }

        public TicketDTO EliminarTicketDAO(int id)
        {
            try
            {
                var ticket = _context.Tickets.Where(
                    d => d.id == id).First();
                _context.Tickets.Remove(ticket);
                _context.DbContext.SaveChanges();

                return TicketMapper.EntityToDto(ticket);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " || " + ex.StackTrace);
                throw new Exception("Fallo al Eliminar el Ticket: " + id, ex);
            }
        }

        public Usuario GetTicket(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Ticket> GetTickets()
        {
            return _context.Tickets.OrderBy(p => p.id).ToList();
        }

        public TicketDTO ModificarTicketDAO(Ticket ticket)
        {
            try
            {
                _context.Tickets.Update(ticket);
                _context.DbContext.SaveChanges();

                return TicketMapper.EntityToDto(ticket);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw new Exception("Transaccion Fallo", ex)!;
            }
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        object ITicketDao.AgregarTicketDAO(Ticket ticket)
        {
            throw new NotImplementedException();
        }
    }
}
