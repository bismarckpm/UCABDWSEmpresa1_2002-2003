using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Entity;
using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCABWS.Controllers
{
    [Route("/Grupo")]
    [ApiController]
    public class GrupoController : ControllerBase
    {
        private readonly IGrupoDAO _GrupoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GrupoController> _log;


        public GrupoController(IGrupoDAO grupoRepository, IMapper mapper, ILogger<GrupoController> logger)
        {
            _GrupoRepository = grupoRepository;
            _mapper = mapper;
            _log = logger;
        }


        //Agregar Grupo
        [HttpPost]
        [Route("CreateGrupo/")]
        public async Task<ActionResult> Post([FromBody] GrupoDTO dto)
        {
            try
            {
                var grupo = _mapper.Map<Grupo>(dto);
                var result = await _GrupoRepository.AgregarGrupoDAO(grupo);
                _log.LogInformation("Grupo agregado con exito");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                throw ex.InnerException!;
            }
        }

 
        //Consultar todos los Grupos
        [HttpGet]
        [Route("ConsultaGrupo/")]
        public async Task<ActionResult<List<GrupoDTO>>> Get()
        {
            try
            {
                var result = await _GrupoRepository.ConsultarGrupoDAO();
                _log.LogInformation("Grupos consultados con exito");
                return Ok(_mapper.Map<List<GrupoDTO>>(result));
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                throw ex.InnerException!;
            }
        }


        //Consultar Grupo por Id
        [HttpGet("{id:int}", Name = "obtenerGrupo")]
        public async Task<ActionResult<GrupoDTO>> Get(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("El id debe ser mayor a 0");
                }
                var result = await _GrupoRepository.ConsultarGrupoByIdDAO(id);
                if (result.Value!.id == id)
                {
                    _log.LogInformation("Grupo consultado con exito");
                    return Ok(_mapper.Map<GrupoDTO>(result.Value));
                }
                else
                {
                    return NotFound("No se encontro el Grupo");
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                throw ex.InnerException!;
            }
        }


        //Actualizar Grupo
        [HttpPut]
        [Route("ActualizarGrupo/")]
        public async Task<ActionResult> ActualizarGrupo([FromBody] GrupoDTO dto, int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("El id debe ser mayor a 0");
                }
                var grupo = _mapper.Map<Grupo>(dto);
                var result = await _GrupoRepository.ActualizarGrupoDAO(grupo, id);
                if (result.Value!.id == id)
                {
                    _log.LogInformation("Grupo actualizado con exito");
                    return Ok(result.Value);
                }
                else
                {
                    return NotFound("No se encontro el Grupo");
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                throw ex.InnerException!;
            }

        }

        //Eliminar Grupo
        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<ActionResult> EliminarGrupo([Required] int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("El id debe ser mayor a 0");
                }

                var result = await _GrupoRepository.EliminarGrupoDAO(id);
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