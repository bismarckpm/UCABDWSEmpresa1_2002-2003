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
       

        public string AgregarFlujoAprobacionDAO(FlujoAprobacionDTO flujoAprobacion)
        {
            try
            {
                Ticket listaTickets = new Ticket();
                
                Ticket ticket1 = new Ticket();

                listaTickets = _context.Tickets.Where(c => c.id == flujoAprobacion.ticketId).Include(c=>c.asginadoa).
                    Include(c=>c.categoria).
                    Include(c => c.Estado).
                    Include(c=>c.creadopor).FirstOrDefault();

                var Modelo = _context.ModeloAprobacion.Where(c => c.categoriaid == listaTickets.categoria.id).FirstOrDefault();    


                if (Modelo.Discriminator == "ModeloJerarquico")
                {
                var ModeloCargos = _context.ModeloJerarquicoCargos.Where(c => c.modelojerarquicoid == Modelo.id).ToList();    
                                       
                    foreach(ModeloJerarquicoCargos modelo in ModeloCargos)
                    {
                   var a = (from usua in _context.Usuario
                     join dep in _context.Cargos on usua.cargo equals dep
                     join tp in _context.TipoCargos on dep.tipoCargo equals tp
                     where tp.id == modelo.TipoCargoid
                     select new UsuarioDTO()
                     {
                        id = usua.id
                      }).FirstOrDefault();
                      var emp = _context.Usuario.Where(c => c.id == a.id).FirstOrDefault();
                        FlujoAprobacion f = new FlujoAprobacion();
                        f.empleadoid = a.id;
                        f.modeloid = modelo.Id;
                        f.ticketid = listaTickets.id;
                        f.ModeloAprobacion = Modelo;
                        f.estatus = 0;
                        Console.WriteLine(f.ToString());
                        
                    }
                  
                }
                return "Flujo creado";
            }
            catch (Exception ex)
            {
                throw new FlujoAprobacionException("Error al agregar el flujo de aprobacion", ex, _logger);
            }
        }



    }
}
