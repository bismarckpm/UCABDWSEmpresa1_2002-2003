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


        public async Task<EstadoDTO> AgregarEstadoDAO(Estado estado)
        {
            try
            {
                // validar que la etiqueta exista
                var etiqueta = await _context.Etiquetas.FindAsync(estado.EtiquetaId);
                if (etiqueta == null)
                {
                    _logger.LogError("La etiqueta no existe");
                    throw new Exception("La etiqueta con id: " + estado.EtiquetaId + " no existe");
                }
                // guardar en la base de datos
                await _context.Estados.AddAsync(estado);
                await _context.DbContext.SaveChangesAsync();
                _logger.LogInformation("Estado agregado exitosamente en la base de datos");
                return _mapper.Map<EstadoDTO>(estado);
            }
            catch (Exception ex)
            {
                throw new EstadoException(ex.Message, ex, _logger);
            }

        }


        public async Task<EstadoResponseDTO> GetEstadoDAO(int id)
        {
            try
            {
                var query = await (from e in _context.Estados
                                   join et in _context.Etiquetas on e.EtiquetaId equals et.id
                                   where e.id == id
                                   select new EstadoResponseDTO()
                                   {
                                       id = e.id,
                                       Nombre = e.nombre,
                                       EtiquetaId = e.EtiquetaId,
                                       Etiqueta = et.nombre
                                   }).FirstOrDefaultAsync();

                if (query == null)
                {
                    _logger.LogError("El estado no existe");
                    throw new Exception("El estado con id: " + id + " no existe");
                }
                _logger.LogInformation("Estado consultado exitosamente");

                return query;
            }
            catch (Exception ex)
            {
                throw new EstadoException(ex.Message, ex, _logger);
            }
        }


        public async Task<List<EstadoResponseDTO>> GetEstadosDAO()
        {

            try
            {
                var query = await (from e in _context.Estados
                                   join et in _context.Etiquetas on e.EtiquetaId equals et.id
                                   select new EstadoResponseDTO()
                                   {
                                       id = e.id,
                                       Nombre = e.nombre,
                                       EtiquetaId = e.EtiquetaId,
                                       Etiqueta = et.nombre
                                   }).ToListAsync();
                _logger.LogInformation("Estados consultados exitosamente");
                return query;
            }
            catch (Exception ex)
            {
                throw new EstadoException("Error al consultar los estados", ex, _logger);
            }

        }

        public async Task<EstadoDTO> ActualizarEstadoDAO(Estado estado, int id)
        {

            try
            {
                var estadoOld = await _context.Estados.FirstOrDefaultAsync(estadoBD => estadoBD.id == id);
                if (estadoOld == null)
                {
                    _logger.LogError("El estado no existe");
                    throw new Exception("El estado con id: " + id + " no existe");
                }
                if (estadoOld.EtiquetaId != estado.EtiquetaId)
                {
                    //validar que la etiqueta exista
                    var etiqueta = await _context.Etiquetas.FindAsync(estado.EtiquetaId);
                    if (etiqueta == null)
                    {
                        _logger.LogError("La etiqueta no existe");
                        throw new Exception("La etiqueta con id: " + estado.EtiquetaId + " no existe");
                    }
                }
                estadoOld.nombre = estado.nombre;
                estadoOld.EtiquetaId = estado.EtiquetaId;
                await _context.DbContext.SaveChangesAsync();
                _logger.LogInformation("Estado actualizado exitosamente");
                return _mapper.Map<EstadoDTO>(estadoOld);
            }
            catch (Exception ex)
            {
                throw new EstadoException(ex.Message, ex, _logger);
            }

        }

        public async Task<Boolean> EliminarEstadoDAO(int id)
        {
            try
            {
                var estado = await _context.Estados.FirstOrDefaultAsync(estadoBD => estadoBD.id == id);
                if (estado == null)
                {
                    _logger.LogWarning("El estado no existe");
                    return false;
                }
                _context.Estados.Remove(estado);
                await _context.DbContext.SaveChangesAsync();
                _logger.LogInformation("Estado eliminado exitosamente");
                return true;
            }
            catch (Exception ex)
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
