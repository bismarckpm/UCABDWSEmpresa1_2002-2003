using ServicesDeskUCABWS.BussinessLogic.Mapper;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Entity;
using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace ServicesDeskUCABWS.Controllers
{
    [ApiController]
    [Route("/Usuario")]
    public class UsuarioController : Controller
    {
        public readonly IUsuarioDao _UsuarioRepository;
        
        public readonly ICargoDAO _CargoRepository;
        public readonly IMapper _mapper;             
        

        public UsuarioController(IUsuarioDao usuarioRepository, ICargoDAO cargoRepository, IMapper mapper)
        {
            _UsuarioRepository = usuarioRepository;
            _mapper=mapper;
            _CargoRepository =cargoRepository;
        }
     
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UsuarioDTO>))]
        public IActionResult GetCollection(){
            var usuarios= _mapper.Map<List<UsuarioDTO>>(_UsuarioRepository.GetUsuarios());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return Ok(usuarios);
        }
        [HttpGet("{username}/{password}/{confirmationpassword}")]
         public IActionResult Changepassword(string username, string password){
             
            if (!_UsuarioRepository.UsuarioExists(username,password))
                return NotFound();
            
            var usuario = _mapper.Map<UsuarioDTO>(_UsuarioRepository.GetUsuario(username));

              if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return Ok(usuario);
        }
        [HttpPost]
        public IActionResult CreateAdministrador([FromQuery] int cargoid,[FromBody] AdministratorDTO usuario){
            if(usuario == null)
            return  BadRequest(ModelState);

            var usuariocreate =  _UsuarioRepository.GetUsuarios().Where(c =>c.email.Trim().ToUpper() == usuario.Email.Trim().ToUpper()).FirstOrDefault();
            if(usuariocreate != null){
                ModelState.AddModelError("", "Usuario ya exite");
                 return StatusCode(422, ModelState);
            }
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuarioMap = _mapper.Map<administrador>(usuario);
            usuarioMap.cargo = _CargoRepository.GetCargo(cargoid);
            if(!_UsuarioRepository.CreateUsuario(usuarioMap)){
                ModelState.AddModelError("", "Error al guardar");
                return StatusCode(500, ModelState);       
                }
        return Ok("Administrador Creado");
        }

    }
}