using ServicesDeskUCABWS.BussinessLogic.Mapper;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Database;
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
    [Route("api/etiquetas")]
    public class EtiquetaController : Controller
    {
        private readonly IEtiquetaDAO _dao;
        private readonly ILogger<EtiquetaController> _log;

        private readonly IMapper _mapper;



        public EtiquetaController(ILogger<EtiquetaController> logger, IEtiquetaDAO dao, IMapper mapper)
        {
            _log = logger;
            _dao = dao;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ApplicationResponse<EtiquetaDTO>> Post([FromBody] EtiquetaDTOCreate dto)
        {
            var response = new ApplicationResponse<EtiquetaDTO>();
            try
            {
                response.Data = await _dao.AgregarEtiquetaDAO(_mapper.Map<Etiqueta>(dto));
                response.Message = "Etiqueta agregada con exito";
                response.StatusCode = HttpStatusCode.OK;
                _log.LogInformation("Etiqueta agregada con exito");

            }
            catch (EtiquetaException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Exception = ex.innerException.ToString();
                _log.LogError("Error al agregar etiqueta", ex);
            }
            return response;

        }

        [HttpGet]
        public async Task<ApplicationResponse<List<EtiquetaDTO>>> Get()
        {
            var response = new ApplicationResponse<List<EtiquetaDTO>>();
            try
            {

                response.Data = _mapper.Map<List<EtiquetaDTO>>(await _dao.ConsultarEtiquetasDAO());
                response.Message = "Etiquetas consultadas con exito";
                response.StatusCode = HttpStatusCode.OK;
                _log.LogInformation("Etiquetas consultadas con exito");
            }
            catch (EtiquetaException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Exception = ex.innerException.ToString();
                _log.LogError("Error al consultar etiquetas", ex);
            }
            return response;


        }

        [HttpGet("{id:int}", Name = "obtenerEtiqueta")]
        public async Task<ApplicationResponse<EtiquetaDTO>> Get([Required][Range(1, int.MaxValue, ErrorMessage = "El id de la etiqueta debe ser mayor a 0")] int id)
        {

            var response = new ApplicationResponse<EtiquetaDTO>();

            try
            {
                response.Data = _mapper.Map<EtiquetaDTO>(await _dao.ObtenerEtiquetaDAO(id));
                if (response.Data.id == 0)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Etiqueta no encontrada";
                    response.Success = false;
                    return response;
                }
                response.Message = "Etiqueta consultada con exito";
                response.StatusCode = HttpStatusCode.OK;
                _log.LogInformation("Etiqueta consultada con exito");

            }
            catch (EtiquetaException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Exception = ex.innerException.ToString();
                _log.LogError("Error al consultar etiqueta", ex);
            }
            return response;
        }


        [HttpPut("{id:int}")]

        public async Task<ApplicationResponse<EtiquetaDTO>> ActualizarEtiqueta(
                                                            [FromBody] EtiquetaDTOCreate dto,
                                                            [Required][Range(1, int.MaxValue, ErrorMessage = "El id de la etiqueta debe ser mayor a 0")] int id)
        {
            var response = new ApplicationResponse<EtiquetaDTO>();

            try
            {
                response.Data = _mapper.Map<EtiquetaDTO>(await _dao.ActualizarEtiquetaDAO(_mapper.Map<Etiqueta>(dto), id));
                if (response.Data.id == 0)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "No se encontro la etiqueta";
                    response.Success = false;
                    return response;
                }
                response.Message = "Etiqueta actualizada con exito";
                response.StatusCode = HttpStatusCode.OK;
                _log.LogInformation("Etiqueta actualizada con exito");
            }
            catch (EtiquetaException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Exception = ex.innerException.ToString();
                _log.LogError("Error al actualizar etiqueta", ex);
            }
            return response;

        }

        [HttpDelete("{id:int}")]
        public async Task<ApplicationResponse<ActionResult>> EliminarEtiqueta([Required][Range(1, int.MaxValue, ErrorMessage = "El id de la etiqueta debe ser mayor a 0")] int id)
        {
            var response = new ApplicationResponse<ActionResult>();

            try
            {
                response.Success = await _dao.EliminarEtiquetaDAO(id);
                if (response.Success == false)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "No se encontro la etiqueta";
                    response.Data = NotFound("No se encontro la etiqueta");
                }
                else
                {
                    response.Data = Ok("Etiqueta eliminada con exito");
                    response.Message = "Etiqueta eliminada con exito";
                    response.StatusCode = HttpStatusCode.OK;
                    _log.LogInformation("Etiqueta eliminada con exito");
                }

            }
            catch (EtiquetaException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Exception = ex.innerException.ToString();
                _log.LogError("Error al eliminar etiqueta", ex);

            }
            return response;

        }

    }
}