using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Entity;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface
{
    public interface ICargoDAO
    {

        public Task<List<Cargo>> ConsultarCargoDAO();

        public Task<ActionResult<Cargo>> ObtenerCargoByIdDAO(int id);

        public Task<ActionResult<CargoDTO>> AgregarCargoDAO(Cargo cargo);

        public Task<ActionResult<Cargo>> ActualizarCargoDAO(Cargo cargo, int id);

        public Task<ActionResult> EliminarCargoDAO(int id);

        public Task<List<Usuario>> ObtenerUsuariosByCargoIdDAO(int id);
    }
}