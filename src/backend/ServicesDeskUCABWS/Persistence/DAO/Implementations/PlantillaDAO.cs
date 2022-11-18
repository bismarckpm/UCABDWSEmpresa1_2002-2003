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
            try
            {
                _context.Plantillas.Add(Plantilla);
                await _context.DbContext.SaveChangesAsync();

                return _mapper.Map<PlantillaDTO>(Plantilla);
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.InnerException!.Message);
                throw new Exception("Error al agregar la plantilla");
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
                Console.WriteLine(ex.InnerException!.Message);
                throw new Exception("Error al consultar las plantillas");
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
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.InnerException!.Message);
                throw new Exception("Error al obtener la plantilla");
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
                Console.WriteLine(ex.InnerException!.Message);
                throw new Exception("Error al actualizar la plantilla");
            }
        }

        public async Task<ActionResult> EliminarPlantillaDAO(int id)
        {
            try
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
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.InnerException!.Message);
                throw new Exception("Error al eliminar la plantilla");
            }
        }
    }
}