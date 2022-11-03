using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Entity;
using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using AutoMapper;


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
        public async Task<ActionResult> Post([FromBody] PlantillaDTOCreate dto)
        {

            var Plantilla = _mapper.Map<Plantilla>(dto);
            var result = await _dao.AgregarPlantillaDAO(Plantilla);
            _log.LogInformation("Plantilla agregada con exito");
            return Ok(result);

        }

        [HttpGet]
        public async Task<ActionResult<List<PlantillaDTO>>> Get()
        {


            var result = await _dao.ObtenerPlantillasDAO();
            _log.LogInformation("Plantillas consultadas con exito");
            var plantillas = _mapper.Map<List<PlantillaDTO>>(result);
            return plantillas;


        }

        [HttpGet("{id:int}", Name = "obtenerPlantilla")]
        public async Task<ActionResult<PlantillaDTO>> Get(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("El id debe ser mayor a 0");
                }
                var result = await _dao.ObtenerPlantillaDAO(id);
                if (result.Value != null)
                {
                    _log.LogInformation("Plantilla consultada con exito");
                    return Ok(_mapper.Map<PlantillaDTO>(result.Value));
                }
                else
                {
                    return NotFound("No se encontro la Plantilla");
                }

            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                throw ex;
            }
        }


        [HttpPut("{id:int}")]

        public async Task<ActionResult> ActualizarPlantilla([FromBody] PlantillaDTOCreate dto, int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("El id debe ser mayor a 0");
                }
                var Plantilla = _mapper.Map<Plantilla>(dto);
                var result = await _dao.ActualizarPlantillaDAO(Plantilla, id);
                return Ok(result);

            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                throw ex;
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> EliminarPlantilla([Required] int id)
        {

            if (id <= 0)
            {
                return BadRequest("El id debe ser mayor a 0");
            }

            var result = await _dao.EliminarPlantillaDAO(id);
            return result;

        }

    }
}