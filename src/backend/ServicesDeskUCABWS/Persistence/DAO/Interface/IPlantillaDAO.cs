using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface
{
    public interface IPlantillaDAO
    {
        public Task<List<PlantillaDTO>> ObtenerPlantillasDAO();

        public Task<PlantillaDTO> ObtenerPlantillaDAO(int id);

        public Task<PlantillaDTO> AgregarPlantillaDAO(Plantilla plantilla);

        public Task<PlantillaDTO> ActualizarPlantillaDAO(Plantilla plantilla, int id);

        public Task<Boolean> EliminarPlantillaDAO(int id);
    }
}