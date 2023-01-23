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
    public class GrupoControllerTest : BasePrueba
    {

        private readonly GrupoController _controller;
        private readonly Mock<IGrupoDAO> _servicesMock;
        public GrupoDTO grupoDto = It.IsAny<GrupoDTO>();

        public GrupoCreateDTO grupoCreateDto = It.IsAny<GrupoCreateDTO>();
        public Grupo grupo = It.IsAny<Grupo>();
        public ILogger<GrupoDAO> loggerDAO = new NullLogger<GrupoDAO>();

        private readonly Mock<IMigrationDbContext> _contextMock;

        public GrupoControllerTest()
        {
            _contextMock = new Mock<IMigrationDbContext>();
            var _logger = new NullLogger<GrupoController>();
            var _mapper = ConfigurarAutoMapper();
            _servicesMock = new Mock<IGrupoDAO>();
            _controller = new GrupoController(_logger, _servicesMock.Object, _mapper);
        }

        [Fact(DisplayName = "Agregar un Grupo")]
        public async void CreateGrupoControllerTest()
        {
            var dto = new GrupoCreateDTO() { nombre = "Grupo de Finanzas", departamentoid = 1 };
            var response = new ApplicationResponse<GrupoDTO>();
            // preparacion de los datos
            _servicesMock.Setup(x => x.AgregarGrupoDAO(It.IsAny<Grupo>())).ReturnsAsync(new GrupoDTO() { id = 1, nombre = "Grupo de Finanzas", departamentoid = 1 });
            Boolean expected = true;
            //probar metodo post
            response = await _controller.Post(dto);
            //verificar 
            Assert.Equal<Boolean>(expected, response.Success);
        }

        [Fact(DisplayName = "Agregar Grupo con Exception")]
        public async void CreateGrupoControllerException()
        {
            var dto = new GrupoCreateDTO() { nombre = "Grupo de Finanzas", departamentoid = 1 };
            var response = new ApplicationResponse<GrupoDTO>();
            // preparacion de los datos
            _servicesMock.Setup(x => x.AgregarGrupoDAO(It.IsAny<Grupo>())).Throws(new GrupoException("Error al agregar Grupo", new System.Exception(), loggerDAO));
            Boolean expected = false;
            //probar metodo post
            response = await _controller.Post(dto);
            //verificar 
            Assert.Equal<Boolean>(expected, response.Success);
        }

        [Fact(DisplayName = "Obtener lista de Grupos")]
        public async void GetGruposControllerTest()
        {
            var response = new ApplicationResponse<List<GrupoResponseDTO>>();
            // preparacion de los datos
            _servicesMock.Setup(x => x.ObtenerGruposDAO()).ReturnsAsync(new List<GrupoResponseDTO> { new GrupoResponseDTO() { id = 1, nombre = "Grupo de Finanzas", departamentoid = 1 } });
            Boolean expected = true;
            //probar metodo get
            response = await _controller.Get();
            //verificar 
            Assert.Equal<Boolean>(expected, response.Success);
        }

        [Fact(DisplayName = "Obtener lista de Grupos con Exception")]
        public async void GetGruposControllerException()
        {
            var response = new ApplicationResponse<List<GrupoResponseDTO>>();
            // preparacion de los datos
            _servicesMock.Setup(x => x.ObtenerGruposDAO()).Throws(new GrupoException("Error al obtener Grupos", new System.Exception(), loggerDAO));
            Boolean expected = false;
            //probar metodo get
            response = await _controller.Get();
            //verificar 
            Assert.Equal<Boolean>(expected, response.Success);
        }

        [Fact(DisplayName = "Obtener Grupo por Id")]
        public async void GetGrupoByIdControllerTest()
        {
            var response = new ApplicationResponse<GrupoResponseDTO>();
            // preparacion de los datos
            _servicesMock.Setup(x => x.ObtenerGrupoByIdDAO(It.IsAny<int>())).ReturnsAsync(new GrupoResponseDTO() { id = 1, nombre = "Grupo de Finanzas", departamentoid = 1 });
            Boolean expected = true;
            //probar metodo get
            response = await _controller.Get(1);
            //verificar 
            Assert.Equal<Boolean>(expected, response.Success);
        }

        [Fact(DisplayName = "Obtener Grupo por Id con Exception")]
        public async void GetGrupoByIdControllerException()
        {
            var response = new ApplicationResponse<GrupoResponseDTO>();
            // preparacion de los datos
            _servicesMock.Setup(x => x.ObtenerGrupoByIdDAO(It.IsAny<int>())).Throws(new GrupoException("Error al obtener Grupo", new System.Exception(), loggerDAO));
            Boolean expected = false;
            //probar metodo get
            response = await _controller.Get(1);
            //verificar 
            Assert.Equal<Boolean>(expected, response.Success);
        }

        [Fact(DisplayName = "Actualizar Grupo")]
        public async void UpdateGrupoControllerTest()
        {
            var dto = new GrupoCreateDTO() { nombre = "Grupo de Finanzas", departamentoid = 1 };
            var response = new ApplicationResponse<GrupoDTO>();
            // preparacion de los datos
            _servicesMock.Setup(x => x.ActualizarGrupoDAO(It.IsAny<Grupo>(), 1)).ReturnsAsync(new GrupoDTO() { id = 1, nombre = "Grupo de Finanzas", departamentoid= 1 });
            Boolean expected = true;
            //probar metodo Actualizar Grupo
            response = await _controller.ActualizarGrupo(dto, 1);
            //verificar 
            Assert.Equal<Boolean>(expected, response.Success);
        }

        [Fact(DisplayName = "Actualizar Grupo con Exception")]
        public async void UpdateGrupoControllerException()
        {
            var dto = new GrupoCreateDTO() { nombre = "Grupo de Finanzas", departamentoid = 1 };
            var response = new ApplicationResponse<GrupoDTO>();
            // preparacion de los datos
            _servicesMock.Setup(x => x.ActualizarGrupoDAO(It.IsAny<Grupo>(), 1)).Throws(new GrupoException("Error al actualizar Grupo", new System.Exception(), loggerDAO));
            Boolean expected = false;
            //probar metodo put
            response = await _controller.ActualizarGrupo(dto, 1);
            //verificar 
            Assert.Equal<Boolean>(expected, response.Success);
        }

        [Fact(DisplayName = "Eliminar Grupo")]
        public async void DeleteGrupoControllerTest()
        {
            var response = new ApplicationResponse<ActionResult>();
            // preparacion de los datos
            _servicesMock.Setup(x => x.EliminarGrupoDAO(It.IsAny<int>())).ReturnsAsync(true);
            Boolean expected = true;
            //probar metodo delete
            response = await _controller.EliminarGrupo(1);
            //verificar 
            Assert.Equal<Boolean>(expected, response.Success);
        }

        [Fact(DisplayName = "Grupo no encontrado para eliminar")]
        public async void DeleteGrupoControllerNotFound()
        {
            var response = new ApplicationResponse<ActionResult>();
            // preparacion de los datos
            _servicesMock.Setup(x => x.EliminarGrupoDAO(It.IsAny<int>())).ReturnsAsync(false);
            Boolean expected = false;
            //probar metodo delete
            response = await _controller.EliminarGrupo(1);
            //verificar 
            Assert.Equal<Boolean>(expected, response.Success);
        }

        [Fact(DisplayName = "Eliminar Grupo con Exception")]
        public async void DeleteGrupoControllerException()
        {
            var response = new ApplicationResponse<ActionResult>();
            // preparacion de los datos
            _servicesMock.Setup(x => x.EliminarGrupoDAO(It.IsAny<int>())).Throws(new GrupoException("Error al eliminar Grupo", new System.Exception(), loggerDAO));
            Boolean expected = false;
            //probar metodo EliminarGrupo
            response = await _controller.EliminarGrupo(1);
            //verificar 
            Assert.Equal<Boolean>(expected, response.Success);
        }

    }
}
