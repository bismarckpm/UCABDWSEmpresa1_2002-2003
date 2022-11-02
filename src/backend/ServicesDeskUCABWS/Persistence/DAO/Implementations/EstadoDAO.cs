using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Database;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;


namespace ServicesDeskUCABWS.Persistence.DAO.Implementations
{
    public class EstadoDAO : IEstadoDAO
    {
        private static DesignTimeDBContextFactory design = new DesignTimeDBContextFactory();
        public readonly IMigrationDbContext _context = design.CreateDbContext(null);

        private readonly IMapper _mapper;
        public EstadoDAO(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ActionResult<EstadoEtiquetaDTO>> AgregarEstadoDAO(Estado estado)
        {
            try
            {
                _context.Estados.Add(estado);
                await _context.DbContext.SaveChangesAsync();

                return _mapper.Map<EstadoEtiquetaDTO>(estado);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw ex.InnerException!;
            }
        }

        public async Task<ActionResult<List<EstadoDTO>>> GetEstadosEtiquetaDAO(int idEtiqueta)
        {
            try
            {
                var estados = await _context.Estados
                                .Include(estadoBD => estadoBD.etiqueta)
                                .Where(e => e.EtiquetaId == idEtiqueta).ToListAsync();

                return _mapper.Map<List<EstadoDTO>>(estados);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw ex.InnerException!;
            }
        }

        public async Task<ActionResult<EstadoDTO>> GetEstadoDAO(int id)
        {
            try
            {
                var estado = await _context.Estados
                             .Include(estadoBD => estadoBD.etiqueta)
                             .FirstOrDefaultAsync(estadoBD => estadoBD.id == id);
                if (estado == null)
                {
                    return new NotFoundResult();
                }

                return _mapper.Map<EstadoDTO>(estado);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw ex.InnerException!;
            }
        }


        public async Task<ActionResult<List<EstadoDTO>>> GetEstadosDAO()
        {
            try
            {
                var estados = await _context.Estados.ToListAsync();

                return _mapper.Map<List<EstadoDTO>>(estados);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw ex.InnerException!;
            }
        }

        public async Task<ActionResult> ActualizarEstadoDAO(Estado estado, int id)
        {
            try
            {
                var estadoOld = await _context.Estados.FirstOrDefaultAsync(estadoBD => estadoBD.id == id);
                if (estadoOld == null)
                {
                    return new NotFoundResult();
                }
                estadoOld.nombre = estado.nombre;
                await _context.DbContext.SaveChangesAsync();
                return new OkResult();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw ex.InnerException!;
            }
        }

        public async Task<ActionResult> EliminarEstadoDAO(int id)
        {
            try
            {
                var estado = await _context.Estados.FirstOrDefaultAsync(estadoBD => estadoBD.id == id);
                if (estado == null)
                {
                    return new NotFoundResult();
                }
                _context.Estados.Remove(estado);
                await _context.DbContext.SaveChangesAsync();
                return new OkResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw ex.InnerException!;
            }
        }

        public async Task<ActionResult> ActualizarEstadoEtiquetaDAO(EstadoEtiquetaUpdateDTO estadoEtiquetaUpdateDTO)
        {
            try
            {
                var estado = await _context.Estados
                                .Include(estadoBD => estadoBD.etiqueta)
                                .FirstOrDefaultAsync(estadoBD => estadoBD.id == estadoEtiquetaUpdateDTO.id);
                if (estado == null)
                {
                    return new NotFoundResult();
                }
                estado.EtiquetaId = estadoEtiquetaUpdateDTO.New_EtiquetaId;
                await _context.DbContext.SaveChangesAsync();
                return new OkResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw ex.InnerException!;
            }
        }



    }
}
