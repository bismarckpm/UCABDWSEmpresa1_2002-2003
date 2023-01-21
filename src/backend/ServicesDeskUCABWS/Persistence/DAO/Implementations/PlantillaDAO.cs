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
    public class PlantillaDAO : IPlantillaDAO
    {

        private readonly IMigrationDbContext _context;


        private readonly IMapper _mapper;
        private readonly ILogger<PlantillaDAO> _logger;
        public PlantillaDAO(IMapper mapper, IMigrationDbContext context, ILogger<PlantillaDAO> logger)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }



        public async Task<PlantillaDTO> AgregarPlantillaDAO(Plantilla plantilla)
        {
            try
            {
                // validar que el estado exista
                var estado = await _context.Estados.FirstOrDefaultAsync(x => x.id == plantilla.EstadoId);
                if (estado == null)
                {
                    _logger.LogError("El estado no existe");
                    throw new Exception("El estado con id: " + plantilla.EstadoId + " no existe");
                }
                var etiqueta = await _context.Etiquetas.FirstOrDefaultAsync(x => x.id == estado.EtiquetaId);
                var cuerpo = "Estado: " + estado.nombre + " -- " + etiqueta!.nombre;
                plantilla.cuerpo = string.Format("{0}{2}{1}", plantilla.cuerpo, cuerpo, "| |");
                _context.Plantillas.Add(plantilla);
                await _context.DbContext.SaveChangesAsync();
                _logger.LogInformation("Plantilla agregada exitosamente en la base de datos");
                return _mapper.Map<PlantillaDTO>(plantilla);
            }
            catch (Exception ex)
            {
                throw new PlantillaException(ex.Message, ex, _logger);
            }
        }

        public Task<List<PlantillaDTO>> ObtenerPlantillasDAO()
        {
            try
            {
                return _context.Plantillas.Select(x => _mapper.Map<PlantillaDTO>(x)).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new PlantillaException("Error al consultar las plantillas", ex, _logger);
            }

        }

        public async Task<PlantillaDTO> ObtenerPlantillaDAO(int id)
        {
            try
            {
                var Plantilla = await _context.Plantillas.FindAsync(id);
                if (Plantilla == null)
                {
                    _logger.LogWarning("No se encontro la plantilla con id: " + id);
                    throw new Exception("No se encontro la plantilla con id: " + id);
                }
                _logger.LogInformation("Plantilla encontrada exitosamente");
                return _mapper.Map<PlantillaDTO>(Plantilla);
            }
            catch (Exception ex)
            {
                throw new PlantillaException(ex.Message, ex, _logger);
            }
        }

        public async Task<PlantillaDTO> ActualizarPlantillaDAO(Plantilla plantilla, int id)
        {
            try
            {
                var plantillaOld = await _context.Plantillas.FindAsync(id);

                if (plantillaOld == null)
                {
                    _logger.LogWarning("No se encontro la plantilla con id: " + id);
                    throw new Exception("No se encontro la plantilla con id: " + id);
                }

                plantillaOld.titulo = plantilla.titulo;
                plantillaOld.cuerpo = plantilla.cuerpo;
                plantillaOld.EstadoId = plantilla.EstadoId;

                await _context.DbContext.SaveChangesAsync();
                _logger.LogInformation("Plantilla actualizada exitosamente");
                return _mapper.Map<PlantillaDTO>(plantillaOld);

            }
            catch (Exception ex)
            {
                throw new PlantillaException(ex.Message, ex, _logger);
            }
        }

        public async Task<Boolean> EliminarPlantillaDAO(int id)
        {
            try
            {
                var existe = await _context.Plantillas.FindAsync(id);
                if (existe == null)
                {
                    _logger.LogWarning("No se encontro la plantilla con id: " + id);
                    return false;
                }

                _context.Plantillas.Remove(existe);
                await _context.DbContext.SaveChangesAsync();
                _logger.LogInformation("Plantilla eliminada exitosamente");
                return true;
            }
            catch (Exception ex)
            {
                throw new PlantillaException("Error al eliminar la plantilla", ex, _logger);
            }
        }
    }
}