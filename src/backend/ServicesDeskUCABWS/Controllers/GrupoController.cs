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
    [Tags("Grupo")]
    [Route("api/grupos")]
    public class GrupoController : Controller
    {
        private readonly IGrupoDAO _dao;
        private readonly ILogger<GrupoController> _log;
        private readonly IMapper _mapper;

        public GrupoController(ILogger<GrupoController> logger, IGrupoDAO dao, IMapper mapper)
        {
            _log = logger;
            _dao = dao;
            _mapper = mapper;
        }

        //CREAR GRUPO
        [HttpPost]
        public async Task<ApplicationResponse<GrupoDTO>> Post([FromBody] GrupoCreateDTO dto)
        {

            var response = new ApplicationResponse<GrupoDTO>();
            try
            {
                var grupo = _mapper.Map<Grupo>(dto);
                response.Data = await _dao.AgregarGrupoDAO(grupo);
                response.Message = "Grupo creado con exito";
                response.StatusCode = HttpStatusCode.OK;
                _log.LogInformation("Grupo creado con exito");

            }
            catch (GrupoException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Exception = ex.innerException.ToString();
                _log.LogError("Error al crear grupo", ex);

            }
            return response;
        }

        //CONSULTAR TODOS LOS GRUPOS
        [HttpGet]
        public async Task<ApplicationResponse<List<GrupoResponseDTO>>> Get()
        {

            var response = new ApplicationResponse<List<GrupoResponseDTO>>();
            try
            {
                response.Data = await _dao.ObtenerGruposDAO();
                response.Message = "Lista de Grupos obtenida con exito";
                response.StatusCode = HttpStatusCode.OK;
                _log.LogInformation("Lista de Grupos obtenida con exito");

            }
            catch (GrupoException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Exception = ex.innerException.ToString();
                _log.LogError("Error al obtener los grupos", ex);
            }
            return response;

        }

        //CONSULTAR UN GRUPO MEDIANTE SU ID
        [HttpGet("{id:int}")]
        public async Task<ApplicationResponse<GrupoResponseDTO>> Get([FromRoute][Required][Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0")] int id)
        {
            var response = new ApplicationResponse<GrupoResponseDTO>();
            try
            {
                response.Data = await _dao.ObtenerGrupoByIdDAO(id);
                response.Message = "Grupo obtenido con exito";
                response.StatusCode = HttpStatusCode.OK;
                _log.LogInformation("Grupo obtenido con exito");

            }
            catch (GrupoException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Exception = ex.innerException.ToString();
                _log.LogError("Error al obtener grupo", ex);
            }
            return response;

        }

        //ACTUALIZAR GRUPO
        [HttpPut("{id:int}")]
        public async Task<ApplicationResponse<GrupoDTO>> ActualizarGrupo(
                                        [FromBody] GrupoCreateDTO dto,
                                        [FromRoute][Required][Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0")] int id)
        {
            var response = new ApplicationResponse<GrupoDTO>();
            try
            {
                var grupo = _mapper.Map<Grupo>(dto);
                response.Data = await _dao.ActualizarGrupoDAO(grupo, id);
                response.Message = "Grupo actualizado con exito";
                response.StatusCode = HttpStatusCode.OK;
                _log.LogInformation("Grupo actualizado con exito");

            }
            catch (GrupoException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Exception = ex.innerException.ToString();
                _log.LogError("Error al actualizar grupo", ex);
            }
            return response;

        }

        //ELIMINAR GRUPO
        [HttpDelete("{id:int}")]
        public async Task<ApplicationResponse<ActionResult>> EliminarGrupo([FromRoute][Required][Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0")] int id)
        {
            var response = new ApplicationResponse<ActionResult>();
            try
            {
                response.Success = await _dao.EliminarGrupoDAO(id);
                if (!response.Success)
                {
                    response.Message = "No se pudo eliminar el grupo";
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Data = NotFound("No se encontro la etiqueta");
                    _log.LogInformation("No se pudo eliminar el grupo");
                    return response;
                }
                response.Message = "Grupo eliminado con exito";
                response.StatusCode = HttpStatusCode.OK;
                response.Data = Ok("Grupo eliminado con exito");
                _log.LogInformation("Grupo eliminado con exito");

            }
            catch (GrupoException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Exception = ex.innerException.ToString();
                _log.LogError("Error al eliminar grupo", ex);
            }
            return response;

        }



    }


}
