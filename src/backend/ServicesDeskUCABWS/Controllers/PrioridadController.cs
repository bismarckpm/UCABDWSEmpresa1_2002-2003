using ServicesDeskUCABWS.BussinessLogic.Mapper;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using System.ComponentModel.DataAnnotations;
using ServicesDeskUCABWS.Exceptions;
using static ServicesDeskUCABWS.Reponses.AplicationResponse;
using System.Net;

namespace ServicesDeskUCABWS.Controllers
{
    [ApiController]
    [Route("Prioridad")]
    public class PrioridadController : Controller
    {
        //DECLARACION DE VARIABLES
        private readonly IPrioridadDAO _dao;
        private readonly ILogger<PrioridadController> _log;

        //CONSTANTE DE MENSAJE SOLITUD EXITOSA
        static string MSG_SOL_EXITOSA = "Solicitud exitosa";

        //CONSTRUCTOR
        public PrioridadController(ILogger<PrioridadController> log, IPrioridadDAO dao)
        {
            this._log = log;
            this._dao = dao;
        }

        //ENDPOINT PARA CREAR LA PRIORIDAD
        [HttpPost]
        [Route("CreatePrioridad/")]
        public ApplicationResponse<PrioridadDTO> CreatePrioridad([FromBody] PrioridadDTO dto)
        {
            var response = new ApplicationResponse<PrioridadDTO>();
            try
            {
                response.Data = _dao.AgregarPrioridadDAO(PrioridadMapper.DtoToEntity(dto));
                response.Message = MSG_SOL_EXITOSA;
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                throw new PlantillaException("Error al crear prioridad", ex, _log);
            }
            return response;
        }

        //ENDPOINT PARA CONSULTAR TODAS LAS PRIORIDADES
        [HttpGet]
        [Route("ConsultaPrioridades/")]
        public ApplicationResponse<List<PrioridadDTO>> ConsultaPrioridades()
        {
            var response = new ApplicationResponse<List<PrioridadDTO>>();
            try
            {
                response.Data = _dao.ConsultarTodosPrioridadesDAO();
                if (!response.Data.Any())
                {
                    response.Message = "No existen prioridades en el sistema";
                }
                else
                {
                    response.Message = MSG_SOL_EXITOSA;
                }
                response.StatusCode = HttpStatusCode.OK;
            }
            catch(Exception ex) { 
                response.Success = false;
                response.Message = ex.Message;
                throw new PlantillaException("Error al consultar prioridades", ex, _log);
            }
            return response;
        }

        //ENDPOINT PARA CONSULTAR UNA PRIORIDAD DADO EL ID
        [HttpGet]
        [Route("ConsultaPrioridad/{id}")]
        public ApplicationResponse<PrioridadDTO> ConsultaPrioridad([Required][FromRoute] int id)
        {
            var response = new ApplicationResponse<PrioridadDTO>();
            try
            {
                response.Data = _dao.ConsultaPrioridadDAO(id);
                response.Message = MSG_SOL_EXITOSA;
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                throw new PlantillaException("Error al consultar la prioridad de id: "+id, ex, _log);
            }
            return response;
        }

        //ENDPOINT PARA ACTUALIZAR LA PRIORIDAD
        [HttpPut]
        [Route("Actualizar/")]
        public ApplicationResponse<PrioridadDTO> ActualizarPrioridad([Required][FromBody] PrioridadDTO dto)
        {
            var response = new ApplicationResponse<PrioridadDTO>();
            try
            {
                response.Data = _dao.ActualizarPrioridadDAO(PrioridadMapper.DtoToEntity(dto));
                response.Message = MSG_SOL_EXITOSA;
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                throw new PlantillaException("Error al actualizar la prioridad de id: " + dto.Id, ex, _log);
            }
            return response;
        }

        //ENDPOINT PARA ELIMINAR LA PRIORIDAD DADO EL ID
        [HttpDelete]
        [Route("Eliminar/{id}")]
        public ApplicationResponse<PrioridadDTO> EliminarPrioridad([Required][FromRoute] int id)
        {
            var response = new ApplicationResponse<PrioridadDTO>();
            try
            {
                response.Data = _dao.EliminarPrioridadDAO(id);
                response.Message = MSG_SOL_EXITOSA;
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                throw new PlantillaException("Error al eliminar la prioridad de id: " + id, ex, _log);
            }
            return response;
        }
    }

}