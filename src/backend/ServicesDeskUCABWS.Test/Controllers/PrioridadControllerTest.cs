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
    public class PrioridadControllerTest
    {

        private readonly PrioridadController _controller;
        private readonly Mock<IPrioridadDAO> _servicesMock;
        private readonly Mock<ILogger<PrioridadController>> _log;
        public PrioridadDTO prioridadDto = It.IsAny<PrioridadDTO>();
        public Prioridad prioridad = It.IsAny<Prioridad>();

        public PrioridadControllerTest()
        {
            _log = new Mock<ILogger<PrioridadController>>();
            _servicesMock = new Mock<IPrioridadDAO>();
            _controller = new PrioridadController(_log.Object, _servicesMock.Object);
            _controller.ControllerContext = new ControllerContext();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.ActionDescriptor = new ControllerActionDescriptor();
        }

        [Fact(DisplayName = "Agregar Prioridad")]
        public Task CreatePrioridadControllerTest()
        {
            var dto = new PrioridadDTO() { Id = 3, Nombre = "Muy alto" };

            _servicesMock.Setup(t => t.AgregarPrioridadDAO(prioridad))
            .Returns(prioridadDto);

            var result = _controller.CreatePrioridad(dto);

            Assert.IsType<ActionResult<PrioridadDTO>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Validar agregar prioridad excepcion")]
        public Task CreatePrioridadControllerTestException()
        {
            _servicesMock.Setup(t => t.AgregarPrioridadDAO(prioridad))
            .Throws(new NullReferenceException());

            Assert.Throws<NullReferenceException>(() => _controller.CreatePrioridad(prioridadDto));
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Consultar Lista Prioridad")]
        public Task ConsultarPrioridadControllerTest()
        {
            _servicesMock.Setup(t => t.ConsultarTodosPrioridadesDAO())
            .Returns(new List<PrioridadDTO>());

            var result = _controller.ConsultaPrioridades();

            Assert.IsType<ActionResult<List<PrioridadDTO>>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Valida consulta lista prioridad Excepcion")]
        public Task ConsultarPrioridadControllerTestException()
        {
            _servicesMock
                .Setup(t => t.ConsultarTodosPrioridadesDAO())
                .Throws(new Exception("", new NullReferenceException()));

            Assert.Throws<NullReferenceException>(() => _controller.ConsultaPrioridades());
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Actualizar Prioridad")]
        public Task ActualizarPrioridadControllerTest()
        {
            var pr = new PrioridadDTO() { Id =1, Nombre = "Ultra baja" };

            _servicesMock.Setup(t => t.ActualizarPrioridadDAO(prioridad))
                .Returns(prioridadDto);

            var result = _controller.ActualizarPrioridad(pr);
            Assert.IsType<ActionResult<PrioridadDTO>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Valida actualizar prioridad excepcion")]
        public Task ActualizarPrioridadControllerTestException()
        {
            _servicesMock.Setup(t => t.ActualizarPrioridadDAO(prioridad)).Throws(new Exception("", new NullReferenceException()));

            Assert.Throws<NullReferenceException>(() => _controller.ActualizarPrioridad(prioridadDto));
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Elimina Prioridad")]
        public Task EliminarPrioridadControllerTest()
        {
            _servicesMock.Setup(t => t.EliminarPrioridadDAO(It.IsAny<int>()))
                .Returns(prioridadDto);

            var result = _controller.EliminarPrioridad(1);

            Assert.IsType<ActionResult<PrioridadDTO>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Valida eliminacion prioridad excepcion")]
        public Task EliminarPrioridadControllerTestException()
        {
            _servicesMock.Setup(t => t.EliminarPrioridadDAO(It.IsAny<int>()))
            .Throws(new Exception("", new NullReferenceException()));

            Assert.Throws<NullReferenceException>(() => _controller.EliminarPrioridad(It.IsAny<int>()));
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Consultar Prioridad por id")]
        public Task ConsultarPrioridadIdControllerTest()
        {
            _servicesMock.Setup(t => t.ConsultaPrioridadDAO(It.IsAny<int>()))
            .Returns(prioridadDto);

            var result = _controller.ConsultaPrioridad(1);

            Assert.IsType<ActionResult<PrioridadDTO>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Valida consultar Prioridad por id excepcion")]
        public Task ConsultarPrioridadIdControllerTestException()
        {
            _servicesMock.Setup(t => t.ConsultaPrioridadDAO(It.IsAny<int>()))
            .Throws((new Exception("", new NullReferenceException())));

            Assert.Throws<NullReferenceException>(() => _controller.ConsultaPrioridad(It.IsAny<int>())); ;
            return Task.CompletedTask;
        }

    }
}
