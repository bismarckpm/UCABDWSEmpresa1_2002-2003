using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
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
        public PlantillaDAO(IMapper mapper, IMigrationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }


        public async Task<ActionResult<PlantillaDTO>> AgregarPlantillaDAO(Plantilla Plantilla)
        {

            _context.Plantillas.Add(Plantilla);
            await _context.DbContext.SaveChangesAsync();

            return _mapper.Map<PlantillaDTO>(Plantilla);


        }

        public Task<List<Plantilla>> ObtenerPlantillasDAO()
        {

            return _context.Plantillas.ToListAsync();

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
            catch (DbUpdateException ex)
            {
                throw ex.InnerException!;
            }
        }

        public async Task<ActionResult> ActualizarPlantillaDAO(Plantilla plantilla, int id)
        {
            try
            {
                var plantillaOld = await ObtenerPlantillaDAO(id);

                if (plantillaOld == null)
                {
                    return new NotFoundResult();
                }

                plantillaOld.Value!.titulo = plantilla.titulo;
                plantillaOld.Value!.cuerpo = plantilla.cuerpo;
                plantillaOld.Value!.tipo = plantilla.tipo;

                await _context.DbContext.SaveChangesAsync();
                return new OkResult();

            }
            catch (DbUpdateException ex)
            {
                throw ex.InnerException!;
            }
        }

        public async Task<ActionResult> EliminarPlantillaDAO(int id)
        {

            var existe = await ObtenerPlantillaDAO(id);
            if (existe == null)
            {
                return new NotFoundResult();
            }

            _context.Plantillas.Remove(existe.Value!);
            await _context.DbContext.SaveChangesAsync();

            return new OkResult();

        }
    }
}