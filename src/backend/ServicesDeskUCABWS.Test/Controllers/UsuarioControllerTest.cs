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

namespace ServicesDeskUCABWS.Test.Controllers
{
    public class UsuarioControllerTest
    {
        private readonly UsuarioController _controller;
        private readonly Mock<IUsuarioDao> _servicesMock;
        private readonly Mock<ICargoDAO> _serMockCargo;
        private readonly Mock<IMapper>  _serMapper;
        private readonly Mock<IEmailDao> _emailMock;
        private readonly Mock<ILogger<UsuarioController>> _log;

        public UsuarioControllerTest()
        {
            _log = new Mock<ILogger<UsuarioController>>();
            _servicesMock = new Mock<IUsuarioDao>();
            _serMockCargo = new Mock<ICargoDAO>();
            _serMapper = new Mock<IMapper>();
            _emailMock = new Mock<IEmailDao>();
            _controller = new UsuarioController(_log.Object,_servicesMock.Object
            ,_serMockCargo.Object,_serMapper.Object,_emailMock.Object);

            _controller.ControllerContext = new ControllerContext();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.ActionDescriptor = new ControllerActionDescriptor();

        }

        [Fact(DisplayName="Obtener listado de usuarios")]
        public Task GetUsuarios()
        {
                _servicesMock.Setup(u=>u.GetUsuarios())
                .Returns(It.IsAny<ICollection<Usuario>>());

                var result = _controller.GetCollection();

                Assert.IsType<OkObjectResult>(result);
            return Task.CompletedTask;
        }

        // [Fact(DisplayName = "Fallo al Obtener listado de usuarios")]
        // public Task GetUsuariosBadRequest()
        // {
        //     _servicesMock.Setup(u=>u.GetUsuarios())
        //     .Returns(It.IsAny<ICollection<Usuario>>());

        //     var result = _controller.GetCollection();

        //     Assert.IsType<BadRequestResult>(result);
        //     return Task.CompletedTask;
        // }

        [Fact(DisplayName="Crear Usuario")]
        public Task CreateUser()
        {
            var cargoid = 1;
            var depid = 1;
            var dto = new RegistroDTO()
            {
                Email = "prueba123@gmail.com",
                Password = "12oasda*&qw2",
                confirmationpassword = "12oasda*&qw2"
            };
            var tipoUser = 1;

            _servicesMock.Setup(u=>u.CreateUsuario(It.IsAny<Usuario>(),It.IsAny<int>(),It.IsAny<int>()))
            .Returns(true);

            var result = _controller.CreateUsuario(cargoid, depid, dto,tipoUser);

            Assert.IsType<OkObjectResult>(result);
            return Task.CompletedTask;
        }
    }
}