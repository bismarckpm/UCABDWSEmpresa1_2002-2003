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

        public CargoDAO(IMapper mapper)
        {
            _mapper = mapper;
        }

        public CargoDAO(IMigrationDbContext context)
        {
            this._context = context;
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
                throw ex.InnerException!;
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
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw ex.InnerException!;
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
                var cargoOld = await ObtenerCargoByIdDAO(id);
                if (cargoOld.Value == null)
                {
                    return new Cargo();
                }

                cargoOld.Value.nombre = cargo.nombre;
                cargoOld.Value.tipoCargoId = cargo.tipoCargoId;

                await _context.DbContext.SaveChangesAsync();
                return cargoOld;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw ex.InnerException!;
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
                var existe = await ObtenerCargoByIdDAO(id);
                if (existe.Value?.id == 0)
                {
                    return new NotFoundResult();
                }

                _context.Cargos.Remove(existe.Value!);
                await _context.DbContext.SaveChangesAsync();

                return new OkResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw ex.InnerException!;

            }
        }

        /// <summary>
        /// Obtener Usuarios By CargoId   -- En elaboracion
        /// </summary>
        /// <param name="cargoid"></param>
        /// <returns></returns>
        public async Task<List<Usuario>> ObtenerUsuariosByCargoIdDAO(int cargoid)
        {
            try
            {
                return _context.Usuario.Where(p => p.cargo.id == cargoid).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw ex.InnerException!;
            }
        }
    }
}