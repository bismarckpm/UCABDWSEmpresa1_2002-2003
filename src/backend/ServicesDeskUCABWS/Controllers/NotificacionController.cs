using ServicesDeskUCABWS.BussinessLogic.Mapper;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Entity;
using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCABWS.Controllers
{
    [ApiController]
    [Route("Notificacion")]
    public class NotificacionController : Controller
    {
        private readonly INotificacionDAO _dao;

        private readonly ILogger<NotificacionController> _logger;

        public NotificacionController(INotificacionDAO dao, ILogger<NotificacionController> logger)
        {
            _dao = dao;
            _logger = logger;
        }

        [HttpPost]
        [Route("Create/")]
        public NotificacionDTO CreateNotification(NotificacionDTO notify)
        {
            try
            {
                NotificacionMapper mapper = new NotificacionMapper();
                var data = _dao.AgregarNotificacionDAO(mapper.DtoToEntity(notify));
                return data;

            }catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw ex.InnerException!;
            }
        }

        [HttpGet]
        [Route("Consulta/")]
        public ICollection<NotificacionDTO> GetNotification()
        {
            try
            {
                return _dao.ConsultarTodaNotificacionDAO();

            }catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw ex.InnerException!;
            }
        }

        [HttpPut]
        [Route("Actualizar/")]
        public NotificacionDTO UpdateNotification([Required][FromBody] NotificacionDTO dTO)
        {
            try
            {
                NotificacionMapper mapper = new NotificacionMapper();
                var conversion = mapper.DtoToEntity(dTO);
                return _dao.ActualizarNotificacionDAO(conversion);
            }catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw ex.InnerException!;
            }
        }
    }
}