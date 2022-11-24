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



        public async Task<ActionResult<PlantillaDTO>> AgregarPlantillaDAO(Plantilla Plantilla)
        {
            try
            {
                _context.Plantillas.Add(Plantilla);
                await _context.DbContext.SaveChangesAsync();
                _logger.LogInformation("Plantilla agregada exitosamente en la base de datos");
                return _mapper.Map<PlantillaDTO>(Plantilla);
            }
            catch (DbUpdateException ex)
            {
                throw new PlantillaException("Error al agregar la plantilla", ex, _logger);
            }
        }

        public Task<List<Plantilla>> ObtenerPlantillasDAO()
        {
            try
            {
                return _context.Plantillas.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new PlantillaException("Error al consultar las plantillas", ex, _logger);
            }

        }

        public async Task<ActionResult<Plantilla>> ObtenerPlantillaDAO(int id)
        {
            try
            {
                var Plantilla = await _context.Plantillas.FindAsync(id);
                if (Plantilla == null)
                {
                    _logger.LogWarning("No se encontro la plantilla con id: " + id);
                    return new NotFoundResult();
                }
                _logger.LogInformation("Plantilla encontrada exitosamente");
                return Plantilla;
            }
            catch (DbUpdateException ex)
            {
                throw new PlantillaException("Error al obtener la plantilla", ex, _logger);
            }
        }

        public async Task<ActionResult> ActualizarPlantillaDAO(Plantilla plantilla, int id)
        {
            try
            {
                var plantillaOld = await _context.Plantillas.FindAsync(id);

                if (plantillaOld == null)
                {
                    _logger.LogWarning("No se encontro la plantilla con id: " + id);
                    return new NotFoundResult();
                }

                plantillaOld.titulo = plantilla.titulo;
                plantillaOld.cuerpo = plantilla.cuerpo;
                plantillaOld.tipo = plantilla.tipo;

                await _context.DbContext.SaveChangesAsync();
                _logger.LogInformation("Plantilla actualizada exitosamente");
                return new OkResult();

            }
            catch (DbUpdateException ex)
            {
                throw new PlantillaException("Error al actualizar la plantilla", ex, _logger);
            }
        }

        public async Task<ActionResult> EliminarPlantillaDAO(int id)
        {
            try
            {
                var existe = await _context.Plantillas.FindAsync(id);
                if (existe == null)
                {
                    _logger.LogWarning("No se encontro la plantilla con id: " + id);
                    return new NotFoundResult();
                }

                _context.Plantillas.Remove(existe);
                await _context.DbContext.SaveChangesAsync();
                _logger.LogInformation("Plantilla eliminada exitosamente");
                return new OkResult();
            }
            catch (DbUpdateException ex)
            {
                throw new PlantillaException("Error al eliminar la plantilla", ex, _logger);
            }
        }
    }
}