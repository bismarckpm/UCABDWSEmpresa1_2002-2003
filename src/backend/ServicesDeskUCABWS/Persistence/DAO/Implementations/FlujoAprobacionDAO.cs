using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NLog.LayoutRenderers.Wrappers;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Exceptions;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Database;
using ServicesDeskUCABWS.Persistence.Entity;

namespace ServicesDeskUCABWS.Persistence.DAO.Implementations
{
    public class FlujoAprobacionDAO : IFlujoAprobacionDAO
    {
        private readonly IMigrationDbContext _context;

        private readonly IMapper _mapper;
        private readonly ILogger<FlujoAprobacionDAO> _logger;
        public FlujoAprobacionDAO(IMapper mapper, IMigrationDbContext context, ILogger<FlujoAprobacionDAO> logger)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Agregar flujo de aprobación en la BD
        /// </summary>
        /// <param name="flujoAprobacion"></param>
        /// <returns></returns>
        /// <exception cref="FlujoAprobacionException"></exception>
        public async Task<FlujoAprobacionDTO> AgregarFlujoDAO(FlujoAprobacionDTO flujoAprobacion)
        {
            try
            {
                List<FlujoAprobacion> listaNuevoFlujo = new List<FlujoAprobacion>();
                List<Ticket> listaTickets  = new List<Ticket>();
                listaTickets = _context.Tickets.Where(c => c.categoria.id == flujoAprobacion.categoriaId && c.asginadoa.id==flujoAprobacion.empleadoId).ToList();
               
                foreach (Ticket ticket in listaTickets)
                {
                    FlujoAprobacion nuevoFlujo = new FlujoAprobacion();

                    nuevoFlujo.ticketid = ticket.id;
                    nuevoFlujo.empleadoid = flujoAprobacion.empleadoId;
                    nuevoFlujo.modeloid = flujoAprobacion.modeloId;
                    nuevoFlujo.ModeloAprobacion = _context.ModeloAprobacion.Where(c => c.id == flujoAprobacion.modeloId).FirstOrDefault();
                    nuevoFlujo.estatus = flujoAprobacion.estatus;
                    
                    listaNuevoFlujo.Add(nuevoFlujo);
                }

                foreach (FlujoAprobacion flujo in listaNuevoFlujo)
                {
                    _context.FlujoAprobaciones.Add(flujo);
                    await _context.DbContext.SaveChangesAsync();
                }

                return flujoAprobacion;
            }
            catch (Exception ex)
            {
                throw new FlujoAprobacionException("Error al agregar el flujo de aprobacion", ex, _logger);
            }
        }




        public async Task<FlujoAprobacionDTO> ActualizarEstadoTicketFlujoDAO(FlujoAprobacionDTO flujoAprobacion)
        {
            try
            {
                var ticket = await _context.Tickets.FindAsync(flujoAprobacion.ticketId);
                List<FlujoAprobacion> listaEstadoTicket = new List<FlujoAprobacion>();
                List<FlujoAprobacion> listaEstadoActualizadoTicket = new List<FlujoAprobacion>();
                listaEstadoTicket = _context.FlujoAprobaciones.Where(c => c.ticketid == ticket.id).ToList();

                foreach(FlujoAprobacion flujo in listaEstadoTicket)
                {
                    flujo.estatus = flujoAprobacion.estatus;
                    listaEstadoActualizadoTicket.Add(flujo);
                }

                _context.FlujoAprobaciones.UpdateRange(listaEstadoActualizadoTicket);
                await _context.DbContext.SaveChangesAsync();

                return flujoAprobacion;
            }
            catch (Exception ex)
            {
                throw new FlujoAprobacionException("Error al actualizar el flujo de aprobacion", ex, _logger);
            }
        }
    }
}
