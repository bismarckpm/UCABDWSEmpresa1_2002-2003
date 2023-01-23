using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Controllers;
using ServicesDeskUCABWS.Exceptions;
using ServicesDeskUCABWS.Persistence.DAO.Implementations;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Database;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.Test.Configuraciones;
using static ServicesDeskUCABWS.Reponses.AplicationResponse;

namespace ServicesDeskUCABWS.Test.Controllers
{
    public class EstadoControllerTest : BasePrueba
    {

        private readonly EstadoController _controller;
        private readonly Mock<IEstadoDAO> _servicesMock;
        public EstadoDTO estadoDto = It.IsAny<EstadoDTO>();

        public EstadoCreateDTO estadoCreateDto = It.IsAny<EstadoCreateDTO>();
        public Estado estado = It.IsAny<Estado>();
        public ILogger<EstadoDAO> loggerDAO = new NullLogger<EstadoDAO>();

        private readonly Mock<IMigrationDbContext> _contextMock;

        public EstadoControllerTest()
        {
            _contextMock = new Mock<IMigrationDbContext>();
            var _logger = new NullLogger<EstadoController>();
            var _mapper = ConfigurarAutoMapper();
            _servicesMock = new Mock<IEstadoDAO>();
            _controller = new EstadoController(_logger, _servicesMock.Object, _mapper);
        }

        [Fact(DisplayName = "Agregar un Estado")]
        public async void CreateEstadoControllerTest()
        {
            var dto = new EstadoCreateDTO() { Nombre = "Estado 1", EtiquetaId = 1 };
            var response = new ApplicationResponse<EstadoDTO>();
            // preparacion de los datos
            _servicesMock.Setup(x => x.AgregarEstadoDAO(It.IsAny<Estado>())).ReturnsAsync(new EstadoDTO() { id = 1, Nombre = "Estado 1", EtiquetaId = 1 });
            Boolean expected = true;
            //probar metodo post
            response = await _controller.Post(dto);
            //verificar 
            Assert.Equal<Boolean>(expected, response.Success);
        }

        [Fact(DisplayName = "Agregar Estado con Exception")]
        public async void CreateEstadoControllerException()
        {
            var dto = new EstadoCreateDTO() { Nombre = "Estado 1", EtiquetaId = 1 };
            var response = new ApplicationResponse<EstadoDTO>();
            // preparacion de los datos
            _servicesMock.Setup(x => x.AgregarEstadoDAO(It.IsAny<Estado>())).Throws(new EstadoException("Error al agregar Estado", new System.Exception(), loggerDAO));
            Boolean expected = false;
            //probar metodo post
            response = await _controller.Post(dto);
            //verificar 
            Assert.Equal<Boolean>(expected, response.Success);
        }

        [Fact(DisplayName = "Obtener lista de Estados")]
        public async void GetEstadosControllerTest()
        {
            var response = new ApplicationResponse<List<EstadoResponseDTO>>();
            // preparacion de los datos
            _servicesMock.Setup(x => x.GetEstadosDAO()).ReturnsAsync(new List<EstadoResponseDTO> { new EstadoResponseDTO() { id = 1, Nombre = "Estado 1", EtiquetaId = 1 } });
            Boolean expected = true;
            //probar metodo get
            response = await _controller.Get();
            //verificar 
            Assert.Equal<Boolean>(expected, response.Success);
        }

        [Fact(DisplayName = "Obtener lista de Estados con Exception")]
        public async void GetEstadosControllerException()
        {
            var response = new ApplicationResponse<List<EstadoResponseDTO>>();
            // preparacion de los datos
            _servicesMock.Setup(x => x.GetEstadosDAO()).Throws(new EstadoException("Error al obtener Estados", new System.Exception(), loggerDAO));
            Boolean expected = false;
            //probar metodo get
            response = await _controller.Get();
            //verificar 
            Assert.Equal<Boolean>(expected, response.Success);
        }

        [Fact(DisplayName = "Obtener Estado por Id")]
        public async void GetEstadoByIdControllerTest()
        {
            var response = new ApplicationResponse<EstadoResponseDTO>();
            // preparacion de los datos
            _servicesMock.Setup(x => x.GetEstadoDAO(It.IsAny<int>())).ReturnsAsync(new EstadoResponseDTO() { id = 1, Nombre = "Estado 1", EtiquetaId = 1 });
            Boolean expected = true;
            //probar metodo get
            response = await _controller.Get(1);
            //verificar 
            Assert.Equal<Boolean>(expected, response.Success);
        }

        [Fact(DisplayName = "Obtener Estado por Id con Exception")]
        public async void GetEstadoByIdControllerException()
        {
            var response = new ApplicationResponse<EstadoResponseDTO>();
            // preparacion de los datos
            _servicesMock.Setup(x => x.GetEstadoDAO(It.IsAny<int>())).Throws(new EstadoException("Error al obtener Estado", new System.Exception(), loggerDAO));
            Boolean expected = false;
            //probar metodo get
            response = await _controller.Get(1);
            //verificar 
            Assert.Equal<Boolean>(expected, response.Success);
        }

        [Fact(DisplayName = "Actualizar Estado")]
        public async void UpdateEstadoControllerTest()
        {
            var dto = new EstadoCreateDTO() { Nombre = "Estado 1", EtiquetaId = 1 };
            var response = new ApplicationResponse<EstadoDTO>();
            // preparacion de los datos
            _servicesMock.Setup(x => x.ActualizarEstadoDAO(It.IsAny<Estado>(), 1)).ReturnsAsync(new EstadoDTO() { id = 1, Nombre = "Estado 1", EtiquetaId = 1 });
            Boolean expected = true;
            //probar metodo put
            response = await _controller.Put(dto, 1);
            //verificar 
            Assert.Equal<Boolean>(expected, response.Success);
        }

        [Fact(DisplayName = "Actualizar Estado con Exception")]
        public async void UpdateEstadoControllerException()
        {
            var dto = new EstadoCreateDTO() { Nombre = "Estado 1", EtiquetaId = 1 };
            var response = new ApplicationResponse<EstadoDTO>();
            // preparacion de los datos
            _servicesMock.Setup(x => x.ActualizarEstadoDAO(It.IsAny<Estado>(), 1)).Throws(new EstadoException("Error al actualizar Estado", new System.Exception(), loggerDAO));
            Boolean expected = false;
            //probar metodo put
            response = await _controller.Put(dto, 1);
            //verificar 
            Assert.Equal<Boolean>(expected, response.Success);
        }

        [Fact(DisplayName = "Eliminar Estado")]
        public async void DeleteEstadoControllerTest()
        {
            var response = new ApplicationResponse<ActionResult>();
            // preparacion de los datos
            _servicesMock.Setup(x => x.EliminarEstadoDAO(It.IsAny<int>())).ReturnsAsync(true);
            Boolean expected = true;
            //probar metodo delete
            response = await _controller.Delete(1);
            //verificar 
            Assert.Equal<Boolean>(expected, response.Success);
        }

        [Fact(DisplayName = "Estado no encontrado para eliminar")]
        public async void DeleteEstadoControllerNotFound()
        {
            var response = new ApplicationResponse<ActionResult>();
            // preparacion de los datos
            _servicesMock.Setup(x => x.EliminarEstadoDAO(It.IsAny<int>())).ReturnsAsync(false);
            Boolean expected = false;
            //probar metodo delete
            response = await _controller.Delete(1);
            //verificar 
            Assert.Equal<Boolean>(expected, response.Success);
        }

        [Fact(DisplayName = "Eliminar Estado con Exception")]
        public async void DeleteEstadoControllerException()
        {
            var response = new ApplicationResponse<ActionResult>();
            // preparacion de los datos
            _servicesMock.Setup(x => x.EliminarEstadoDAO(It.IsAny<int>())).Throws(new EstadoException("Error al eliminar Estado", new System.Exception(), loggerDAO));
            Boolean expected = false;
            //probar metodo delete
            response = await _controller.Delete(1);
            //verificar 
            Assert.Equal<Boolean>(expected, response.Success);
        }

    }
}