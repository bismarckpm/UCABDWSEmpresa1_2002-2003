using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface
{
    public interface INotificacionDAO
    {
        NotificacionDTO AgregarNotificacionDAO(Notification ntfy);

        List<NotificacionDTO> ConsultarTodaNotificacionDAO();

        NotificacionDTO ActualizarNotificacionDAO(Notification ntf);
    }
}