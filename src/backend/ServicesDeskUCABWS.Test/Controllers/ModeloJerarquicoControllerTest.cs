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
    public class ModeloJerarquicoControllerTest : BasePrueba
    {
        private readonly ModeloJerarquicoController _controller;
        private readonly Mock<IModeloJerarquicoDAO> _servicesMock;
        public ModeloJerarquicoDTO modeloJerarquicoDTO = It.IsAny<ModeloJerarquicoDTO>();
        // public ModeloJerarquicoCreateDTO modeloJerarquicoCreateDTO = It.IsAny<ModeloJerarquicoCreateDTO>();
        public ModeloJerarquico modeloJerarquico = It.IsAny<ModeloJerarquico>();
        private readonly Mock<IMigrationDbContext> _contextMock;
        private readonly Mock<ILogger<ModeloJerarquicoController>> _logger;
        public ModeloJerarquicoControllerTest()
        {
            _contextMock = new Mock<IMigrationDbContext>();
            _logger = new Mock<ILogger<ModeloJerarquicoController>>();
            var _mapper = ConfigurarAutoMapper();
            _servicesMock = new Mock<IModeloJerarquicoDAO>();
            _controller = new ModeloJerarquicoController(_logger.Object, _servicesMock.Object, _mapper);
            _controller.ControllerContext = new ControllerContext();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.ActionDescriptor = new ControllerActionDescriptor();
        }

        #region  Casos Existosos

        /// <summary>
        /// Agregar un Modelo Jerarquico
        /// </summary>
        [Fact(DisplayName = "Agrega un Modelo Jerarquico")]
        public Task CreateModeloJerarquicoControllerTest()
        {
            _servicesMock.Setup(m => m.AgregarModeloJerarquicoDAO(modeloJerarquico))
            .Returns(modeloJerarquicoDTO);

            var result = _controller.Post(ModelDTO());

            Assert.IsType<ApplicationResponse<ModeloJerarquicoDTO>>(result);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Agregar un Modelo Jerarquico
        /// </summary>
        [Fact(DisplayName = "Consulta lista de modelos jerarquicos")]
        public Task GetModeloJerarquicoControllerTest()
        {
            _servicesMock.Setup(j => j.ConsultarModeloJerarquicosDAO())
            .Returns(It.IsAny<List<ModeloJerarquicoDTO>>());

            var result = _controller.GetModeloJerarquico();

            Assert.IsType<ApplicationResponse<List<ModeloJerarquicoDTO>>>(result);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Agregar un Modelo Jerarquico
        /// </summary>
        [Fact(DisplayName = "Consulta de modelo jerarquico por ID")]
        public Task ConsultaModeloJerarquicoPorIdControllerTest()
        {
            _servicesMock.Setup(j => j.ObtenerModeloJerarquicoDAO(It.IsAny<int>()))
            .Returns(ModelDTO());

            var result = _controller.ConsultaMJerarquicoPorId(It.IsAny<int>());

            Assert.IsType<ApplicationResponse<ModeloJerarquicoDTO>>(result);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Prueba el metodo actualizar del controlador ModeloJerarquico
        /// </summary>
        [Fact(DisplayName = "Actualiza un Modelo Jerarquico")]
        public Task ActualizarModeloJerarquicoControllerTest()
        {
            _servicesMock.Setup(m =>m.ActualizarModeloJerarquicoDAO(modeloJerarquico))
            .Returns(modeloJerarquicoDTO);

            var result = _controller.ActualizarModeloJerarquico(ModelDTO());

            Assert.IsType<ApplicationResponse<ModeloJerarquicoDTO>>(result);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Prueba el metodo eliminar del controlador ModeloJerarquico
        /// </summary>
        [Fact(DisplayName = "Elimina un modelo Jerarquico")]
        public Task EliminarModeloJerarquicoControllerTest()
        {
            _servicesMock.Setup(j => j.EliminarModeloJerarquicoDAO(It.IsAny<int>()))
            .Returns(modeloJerarquicoDTO);

            var result = _controller.EliminarModeloJerarquico(It.IsAny<int>());

            Assert.IsType<ApplicationResponse<ModeloJerarquicoDTO>>(result);
            return Task.CompletedTask;
        }
    #endregion

    #region Caso Particular
//Por corregir
        [Fact(DisplayName = "Agregar modelo jerarquico con Excepcion")]
        public Task CreateModeloJerarquicoControllerExceptionTest()
        {
            _servicesMock.Setup(e => e.AgregarModeloJerarquicoDAO(modeloJerarquico))
                .Throws(new ServicesDeskUcabWsException("", new NullReferenceException()));

                var response = _controller.Post(ErrorModelDTO());

            Assert.NotNull(response);
            Assert.False(response.Success);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Consulta listado de modelo jerarquicon con excepcion")]
        public Task ConsultarModeloJerarquicoControllerExceptionTest()
        {
            _servicesMock.Setup(e => e.ConsultarModeloJerarquicosDAO())
            .Throws(new ServicesDeskUcabWsException("",new ArgumentOutOfRangeException()));

            var response = _controller.GetModeloJerarquico();

            Assert.NotNull(response);
            Assert.False(response.Success);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Consultar Modelo Jerarquico por Id con excepcion")]
        public Task ConsultarModeloJerarquicoIdControllerExceptionTest()
        {
            _servicesMock.Setup(e => e.ObtenerModeloJerarquicoDAO(It.IsAny<int>()))
                        .Throws(new ServicesDeskUcabWsException("", new Exception()));

            var buscarModelo = -1;
            var response = _controller.ConsultaMJerarquicoPorId(buscarModelo);

            Assert.NotNull(response);
            Assert.False(response.Success);
            return Task.CompletedTask;
        }

//por corregir
        [Fact(DisplayName = "Actualizar modelo jerarquico con excepcion")]
        public Task ActualizarModeloJerarquicoControllerExceptionTest()
        {
            _servicesMock.Setup(e => e.ActualizarModeloJerarquicoDAO(modeloJerarquico))
                        .Throws(new ServicesDeskUcabWsException("", new Exception()));

            var response = _controller.ActualizarModeloJerarquico(ErrorModelDTO());

            Assert.NotNull(response);
            Assert.False(response.Success);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Eliminar un modelo jerarquico con excepcion")]
        public Task EliminarModeloJerarquicoExceptionControllerTest()
        {
            _servicesMock.Setup(e => e.EliminarModeloJerarquicoDAO(It.IsAny<int>()))
            .Throws(new ServicesDeskUcabWsException("", new Exception()));

            var response = _controller.EliminarModeloJerarquico(-1);

            Assert.NotNull(response);
            Assert.False(response.Success);
            return Task.CompletedTask;
        }

    #endregion


    #region  Metodos Privados
        private ModeloJerarquico ModelModeloJerarquicoTest()
        {
            return new ModeloJerarquico()
            {
                id = 1,
                        nombre = "Prueba Modelo",
                        categoriaid = 1,
                        categoria = new Categoria()
                        {
                            id = 1,
                            nombre = "Guardado"
                        },
                        Jeraruia = new List<ModeloJerarquicoCargos>()
            };
        }

        private ModeloJerarquicoDTO ModelDTO()
        {
            return new ModeloJerarquicoDTO()
            {
            id = 1,
            Nombre = "Prueba Modelo",
            CategoriaId = 1,
            orden = new List<JerarquicoTipoCargoDTO>()
            };
        }

private ModeloJerarquicoDTO  ErrorModelDTO()
{
    var lista = new List<JerarquicoTipoCargoDTO>();
    return new ModeloJerarquicoDTO()
    {
        orden = lista
    };
}
        
        #endregion

    }

}

