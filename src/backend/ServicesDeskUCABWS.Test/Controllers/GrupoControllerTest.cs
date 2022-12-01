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
using System;
using Moq;
using Xunit;
using ServicesDeskUCABWS.Persistence.DAO.Implementations;

namespace ServicesDeskUCABWS.Test.Controllers
{
   
    public class GrupoControllerTest
    {
        private readonly GrupoController _controller;
        private readonly Mock<IGrupoDAO> _servicesMock;
        private readonly Mock<ILogger<GrupoController>> _log;
        public GrupoDTO grupo = It.IsAny<GrupoDTO>();
        public Grupo tipo = It.IsAny<Grupo>();

        public GrupoControllerTest()
        {
            _log = new Mock<ILogger<GrupoController>>();
            _servicesMock = new Mock<IGrupoDAO>();
            _controller = new GrupoController(_log.Object, _servicesMock.Object);
            _controller.ControllerContext = new ControllerContext();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.ActionDescriptor = new ControllerActionDescriptor();
        }
        [Fact (DisplayName = "Agregar Grupo")]

        public Task CrearGrupoControllerTest()
        {
            var dto = new GrupoDTO()
            {
                id = 1,
                nombre = "Grupo2",
                departamentoid = 1
            };
            _servicesMock.Setup(t =>t.AgregarGrupoDAO(tipo)).Returns(grupo);

            var result = _controller.AgregarGrupo(dto);

            Assert.IsType<ActionResult<GrupoDTO>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Agregar Grupo con Excepcion")]
        public Task CreateGrupoControllerTestException()
        {
            _servicesMock.Setup(t => t.AgregarGrupoDAO(tipo))
            .Throws(new NullReferenceException());

            Assert.Throws<NullReferenceException>(() => _controller.AgregarGrupo(grupo));
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Consultar Lista Grupo")]
        public Task ConsultarGrupoControllerTest()
        {
            _servicesMock.Setup(t => t.ConsultarGrupoDAO())
            .Returns(new List<GrupoDTO>());

            var result = _controller.ConsultarGrupo();

            Assert.IsType<ActionResult<List<GrupoDTO>>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Consulta Lista Grupo con Excepcion")]
        public Task ConsultarGrupoControllerTestException()
        {
            _servicesMock
                .Setup(t => t.ConsultarGrupoDAO())
                .Throws(new Exception("", new NullReferenceException()));

            Assert.Throws<NullReferenceException>(() => _controller.ConsultarGrupo());
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Actualizar Grupo")]
        public Task ActualizarGrupoControllerTest()
        {
            
            var tipo1 = new GrupoDTO() {
                id = 1, nombre = "Grupo 6", departamentoid =1  };

            _servicesMock.Setup(t => t.ActualizarGrupoDAO(tipo))
                .Returns(new GrupoDTO());

            var result = _controller.ActualizarGrupo(tipo1);
            Assert.IsType<ActionResult<GrupoDTO>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Actualiza Grupo con Excepcion")]
        public Task ActualizarGrupoControllerTestException()
        {
            _servicesMock.Setup(t => t.ActualizarGrupoDAO(tipo)).Throws(new Exception("", new NullReferenceException()));

            Assert.Throws<NullReferenceException>(() => _controller.ActualizarGrupo(grupo));
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Elimina un Grupo")]
        public Task EliminarGrupoControllerTest()
        {
            var codigo = 1;
            _servicesMock.Setup(t => t.EliminarGrupoDAO(It.IsAny<int>())).Returns(It.IsAny<GrupoDTO>());

            var result = _controller.EliminarGrupo(codigo);

            Assert.IsType<ActionResult<GrupoDTO>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Elimina un Grupo con excepcion")]
        public Task EliminarGrupoControllerTestException()
        {
            _servicesMock.Setup(t => t.EliminarGrupoDAO(It.IsAny<int>()))
            .Throws(new Exception("", new NullReferenceException()));

            Assert.Throws<NullReferenceException>(() => _controller.EliminarGrupo(It.IsAny<int>()));
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Consultar Grupo por id")]
        public Task ConsultarGrupoIdControllerTest()
        {
            _servicesMock.Setup(t => t.ConsultaGrupoIdDAO(It.IsAny<int>()))
            .Returns(grupo);

            var result = _controller.ConsultaGrupoId(1);

            Assert.IsType<ActionResult<GrupoDTO>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Valida consultar Grupo por id excepcion")]
        public Task ConsultarGrupoIdControllerTestException()
        {
            _servicesMock.Setup(t => t.ConsultaGrupoIdDAO(It.IsAny<int>()))
            .Throws((new Exception("", new NullReferenceException())));

            Assert.Throws<NullReferenceException>(() => _controller.ConsultaGrupoId(It.IsAny<int>())); ;
            return Task.CompletedTask;
        }

    }

}
