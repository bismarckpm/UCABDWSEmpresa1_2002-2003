using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Entity;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface
{
    public interface IFlujoAprobacionDAO
    {
        public string AgregarFlujoAprobacionDAO(FlujoAprobacionDTO flujoAprobacion);
    }
}
