using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Database;
using ServicesDeskUCABWS.Exceptions;
using ServicesDeskUCABWS.Persistence.Entity;
using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using static ServicesDeskUCABWS.Reponses.AplicationResponse;
using System.Net;

namespace ServicesDeskUCABWS.Controllers
{
    [ApiController]
    [Tags("Estado")]
    [Route("api/estados")]
    public class EstadoController : Controller
    {
        //DECLARACION DE VARIABLES
        private readonly IEstadoDAO _dao_Estado;
        private readonly ILogger<EstadoController> _log;
        private readonly IMapper _mapper;

        //CONSTRUCTOR
        public EstadoController(ILogger<EstadoController> logger, IEstadoDAO dao_Estado, IMapper mapper)
        {
            _log = logger;
            _dao_Estado = dao_Estado;
            _mapper = mapper;
        }

        //ENDPOINT PARA CONSULTAR LOS ESTADOS
        [HttpGet]
        public async Task<ApplicationResponse<List<EstadoResponseDTO>>> Get()
        {

            var response = new ApplicationResponse<List<EstadoResponseDTO>>();
            try
            {
                response.Data = await _dao_Estado.GetEstadosDAO();
                response.Message = "Estados obtenidos con exito";
                response.StatusCode = HttpStatusCode.OK;
                _log.LogInformation("Estados obtenidos con exito");

            }
            catch (EstadoException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Exception = ex.innerException.ToString();
                _log.LogError("Error al obtener estados", ex);
            }
            return response;

        }

        //ENDPOINT PARA CONSULTAR LOS ESTADOS POR ID
        [HttpGet("{id:int}")]
        public async Task<ApplicationResponse<EstadoResponseDTO>> Get([FromRoute][Required][Range(1, int.MaxValue, ErrorMessage = "El id de la etiqueta debe ser mayor a 0")] int id)
        {
            var response = new ApplicationResponse<EstadoResponseDTO>();
            try
            {
                response.Data = await _dao_Estado.GetEstadoDAO(id);
                response.Message = "Estado obtenido con exito";
                response.StatusCode = HttpStatusCode.OK;
                _log.LogInformation("Estado obtenido con exito");

            }
            catch (EstadoException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Exception = ex.innerException.ToString();
                _log.LogError("Error al obtener estado", ex);
            }
            return response;

        }

        //ENDPOINT PARA ACTUALIZAR LOS ESTADOS POR ID
        [HttpPut("{id:int}")]
        public async Task<ApplicationResponse<EstadoDTO>> Put(
                                        [FromBody] EstadoCreateDTO dto,
                                        [FromRoute][Required][Range(1, int.MaxValue, ErrorMessage = "El id de la etiqueta debe ser mayor a 0")] int id)
        {
            var response = new ApplicationResponse<EstadoDTO>();
            try
            {
                var estado = _mapper.Map<Estado>(dto);
                response.Data = await _dao_Estado.ActualizarEstadoDAO(estado, id);
                response.Message = "Estado actualizado con exito";
                response.StatusCode = HttpStatusCode.OK;
                _log.LogInformation("Estado actualizado con exito");

            }
            catch (EstadoException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Exception = ex.innerException.ToString();
                _log.LogError("Error al actualizar estado", ex);
            }
            return response;

        }

        //ENDPOINT PARA ELIMINAR LOS ESTADOS POR ID
        [HttpDelete("{id:int}")]
        public async Task<ApplicationResponse<ActionResult>> Delete([FromRoute][Required][Range(1, int.MaxValue, ErrorMessage = "El id de la etiqueta debe ser mayor a 0")] int id)
        {
            var response = new ApplicationResponse<ActionResult>();
            try
            {
                response.Success = await _dao_Estado.EliminarEstadoDAO(id);
                if (!response.Success)
                {
                    response.Message = "No se pudo eliminar el estado";
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Data = NotFound("No se encontro la etiqueta");
                    _log.LogInformation("No se pudo eliminar el estado");
                    return response;
                }
                response.Message = "Estado eliminado con exito";
                response.StatusCode = HttpStatusCode.OK;
                response.Data = Ok("Estado eliminado con exito");
                _log.LogInformation("Estado eliminado con exito");

            }
            catch (EstadoException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Exception = ex.innerException.ToString();
                _log.LogError("Error al eliminar estado", ex);
            }
            return response;

        }

        //ENDPOINT PARA CREAR LOS ESTADOS
        [HttpPost]
        public async Task<ApplicationResponse<EstadoDTO>> Post([FromBody] EstadoCreateDTO dto)
        {

            var response = new ApplicationResponse<EstadoDTO>();
            try
            {
                var estado = _mapper.Map<Estado>(dto);
                response.Data = await _dao_Estado.AgregarEstadoDAO(estado);
                response.Message = "Estado creado con exito";
                response.StatusCode = HttpStatusCode.OK;
                _log.LogInformation("Estado creado con exito");

            }
            catch (EstadoException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Exception = ex.innerException.ToString();
                _log.LogError("Error al crear estado", ex);

            }
            return response;
        }


    }


}