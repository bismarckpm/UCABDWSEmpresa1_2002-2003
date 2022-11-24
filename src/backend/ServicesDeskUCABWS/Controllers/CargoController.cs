
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Entity;
using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCABWS.Controllers
{
    [Route("/Cargo")]
    [ApiController]
    public class CargoController : ControllerBase
    {
        private readonly ICargoDAO _CargoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CargoController> _log;


        public CargoController(ICargoDAO cargoRepository, IMapper mapper, ILogger<CargoController> logger)
        {
            _CargoRepository = cargoRepository;
            _mapper = mapper;
            _log = logger;
        }

        /// <summary>
        /// Agregar Cargo
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateCargo/")]
        public async Task<ActionResult> Post([FromBody] CargoDTO dto)
        {
            try
            {
                var cargo = _mapper.Map<Cargo>(dto);
                var result = await _CargoRepository.AgregarCargoDAO(cargo);
                _log.LogInformation("Cargo agregado con exito");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                throw ex.InnerException!;
            }
        }

        /// <summary>
        /// Consultar todos los cargos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ConsultaCargo/")]
        public async Task<ActionResult<List<CargoDTO>>> Get()
        {
            try
            {
                var result = await _CargoRepository.ConsultarCargoDAO();
                _log.LogInformation("Cargos consultados con exito");
                return Ok(_mapper.Map<List<CargoDTO>>(result));
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                throw ex.InnerException!;
            }
        }

        /// <summary>
        /// Obtener Cargo por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "obtenerCargo")]
        public async Task<ActionResult<CargoDTO>> Get(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("El id debe ser mayor a 0");
                }
                var result = await _CargoRepository.ObtenerCargoByIdDAO(id);
                if (result.Value!.id == id)
                {
                    _log.LogInformation("Cargo consultado con exito");
                    return Ok(_mapper.Map<CargoDTO>(result.Value));
                }
                else
                {
                    return NotFound("No se encontro el cargo");
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                throw ex.InnerException!;
            }
        }

        /// <summary>
        /// Actualizar Cargo
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("ActualizarCargo/")]
        public async Task<ActionResult> ActualizarCargo([FromBody] CargoDTO dto, int id)
        {

            if (id <= 0)
            {
                return BadRequest("El id debe ser mayor a 0");
            }
            var cargo = _mapper.Map<Cargo>(dto);
            var result = await _CargoRepository.ActualizarCargoDAO(cargo, id);
            if (result.Value!.id == id)
            {
                _log.LogInformation("Cargo actualizado con exito");
                return Ok(result);
            }
            else
            {
                return NotFound("No se encontro el cargo");
            }

        }

        /// <summary>
        /// Eliminar Cargo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> EliminarCargo([Required] int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("El id debe ser mayor a 0");
                }

                var result = await _CargoRepository.EliminarCargoDAO(id);
                return result;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw ex.InnerException!;
            }

        }




        // En elaboracion
        /*
        [HttpGet("/usuarios/{cargoid}")]
        public async Task<ActionResult<List<UsuarioDTO>>> Get()(int cargoid)
        {

            try
            {
                if (cargoid <= 0)
                {
                    return BadRequest("El id debe ser mayor a 0");
                }

                var result = await _CargoRepository.ObtenerUsuariosByCargoIdDAO(cargoid);

                _log.LogInformation("Cargos consultados con exito");
                return Ok(_mapper.Map<List<UsuarioDTO>>(result));
            }
            catch (Exception ex)
            {
            _log.LogError(ex.ToString());
            throw ex.InnerException!;
            }

         }**/
    }

}