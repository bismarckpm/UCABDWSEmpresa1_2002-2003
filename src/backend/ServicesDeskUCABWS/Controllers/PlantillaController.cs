using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Entity;
using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using static ServicesDeskUCABWS.Reponses.AplicationResponse;
using System.Net;
using ServicesDeskUCABWS.Exceptions;

namespace ServicesDeskUCABWS.Controllers
{
    [ApiController]
    [Route("api/plantillas")]
    public class PlantillaController : Controller
    {
        private readonly IPlantillaDAO _dao;
        private readonly ILogger<PlantillaController> _log;

        private readonly IMapper _mapper;


        public PlantillaController(ILogger<PlantillaController> logger, IPlantillaDAO dao, IMapper mapper)
        {
            _log = logger;
            _dao = dao;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ApplicationResponse<PlantillaDTO>> Post([FromBody] PlantillaDTOCreate dto)
        {
            var response = new ApplicationResponse<PlantillaDTO>();
            try
            {
                var plantilla = _mapper.Map<Plantilla>(dto);
                response.Data = await _dao.AgregarPlantillaDAO(plantilla);
                response.Message = "Plantilla agregada con exito";
                response.StatusCode = HttpStatusCode.OK;
                _log.LogInformation("Plantilla agregada con exito");
            }
            catch (PlantillaException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Exception = ex.innerException.ToString();
                _log.LogError("Error al agregar Plantilla", ex);
            }
            return response;

        }

        [HttpGet]
        public async Task<ApplicationResponse<List<PlantillaDTO>>> Get()
        {

            var response = new ApplicationResponse<List<PlantillaDTO>>();
            try
            {
                response.Data = await _dao.ObtenerPlantillasDAO();
                response.Message = "Plantillas consultadas con exito";
                response.StatusCode = HttpStatusCode.OK;
                _log.LogInformation("Plantillas consultadas con exito");
            }
            catch (PlantillaException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Exception = ex.innerException.ToString();
                _log.LogError("Error al consultar Plantillas", ex);
            }
            return response;

        }

        [HttpGet("{id:int}", Name = "obtenerPlantilla")]
        public async Task<ApplicationResponse<PlantillaDTO>> Get([FromRoute][Required][Range(1, int.MaxValue, ErrorMessage = "El id de la etiqueta debe ser mayor a 0")] int id)
        {
            var response = new ApplicationResponse<PlantillaDTO>();
            try
            {
                response.Data = await _dao.ObtenerPlantillaDAO(id);
                response.Message = "Plantilla consultada con exito";
                response.StatusCode = HttpStatusCode.OK;
                _log.LogInformation("Plantilla consultada con exito");
            }
            catch (PlantillaException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Exception = ex.innerException.ToString();
                _log.LogError("Error al consultar Plantilla", ex);
            }
            return response;
        }



        [HttpPut("{id:int}")]

        public async Task<ApplicationResponse<PlantillaDTO>> ActualizarPlantilla(
                                        [FromBody] PlantillaDTOCreate dto,
                                        [FromRoute][Required][Range(1, int.MaxValue, ErrorMessage = "El id de la etiqueta debe ser mayor a 0")] int id)
        {
            var response = new ApplicationResponse<PlantillaDTO>();
            try
            {
                var plantilla = _mapper.Map<Plantilla>(dto);
                response.Data = await _dao.ActualizarPlantillaDAO(plantilla, id);
                response.Message = "Plantilla actualizada con exito";
                response.StatusCode = HttpStatusCode.OK;
                _log.LogInformation("Plantilla actualizada con exito");
            }
            catch (PlantillaException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Exception = ex.innerException.ToString();
                _log.LogError("Error al actualizar Plantilla", ex);
            }
            return response;
        }

        [HttpDelete("{id:int}")]
        public async Task<ApplicationResponse<ActionResult>> EliminarPlantilla([FromRoute][Required][Range(1, int.MaxValue, ErrorMessage = "El id de la etiqueta debe ser mayor a 0")] int id)
        {
            var response = new ApplicationResponse<ActionResult>();
            try
            {
                response.Success = await _dao.EliminarPlantillaDAO(id);
                if (!response.Success)
                {
                    response.Message = "La plantilla con el id " + id + " no existe";
                    response.StatusCode = HttpStatusCode.NotFound;
                    _log.LogInformation("No se pudo eliminar la plantilla");
                    response.Data = NotFound();
                    return response;
                }
                response.Message = "Plantilla eliminada con exito";
                response.StatusCode = HttpStatusCode.OK;
                _log.LogInformation("Plantilla eliminada con exito");
                response.Data = Ok();
            }
            catch (PlantillaException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Exception = ex.innerException.ToString();
                _log.LogError("Error al eliminar Plantilla", ex);
            }
            return response;
        }

    }
}