using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Controllers;
using ServicesDeskUCABWS.Exceptions;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Database;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.Test.Configuraciones;


namespace ServicesDeskUCABWS.Test.Controllers
{
    public class EstadoControllerTest : BasePrueba
    {

        private readonly EstadoController _controller;
        private readonly Mock<IEstadoDAO> _servicesMock;
        public EstadoDTO estadoDto = It.IsAny<EstadoDTO>();

        public EstadoCreateDTO estadoCreateDto = It.IsAny<EstadoCreateDTO>();
        public Estado estado = It.IsAny<Estado>();

        private readonly Mock<IMigrationDbContext> _contextMock;

        public EstadoControllerTest()
        {
            _contextMock = new Mock<IMigrationDbContext>();
            var _logger = new NullLogger<EstadoController>();
            var _mapper = ConfigurarAutoMapper();
            _servicesMock = new Mock<IEstadoDAO>();
            _controller = new EstadoController(_logger, _servicesMock.Object, _mapper);
            _controller.ControllerContext = new ControllerContext();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.ActionDescriptor = new ControllerActionDescriptor();
        }

        [Fact(DisplayName = "Agregar un Estado")]
        public async void CreateEstadoControllerTest()
        {
            // var dto = new EstadoCreateDTO() { Nombre = "Estado 1", EtiquetaId = 1 };
            // // preparacion de los datos
            // _servicesMock.Setup(x => x.AgregarEstadoDAO(It.IsAny<Estado>())).ReturnsAsync(new EstadoDTO() { id = 1, Nombre = "Estado 1", EtiquetaId = 1 });
            // //probar metodo post
            // var result = await _controller.Post(dto);
            // //verificar 
            // Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact(DisplayName = "Agregar Estado con Etiqueta inexistente")]
        public async void CreateEstadoControllerBadRequest()
        {
            // var dto = new EstadoCreateDTO() { Nombre = "Estado 1", EtiquetaId = 1 };
            // // preparacion de los datos
            // // return not found object result
            // _servicesMock.Setup(x => x.AgregarEstadoDAO(It.IsAny<Estado>())).ReturnsAsync(new NotFoundObjectResult("Etiqueta no encontrada"));
            // //probar metodo post - catch EstadoException
            // var result = await _controller.Post(dto);
            // var estadoResult = result.Result;

            // //verificar
            // Assert.IsType<NotFoundObjectResult>(estadoResult);

        }



        [Fact(DisplayName = "Obtener lista de Estados")]
        public async void GetEstadosControllerTest()
        {
            // preparacion de los datos
         //   _servicesMock.Setup(x => x.GetEstadosDAO()).ReturnsAsync(new List<EstadoDTO> { new EstadoDTO() { id = 1, Nombre = "Estado 1", EtiquetaId = 1 } });
            //probar metodo get
          //  var result = await _controller.Get();
          //  var listEstados = result.Result;
            // validar cantidad de elementos
         //   Assert.IsType<OkObjectResult>(listEstados);
        }

        [Fact(DisplayName = "Id menor a 0 Obtener Estado")]
        public async void GetIdMenor0EstadoControllerTest()
        {
            // preparacion de los datos
        //    _servicesMock.Setup(x => x.GetEstadoDAO(0)).ReturnsAsync(new EstadoDTO());
            //probar metodo get
       //     var result = await _controller.Get(0);
            // validar statusCode

       //     Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact(DisplayName = "Obtener Estado")]
        public async void GetEstadoControllerTest()
        {
            // preparacion de los datos
         //   _servicesMock.Setup(x => x.GetEstadoDAO(1)).ReturnsAsync(new EstadoDTO() { id = 1, Nombre = "Estado 1", EtiquetaId = 1 });
            //probar metodo get
        //    var result = await _controller.Get(1);
            // validar statusCode

        //    Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact(DisplayName = "No existe Estado")]
        public async void GetNoExisteEstadoControllerTest()
        {
            // preparacion de los datos
           // _servicesMock.Setup(x => x.GetEstadoDAO(It.IsAny<int>())).ReturnsAsync(new NotFoundObjectResult("Estado no encontrado"));
            //probar metodo get
          //  var result = await _controller.Get(5);
            // validar statusCode

        //    Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact(DisplayName = "Actualizar Estado")]
        public async void PutEstadoControllerTest()
        {
            // preparacion de los datos
         //   _servicesMock.Setup(x => x.ActualizarEstadoDAO(It.IsAny<Estado>(), 1)).ReturnsAsync(new OkResult());
            //probar metodo put
          //  var result = await _controller.Put(new EstadoCreateDTO() { }, 1);
            // validar statusCode

       //     Assert.IsType<OkResult>(result);
        }

        [Fact(DisplayName = "Id menor a 0 Actualizar Estado")]
        public async void PutIdMenor0EstadoControllerTest()
        {
            // preparacion de los datos
            //probar metodo put
       //     var result = await _controller.Put(estadoCreateDto, 0);
            // validar statusCode

         //   Assert.IsType<BadRequestObjectResult>(result);
        }


        [Fact(DisplayName = "Id menor a 0 Eliminar Estado")]
        public async void DeleteIdMenor0EstadoControllerTest()
        {
            // preparacion de los datos
            //probar metodo delete
           // var result =  _controller.StatusCode;
            // validar statusCode

         //   Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact(DisplayName = "Eliminar Estado")]
        public async void DeleteEstadoControllerTest()
        {
            // preparacion de los datos
          //  _servicesMock.Setup(x => x.EliminarEstadoDAO(1)).ReturnsAsync(new OkResult());
            //probar metodo delete
           // var result = await _controller.Delete(1);
            // validar statusCode

           // Assert.IsType<OkResult>(result);
        }

        // [Fact(DisplayName = "No existe Estado Eliminar")]
        // public async void DeleteNoExisteEstadoControllerTest()
        // {
        //     // preparacion de los datos
        //     _servicesMock.Setup(x => x.EliminarEstadoDAO(5)).ReturnsAsync(new NotFoundResult());
        //     //probar metodo delete
        //     var result = await _controller.EliminarEstado(5);
        //     // validar statusCode

        //     Assert.IsType<NotFoundResult>(result);
        // }


    }
}