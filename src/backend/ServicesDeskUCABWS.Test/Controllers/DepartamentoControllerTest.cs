using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Controllers;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Entity;

namespace ServicesDeskUCABWS.Test.Controllers
{
    public class DepartamentoControllerTest
    {

        private readonly DepartamentoController _controller;
        private readonly Mock<IDepartamentoDAO> _servicesMock;
        private readonly Mock<ILogger<DepartamentoController>> _log;
        public DepartamentoDTO departamentoDto = It.IsAny<DepartamentoDTO>();
        public Departamento departamento = It.IsAny<Departamento>();

        public DepartamentoControllerTest()
        {
            _log = new Mock<ILogger<DepartamentoController>>();
            _servicesMock = new Mock<IDepartamentoDAO>();
            _controller = new DepartamentoController(_log.Object, _servicesMock.Object);
            _controller.ControllerContext = new ControllerContext();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.ActionDescriptor = new ControllerActionDescriptor();
        }

        [Fact(DisplayName = "Agregar Departamento")]
        public Task CreateDepartamentoControllerTest()
        {
            var dto = new DepartamentoDTO() { Id = 3, Nombre = "departamento3" };

            _servicesMock.Setup(t => t.AgregarDepartamentoDAO(departamento))
            .Returns(departamentoDto);

            var result = _controller.CreateDepartamento(dto);

            Assert.IsType<ActionResult<DepartamentoDTO>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Validar agregar departamento excepcion")]
        public Task CreateDepartamentoControllerTestException()
        {
            _servicesMock.Setup(t => t.AgregarDepartamentoDAO(departamento))
            .Throws(new NullReferenceException());

            Assert.Throws<NullReferenceException>(() => _controller.CreateDepartamento(departamentoDto));
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Consultar Lista Departamento")]
        public Task ConsultarDepartamentoControllerTest()
        {
            _servicesMock.Setup(t => t.ConsultarDepartamentosDAO())
            .Returns(new List<DepartamentoDTO>());

            var result = _controller.ConsultaDepartamentos();

            Assert.IsType<ActionResult<List<DepartamentoDTO>>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Valida consulta lista departamentos Excepcion")]
        public Task ConsultarDepartamentoControllerTestException()
        {
            _servicesMock
                .Setup(t => t.ConsultarDepartamentosDAO())
                .Throws(new Exception("", new NullReferenceException()));

            Assert.Throws<NullReferenceException>(() => _controller.ConsultaDepartamentos());
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Actualizar Departamento")]
        public Task ActualizarDepartamentoControllerTest()
        {
            var dep = new DepartamentoDTO(){ Id =1, Nombre = "departamento1" };

            _servicesMock.Setup(t => t.ModificarDepartamentoDAO(departamento))
                .Returns(departamentoDto);

            var result = _controller.ActualizarDepartamento(dep);
            Assert.IsType<ActionResult<DepartamentoDTO>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Valida actualizar departamento excepcion")]
        public Task ModificarDepartamentoControllerTestException()
        {
            _servicesMock.Setup(t => t.ModificarDepartamentoDAO(departamento)).Throws(new Exception("", new NullReferenceException()));

            Assert.Throws<NullReferenceException>(() => _controller.ActualizarDepartamento(departamentoDto));
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Elimina Departamento")]
        public Task EliminarDepartamentoControllerTest()
        {
            _servicesMock.Setup(t => t.EliminarDepartamentoDAO(It.IsAny<int>()))
                .Returns(departamentoDto);

            var result = _controller.EliminarDepartamento(1);

            Assert.IsType<ActionResult<DepartamentoDTO>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Valida eliminacion departamento excepcion")]
        public Task EliminarDepartamentoControllerTestException()
        {
            _servicesMock.Setup(t => t.EliminarDepartamentoDAO(It.IsAny<int>()))
            .Throws(new Exception("", new NullReferenceException()));

            Assert.Throws<NullReferenceException>(() => _controller.EliminarDepartamento(It.IsAny<int>()));
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Consultar Departamento por id")]
        public Task ConsultarDepartamentoIdControllerTest()
        {
            _servicesMock.Setup(t => t.ConsultaUnDepartamentoDAO(It.IsAny<int>()))
            .Returns(departamentoDto);

            var result = _controller.ConsultaDepartamento(1);

            Assert.IsType<ActionResult<DepartamentoDTO>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Valida consultar Departamento por id excepcion")]
        public Task ConsultarDepartamentoIdControllerTestException()
        {
            _servicesMock.Setup(t => t.ConsultaUnDepartamentoDAO(It.IsAny<int>()))
            .Throws((new Exception("", new NullReferenceException())));

            Assert.Throws<NullReferenceException>(() => _controller.ConsultaDepartamento(It.IsAny<int>())); ;
            return Task.CompletedTask;
        }

    }
}
