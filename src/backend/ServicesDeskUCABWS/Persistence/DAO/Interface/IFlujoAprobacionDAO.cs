using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Entity;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface
{
    public interface IFlujoAprobacionDAO
    {
        public string AgregarFlujoAprobacionDAO(FlujoAprobacionDTO flujoAprobacion);
        public string ActualizarEstadoFlujoDAO(FlujoAprobacionDTO flujoAprobacion);
        public FlujoAprobacionDTO ObtenerEstadoTicketFlujoDAO(int ticketId); 

    }
}
