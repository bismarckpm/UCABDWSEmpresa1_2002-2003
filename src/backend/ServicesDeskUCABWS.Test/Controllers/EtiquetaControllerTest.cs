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


namespace ServicesDeskUCABWS.Test.Controllers
{
    public class EtiquetaControllerTest : BasePrueba
    {

        private readonly EtiquetaController _controller;
        private readonly Mock<IEtiquetaDAO> _servicesMock;
        public EtiquetaDTO etiquetaDto = It.IsAny<EtiquetaDTO>();

        public EtiquetaDTOCreate etiquetaDtoCreate = It.IsAny<EtiquetaDTOCreate>();
        public Etiqueta etiqueta = It.IsAny<Etiqueta>();

        private readonly Mock<IMigrationDbContext> _contextMock;

        public EtiquetaControllerTest()
        {
            _contextMock = new Mock<IMigrationDbContext>();
            var _logger = new NullLogger<EtiquetaController>();
            var _mapper = ConfigurarAutoMapper();
            _servicesMock = new Mock<IEtiquetaDAO>();
            _controller = new EtiquetaController(_logger, _servicesMock.Object, _mapper);
            _controller.ControllerContext = new ControllerContext();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.ActionDescriptor = new ControllerActionDescriptor();
        }

        [Fact(DisplayName = "Agregar una Etiqueta")]
        public async void CreateEtiquetaControllerTest()
        {
            var dto = new EtiquetaDTOCreate() { Nombre = "Importante", Descripcion = "Etiqueta alta" };
            // preparacion de los datos
            _servicesMock.Setup(x => x.AgregarEtiquetaDAO(new Etiqueta())).ReturnsAsync(new EtiquetaDTO() { id = 1, Nombre = "Importante", Descripcion = "Etiqueta alta" });
            //probar metodo post
            var result = await _controller.Post(dto);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact(DisplayName = "Obtener lista de Etiquetas")]
        public async void GetEtiquetasControllerTest()
        {
            // preparacion de los datos
            _servicesMock.Setup(x => x.ConsultarEtiquetasDAO()).ReturnsAsync(new List<Etiqueta> { new Etiqueta() { id = 1, nombre = "Importante", descripcion = "Etiqueta alta", estados = null } });
            //probar metodo get
            var result = await _controller.Get();
            // validar statusCode

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact(DisplayName = "Id menor a 0 Obtener Etiqueta")]
        public async void GetIdMenor0EtiquetaControllerTest()
        {
            // preparacion de los datos
            _servicesMock.Setup(x => x.ObtenerEtiquetaDAO(0)).ReturnsAsync(new Etiqueta() { id = 0, nombre = "Importante", descripcion = "Etiqueta alta", estados = null });
            //probar metodo get
            var result = await _controller.Get(0);
            // validar statusCode

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact(DisplayName = "Obtener Etiqueta")]
        public async void GetEtiquetaControllerTest()
        {
            // preparacion de los datos
            _servicesMock.Setup(x => x.ObtenerEtiquetaDAO(1)).ReturnsAsync(new Etiqueta() { id = 1, nombre = "Importante", descripcion = "Etiqueta alta", estados = null });
            //probar metodo get
            var result = await _controller.Get(1);
            // validar statusCode

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact(DisplayName = "No existe Etiqueta")]
        public async void GetNoExisteEtiquetaControllerTest()
        {
            // preparacion de los datos
            _servicesMock.Setup(x => x.ObtenerEtiquetaDAO(5)).ReturnsAsync(new Etiqueta());
            //probar metodo get
            var result = await _controller.Get(5);
            // validar statusCode

            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact(DisplayName = "Id menor a 0 Actualizar Etiqueta")]
        public async void PutIdMenor0EtiquetaControllerTest()
        {
            // preparacion de los datos
            //probar metodo put
            var result = await _controller.ActualizarEtiqueta(etiquetaDtoCreate, 0);
            // validar statusCode

            Assert.IsType<BadRequestObjectResult>(result);
        }
        // REVISAR
        // [Fact(DisplayName = "Actualizar Etiqueta")]
        // public async void PutEtiquetaControllerTest()
        // {
        //     // preparacion de los datos
        //     _servicesMock.Setup(x => x.ActualizarEtiquetaDAO(new Etiqueta(), 1)).ReturnsAsync(new OkObjectResult(new Etiqueta() { id = 1, nombre = "Importante", descripcion = "Etiqueta alta" }));
        //     //probar metodo put
        //     var result = await _controller.ActualizarEtiqueta(etiquetaDtoCreate, 1);
        //     // validar statusCode

        //     Assert.IsType<OkObjectResult>(result);
        // }

        [Fact(DisplayName = "No existe Etiqueta Actualizar")]
        public async void PutNoExisteEtiquetaControllerTest()
        {
            // preparacion de los datos
            _servicesMock.Setup(x => x.ActualizarEtiquetaDAO(etiqueta, 5)).ReturnsAsync(new Etiqueta());
            //probar metodo put
            var result = await _controller.ActualizarEtiqueta(etiquetaDtoCreate, 5);
            // validar statusCode

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact(DisplayName = "Id menor a 0 Eliminar Etiqueta")]
        public async void DeleteIdMenor0EtiquetaControllerTest()
        {
            // preparacion de los datos
            //probar metodo delete
            var result = await _controller.EliminarEtiqueta(0);
            // validar statusCode

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact(DisplayName = "Eliminar Etiqueta")]
        public async void DeleteEtiquetaControllerTest()
        {
            // preparacion de los datos
            _servicesMock.Setup(x => x.EliminarEtiquetaDAO(1)).ReturnsAsync(new OkResult());
            //probar metodo delete
            var result = await _controller.EliminarEtiqueta(1);
            // validar statusCode

            Assert.IsType<OkResult>(result);
        }

        [Fact(DisplayName = "No existe Etiqueta Eliminar")]
        public async void DeleteNoExisteEtiquetaControllerTest()
        {
            // preparacion de los datos
            _servicesMock.Setup(x => x.EliminarEtiquetaDAO(5)).ReturnsAsync(new NotFoundResult());
            //probar metodo delete
            var result = await _controller.EliminarEtiqueta(5);
            // validar statusCode

            Assert.IsType<NotFoundResult>(result);
        }


    }
}