using ServicesDeskUCABWS.BussinessLogic.Mapper;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Database;
using ServicesDeskUCABWS.Persistence.Entity;
using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using AutoMapper;


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
        public async Task<ActionResult> Post([FromBody] EtiquetaDTO dto)
        {
            try
            {
                var etiqueta = _mapper.Map<Etiqueta>(dto);
                var result = await _dao.AgregarEtiquetaDAO(etiqueta);
                _log.LogInformation("Etiqueta agregada con exito");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                throw ex.InnerException!;
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Etiqueta>>> Get()
        {
            try
            {
                var result = await _dao.ConsultarEtiquetasDAO();
                _log.LogInformation("Etiquetas consultadas con exito");
                return result;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                throw ex.InnerException!;
            }
        }

        [HttpGet("{id:int}", Name = "obtenerEtiqueta")]
        public async Task<ActionResult<Etiqueta>> Get(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("El id debe ser mayor a 0");
                }
                var result = await _dao.ObtenerEtiquetaDAO(id);
                if (result.Value!.id == id)
                {
                    _log.LogInformation("Etiqueta consultada con exito");
                    return Ok(result);
                }
                else
                {
                    return NotFound("No se encontro la etiqueta");
                }

            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                throw ex.InnerException!;
            }
        }


        [HttpPut("{id:int}")]

        public async Task<ActionResult> ActualizarEtiqueta([FromBody] EtiquetaDTO dto, int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("El id debe ser mayor a 0");
                }
                var etiqueta = _mapper.Map<Etiqueta>(dto);
                var result = await _dao.ActualizarEtiquetaDAO(etiqueta, id);
                if (result.Value!.id == id)
                {
                    _log.LogInformation("Etiqueta actualizada con exito");
                    return Ok(result);
                }
                else
                {
                    return NotFound("No se encontro la etiqueta");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw ex.InnerException!;
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> EliminarEtiqueta([Required] int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("El id debe ser mayor a 0");
                }

                var result = await _dao.EliminarEtiquetaDAO(id);
                return result;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw ex.InnerException!;
            }

        }

    }
}