using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Exceptions;
using ServicesDeskUCABWS.Persistence.DAO.Implementations;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.Reponses;
using System.Net;
using static ServicesDeskUCABWS.Reponses.AplicationResponse;

namespace ServicesDeskUCABWS.Controllers
{

    [ApiController]
    [Route("/FlujoAprobacion/")]
    public class FlujoAprobacionController : Controller
    {
        public readonly IFlujoAprobacionDAO _flujoAprobacionDAO;
        public readonly IMapper _mapper;
        private readonly ILogger<FlujoAprobacionController> _log;

        public FlujoAprobacionController(IFlujoAprobacionDAO flujoAprobacionDAO, IMapper mapper, ILogger<FlujoAprobacionController> logger)
        {
            _flujoAprobacionDAO = flujoAprobacionDAO;
            _mapper = mapper;
            _log = logger;
        }

        /// <summary>
        /// Agregar un flujo de aprobacion
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AgregarFlujo/")]
        public async Task<ApplicationResponse<string>> AgregarFlujo([FromBody] FlujoAprobacionDTO dto)
        {
            var response = new ApplicationResponse<string>();
            try
            {
                response.Data =  _flujoAprobacionDAO.AgregarFlujoAprobacionDAO(dto);
                response.Message = "Flujo agregado con exito";
                response.StatusCode = HttpStatusCode.OK;
                _log.LogInformation("Flujo agregado con exito");

            }
            catch (FlujoAprobacionException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Exception = ex.innerException.ToString();
                _log.LogError("Error al agregar el flujo de aprobacion", ex);
            }
            return response;

        }

        /// <summary>
        /// Actualiza el estado del ticket en la tabla flujo de aprobaciones
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("ActualizarEstadoFlujo/")]
        public async Task<ApplicationResponse<string>> ActualizarEstadoFlujo([FromBody] FlujoAprobacionDTO dto)
        {
            var response = new ApplicationResponse<string>();
            try
            {
                response.Data = _flujoAprobacionDAO.ActualizarEstadoFlujoDAO(dto);
                response.Message = "Flujo actualizado con exito";
                response.StatusCode = HttpStatusCode.OK;
                _log.LogInformation("Flujo actualizado con exito");

            }
            catch (FlujoAprobacionException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Exception = ex.innerException.ToString();
                _log.LogError("Error al actualizar el estado en el flujo de aprobacion", ex);
            }
            return response;

        }


        /// <summary>
        /// Obtiene el id del estado del ticket dado el id del ticket
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("ObtenerEstadoTicketFlujo/{ticketId}")]
        public async Task<ApplicationResponse<FlujoAprobacionDTO>> ObtenerEstadoTicketFlujo(int ticketId)
        {
            var response = new ApplicationResponse<FlujoAprobacionDTO>();

            try
            {
                response.Data = _flujoAprobacionDAO.ObtenerEstadoTicketFlujoDAO(ticketId);
                response.Message = "Estados tickets obtenidos con exito";
                response.StatusCode = HttpStatusCode.OK;
                _log.LogInformation("Estados tickets obtenidos con exito");

            }
            catch (FlujoAprobacionException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Exception = ex.innerException.ToString();
                _log.LogError("Error al obtener el estado en el flujo de aprobacion", ex);
            }
            return response;
        }

    }
}
