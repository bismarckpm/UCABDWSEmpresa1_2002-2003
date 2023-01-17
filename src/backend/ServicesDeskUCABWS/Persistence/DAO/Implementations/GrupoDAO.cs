using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Database;
using ServicesDeskUCABWS.Persistence.Entity;

namespace ServicesDeskUCABWS.Persistence.DAO.Implementations
{
    public class GrupoDAO : IGrupoDAO
    {
        private static DesignTimeDBContextFactory design = new DesignTimeDBContextFactory();
        public readonly IMigrationDbContext _context = design.CreateDbContext(null);
        private readonly IMapper _mapper;
        private readonly ILogger<GrupoDAO> _log;


        public GrupoDAO(IMapper mapper, ILogger<GrupoDAO> log, IMigrationDbContext context)
        {
            _mapper = mapper;
            _log = log;
            _context = context;
        }



        /// Agregar Grupo
        public async Task<ActionResult<GrupoDTO>> AgregarGrupoDAO(Grupo grupo)
        {
            try
            {
                _context.Grupo.Add(grupo);
                await _context.DbContext.SaveChangesAsync();

                return _mapper.Map<GrupoDTO>(grupo);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw;
            }
        }


        /// Consultar Grupos
        public Task<List<Grupo>> ConsultarGrupoDAO()
        {
            try
            {
                return _context.Grupo.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw ex.InnerException!;
            }
        }

        /// Consultar Grupo por ID
        public async Task<ActionResult<Grupo>> ConsultarGrupoByIdDAO(int id)
        {
            try
            {
                var grupo = await _context.Grupo.FindAsync(id);

                if (grupo == null)
                {
                    return new Grupo();
                }
                return grupo;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception("Error al Consultar por id: " + id, ex);
            }
        }


        /// Actualizar Grupo
        public async Task<ActionResult<Grupo>> ActualizarGrupoDAO(Grupo grupo, int id)
        {
            try
            {
                var grupoOld = await _context.Grupo.FindAsync(id);
                if (grupoOld == null)
                {
                    return new Grupo();
                }

                grupoOld.nombre = grupo.nombre;
                grupoOld.departamentoid = grupo.departamentoid;

                await _context.DbContext.SaveChangesAsync();
                return grupoOld;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw;
            }
        }


        /// Eliminar cargo
        public async Task<ActionResult> EliminarGrupoDAO(int id)
        {
            try
            {
                var existe = await _context.Grupo.FindAsync(id);
                if (existe == null)
                {
                    return new NotFoundResult();
                }

                _context.Grupo.Remove(existe);
                await _context.DbContext.SaveChangesAsync();

                return new OkResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw;

            }
        }



    }
}
