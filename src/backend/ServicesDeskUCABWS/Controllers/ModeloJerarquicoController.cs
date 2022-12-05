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
    [Route("api/ModeloAprobacion")]
    public class ModeloJerarquicoController : Controller
    {
        private readonly IModeloJerarquicoDAO _dao;
        private readonly ILogger<ModeloJerarquicoController> _log;

        private readonly IMapper _mapper;


        public ModeloJerarquicoController(ILogger<ModeloJerarquicoController> logger, IModeloJerarquicoDAO dao, IMapper mapper)
        {
            _log = logger;
            _dao = dao;
            _mapper = mapper;
        }

        // [HttpPost("Jerarquico")]

        // public async Task<ActionResult> Post([FromBody] ModeloJerarquicoCreateDTO dto)
        // {

        //     var ModeloJerarquico = _mapper.Map<ModeloJerarquico>(dto);
        //     var result = await _dao.AgregarModeloJerarquicoDAO(ModeloJerarquico);
        //     _log.LogInformation("ModeloJerarquico agregada con exito");
        //     return Ok(result);
        // }

        // [HttpGet]
        // public async Task<ActionResult<List<ModeloJerarquicoDTO>>> Get()
        // {
        //   var result = await _dao.ConsultarModeloJerarquicosDAO();
        //   if (result == null)
        //   {
        //     return BadRequest("No se encontraron los modelos paralelos");
        //   }
        //     _log.LogInformation("ModeloJerarquicos consultadas con exito");
        //     return Ok(_mapper.Map<List<ModeloJerarquicoDTO>>(result));
        // }        

        // [HttpGet("Jerarquico/{id:int}", Name = "obtenerModeloJerarquico")]
        // public async Task<ActionResult<ModeloJerarquicoDTO>> Get(int id)
        // {

        //     if (id <= 0)
        //     {
        //         return BadRequest("El id debe ser mayor a 0");
        //     }
        //     var result = await _dao.ObtenerModeloJerarquicoDAO(id);
        //     if (result.Value?.Id == id)
        //     {
        //         return Ok(_mapper.Map<ModeloJerarquicoDTO>(result.Value));  
        //     }
        //     return NotFound("No se encontro el ModeloJerarquico");
        // }


        //  [HttpPut("{id:int}")]
        //  public async Task<ActionResult> ActualizarModeloJerarquico([FromBody] ModeloJerarquicoCreateDTO dto, int id)
        //  {
        //      if (id <= 0)
        //      {
        //          return BadRequest("El id debe ser mayor a 0");
        //      }
        //      var ModeloJerarquico = _mapper.Map<ModeloJerarquicoCreateDTO>(dto);
        //      var result = await _dao.ActualizarModeloJerarquicoDAO(ModeloJerarquico, id);
        //      if (result.Value!.Id == id)
        //      {
        //          _log.LogInformation("ModeloJerarquico actualizada con exito");
        //          return result.Result;
        //      }
        //      else
        //      {
        //          return NotFound("No se encontro la ModeloJerarquico");
        //      }
             
        //  }

        // [HttpDelete("Jerarquico/{id:int}")]
        // public async Task<ActionResult> EliminarModeloJerarquico([Required] int id)
        // {

        //     if (id <= 0)
        //     {
        //         return BadRequest("El id debe ser mayor a 0");
        //     }
        //     var result = await _dao.EliminarModeloJerarquicoDAO(id);
        //     return result;
        // }
    }
}