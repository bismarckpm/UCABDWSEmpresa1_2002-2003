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

<<<<<<< HEAD
                var Modelo = _context.ModeloAprobacion.Where(c => c.categoriaid == listaTickets.categoria.id).FirstOrDefault();    
                List<FlujoAprobacion> deps = new List<FlujoAprobacion>();
=======
                var Modelo = _context.ModeloAprobacion.Where(c => c.categoriaid == listaTickets.categoria.id).FirstOrDefault();

>>>>>>> 46f405efc3fcb381439454889b1ee67f74588563

                //if (Modelo.Discriminator == "ModeloJerarquico")
                //{
                //var ModeloCargos = _context.ModeloJerarquicoCargos.Where(c => c.modelojerarquicoid == Modelo.id).ToList();

                //    foreach (ModeloJerarquicoCargos modelo in ModeloCargos)
                //    {
                //        var a = (from usua in _context.Usuario
                //                 join dep in _context.Cargos on usua.cargo equals dep
                //                 join tp in _context.TipoCargos on dep.tipoCargo equals tp
                //                 where tp.id == modelo.TipoCargoid
                //                 select new UsuarioDTO()
                //                 {
                //                     id = usua.id
                //                 }).FirstOrDefault();
                //        var emp = _context.Usuario.Where(c => c.id == a.id).FirstOrDefault();
                //        FlujoAprobacion f = new FlujoAprobacion
                //        {
                //            empleadoid = a.id,
                //            modeloid = modelo.Id,
                //            ticketid = listaTickets.id,
                //            ModeloAprobacion = Modelo,
                //            estatus = 0
                //        };
                //        _context.FlujoAprobaciones.AddRange(f);

                //    }
                //    _context.DbContext.SaveChanges();


                //}

                if (Modelo.Discriminator == "ModeloJerarquico")
                {
<<<<<<< HEAD
                var ModeloCargos = _context.ModeloJerarquicoCargos.Where(c => c.modelojerarquicoid == Modelo.id).ToList();
               
=======
                    var ModeloCargos = _context.ModeloJerarquicoCargos.Where(c => c.modelojerarquicoid == Modelo.id).ToList();
                    List<FlujoAprobacion> deps = new List<FlujoAprobacion>();
>>>>>>> 46f405efc3fcb381439454889b1ee67f74588563
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
<<<<<<< HEAD
                        var emp = _context.Empleados.Where(c => c.id == a.id).FirstOrDefault();
                        var i = 1;
                        deps.Add(new FlujoAprobacion
=======
                        var emp = _context.Usuario.Where(c => c.id == a.id).FirstOrDefault();

                        FlujoAprobacion f = new FlujoAprobacion
>>>>>>> 46f405efc3fcb381439454889b1ee67f74588563
                        {
                            Empleado = emp,
                            empleadoid = a.id,
                            modeloid = modelo.Id,
                            ticketid = listaTickets.id,
                            ModeloAprobacion = Modelo,
<<<<<<< HEAD
                            estatus = 0, 
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
                            estatus = 0, 
                            Ticket = listaTickets
                        });
                    }
                     _context.FlujoAprobaciones.AddRange(deps);
                      _context.DbContext.SaveChanges();
                    } 
=======
                            estatus = 0
                        };
                        deps.Add(f);


                    }
                    _context.FlujoAprobaciones.AddRange(deps);
                    _context.DbContext.SaveChanges();


                }
>>>>>>> 46f405efc3fcb381439454889b1ee67f74588563
                return "Flujo creado";
            }
            catch (Exception ex)
            {
                throw new FlujoAprobacionException("Error al agregar el flujo de aprobacion", ex, _logger);
            }
        }
       


    }
}
