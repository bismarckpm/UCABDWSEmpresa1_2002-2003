using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace ServicesDeskUCABWS.Persistence.DAO.Implementations
{
    public class PlantillaDAO : IPlantillaDAO
    {
        private static DesignTimeDBContextFactory design = new DesignTimeDBContextFactory();
        public readonly IMigrationDbContext _context = design.CreateDbContext(null);

        private readonly IMapper _mapper;
        public PlantillaDAO(IMapper mapper)
        {
            _mapper = mapper;
        }


        public async Task<ActionResult<PlantillaDTO>> AgregarPlantillaDAO(Plantilla Plantilla)
        {
            try
            {
                _context.Plantillas.Add(Plantilla);
                await _context.DbContext.SaveChangesAsync();

                return _mapper.Map<PlantillaDTO>(Plantilla);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw ex.InnerException!;
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
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw ex.InnerException!;
            }
        }

        public async Task<ActionResult<Plantilla>> ObtenerPlantillaDAO(int id)
        {
            try
            {
                var Plantilla = await _context.Plantillas.FindAsync(id);
                if (Plantilla == null)
                {
                    return new NotFoundResult();
                }
                return Plantilla;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw ex.InnerException!;
            }
        }

        public async Task<ActionResult> ActualizarPlantillaDAO(Plantilla plantilla, int id)
        {
            try
            {
                var PlantillaOld = await ObtenerPlantillaDAO(id);
                if (PlantillaOld.Value == null)
                {
                    return new NotFoundResult();
                }



                await _context.DbContext.SaveChangesAsync();
                return new OkResult();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw ex.InnerException!;
            }
        }

        public async Task<ActionResult> EliminarPlantillaDAO(int id)
        {
            try
            {
                var existe = await ObtenerPlantillaDAO(id);
                if (existe.Value?.id == 0)
                {
                    return new NotFoundResult();
                }


                _context.Plantillas.Remove(existe.Value!);
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