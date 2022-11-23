using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Exceptions;
using ServicesDeskUCABWS.Persistence.Database;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;


namespace ServicesDeskUCABWS.Persistence.DAO.Implementations
{
    public class EstadoDAO : IEstadoDAO
    {
        private readonly IMigrationDbContext _context;

        private readonly IMapper _mapper;
        private readonly ILogger<EstadoDAO> _logger;
        public EstadoDAO(IMapper mapper, IMigrationDbContext context, ILogger<EstadoDAO> logger)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }


        public async Task<ActionResult<EstadoDTO>> AgregarEstadoDAO(Estado estado)
        {
            try
            {
                // validar que la etiqueta exista
                var etiqueta = await _context.Etiquetas.FindAsync(estado.EtiquetaId);
                if (etiqueta == null)
                {
                    _logger.LogError("La etiqueta no existe");
                    throw new EstadoException("La etiqueta no existe");
                }
                // guardar en la base de datos
                await _context.Estados.AddAsync(estado);
                await _context.DbContext.SaveChangesAsync();
                _logger.LogInformation("Estado agregado exitosamente en la base de datos");
                return _mapper.Map<EstadoDTO>(estado);
            }
            catch (DbUpdateException ex)
            {
                throw new EstadoException("Error al agregar el estado", ex, _logger);
            }

        }

        // public async Task<ActionResult<List<EstadoDTO>>> GetEstadosEtiquetaDAO(int idEtiqueta)
        // {
        //     try
        //     {
        //         var estados = await _context.Estados
        //                         .Include(estadoBD => estadoBD.etiqueta)
        //                         .Where(e => e.EtiquetaId == idEtiqueta).ToListAsync();

        //         _logger.LogInformation("Estados de la etiqueta consultados exitosamente");
        //         return _mapper.Map<List<EstadoDTO>>(estados);
        //     }
        //     catch (Exception ex)
        //     {
        //         throw new EstadoException("Error al consultar los estados de la etiqueta", ex, _logger);
        //     }
        //  }

        public async Task<ActionResult<EstadoDTO>> GetEstadoDAO(int id)
        {
            try
            {
                var estado = await _context.Estados
                             .Include(estadoBD => estadoBD.etiqueta)
                             .FirstOrDefaultAsync(estadoBD => estadoBD.id == id);
                if (estado == null)
                {
                    _logger.LogError("El estado no existe");
                    throw new EstadoException("El estado no existe");
                }
                _logger.LogInformation("Estado consultado exitosamente");
                return _mapper.Map<EstadoDTO>(estado);
            }
            catch (Exception ex)
            {
                throw new EstadoException("Error al consultar el estado", ex, _logger);
            }
        }


        public async Task<ActionResult<List<EstadoDTO>>> GetEstadosDAO()
        {

            try
            {
                var estados = await _context.Estados.ToListAsync();
                _logger.LogInformation("Estados consultados exitosamente");
                return _mapper.Map<List<EstadoDTO>>(estados);
            }
            catch (Exception ex)
            {
                throw new EstadoException("Error al consultar los estados", ex, _logger);
            }

        }

        public async Task<ActionResult> ActualizarEstadoDAO(Estado estado, int id)
        {

            try
            {
                var estadoOld = await _context.Estados.FirstOrDefaultAsync(estadoBD => estadoBD.id == id);
                if (estadoOld == null)
                {
                    _logger.LogError("El estado no existe");
                    throw new EstadoException("El estado no existe");
                }
                if (estadoOld.EtiquetaId != estado.EtiquetaId)
                {
                    //validar que la etiqueta exista
                    var etiqueta = await _context.Etiquetas.FindAsync(estado.EtiquetaId);
                    if (etiqueta == null)
                    {
                        _logger.LogError("La etiqueta no existe");
                        throw new EstadoException("La etiqueta no existe");
                    }
                }
                estadoOld.nombre = estado.nombre;
                estadoOld.EtiquetaId = estado.EtiquetaId;
                await _context.DbContext.SaveChangesAsync();
                _logger.LogInformation("Estado actualizado exitosamente");
                return new OkResult();
            }
            catch (DbUpdateException ex)
            {
                throw new EstadoException("Error al actualizar el estado", ex, _logger);
            }

        }

        public async Task<ActionResult> EliminarEstadoDAO(int id)
        {
            try
            {
                var estado = await _context.Estados.FirstOrDefaultAsync(estadoBD => estadoBD.id == id);
                if (estado == null)
                {
                    _logger.LogWarning("El estado no existe");
                    return new NotFoundResult();
                }
                _context.Estados.Remove(estado);
                await _context.DbContext.SaveChangesAsync();
                _logger.LogInformation("Estado eliminado exitosamente");
                return new OkResult();
            }
            catch (DbUpdateException ex)
            {
                throw new EstadoException("Error al eliminar el estado", ex, _logger);
            }

        }

        // public async Task<ActionResult> ActualizarEstadoEtiquetaDAO(EstadoEtiquetaUpdateDTO estadoEtiquetaUpdateDTO)
        // {
        //     try
        //     {
        //         var estado = await _context.Estados
        //                         .Include(estadoBD => estadoBD.etiqueta)
        //                         .FirstOrDefaultAsync(estadoBD => estadoBD.id == estadoEtiquetaUpdateDTO.id);
        //         if (estado == null)
        //         {
        //             _logger.LogWarning("El estado no existe");
        //             return new NotFoundResult();
        //         }
        //         estado.EtiquetaId = estadoEtiquetaUpdateDTO.New_EtiquetaId;
        //         await _context.DbContext.SaveChangesAsync();
        //         _logger.LogInformation("Estado actualizado exitosamente");
        //         return new OkResult();
        //     }
        //     catch (DbUpdateException ex)
        //     {
        //         throw new EstadoException("Error al actualizar el estado", ex, _logger);
        //     }
        // }



    }
}
