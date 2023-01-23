using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Entity;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface
{
    public interface IFlujoAprobacionDAO
    {
        public Task<FlujoAprobacionDTO> AgregarFlujoDAO(FlujoAprobacionDTO flujoAprobacion);
        public Task<FlujoAprobacionDTO> AgregarFlujoAprobacionDAO(FlujoAprobacionDTO flujoAprobacion);
        public Task<FlujoAprobacionDTO> ActualizarEstadoTicketFlujoDAO(FlujoAprobacionDTO flujoAprobacion);
    }
}
