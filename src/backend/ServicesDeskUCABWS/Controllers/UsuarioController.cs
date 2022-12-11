using ServicesDeskUCABWS.BussinessLogic.Mapper;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Entity;
using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using Microsoft.Extensions.Logging;
using AutoMapper;
using System.Security.Cryptography;
using ServicesDeskUCABWS.Persistence.DAO.Implementations;
using ServicesDeskUCABWS.Exceptions;
using static ServicesDeskUCABWS.Reponses.AplicationResponse;

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


        [HttpGet("Empleados/Departamento/{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UsuarioDTO>))]
        public ApplicationResponse<ICollection<UsuarioDTO>> GetUsuarioDepartamento([FromRoute] int id)
        {
            var response = new ApplicationResponse<ICollection<UsuarioDTO>>();
             try{
             response.Data =_UsuarioRepository.GetUsuariosPorDepartamento(id);
           
             } catch (UsuarioExepcion ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
           
        }
       
        [HttpPost("Registrar")]
        public ApplicationResponse<string> CreateUsuario([FromBody] RegistroDTO usuario)
        {
            var response = new ApplicationResponse<string>();
             try
            {
            if (usuario.tipousuario == 1)
            {
                 mapeado = _UsuarioRepository.CreatePasswordHash(_mapper.Map<administrador>(usuario), usuario.Password);
            }
            //empleado
            else if (usuario.tipousuario == 2)
            {
              mapeado = _UsuarioRepository.CreatePasswordHash(_mapper.Map<Empleado>(usuario), usuario.Password);

            }
            else if (usuario.tipousuario == 3)
            {
                mapeado = _UsuarioRepository.CreatePasswordHash(_mapper.Map<Cliente>(usuario), usuario.Password);

            }
            mapeado.VerificationToken = CreateRamdonToken();
            response.Data = _UsuarioRepository.CreateUsuario(mapeado,usuario.cargoid,usuario.GrupoId);
            var email = new EmailDTO();
            email.para = mapeado.email;
            email.Cuerpo = mapeado.VerificationToken;
            email.asunto = "Token de verificacion";
            _emailRepository.SendEmail(email);
            }
            catch (UsuarioExepcion ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }

        [HttpPost("Login")]
        public ApplicationResponse<string> Login([FromBody] UserLoginDTO usuario)
        {
            var response = new ApplicationResponse<string>();
           try{
           
            var usuariocreated =_UsuarioRepository.GetUsuario().Where(c => c.email == usuario.Email).FirstOrDefault();
            if ( usuariocreated ==  null)
            {
                throw new UsuarioExepcion("Ha ocurrido un error el Usuario no existe");
            }
            if (usuariocreated.VerifiedAt == null)
            {
                throw new UsuarioExepcion("Ha ocurrido un error usuario no verificado ");
            }

            if (!_UsuarioRepository.VerifyPasswordHash(usuario.Password!, usuariocreated.passwordHash, usuariocreated.passwordSalt))
            {
                throw new UsuarioExepcion("Ha ocurrido un error Contrasena incorrecta ");
            }
            response.Data = "Exitoso";
           }
           catch(UsuarioExepcion ex){
                response.Success = false;
                response.Message = ex.Mensaje;
               
           }
           return response;
        }
        [HttpGet("Verificar")]
        public ApplicationResponse<string> Verificar([FromQuery] string token)
        {
            var response = new ApplicationResponse<string>();
           try{
           
            var usuariocreated = _UsuarioRepository.GetUsuario().Where(c => c.VerificationToken == token).FirstOrDefault();
            if (usuariocreated == null)
            {
                 throw new UsuarioExepcion("Ha ocurrido un error Token Invalido ");
            }
            usuariocreated.VerifiedAt = DateTime.Now;
            response.Data =_UsuarioRepository.UpdateU(usuariocreated);
           }
             catch(UsuarioExepcion ex){
                response.Success = false;
                response.Message = ex.Mensaje;
                
           }
           return response;


        }
        [HttpGet("olvido-contrasena")]
        public ApplicationResponse<string> olvidoContrasena([FromQuery] string email)
        {
            var response = new ApplicationResponse<string>();
           try{
            var usuariocreated = _UsuarioRepository.GetUsuario().Where(c => c.email == email).FirstOrDefault();
            if (usuariocreated == null)
            {
                  throw new UsuarioExepcion("Ha ocurrido un error Usuario no encontrado ");
            }
            usuariocreated.PasswordResetToken = CreateRamdonToken();
            usuariocreated.ResetTokenExpires = DateTime.Now.AddDays(1);
            response.Data =_UsuarioRepository.UpdateU(usuariocreated);
            var emailsend = new EmailDTO();
            emailsend.para = usuariocreated.email;
            emailsend.Cuerpo = usuariocreated.PasswordResetToken;
            emailsend.asunto = "Token de reseteo de clave";
            _emailRepository.SendEmail(emailsend);
           } catch(UsuarioExepcion ex){
                response.Success = false;
                response.Message = ex.Mensaje;
           }
           return response;
            }
        
        [HttpPost("Reset-Password")]
        public ApplicationResponse<string> ResetPassword([FromBody] ResetPasswordDTO usuario)
        {   
        var response = new ApplicationResponse<string>();
           try{       
            var usuariocreated = _UsuarioRepository.GetUsuario().Where(u => u.PasswordResetToken == usuario.token).FirstOrDefault();
            if (usuariocreated == null || usuariocreated.ResetTokenExpires < DateTime.Now )
            {
                   throw new UsuarioExepcion("Ha ocurrido un error Token invalido");
            }
            usuariocreated =  _UsuarioRepository.CreatePasswordHash(usuariocreated, usuario.Password);
            usuariocreated.ResetTokenExpires = null;
            usuariocreated.PasswordResetToken = null;
            response.Data =_UsuarioRepository.UpdateU(usuariocreated);
            }
            catch(UsuarioExepcion ex){
                response.Success = false;
                response.Message =ex.Mensaje;
           }
           return response;
            }
           
        private string? CreateRamdonToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }

       
    }
    }
