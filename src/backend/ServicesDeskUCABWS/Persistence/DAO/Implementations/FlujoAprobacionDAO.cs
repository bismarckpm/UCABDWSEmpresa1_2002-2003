using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NLog.LayoutRenderers.Wrappers;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Exceptions;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Database;
using ServicesDeskUCABWS.Persistence.Entity;
using System.Collections.Generic;

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
        public string AgregarFlujoAprobacionDAO(FlujoAprobacionDTO flujoAprobacion)
        {
            try
            {
                Ticket listaTickets = new Ticket();
                
                listaTickets = _context.Tickets.Where(c => c.id == flujoAprobacion.ticketId).Include(c=>c.asginadoa).
                    Include(c=>c.categoria).
                    Include(c => c.Estado).
                    Include(c=>c.creadopor).FirstOrDefault();

                var Modelo = _context.ModeloAprobacion.Where(c => c.categoriaid == listaTickets.categoria.id).FirstOrDefault();    
                List<FlujoAprobacion> deps = new List<FlujoAprobacion>();

                if (Modelo.Discriminator == "ModeloJerarquico")
                {
                var ModeloCargos = _context.ModeloJerarquicoCargos.Where(c => c.modelojerarquicoid == Modelo.id).ToList();
               
                    foreach (ModeloJerarquicoCargos modelo in ModeloCargos)
                    {
                        var a = (from usua in _context.Usuario
                                 join dep in _context.Cargos on usua.cargo equals dep
                                 join tp in _context.TipoCargos on dep.tipoCargo equals tp
                                 where tp.id == modelo.TipoCargoid
                                 select new UsuarioDTO()
                                 {
                                     id = usua.id
                                 }).FirstOrDefault();
                        var emp = _context.Empleados.Where(c => c.id == a.id).FirstOrDefault();
                        var i = 1;
                        deps.Add(new FlujoAprobacion
                        {
                            Empleado = emp,
                            empleadoid = a.id,
                            modeloid = modelo.Id,
                            ticketid = listaTickets.id,
                            ModeloAprobacion = Modelo,
                            Estado = listaTickets.Estado, 
                            estatusid = listaTickets.Estado.id,
                            Ticket = listaTickets
                        });
                    }
                     _context.FlujoAprobaciones.AddRange(deps);
                      _context.DbContext.SaveChanges();
                      
                }else{
                   var Modeloparalelo = _context.ModeloParalelos.Where(c => c.id == Modelo.id).FirstOrDefault(); 
                     var a = (from usua in _context.Usuario
                                 where usua.Grupo == listaTickets.asginadoa.Grupo 
                                 select new UsuarioDTO()
                                 {
                                     id = usua.id
                                 }).ToList().Take(Modeloparalelo.cantidaddeaprobacion);
                   foreach (UsuarioDTO modelo in a)
                    {
                        deps.Add(new FlujoAprobacion
                        {
                            empleadoid = modelo.id,
                            modeloid = Modelo.id,
                            ticketid = listaTickets.id,
                            ModeloAprobacion = Modelo,
                            Estado = listaTickets.Estado,
                            estatusid = listaTickets.Estado.id,
                            Ticket = listaTickets
                        });
                    }
                     _context.FlujoAprobaciones.AddRange(deps);
                      _context.DbContext.SaveChanges();
                    } 
                return "Flujo creado";
            }
            catch (Exception ex)
            {
                throw new FlujoAprobacionException("Error al agregar el flujo de aprobacion", ex, _logger);
            }
        }

        /// <summary>
        /// Actualiza el estado en la tabla de flujoaprobaciones dado el ticketId y el estadoId
        /// </summary>
        /// <param name="flujoAprobacion"></param>
        /// <param name="estadoId"></param>
        /// <returns></returns>
        /// <exception cref="FlujoAprobacionException"></exception>
        public string ActualizarEstadoFlujoDAO(FlujoAprobacionDTO flujoAprobacion)
        {
            try
            {
                FlujoAprobacion flujo = _context.FlujoAprobaciones.
                                        Where(c=>c.ticketid == flujoAprobacion.ticketId).Include(c=>c.Estado).FirstOrDefault();

                flujo.estatusid = flujoAprobacion.estadoId;
                flujo.Estado = _context.Estados.Find(flujoAprobacion.estadoId);

                _context.FlujoAprobaciones.Update(flujo);
                _context.DbContext.SaveChanges();

                return "Estado ticket en flujo, actualizado";
            }
            catch (Exception ex)
            {
                throw new FlujoAprobacionException("Error al agregar el flujo de aprobacion", ex, _logger);
            }
        }


        /// <summary>
        /// Obtiene el id de ticket con el id del estado
        /// </summary>
        /// <param name="flujoAprobacion"></param>
        /// <returns></returns>
        /// <exception cref="FlujoAprobacionException"></exception>
        public FlujoAprobacionDTO ObtenerEstadoTicketFlujoDAO(int ticketId)
        {
            try
            {
                FlujoAprobacionDTO estadoTicket = new FlujoAprobacionDTO();
                FlujoAprobacion flujo = _context.FlujoAprobaciones.
                                        Where(c => c.ticketid == ticketId).Include(c => c.Estado).FirstOrDefault();
                estadoTicket.estadoId = flujo.Estado.id;
                estadoTicket.ticketId = ticketId;

                return estadoTicket;
            }
            catch (Exception ex)
            {
                throw new FlujoAprobacionException("Error al obtener estado del ticket en el flujo de aprobacion", ex, _logger);
            }

        }



    }
}
