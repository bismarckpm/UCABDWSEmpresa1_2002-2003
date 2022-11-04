using ServicesDeskUCABWS.BussinessLogic.Mapper;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Entity;
using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using Microsoft.Extensions.Logging;
using AutoMapper;
using System.Security.Cryptography;

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

        public UsuarioController(IUsuarioDao usuarioRepository, ICargoDAO cargoRepository, IMapper mapper , IEmailDao emailRepository)
        {
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
       
        [HttpPost("Registrar")]
        public IActionResult CreateAdministrador([FromQuery] int cargoid, [FromBody] AdministratorDTO usuario)
        {
            if (usuario == null)
                return BadRequest(ModelState);

            var usuariocreate = _UsuarioRepository.GetUsuarios().Where(c => c.email.Trim().ToUpper() == usuario.Email.Trim().ToUpper()).FirstOrDefault();
            if (usuariocreate != null)
            {
                ModelState.AddModelError("", "Usuario ya exite");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);



            CreatePasswordHash(usuario.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var usuarioMap = _mapper.Map<administrador>(usuario);
            usuarioMap.cargo = _CargoRepository.GetCargo(cargoid);
            usuarioMap.passwordHash = passwordHash;
            usuarioMap.passwordSalt = passwordSalt;
            usuarioMap.VerificationToken = CreateRamdonToken();



            if (!_UsuarioRepository.CreateUsuario(usuarioMap))
            {
                ModelState.AddModelError("", "Error al guardar");
                return StatusCode(500, ModelState);
            }
            var email = new EmailDTO();
            email.para = usuarioMap.email;
            email.Cuerpo = usuarioMap.VerificationToken;
            email.asunto = "Token de verificacion";
            _emailRepository.SendEmail(email);
            return Ok("Administrador Creado");
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserLoginDTO usuario)
        {
            if (usuario == null)
                return BadRequest(ModelState);

            var usuariocreated = _UsuarioRepository.GetUsuarios().Where(c => c.email.Trim().ToUpper() == usuario.Email.Trim().ToUpper()).FirstOrDefault();
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

            if (!VerifyPasswordHash(usuario.Password, usuariocreated.passwordHash, usuariocreated.passwordSalt))
            {
                ModelState.AddModelError("", "Contrasena incorrecta");
                return StatusCode(422, ModelState);
            }
            return Ok(usuariocreated);
        }
        [HttpPost("Verificar")]
        public IActionResult Verificar(string token)
        {


            var usuariocreated = _UsuarioRepository.GetUsuarios().Where(c => c.VerificationToken.Trim().ToUpper() == token.Trim().ToUpper()).FirstOrDefault();
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
        [HttpPost("olvido-contrasena")]
        public IActionResult olvidoContrasena(string email)
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

            CreatePasswordHash(usuario.Password, out byte[] passwordHash, out byte[] passwordSalt);
            usuariocreated.passwordHash = passwordHash;
            usuariocreated.passwordSalt = passwordSalt;
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

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hash = new HMACSHA512())
            {
                passwordSalt = hash.Key;
                passwordHash = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hash = new HMACSHA512(passwordSalt))
            {
                var ComputedHash = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return ComputedHash.SequenceEqual(passwordHash);
            }
        }
    }
       
}