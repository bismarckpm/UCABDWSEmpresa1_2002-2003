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
    public class UsuarioController
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IUsuarioDao _userDao;
        public UsuarioController(ILogger<UsuarioController> logger, IUsuarioDao usuarioDao)
        {
            _logger = logger;
            _userDao = usuarioDao;
        }

        [HttpPost]
        [Route("/CreateUser/")]
        public UsuarioDTO AgregarUsuario([FromBody] UsuarioDTO userDTO)
        {   
            try
            {
                UsuarioMapper mapper = new UsuarioMapper();
                var user = mapper.DtoToEntity(userDTO);
                   return _userDao.AgregarUsuario(user);
            }catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                    throw ex.InnerException!;
            }
        }
    }
}