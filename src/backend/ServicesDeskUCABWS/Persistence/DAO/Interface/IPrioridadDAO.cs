using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface
{
    public interface IPrioridadDAO
    {
        PrioridadDTO AgregarPrioridadDAO(Prioridad p);

        List<PrioridadDTO> ConsultarTodosPrioridadesDAO();

        PrioridadDTO ActualizarPrioridadDAO(Prioridad p);

        PrioridadDTO EliminarPrioridadDAO(int id);

        PrioridadDTO ConsultaPrioridadDAO(int id);
    }
}