using ServicesDeskUCABWS.BussinessLogic.Mapper;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Entity;
using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using Microsoft.Extensions.Logging;
using AutoMapper;
using System.Security.Cryptography;
using ServicesDeskUCABWS.Persistence.DAO.Implementations;

namespace ServicesDeskUCABWS.Controllers
{
    [ApiController]
    [Route("/Usuario")]
    public class UsuarioController : Controller
    {
        public readonly IUsuarioDao _UsuarioRepository;

        public readonly ICargoDAO _CargoRepository;
        public readonly IMapper _mapper;
        private readonly IEmailDao _emailRepository;
        private readonly ILogger<UsuarioController> _log;
        private readonly IUsuarioDao _dao;
        public static Usuario mapeado;

   

        public UsuarioController(ILogger<UsuarioController> log,IUsuarioDao usuarioRepository, ICargoDAO cargoRepository, IMapper mapper, IEmailDao emailRepository)
        {
            this._log = log;
            _UsuarioRepository = usuarioRepository;
            _mapper = mapper;
            this._emailRepository = emailRepository;
            _CargoRepository = cargoRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UsuarioDTO>))]
        public IActionResult GetCollection()
        {
            var usuarios = _mapper.Map<List<UsuarioDTO>>(_UsuarioRepository.GetUsuarios());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(usuarios);
        }

     

        [HttpGet("Administradores")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UsuarioDTO>))]
        public IActionResult GetCollectionA()
        {
            var usuarios = _mapper.Map<List<UsuarioDTO>>(_UsuarioRepository.GetAdministradores());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(usuarios);
        }
         [HttpGet("Empleados")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UsuarioDTO>))]
        public IActionResult GetCollectionE()
        {
            var usuarios = _mapper.Map<List<UsuarioDTO>>(_UsuarioRepository.GetEmpleados());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(usuarios);
        }
           [HttpGet("Clientes")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UsuarioDTO>))]
        public IActionResult GetCollectionC()
        {
            var usuarios = _mapper.Map<List<UsuarioDTO>>(_UsuarioRepository.GetClientes());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(usuarios);
        }

         [HttpGet("Empleados/Departamento/{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UsuarioDTO>))]
        public IActionResult GetUsuarioDepartamento([FromRoute] int id)
        {
            var usuarios = _UsuarioRepository.GetUsuariosPorDepartamento(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(usuarios);
        }

       
        [HttpPost("Registrar")]
        public IActionResult CreateUsuario([FromQuery] int cargoid,[FromQuery] int GrupoId, [FromBody] RegistroDTO usuario, [FromQuery] int tipousuario)
        {
            if (usuario == null)
                return BadRequest(ModelState);

            var usuariocreate = _UsuarioRepository.GetUsuarioTrimToUpper(usuario);
            if (usuariocreate != null)
            {
                ModelState.AddModelError("", "Usuario ya exite");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //administrador
            if (tipousuario == 1)
            {
                 var clave = usuario.Password;
                 var usuarioMap = _mapper.Map<administrador>(usuario);
                 mapeado = _UsuarioRepository.CreatePasswordHash(usuarioMap, clave);
                 
            }
            //empleado
            else if (tipousuario == 2)
            {
                var clave = usuario.Password;
                var usuarioMap = _mapper.Map<Empleado>(usuario);
                mapeado = _UsuarioRepository.CreatePasswordHash(usuarioMap, clave);

            }
            else if (tipousuario == 3)
            {
                var clave = usuario.Password;
                var usuarioMap = _mapper.Map<Cliente>(usuario);
                mapeado = _UsuarioRepository.CreatePasswordHash(usuarioMap, clave);
                cargoid = 0;
            }
            else
            {
                ModelState.AddModelError("", "Tipo de usuario no existe");
                return StatusCode(422, ModelState);
            }


            mapeado.VerificationToken = CreateRamdonToken();



            if (!_UsuarioRepository.CreateUsuario(mapeado, cargoid, GrupoId))
            {
                ModelState.AddModelError("", "Error al guardar");
                return StatusCode(500, ModelState);
            }
            var email = new EmailDTO();
            email.para = mapeado.email;
            email.Cuerpo = mapeado.VerificationToken;
            email.asunto = "Token de verificacion";
            _emailRepository.SendEmail(email);
            return Ok("Usuario Creado");
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserLoginDTO usuario)
        {
            if (usuario == null)
                return BadRequest(ModelState);

            var usuariocreated = _UsuarioRepository.GetUsuarios().Where(c => c.email!.Trim().ToUpper() == usuario.Email!.Trim().ToUpper()).FirstOrDefault();
            if (usuariocreated == null)
            {
                ModelState.AddModelError("", "Usuario no existe");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (usuariocreated.VerifiedAt == null)
            {
                ModelState.AddModelError("", "Usuario no Verificado");
                return StatusCode(422, ModelState);
            }

            if (!_UsuarioRepository.VerifyPasswordHash(usuario.Password!, usuariocreated.passwordHash, usuariocreated.passwordSalt))
            {
                ModelState.AddModelError("", "Contrasena incorrecta");
                return StatusCode(422, ModelState);
            }
            return Ok(usuariocreated);
        }
        [HttpGet("Verificar")]
        public IActionResult Verificar([FromQuery] string token)
        {


            var usuariocreated = _UsuarioRepository.GetUsuarios().Where(c => c.VerificationToken!.Trim().ToUpper() == token.Trim().ToUpper()).FirstOrDefault();
            if (usuariocreated == null)
            {
                ModelState.AddModelError("", "Token invalido");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            usuariocreated.VerifiedAt = DateTime.Now;
            if (_UsuarioRepository.Save())
            {
                return Ok("Usuario Verificado");
            }
            return BadRequest(ModelState);

        }
        [HttpGet("olvido-contrasena")]
        public IActionResult olvidoContrasena([FromQuery] string email)
        {


            var usuariocreated = _UsuarioRepository.GetUsuarios().Where(c => c.email == email).FirstOrDefault();
            if (usuariocreated == null)
            {
                ModelState.AddModelError("", "Usuario no encontrado");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            usuariocreated.PasswordResetToken = CreateRamdonToken();
            usuariocreated.ResetTokenExpires = DateTime.Now.AddDays(1);
            if (_UsuarioRepository.Save())
            {
            var emailsend = new EmailDTO();
            emailsend.para = usuariocreated.email;
            emailsend.Cuerpo = usuariocreated.PasswordResetToken;
            emailsend.asunto = "Token de reseteo de clave";
            _emailRepository.SendEmail(emailsend);
                return Ok("Token enviado a su correo");
            }
            return BadRequest(ModelState);
        }
        [HttpPost("Reset-Password")]
        public IActionResult ResetPassword([FromBody] ResetPasswordDTO usuario)
        {
            if (usuario == null )
                return BadRequest(ModelState);
            
            var usuariocreated = _UsuarioRepository.GetUsuarios().Where(u => u.PasswordResetToken == usuario.token).FirstOrDefault();
           
            if (usuariocreated == null || usuariocreated.ResetTokenExpires < DateTime.Now )
            {
                ModelState.AddModelError("", "Token invalido");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            usuariocreated =  _UsuarioRepository.CreatePasswordHash(usuariocreated, usuario.Password);
           
            usuariocreated.ResetTokenExpires = null;
            usuariocreated.PasswordResetToken = null;


            if (_UsuarioRepository.Save())
            {
                return Ok("Contrasena cambiada con exito");
            }else{
            return BadRequest("ModelState");
            }
           
        }

  


        private string? CreateRamdonToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }

       
    }
       
}