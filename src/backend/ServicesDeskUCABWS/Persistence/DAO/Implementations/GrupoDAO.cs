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
    public class GrupoDAO : IGrupoDAO
    {
        private readonly IMigrationDbContext _context;

        private readonly IMapper _mapper;
        private readonly ILogger<GrupoDAO> _logger;
        public GrupoDAO(IMapper mapper, IMigrationDbContext context, ILogger<GrupoDAO> logger)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }


        public async Task<GrupoDTO> AgregarGrupoDAO(Grupo grupo)
        {
            try
            {
                // validar que exista el departamento
                var dep = await _context.Departamentos.FindAsync(grupo.departamentoid);
                if (dep == null)
                {
                    _logger.LogError("El departamento no existe");
                    throw new Exception("El departamento con id: " + grupo.departamentoid + " no existe");
                }
                // guardar en la base de datos
                await _context.Grupo.AddAsync(grupo);
                await _context.DbContext.SaveChangesAsync();
                _logger.LogInformation("Grupo agregado exitosamente en la base de datos");
                return _mapper.Map<GrupoDTO>(grupo);
            }
            catch (Exception ex)
            {
                throw new GrupoException(ex.Message, ex, _logger);
            }

        }


        public async Task<GrupoResponseDTO> ObtenerGrupoByIdDAO(int id)
        {
            try
            {
                var query = await (from g in _context.Grupo
                                   join gt in _context.Departamentos on g.departamentoid equals gt.id
                                   where g.id == id
                                   select new GrupoResponseDTO()
                                   {
                                       id = g.id,
                                       nombre = g.nombre,
                                       departamentoid = g.departamentoid
                                   }).FirstOrDefaultAsync();

                if (query == null)
                {
                    _logger.LogError("El Grupo no existe");
                    throw new Exception("El Grupo con id: " + id + " no existe");
                }
                _logger.LogInformation("Grupo consultado exitosamente");

                return query;
            }
            catch (Exception ex)
            {
                throw new GrupoException(ex.Message, ex, _logger);
            }
        }


        public async Task<List<GrupoResponseDTO>> ObtenerGruposDAO()
        {

            try
            {
                var query = await (from g in _context.Grupo
                                   join gt in _context.Grupo on g.departamentoid equals gt.id
                                   select new GrupoResponseDTO()
                                   {
                                       id = g.id,
                                       nombre = g.nombre,
                                       departamentoid = g.departamentoid
                                   }).ToListAsync();
                _logger.LogInformation("Grupos consultados exitosamente");
                return query;
            }
            catch (Exception ex)
            {
                throw new GrupoException("Error al consultar los grupos", ex, _logger);
            }

        }

        public async Task<GrupoDTO> ActualizarGrupoDAO(Grupo grupo, int id)
        {

            try
            {
                var Old = await _context.Grupo.FirstOrDefaultAsync(BD => BD.id == id);
                if (Old == null)
                {
                    _logger.LogError("El grupo no existe");
                    throw new Exception("El grupo con id: " + id + " no existe");
                }
                if (Old.departamentoid != grupo.departamentoid)
                {
                    //validar que exista el departamento
                    var dep = await _context.Etiquetas.FindAsync(grupo.departamentoid);
                    if (dep == null)
                    {
                        _logger.LogError("El Departamento no existe");
                        throw new Exception("El departamento con id: " + grupo.departamentoid + " no existe");
                    }
                }
                Old.nombre = grupo.nombre;
                Old.departamentoid = grupo.departamentoid;
                await _context.DbContext.SaveChangesAsync();
                _logger.LogInformation("Grupo actualizado exitosamente");
                return _mapper.Map<GrupoDTO>(Old);
            }
            catch (Exception ex)
            {
                throw new GrupoException(ex.Message, ex, _logger);
            }

        }

        public async Task<Boolean> EliminarGrupoDAO(int id)
        {
            try
            {
                var g = await _context.Grupo.FirstOrDefaultAsync(BD => BD.id == id);
                if (g == null)
                {
                    _logger.LogWarning("El grupo no existe");
                    return false;
                }
                _context.Grupo.Remove(g);
                await _context.DbContext.SaveChangesAsync();
                _logger.LogInformation("Grupo eliminado exitosamente");
                return true;
            }
            catch (Exception ex)
            {
                throw new GrupoException("Error al eliminar el grupo", ex, _logger);
            }

        }




    }
}

