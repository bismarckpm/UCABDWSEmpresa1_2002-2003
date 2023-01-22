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
                // response.Data = await _flujoAprobacionDAO.AgregarFlujoDAO(dto);
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


        
    }
}
