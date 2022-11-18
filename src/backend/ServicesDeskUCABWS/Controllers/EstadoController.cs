using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Database;
using ServicesDeskUCABWS.Persistence.Entity;
using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;


namespace ServicesDeskUCABWS.Controllers
{
    [ApiController]
    [Tags("Estado")]
    [Route("api/estados")]
    public class EstadoController : Controller
    {
        private readonly IEstadoDAO _dao_Estado;
        private readonly ILogger<EstadoEtiquetaController> _log;
        private readonly IMapper _mapper;

        public EstadoController(ILogger<EstadoEtiquetaController> logger, IEstadoDAO dao_Estado, IMapper mapper)
        {
            _log = logger;
            _dao_Estado = dao_Estado;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<List<EstadoDTO>>> Get()
        {

            var result = await _dao_Estado.GetEstadosDAO();
            _log.LogInformation("Estados obtenidos exitosamente");
            return Ok(result);


        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<EstadoDTO>> Get([FromRoute][Required] int id)
        {
            try
            {
                var result = await _dao_Estado.GetEstadoDAO(id);
                _log.LogInformation("Estado obtenido exitosamente");
                return Ok(result);
            }
            catch (Exception e)
            {
                _log.LogError(e.Message);
                throw e;
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put([FromBody] EstadoEtiquetaDTO dto, [FromRoute][Required] int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("El id del estado debe ser mayor a 0");
                }

                var estado = _mapper.Map<Estado>(dto);

                return await _dao_Estado.ActualizarEstadoDAO(estado, id);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                throw ex;
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute][Required] int id)
        {

            if (id <= 0)
            {
                return BadRequest("El id del estado debe ser mayor a 0");
            }

            return await _dao_Estado.EliminarEstadoDAO(id);

        }


    }


}