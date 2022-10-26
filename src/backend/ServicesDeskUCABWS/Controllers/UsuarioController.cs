using ServicesDeskUCABWS.BussinessLogic.Mapper;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Entity;
using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using Microsoft.Extensions.Logging;


namespace ServicesDeskUCABWS.Controllers
{
    [ApiController]
    [Route("/Usuario")]
    public class UsuarioController : Controller
    {
        public readonly IUsuarioDao _UsuarioRepository;
                        
        

        public UsuarioController(IUsuarioDao usuarioRepository)
        {
            _UsuarioRepository = usuarioRepository;
        }
     
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Usuario>))]
        public IActionResult GetCollection(){
            var usuarios= _UsuarioRepository.GetUsuarios();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return Ok(usuarios);
        }
    }
}