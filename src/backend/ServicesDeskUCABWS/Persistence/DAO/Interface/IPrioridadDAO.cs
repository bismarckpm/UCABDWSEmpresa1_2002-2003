using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface
{
    public interface IPrioridadDAO
    {
        public PrioridadDTO AgregarPrioridadDAO(Prioridad p);

        public List<PrioridadDTO> ConsultarTodosPrioridadesDAO();

        public PrioridadDTO ActualizarPrioridadDAO(Prioridad p);

        public PrioridadDTO EliminarPrioridadDAO(int id);

        public PrioridadDTO ConsultaPrioridadDAO(int id);
    }
}