
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.DAO.Interface;

namespace ServicesDeskUCABWS.Controllers
{
    [Route("/Cargo")]
    [ApiController]
    public class CargoController : ControllerBase
    {
        public readonly ICargoDAO _CargoRepository;
        public readonly IMapper _mapper;    
        public CargoController(ICargoDAO cargoRepository, IMapper mapper)
        {
              _CargoRepository = cargoRepository;
            _mapper=mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CargoDTO>))]
        public IActionResult GetCargos(){
            var usuarios= _mapper.Map<List<CargoDTO>>(_CargoRepository.GetCargos());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return Ok(usuarios);
        }
        [HttpGet("/usuarios/{cargoid}")]
         public IActionResult UsuariosByCargos(int cargoid){
             
            if (!_CargoRepository.CargoExist(cargoid))
                return NotFound();
            
            var usuarios = _mapper.Map<List<UsuarioDTO>>(_CargoRepository.GetUsuarioByCargo(cargoid));

              if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return Ok(usuarios);
        }
    }
}