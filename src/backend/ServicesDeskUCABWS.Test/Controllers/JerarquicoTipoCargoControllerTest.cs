using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Controllers;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Database;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.Test.Configuraciones;
using static ServicesDeskUCABWS.Reponses.AplicationResponse;
using ServicesDeskUCABWS.Exceptions;

namespace ServicesDeskUCABWS.Test.Controllers
{
    public class JerarquicoTipoCargoControllerTest
    {
        private readonly JerarquicoTipoCargoController _controller;
        private readonly Mock<IModeloJerarquicoTipoCargo> _servicesMock;
        private readonly Mock<IMigrationDbContext> _contexMock;
        private readonly Mock<ILogger<JerarquicoTipoCargoController>> _logger;
        public JerarquicoTipoCargoDTO jerarquicoTest = It.IsAny<JerarquicoTipoCargoDTO>();
        public JerarquicoTCargoCDTO JerarquicoTcDto = It.IsAny<JerarquicoTCargoCDTO>();
        public JerarquicoTipoCargoControllerTest()
        {
            _contexMock = new Mock<IMigrationDbContext>();
            _logger = new Mock<ILogger<JerarquicoTipoCargoController>>();
            _servicesMock = new Mock<IModeloJerarquicoTipoCargo>();
            _controller = new JerarquicoTipoCargoController(_servicesMock.Object, _logger.Object);
            _controller.ControllerContext = new ControllerContext();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.ActionDescriptor = new ControllerActionDescriptor();
        }

        #region  Casos Existosos

        [Fact(DisplayName = "Agrega un Jerarquico Tipo Cargo")]
        public Task AgregarJerarquicoTCargoControllerTest()
        {
            _servicesMock.Setup(j => j.CreateJerarquicoTipoCargoDAO(It.IsAny<ModeloJerarquicoCargos>()))
                        .Returns(jerarquicoTest);


            var result = _controller.AgregarJerarquicoTipoCargo(DtoJTest());

            Assert.IsType<ApplicationResponse<JerarquicoTipoCargoDTO>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Listado Jerarquico Tipo Cargo")]
        public Task ConsultarListadoJerarquicoTipoCargoControllerTest()
        {
            _servicesMock.Setup(j => j.ListadoJerarquicoTipoCargoDAO())
                        .Returns(It.IsAny<List<JerarquicoTCargoCDTO>>());

            var result = _controller.ObtenerListadoJerarquicoTCargo();

            Assert.IsType<ApplicationResponse<List<JerarquicoTCargoCDTO>>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Consultar Jerarquico Tipo Cargo por Id")]
        public Task ConsultarJerarquicoTCargoIdControllerTest()
        {
            _servicesMock.Setup(j => j.ObtenerJerarquicoTipoCargoDAO(It.IsAny<int>()))
                        .Returns(JerarquicoTcDto);

            var id = 1;

            var result = _controller.ObtenerJerarquicoTCargo(id);

            Assert.IsType<ApplicationResponse<JerarquicoTCargoCDTO>>(result);            
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Actualizar Jerarquico Tipo Cargo")]
        public Task ActualizarJerarquicoTCargoControllerTest()
        {
            _servicesMock.Setup(j => j.ActualizarJerarquicoTipoCargoDAO(It.IsAny<ModeloJerarquicoCargos>()))
                        .Returns(jerarquicoTest);

            var result = _controller.ActualizarJerarquicoTCargo(DtoJTest());

            Assert.IsType<ApplicationResponse<JerarquicoTipoCargoDTO>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Eliminar Jerarquico Tipo Cargo")]
        public Task EliminarJerarquicoTCargoControllerTest()
        {
            _servicesMock.Setup(j => j.EliminarJerarquicoTipoCargoDAO(It.IsAny<int>()))
                        .Returns(jerarquicoTest);

            var id = 1;
            var result = _controller.EliminarJerarquicoTCargo(id);

            Assert.IsType<ApplicationResponse<JerarquicoTipoCargoDTO>>(result);
            return Task.CompletedTask;
        }
        #endregion

        #region  Casos Particulares

        [Fact(DisplayName = "Agregar Jerarquico Tipo Cargo con Excepcion")]
        public Task AgregarjerarquicoTCargoExceptionControllerTest()
        {
            _servicesMock.Setup(e => e.CreateJerarquicoTipoCargoDAO(It.IsAny<ModeloJerarquicoCargos>()))
                        .Throws(new Exception());

            var response = _controller.AgregarJerarquicoTipoCargo(It.IsAny<JerarquicoTipoCargoDTO>());                        

            Assert.NotNull(response);
            Assert.False(response.Success);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Consultar Lista jerarquico tipo cargo con excepcion")]
        public Task ConsultarListadoJerarquicoTCargoExceptionControllerTest()
        {
            _servicesMock.Setup(e => e.ListadoJerarquicoTipoCargoDAO())
                        .Throws(new Exception());

            var response = _controller.ObtenerListadoJerarquicoTCargo();

            Assert.NotNull(response);
            Assert.False(response.Success);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Obtener Jerarquico Tipo Cargo por Id con Excepcion")]
        public Task ConsultarJerarquicoTCargoIdExceptionControllerTest()
        {
            _servicesMock.Setup(e =>e.ObtenerJerarquicoTipoCargoDAO(It.IsAny<int>()))
                        .Throws(new Exception());

            var id = -1;
            var response = _controller.ObtenerJerarquicoTCargo(id);

            Assert.NotNull(response);
            Assert.False(response.Success);                        
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Actualizar Jerarquico Tipo Cargo con Excepcion")]
        public Task ActualizarJerarquicoTCargoExceptionControllerTest()
        {
            _servicesMock.Setup(e => e.ActualizarJerarquicoTipoCargoDAO(It.IsAny<ModeloJerarquicoCargos>()))
                        .Throws(new Exception());

            var response = _controller.ActualizarJerarquicoTCargo(jerarquicoTest);

            Assert.NotNull(response);
            Assert.False(response.Success);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Eliminar Jerarquico Tipo Cargo con Excepcion")]
        public Task EliminarJerarquicoTCargoExceptionControllerTest()
        {
            _servicesMock.Setup(e => e.EliminarJerarquicoTipoCargoDAO(It.IsAny<int>()))
                            .Throws(new Exception());

            var id = -1;

            var response = _controller.EliminarJerarquicoTCargo(id);


            Assert.NotNull(response);
            Assert.False(response.Success);
            return Task.CompletedTask;                            
        }

        #endregion

        #region Metodo Privados

        private JerarquicoTipoCargoDTO DtoJTest()
        {
            return new JerarquicoTipoCargoDTO()
            {
                Id = 1,
                orden = 1,
                tipoCargoid = 1,
                modelojerarquicoid = 1
            };
        }
        #endregion
    }
}