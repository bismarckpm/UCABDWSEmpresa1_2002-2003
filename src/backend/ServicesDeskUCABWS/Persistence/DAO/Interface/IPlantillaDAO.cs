using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface
{
    public interface IPlantillaDAO
    {
        public Task<List<Plantilla>> ObtenerPlantillasDAO();

        public Task<ActionResult<Plantilla>> ObtenerPlantillaDAO(int id);

        public Task<ActionResult<PlantillaDTO>> AgregarPlantillaDAO(Plantilla plantilla);

        public Task<ActionResult> ActualizarPlantillaDAO(Plantilla plantilla, int id);

        public Task<ActionResult> EliminarPlantillaDAO(int id);
    }
}