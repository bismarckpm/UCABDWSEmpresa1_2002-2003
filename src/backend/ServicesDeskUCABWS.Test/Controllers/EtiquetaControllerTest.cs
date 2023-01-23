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
    public class EtiquetaControllerTest : BasePrueba
    {

        private readonly EtiquetaController _controller;
        private readonly Mock<IEtiquetaDAO> _servicesMock;
        public EtiquetaDTO etiquetaDto = It.IsAny<EtiquetaDTO>();

        public EtiquetaDTOCreate etiquetaDtoCreate = It.IsAny<EtiquetaDTOCreate>();
        public Etiqueta etiqueta = It.IsAny<Etiqueta>();

        public ILogger<EtiquetaDAO> loggerDAO = new NullLogger<EtiquetaDAO>();

        private readonly Mock<IMigrationDbContext> _contextMock;

        public EtiquetaControllerTest()
        {
            _contextMock = new Mock<IMigrationDbContext>();
            var _logger = new NullLogger<EtiquetaController>();
            var _mapper = ConfigurarAutoMapper();
            _servicesMock = new Mock<IEtiquetaDAO>();
            _controller = new EtiquetaController(_logger, _servicesMock.Object, _mapper);
        }

        [Fact(DisplayName = "Agregar una Etiqueta")]
        public async void CreateEtiquetaControllerTest()
        {
            var dto = new EtiquetaDTOCreate() { Nombre = "Importante", Descripcion = "Etiqueta alta" };
            // preparacion de los datos
            _servicesMock.Setup(x => x.AgregarEtiquetaDAO(new Etiqueta())).ReturnsAsync(new EtiquetaDTO() { id = 1, Nombre = "Importante", Descripcion = "Etiqueta alta" });
            var response = new ApplicationResponse<EtiquetaDTO>();
            Boolean expected = true;
            //probar metodo post
            response = await _controller.Post(dto);


            Assert.Equal<Boolean>(expected, response.Success);
        }


        [Fact(DisplayName = "Excepcion al agregar una Etiqueta")]
        public async void ExcepcionAgregarEtiqueta()
        {
            var dto = new EtiquetaDTOCreate() { Nombre = "Importante", Descripcion = "Etiqueta alta" };
            // preparacion de los datos
            _servicesMock.Setup(x => x.AgregarEtiquetaDAO(It.IsAny<Etiqueta>())).Throws(new EtiquetaException("Error al agregar Etiqueta", new System.Exception(), loggerDAO));
            var response = new ApplicationResponse<EtiquetaDTO>();
            Boolean expected = false;
            //probar metodo post
            response = await _controller.Post(dto);


            Assert.Equal<Boolean>(expected, response.Success);
        }

        [Fact(DisplayName = "Obtener lista de Etiquetas")]
        public async void GetEtiquetasControllerTest()
        {
            // preparacion de los datos
            _servicesMock.Setup(x => x.ConsultarEtiquetasDAO()).ReturnsAsync(new List<Etiqueta> { new Etiqueta() { id = 1, nombre = "Importante", descripcion = "Etiqueta alta", estados = null } });
            var response = new ApplicationResponse<List<EtiquetaDTO>>();
            Boolean expected = true;
            //probar metodo get
            response = await _controller.Get();
            // validar statusCode

            Assert.Equal<Boolean>(expected, response.Success);
        }

        [Fact(DisplayName = "Excepcion al obtener lista de Etiquetas")]
        public async void ExcepcionGetEtiquetasControllerTest()
        {
            // preparacion de los datos
            _servicesMock.Setup(x => x.ConsultarEtiquetasDAO()).Throws(new EtiquetaException("Error al obtener Etiquetas", new System.Exception(), loggerDAO));
            var response = new ApplicationResponse<List<EtiquetaDTO>>();
            Boolean expected = false;
            //probar metodo get
            response = await _controller.Get();
            // validar statusCode

            Assert.Equal<Boolean>(expected, response.Success);
        }

        [Fact(DisplayName = "Obtener Etiqueta")]
        public async void GetEtiquetaControllerTest()
        {
            // preparacion de los datos
            _servicesMock.Setup(x => x.ObtenerEtiquetaDAO(1)).ReturnsAsync(new Etiqueta() { id = 1, nombre = "Importante", descripcion = "Etiqueta alta", estados = null });
            var response = new ApplicationResponse<EtiquetaDTO>();
            Boolean expected = true;
            //probar metodo get
            response = await _controller.Get(1);
            // validar statusCode

            Assert.Equal<Boolean>(expected, response.Success);
        }

        [Fact(DisplayName = "No Obtener Etiqueta")]
        public async void NoObtenerEtiquetaControllerTest()
        {
            // preparacion de los datos
            _servicesMock.Setup(x => x.ObtenerEtiquetaDAO(1)).ReturnsAsync(new Etiqueta());
            var response = new ApplicationResponse<EtiquetaDTO>();
            Boolean expected = false;
            //probar metodo get
            response = await _controller.Get(1);
            // validar statusCode

            Assert.Equal<Boolean>(expected, response.Success);
        }

        [Fact(DisplayName = "Excepcion al obtener Etiqueta")]
        public async void ExcepcionGetEtiquetaControllerTest()
        {
            // preparacion de los datos
            _servicesMock.Setup(x => x.ObtenerEtiquetaDAO(1)).Throws(new EtiquetaException("Error al obtener Etiqueta", new System.Exception(), loggerDAO));
            var response = new ApplicationResponse<EtiquetaDTO>();
            Boolean expected = false;
            //probar metodo get
            response = await _controller.Get(1);
            // validar statusCode

            Assert.Equal<Boolean>(expected, response.Success);
        }

        [Fact(DisplayName = "Actualizar Etiqueta")]
        public async void UpdateEtiquetaControllerTest()
        {
            var dto = new EtiquetaDTOCreate() { Nombre = "Importante", Descripcion = "Etiqueta alta" };
            // preparacion de los datos
            _servicesMock.Setup(x => x.ActualizarEtiquetaDAO(It.IsAny<Etiqueta>(), 1)).ReturnsAsync(new Etiqueta() { id = 1, nombre = "Importante", descripcion = "Etiqueta alta" });
            var response = new ApplicationResponse<EtiquetaDTO>();
            Boolean expected = true;
            //probar metodo put
            response = await _controller.ActualizarEtiqueta(dto, 1);
            // validar statusCode

            Assert.Equal<Boolean>(expected, response.Success);
        }

        [Fact(DisplayName = "Etiqueta no encontrada para actualizar")]
        public async void UpdateEtiquetaNoEncontradaControllerTest()
        {
            var dto = new EtiquetaDTOCreate() { Nombre = "Importante", Descripcion = "Etiqueta alta" };
            // preparacion de los datos
            _servicesMock.Setup(x => x.ActualizarEtiquetaDAO(It.IsAny<Etiqueta>(), 1)).ReturnsAsync(new Etiqueta());
            var response = new ApplicationResponse<EtiquetaDTO>();
            Boolean expected = false;
            //probar metodo put
            response = await _controller.ActualizarEtiqueta(dto, 1);
            // validar statusCode

            Assert.Equal<Boolean>(expected, response.Success);
        }


        [Fact(DisplayName = "Excepcion al actualizar Etiqueta")]
        public async void ExcepcionUpdateEtiquetaControllerTest()
        {
            var dto = new EtiquetaDTOCreate() { Nombre = "Importante", Descripcion = "Etiqueta alta" };
            // preparacion de los datos
            _servicesMock.Setup(x => x.ActualizarEtiquetaDAO(It.IsAny<Etiqueta>(), 1)).Throws(new EtiquetaException("Error al actualizar Etiqueta", new System.Exception(), loggerDAO));
            var response = new ApplicationResponse<EtiquetaDTO>();
            Boolean expected = false;
            //probar metodo put
            response = await _controller.ActualizarEtiqueta(dto, 1);
            // validar statusCode

            Assert.Equal<Boolean>(expected, response.Success);
        }

        [Fact(DisplayName = "Eliminar Etiqueta")]
        public async void DeleteEtiquetaControllerTest()
        {
            // preparacion de los datos
            _servicesMock.Setup(x => x.EliminarEtiquetaDAO(1)).ReturnsAsync(true);
            var response = new ApplicationResponse<ActionResult>();
            Boolean expected = true;
            //probar metodo delete
            response = await _controller.EliminarEtiqueta(1);
            // validar statusCode

            Assert.Equal<Boolean>(expected, response.Success);
        }

        [Fact(DisplayName = "Etiqueta no encontrada")]
        public async void EtiquetaNoEncontradaControllerTest()
        {
            // preparacion de los datos
            _servicesMock.Setup(x => x.EliminarEtiquetaDAO(1)).ReturnsAsync(false);
            var response = new ApplicationResponse<ActionResult>();
            Boolean expected = false;
            //probar metodo delete
            response = await _controller.EliminarEtiqueta(1);
            // validar statusCode

            Assert.Equal<Boolean>(expected, response.Success);
        }

        [Fact(DisplayName = "Excepcion al eliminar Etiqueta")]
        public async void ExcepcionDeleteEtiquetaControllerTest()
        {
            // preparacion de los datos
            _servicesMock.Setup(x => x.EliminarEtiquetaDAO(1)).Throws(new EtiquetaException("Error al eliminar Etiqueta", new System.Exception(), loggerDAO));
            var response = new ApplicationResponse<ActionResult>();
            Boolean expected = false;
            //probar metodo delete
            response = await _controller.EliminarEtiqueta(1);
            // validar statusCode

            Assert.Equal<Boolean>(expected, response.Success);
        }
    }
}