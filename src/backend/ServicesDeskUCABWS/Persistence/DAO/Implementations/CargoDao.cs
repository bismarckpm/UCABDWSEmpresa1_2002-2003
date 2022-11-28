using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Database;
using ServicesDeskUCABWS.Persistence.Entity;

namespace ServicesDeskUCABWS.Persistence.DAO.Implementations
{
    public class CargoDAO : ICargoDAO
    {
        private static DesignTimeDBContextFactory design = new DesignTimeDBContextFactory();
        public readonly IMigrationDbContext _context = design.CreateDbContext(null);
        private readonly IMapper _mapper;
        private readonly ILogger<CargoDAO> _log;


        public CargoDAO(IMapper mapper, ILogger<CargoDAO> log, IMigrationDbContext context)
        {
            _mapper = mapper;
            _log = log;
            _context = context;
        }


        /// <summary>
        /// Agregar Cargo DAO
        /// </summary>
        /// <param name="cargo"></param>
        /// <returns></returns>
        public async Task<ActionResult<CargoDTO>> AgregarCargoDAO(Cargo cargo)
        {
            try
            {
                _context.Cargos.Add(cargo);
                await _context.DbContext.SaveChangesAsync();

                return _mapper.Map<CargoDTO>(cargo);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw ;
            }
        }

        /// <summary>
        /// Obtener la lista de cargos
        /// </summary>
        /// <returns></returns>
        public Task<List<Cargo>> ConsultarCargoDAO()
        {
            try
            {
                return _context.Cargos.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw ex.InnerException!;
            }
        }

        /// <summary>
        /// Obtener Cargo por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult<Cargo>> ObtenerCargoByIdDAO(int id)
        {
            try
            {
                var cargo = await _context.Cargos.FindAsync(id);

                if (cargo == null)
                {
                    return new Cargo();
                }
                return cargo;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception("Error al Consultar por id: " + id, ex);
            }
        }

        /// <summary>
        /// Actualizar Cargo
        /// </summary>
        /// <param name="cargo"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult<Cargo>> ActualizarCargoDAO(Cargo cargo, int id)
        {
            try
            {
                var cargoOld = await _context.Cargos.FindAsync(id);
                if (cargoOld == null)
                {
                    return new Cargo();
                }

                cargoOld.nombre = cargo.nombre;
                cargoOld.tipoCargoId = cargo.tipoCargoId;

                await _context.DbContext.SaveChangesAsync();
                return cargoOld;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// Eliminar cargo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> EliminarCargoDAO(int id)
        {
            try
            {
                var existe = await _context.Cargos.FindAsync(id);
                if (existe == null)
                {
                    return new NotFoundResult();
                }

                _context.Cargos.Remove(existe);
                await _context.DbContext.SaveChangesAsync();

                return new OkResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw ;

            }
        }

        ///// <summary>
        ///// Obtener Usuarios By CargoId   -- En elaboracion
        ///// </summary>
        ///// <param name="cargoid"></param>
        ///// <returns></returns>
        //public async Task<List<Usuario>> ObtenerUsuariosByCargoIdDAO(int cargoid)
        //{
        //    try
        //    {
        //        return _context.Usuario.Where(p => p.cargo.id == cargoid).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message + " : " + ex.StackTrace);
        //        throw ex.InnerException!;
        //    }
        //}

       
    }
}