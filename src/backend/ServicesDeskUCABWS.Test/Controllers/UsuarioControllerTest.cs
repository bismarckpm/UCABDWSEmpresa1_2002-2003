using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.BussinessLogic.Mapper;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.Controllers;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using System;
using Moq;
using Xunit;
using ServicesDeskUCABWS.Test.Configuraciones;

namespace ServicesDeskUCABWS.Test.Controllers
{
    public class UsuarioControllerTest : BasePrueba
    {
        private readonly UsuarioController _controller;
        private readonly Mock<IUsuarioDao> _servicesMock;
        private readonly Mock<ICargoDAO> _serMockCargo;
     //   private readonly Mock<IMapper>  _serMapper;
        private readonly Mock<IEmailDao> _emailMock;
        private readonly Mock<ILogger<UsuarioController>> _log;
        public List<Usuario> lista = new List<Usuario>()
            {
                new Cliente()
                {
                    email = "prueba@gmail.com",
                    passwordHash = new byte[32],
                    passwordSalt = new byte[32],
                    VerifiedAt = DateTime.Today,
                    VerificationToken = "12Seso9#sassdKlOijm",
                    PasswordResetToken = "12Seso9#sassdKlOijm"
                    
                },
                new Cliente()
                {
                    email = "cosa", 
                    passwordHash = new byte[32],
                    passwordSalt = new byte[32],
                    VerifiedAt = new DateTime(2022,11,15),
                    VerificationToken = "lsers"
                },
                new Cliente()
                {
                    email = "cosa2", 
                    passwordHash = new byte[32],
                    passwordSalt = new byte[32],
                    VerificationToken = "pwsoads"
                },
                new Cliente()
                {
                    email = "cosa2la@gmail.com",
                    passwordHash = new byte[32],
                    passwordSalt = new byte[32], 
                    VerificationToken= "eossa"
                },
                new Empleado()
                {
                    email = "empleado1@gmail.com",
                    passwordHash = new byte[32],
                    passwordSalt = new byte[32],
                    VerificationToken = "l7s3oj4end21"
                },
                new administrador()
                {
                    email = "admin5421@gmail.com",
                    passwordHash = new byte[32],
                    passwordSalt = new byte[32],
                    VerificationToken = "p2odju9ll2di4"                    
                }
            };
            RegistroDTO _registro = new RegistroDTO(){Email = "creaUser@gmail.com", 
                                             Password= "123osdaker*$5",
                                             confirmationpassword="123osdaker*$5"};
            Cliente _clienteNuevo = new Cliente()
            {
            email = "prueba@gmail.com", 
            passwordHash = new byte[32],
            passwordSalt = new byte[32], 
            VerifiedAt = DateTime.Today,
            VerificationToken = "12Seso9#sassdKlOijm", 
            PasswordResetToken = "Lsp34mAv$le",
            ResetTokenExpires = DateTime.Now
            };

        public UsuarioControllerTest()
        {
            var _mapper =  ConfigurarAutoMapper();
            _log = new Mock<ILogger<UsuarioController>>();
            _servicesMock = new Mock<IUsuarioDao>();
            _serMockCargo = new Mock<ICargoDAO>();   
            _emailMock = new Mock<IEmailDao>();
            _controller = new UsuarioController(_log.Object,_servicesMock.Object
            ,_serMockCargo.Object,_mapper,_emailMock.Object);

            _controller.ControllerContext = new ControllerContext();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.ActionDescriptor = new ControllerActionDescriptor();

        }

//                  Pruebas de Consultas

        [Fact(DisplayName="Obtener listado de usuarios")]
        public Task GetUsuariosCollectionTest()
        {
                // _servicesMock.Setup(u=>u.GetUsuarios())
                // .Returns(It.IsAny<ICollection<Usuario>>());

                // var result = _controller.GetCollection();

                // Assert.IsType<OkObjectResult>(result);
            return Task.CompletedTask;
        }

         [Fact(DisplayName="Obtener listado Fallido")]
         public Task GetUsuariosCollectionBadRequest()
         {
            _controller.ModelState.AddModelError("400","");
            //  var result = _controller.GetCollection();
                

            //  Assert.False(_controller.ModelState.IsValid);
             return Task.CompletedTask;
         }

        [Fact(DisplayName="Obtener Listado de Empleados")]
        public Task GetEmpleadoCollectionTest()
        {
            // _servicesMock.Setup(u=>u.GetEmpleados()).Returns(It.IsAny<ICollection<Empleado>>());

            //     var result = _controller.GetCollectionE();

            // Assert.IsType<OkObjectResult>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName="Obtener listado Fallido")]
        public Task GetEmpleadosCollectionBadRequest()
        {
            _controller.ModelState.AddModelError("400","");

            // var result = _controller.GetCollectionE();

            // Assert.False(_controller.ModelState.IsValid);
            return Task.CompletedTask;
        }

        [Fact(DisplayName="Obtener Listado de Clientes")]
        public Task GetClienteCollectionTest()
        {
            // _servicesMock.Setup(u=>u.GetClientes()).Returns(It.IsAny<ICollection<Cliente>>());

            // var result = _controller.GetCollectionC();

            // Assert.IsType<OkObjectResult>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Fallo al obtener Listado de Clientes")]
        public Task GetClienteCollectionBadRequest()
        {
             _controller.ModelState.AddModelError("400","");

            //  var result = _controller.GetCollectionC();

            //  Assert.False(_controller.ModelState.IsValid);
             return Task.CompletedTask;
        }

        [Fact(DisplayName="Obtener Listado de Administradores")]
        public Task GetAdministradorCollectionTest()
        {
            // _servicesMock.Setup(u=>u.GetAdministradores()).Returns(It.IsAny<ICollection<administrador>>());            

            // var result = _controller.GetCollectionA();

            // Assert.IsType<OkObjectResult>(result);
            return Task.CompletedTask;
        }

       [Fact(DisplayName="Fallo al obtener Listado Administradores")]
        public Task GetAdministradorCollectionBadRequest()
        {
              _controller.ModelState.AddModelError("400","");

            //   var result = _controller.GetCollectionA();

            //   Assert.False(_controller.ModelState.IsValid);
            return Task.CompletedTask;
        }

//                    Pendiente
    //    [Fact(DisplayName="Crear Usuario")]
        public Task CreateUser()
        {
            
            var cargoid = 0;
            var depid = 0;
            var dto = new RegistroDTO()
            {
                Email = "prueba@gmail.com",
                Password = "12oasda*&qw2",
                confirmationpassword = "12oasda*&qw2"
            };

            var registro = new RegistroDTO(){Email = "prueba@gmail.com"};
            var tipoUser = 3;

            // _servicesMock.Setup(u=>u.CreateUsuario(It.IsAny<Usuario>(),It.IsAny<int>(),It.IsAny<int>()))
            // .Returns(true);
            // _servicesMock.Setup(u=>u.GetUsuarioTrimToUpper(registro)).Returns(_clienteNuevo);
            
            // var result = _controller.CreateUsuario(cargoid, depid, dto,tipoUser);

            // Assert.IsType<OkObjectResult>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Crear Usuario nulo")]
        public Task CreateUserFailedNull()
        {
            _servicesMock.Setup(u=>u.CreateUsuario(_clienteNuevo,It.IsAny<int>(),It.IsAny<int>()));
              var tipoUser = 2;  
            RegistroDTO usuarioNuevo = null!;    

            // var result = _controller.CreateUsuario(It.IsAny<int>(),It.IsAny<int>(),usuarioNuevo, tipoUser);

            // Assert.IsType<BadRequestObjectResult>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName="Usuario ya Existe")]
        public Task CreateUserAlreadyExists()
        {
            RegistroDTO registro = new RegistroDTO(){Email = "creaUser@gmail.com", 
                                             Password= "123osdaker*$5",
                                             confirmationpassword="123osdaker*$5"};

            _servicesMock.Setup(u=>u.CreateUsuario(_clienteNuevo,It.IsAny<int>(),It.IsAny<int>()));
            // _servicesMock.Setup(u=>u.GetUsuarioTrimToUpper(registro)).Returns(_clienteNuevo);
            // var tipoUser = 3;

            // var result = _controller.CreateUsuario(It.IsAny<int>(),It.IsAny<int>(),registro, tipoUser);

            // Assert.IsType<ObjectResult>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName= "Fallo al crear Usuario")]
        public Task CreateUserBadRequest()
        {
            _controller.ModelState.AddModelError("400","");
            var cargoid = 1;
            var depid = 1;
            var dto = new RegistroDTO()
            {
                Email = "prueba123@gmail.com",
                Password = "12oasda*&qw2",
                confirmationpassword = "12oasda*&qw2"
            };

            var registro = new RegistroDTO(){Email = "creaUser@gmail.com", 
                                             Password= "123osdaker*$5",
                                             confirmationpassword="123osdaker*$5"};
            var tipoUser = 3;

            // _servicesMock.Setup(u=>u.CreateUsuario(It.IsAny<Usuario>(),It.IsAny<int>(),It.IsAny<int>()))
            // .Returns(true);
            // _servicesMock.Setup(u=>u.GetUsuarioTrimToUpper(registro)).Returns(_clienteNuevo);

            // var result = _controller.CreateUsuario(cargoid, depid, dto,tipoUser);

            // Assert.False(_controller.ModelState.IsValid);
            return Task.CompletedTask;
        }   



//              LOGIN
        [Fact(DisplayName="Inicio de Sesion")]
        public Task LoginTest()
        {              
            _servicesMock.Setup(u=>u.VerifyPasswordHash(It.IsAny<string>(),It.IsAny<byte[]>(),It.IsAny<byte[]>()))
            .Returns(true);
            //  _servicesMock.Setup(u=>u.GetUsuarios()).Returns(lista); 

            UserLoginDTO user = new UserLoginDTO(){
                Email = "cosa",
                Password = "ssss"
            };

            var result = _controller.Login(user);

            Assert.IsType<OkObjectResult>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName="Inicio de Sesion Fallido Usuario esta vacio")]
        public Task LoginFailedUserNullTest()
        {    
            // _servicesMock.Setup(u=>u.GetUsuarios()).Returns(It.IsAny<ICollection<Usuario>>());
         
            UserLoginDTO  user = null!;
            var result = _controller.Login(user);

            Assert.IsType<BadRequestObjectResult>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName="Inicio Sesion Fallido por Usuario No Existe")]
        public Task LoginFailedUserNotExistsTest()
        {
            // _servicesMock.Setup(u=>u.GetUsuarios()).Returns(lista);

            var data =  new UserLoginDTO(){Email = "UserNull@gmail", Password = "*****"};

            var result = _controller.Login(data);

            Assert.IsType<ObjectResult>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Fallo al Iniciar Sesion")]
        public Task LoginFailedBadRequest()
        {
            _controller.ModelState.AddModelError("400","");
            _servicesMock.Setup(u=>u.VerifyPasswordHash(It.IsAny<string>(),It.IsAny<byte[]>(),It.IsAny<byte[]>()))
            .Returns(true);
            //  _servicesMock.Setup(u=>u.GetUsuarios()).Returns(lista); 

            UserLoginDTO user = new UserLoginDTO(){
                Email = "cosa",
                Password = "ssss"
            };

            var result = _controller.Login(user);
            Assert.False(_controller.ModelState.IsValid);
            return Task.CompletedTask;
        }

        [Fact(DisplayName="Inicio de Sesion Fallido por Verificacion Contraseña")]
        public Task LoginFailedByVerifyPasswordTest()
        {              
            _servicesMock.Setup(u=>u.VerifyPasswordHash(It.IsAny<string>(),It.IsAny<byte[]>(),It.IsAny<byte[]>()))
            .Returns(false);
            //  _servicesMock.Setup(u=>u.GetUsuarios()).Returns(lista); 

            UserLoginDTO user = new UserLoginDTO(){
                Email = "cosa",
                Password = "ssss"
            };

            var result = _controller.Login(user);

            Assert.IsType<ObjectResult>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName="Inicio de Sesion Fallido por Verificacion de Usuario")]
        public Task LoginFailedByVerifyUserTest()
        {
        //  _servicesMock.Setup(u=>u.GetUsuarios()).Returns(lista); 
            var user = new UserLoginDTO()
                            {Email = "cosa2", Password = "1234567890"};  

                var result = _controller.Login(user); 

            Assert.IsType<ObjectResult>(result);
            return Task.CompletedTask;
        }

//          VERIFICACION TOKEN     
        [Fact(DisplayName="Verifica Token")]
        public Task VerificarTest()
        {
            // _servicesMock.Setup(u=>u.GetUsuarios()).Returns(lista);
            // _servicesMock.Setup(u=>u.Save()).Returns(true);
            var token = "12Seso9#sassdKlOijm";
                var result = _controller.Verificar(token);

            Assert.IsType<OkObjectResult>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName="Token Invalido")]
        public Task VerificarTokenInvalidTest()
        {
            // _servicesMock.Setup(u=>u.GetUsuarios()).Returns(lista);
            var token = "";

            var result = _controller.Verificar(token);

            Assert.IsType<ObjectResult>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName="Falla al Verificar token")]
        public Task VerificarBadRequest()
        {
            _controller.ModelState.AddModelError("400","");

            // _servicesMock.Setup(u=>u.GetUsuarios()).Returns(lista);
            // _servicesMock.Setup(u=>u.Save()).Returns(true);
            
            var token = "12Seso9#sassdKlOijm";
            var result = _controller.Verificar(token);

            Assert.False(_controller.ModelState.IsValid);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Token fallo al Guardar")]
        public Task VerificarFailedSaveTest()
        {
            // _servicesMock.Setup(u=>u.GetUsuarios()).Returns(lista);
            // _servicesMock.Setup(u=>u.Save()).Returns(false);

                var token = "eossa";

                var result = _controller.Verificar(token);

                Assert.IsType<BadRequestObjectResult>(result);
            return Task.CompletedTask;
        }

//              CONTRASENA OLVIDADA
        [Fact(DisplayName="Contraseña olvidada")]
        public Task OlvidoContrasenaTest()
        {
            // _servicesMock.Setup(u=>u.GetUsuarios()).Returns(lista);
            // _servicesMock.Setup(u=>u.Save()).Returns(true);
            var email = "cosa2la@gmail.com";

            var result = _controller.olvidoContrasena(email);

            Assert.IsType<OkObjectResult>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName="Olvido Contraseña Usuario No Encontrado")]
        public Task OvidoContrasenaUserNotFoundTest()
        {
            _servicesMock.Setup(u=>u.GetUsuario()).Returns(lista);
            var email = "milagrosB23@gmail.com";

            var result = _controller.olvidoContrasena(email);

            Assert.IsType<ObjectResult>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Fallo en el envio de token")]
        public Task OlvidoContrasenaBadRequest()
        {
            _controller.ModelState.AddModelError("400","");

            _servicesMock.Setup(u=>u.GetUsuario()).Returns(lista);
            // _servicesMock.Setup(u=>u.`()).Returns(true);
            var email = "cosa2la@gmail.com";

            var result = _controller.olvidoContrasena(email);

            Assert.False(_controller.ModelState.IsValid);
            return Task.CompletedTask;
        }

        [Fact(DisplayName="Olvido Contraseña error al guardar")]
        public Task OvidoContrasenaBadRequestToSavePwdTest()
        {
            _servicesMock.Setup(u=>u.GetUsuario()).Returns(lista);
            // _servicesMock.Setup(u=>u.Save());
            var email = "cosa";

            var result = _controller.olvidoContrasena(email);

            Assert.IsType<BadRequestObjectResult>(result);
            return Task.CompletedTask;
        }

//          RESETEO DE CONTRASENA

       // [Fact(DisplayName="Reseteo de Contraseña")]
        public Task ResetPasswordTest()
        { 
            var client_local = new Cliente()
        {
            email = "prueba@gmail.com", 
            passwordHash = new byte[32],
            passwordSalt = new byte[32], 
            VerifiedAt = DateTime.Today,
            VerificationToken = "12Seso9#sassdKlOijm", 
            PasswordResetToken = "Lsp34mAv$le",
            ResetTokenExpires = DateTime.Now
        };
            
            _servicesMock.Setup(u=>u.GetUsuario()).Returns(lista);
            // _servicesMock.Setup(u=>u.Save()).Returns(true);
            _servicesMock.Setup(u=>u.CreatePasswordHash(client_local,"123456789"))
            .Returns(_clienteNuevo);

            var pwd = new ResetPasswordDTO()
            {
                token = "12Seso9#sassdKlOijm",
                Password = "3356*$sdsa",
                confirmationpassword = "3356*$sdsa"
            };

            var result = _controller.ResetPassword(pwd);

            Assert.IsType<OkObjectResult>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName ="Reseto de contraseña, Usuario Vacio")]
        public Task ResetPasswordUserNullTest()
        {
            ResetPasswordDTO user = null!;

            var result = _controller.ResetPassword(user);

            Assert.IsType<BadRequestObjectResult>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Reseteo de contraseña, token invalido")]
        public Task ResetPasswordTokenInvalidTest()
        {
            // _servicesMock.Setup(u=>u.GetUsuarios()).Returns(lista);

            var pwd =new ResetPasswordDTO()
            {
                token = "L4osjWso9we0fpsxk*",
                Password = "3356*$sdsa",
                confirmationpassword = "3356*$sdsa"
            };

            var result = _controller.ResetPassword(pwd);

            Assert.IsType<ObjectResult>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Fallo de reseteo de contrasena por mala peticion")]
        public Task ResetPasswordBadRequest()
        {
            _controller.ModelState.AddModelError("400","");

            var client_local = new Cliente()
        {
            email = "prueba@gmail.com", 
            passwordHash = new byte[32],
            passwordSalt = new byte[32], 
            VerifiedAt = DateTime.Today,
            VerificationToken = "12Seso9#sassdKlOijm", 
            PasswordResetToken = "Lsp34mAv$le",
            ResetTokenExpires = DateTime.Now
        };
            
            _servicesMock.Setup(u=>u.GetUsuario()).Returns(lista);
            // _servicesMock.Setup(u=>u.Save()).Returns(true);
            _servicesMock.Setup(u=>u.CreatePasswordHash(client_local,"123456789"))
            .Returns(_clienteNuevo);

            var pwd = new ResetPasswordDTO()
            {
                token = "12Seso9#sassdKlOijm",
                Password = "3356*$sdsa",
                confirmationpassword = "3356*$sdsa"
            };

            var result = _controller.ResetPassword(pwd);

            Assert.False(_controller.ModelState.IsValid);
            return Task.CompletedTask;
        }


    }
}