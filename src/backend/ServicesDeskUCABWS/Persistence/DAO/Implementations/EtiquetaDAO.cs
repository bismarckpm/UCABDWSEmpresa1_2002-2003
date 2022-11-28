using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Exceptions;
using ServicesDeskUCABWS.BussinessLogic.Mapper;
using ServicesDeskUCABWS.Persistence.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Collections.Generic;


namespace ServicesDeskUCABWS.Persistence.DAO.Implementations
{
    public class EtiquetaDAO : IEtiquetaDAO
    {
        private readonly IMigrationDbContext _context;

        private readonly IMapper _mapper;
        private readonly ILogger<EtiquetaDAO> _logger;
        public EtiquetaDAO(IMapper mapper, IMigrationDbContext context, ILogger<EtiquetaDAO> logger)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }



        public async Task<ActionResult<EtiquetaDTO>> AgregarEtiquetaDAO(Etiqueta etiqueta)
        {
            try
            {
                _context.Etiquetas.Add(etiqueta);
                await _context.DbContext.SaveChangesAsync();
                _logger.LogInformation("Etiqueta agregada exitosamente en la base de datos");
                return _mapper.Map<EtiquetaDTO>(etiqueta);
            }
            catch (DbUpdateException ex)
            {
                throw new EtiquetaException("Error al agregar la etiqueta", ex, _logger);
            }

        }

        public Task<List<Etiqueta>> ConsultarEtiquetasDAO()
        {
            try
            {
                return _context.Etiquetas.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new EtiquetaException("Error al consultar las etiquetas", ex, _logger);
            }
        }

        public async Task<ActionResult<Etiqueta>> ObtenerEtiquetaDAO(int id)
        {
            try
            {
                var etiqueta = await _context.Etiquetas.FindAsync(id);
                if (etiqueta == null)
                {
                    _logger.LogWarning("No se encontró la etiqueta con id: " + id);
                    return new Etiqueta();
                }
                _logger.LogInformation("Etiqueta encontrada exitosamente");
                return etiqueta;
            }
            catch (Exception ex)
            {
                throw new EtiquetaException("Error al obtener la etiqueta", ex, _logger);
            }
        }

        public async Task<ActionResult<Etiqueta>> ActualizarEtiquetaDAO(Etiqueta etiqueta, int id)
        {

            try
            {
                var etiquetaOld = await _context.Etiquetas.FindAsync(id);
                if (etiquetaOld == null)
                {
                    _logger.LogWarning("No se encontró la etiqueta con id: " + id);
                    return new Etiqueta();
                }

                etiquetaOld.nombre = etiqueta.nombre;
                etiquetaOld.descripcion = etiqueta.descripcion;

                await _context.DbContext.SaveChangesAsync();
                _logger.LogInformation("Etiqueta actualizada exitosamente");
                return etiquetaOld;
            }
            catch (Exception ex)
            {
                throw new EtiquetaException("Error al actualizar la etiqueta", ex, _logger);
            }


        }

        public async Task<ActionResult> EliminarEtiquetaDAO(int id)
        {
            try
            {
                var existe = await _context.Etiquetas.FindAsync(id);
                if (existe == null)
                {
                    _logger.LogWarning("No se encontró la etiqueta con id: " + id);
                    return new NotFoundResult();
                }


                _context.Etiquetas.Remove(existe);
                await _context.DbContext.SaveChangesAsync();
                _logger.LogInformation("Etiqueta eliminada exitosamente");
                return new OkResult();
            }
            catch (Exception ex)
            {
                throw new EtiquetaException("Error al eliminar la etiqueta", ex, _logger);
            }
        }

    }
}