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
    public class PlantillaControllerTest : BasePrueba
    {

        private readonly PlantillaController _controller;
        private readonly Mock<IPlantillaDAO> _servicesMock;
        public PlantillaDTO PlantillaDto = It.IsAny<PlantillaDTO>();

        public PlantillaDTOCreate PlantillaDtoCreate = It.IsAny<PlantillaDTOCreate>();
        public Plantilla plantilla = It.IsAny<Plantilla>();

        public ILogger<PlantillaDAO> loggerDAO = new NullLogger<PlantillaDAO>();

        private readonly Mock<IMigrationDbContext> _contextMock;

        public PlantillaControllerTest()
        {
            _contextMock = new Mock<IMigrationDbContext>();
            var _logger = new NullLogger<PlantillaController>();
            var _mapper = ConfigurarAutoMapper();
            _servicesMock = new Mock<IPlantillaDAO>();
            _controller = new PlantillaController(_logger, _servicesMock.Object, _mapper);
        }

        [Fact(DisplayName = "Agregar una Plantilla")]
        public async void CreatePlantillaControllerTest()
        {
            var dto = new PlantillaDTOCreate() { Titulo = "Plantilla 1", Cuerpo = "Descripcion de la plantilla 1", EstadoId = 1 };
            // preparacion de los datos
            _servicesMock.Setup(x => x.AgregarPlantillaDAO(It.IsAny<Plantilla>())).ReturnsAsync(new PlantillaDTO() { id = 1, Titulo = "Plantilla 1", Cuerpo = "Descripcion de la plantilla 1", EstadoId = 1 });
            var response = new ApplicationResponse<PlantillaDTO>();
            Boolean expected = true;
            //probar metodo post
            response = await _controller.Post(dto);

            Assert.Equal<Boolean>(expected, response.Success);
        }

        [Fact(DisplayName = "Agregar una Plantilla con excepcion")]
        public async void CreatePlantillaControllerExceptionTest()
        {
            var dto = new PlantillaDTOCreate() { Titulo = "Plantilla 1", Cuerpo = "Descripcion de la plantilla 1", EstadoId = 1 };
            // preparacion de los datos
            _servicesMock.Setup(x => x.AgregarPlantillaDAO(It.IsAny<Plantilla>())).ThrowsAsync(new PlantillaException("Error al agregar la plantilla", new System.Exception(), loggerDAO));
            var response = new ApplicationResponse<PlantillaDTO>();
            Boolean expected = false;
            //probar metodo post
            response = await _controller.Post(dto);

            Assert.Equal<Boolean>(expected, response.Success);
        }

        [Fact(DisplayName = "Obtener lista de Plantillas")]
        public async void GetPlantillasControllerTest()
        {
            // preparacion de los datos
            _servicesMock.Setup(x => x.ObtenerPlantillasDAO()).ReturnsAsync(new List<PlantillaDTO> { new PlantillaDTO() { id = 1, Titulo = "Plantilla 1", Cuerpo = "Descripcion de la plantilla 1", EstadoId = 1 } });
            var response = new ApplicationResponse<List<PlantillaDTO>>();
            Boolean expected = true;
            //probar metodo get
            response = await _controller.Get();

            Assert.Equal<Boolean>(expected, response.Success);
        }

        [Fact(DisplayName = "Obtener lista de Plantillas con excepcion")]
        public async void GetPlantillasControllerExceptionTest()
        {
            // preparacion de los datos
            _servicesMock.Setup(x => x.ObtenerPlantillasDAO()).ThrowsAsync(new PlantillaException("Error al obtener las plantillas", new System.Exception(), loggerDAO));
            var response = new ApplicationResponse<List<PlantillaDTO>>();
            Boolean expected = false;
            //probar metodo get
            response = await _controller.Get();

            Assert.Equal<Boolean>(expected, response.Success);
        }

        [Fact(DisplayName = "Obtener Plantilla por Id")]
        public async void GetPlantillaByIdControllerTest()
        {
            // preparacion de los datos
            _servicesMock.Setup(x => x.ObtenerPlantillaDAO(It.IsAny<int>())).ReturnsAsync(new PlantillaDTO() { id = 1, Titulo = "Plantilla 1", Cuerpo = "Descripcion de la plantilla 1", EstadoId = 1 });
            var response = new ApplicationResponse<PlantillaDTO>();
            Boolean expected = true;
            //probar metodo get
            response = await _controller.Get(1);

            Assert.Equal<Boolean>(expected, response.Success);
        }

        [Fact(DisplayName = "Obtener Plantilla por Id con excepcion")]
        public async void GetPlantillaByIdControllerExceptionTest()
        {
            // preparacion de los datos
            _servicesMock.Setup(x => x.ObtenerPlantillaDAO(It.IsAny<int>())).ThrowsAsync(new PlantillaException("Error al obtener la plantilla", new System.Exception(), loggerDAO));
            var response = new ApplicationResponse<PlantillaDTO>();
            Boolean expected = false;
            //probar metodo get
            response = await _controller.Get(1);

            Assert.Equal<Boolean>(expected, response.Success);
        }

        [Fact(DisplayName = "Actualizar una Plantilla")]
        public async void UpdatePlantillaControllerTest()
        {
            var dto = new PlantillaDTOCreate() { Titulo = "Plantilla 1", Cuerpo = "Descripcion de la plantilla 1", EstadoId = 1 };
            // preparacion de los datos
            _servicesMock.Setup(x => x.ActualizarPlantillaDAO(It.IsAny<Plantilla>(), 1)).ReturnsAsync(new PlantillaDTO() { id = 1, Titulo = "Plantilla 1", Cuerpo = "Descripcion de la plantilla 1", EstadoId = 1 });
            var response = new ApplicationResponse<PlantillaDTO>();
            Boolean expected = true;
            //probar metodo put
            response = await _controller.ActualizarPlantilla(dto, 1);

            Assert.Equal<Boolean>(expected, response.Success);
        }

        [Fact(DisplayName = "Actualizar una Plantilla con excepcion")]
        public async void UpdatePlantillaControllerExceptionTest()
        {
            var dto = new PlantillaDTOCreate() { Titulo = "Plantilla 1", Cuerpo = "Descripcion de la plantilla 1", EstadoId = 1 };
            // preparacion de los datos
            _servicesMock.Setup(x => x.ActualizarPlantillaDAO(It.IsAny<Plantilla>(), 1)).ThrowsAsync(new PlantillaException("Error al actualizar la plantilla", new System.Exception(), loggerDAO));
            var response = new ApplicationResponse<PlantillaDTO>();
            Boolean expected = false;
            //probar metodo put
            response = await _controller.ActualizarPlantilla(dto, 1);
            Assert.Equal<Boolean>(expected, response.Success);
        }

        [Fact(DisplayName = "Eliminar una Plantilla")]
        public async void DeletePlantillaControllerTest()
        {
            // preparacion de los datos
            _servicesMock.Setup(x => x.EliminarPlantillaDAO(It.IsAny<int>())).ReturnsAsync(true);
            var response = new ApplicationResponse<ActionResult>();
            Boolean expected = true;
            //probar metodo delete
            response = await _controller.EliminarPlantilla(1);

            Assert.Equal<Boolean>(expected, response.Success);
        }

        [Fact(DisplayName = "Plantilla no encontrada para eliminar")]
        public async void DeletePlantillaNoEncontradaControllerTest()
        {
            // preparacion de los datos
            _servicesMock.Setup(x => x.EliminarPlantillaDAO(It.IsAny<int>())).ReturnsAsync(false);
            var response = new ApplicationResponse<ActionResult>();
            Boolean expected = false;
            //probar metodo delete
            response = await _controller.EliminarPlantilla(1);

            Assert.Equal<Boolean>(expected, response.Success);
        }

        [Fact(DisplayName = "Eliminar una Plantilla con excepcion")]
        public async void DeletePlantillaControllerExceptionTest()
        {
            // preparacion de los datos
            _servicesMock.Setup(x => x.EliminarPlantillaDAO(It.IsAny<int>())).ThrowsAsync(new PlantillaException("Error al eliminar la plantilla", new System.Exception(), loggerDAO));
            var response = new ApplicationResponse<ActionResult>();
            Boolean expected = false;
            //probar metodo delete
            response = await _controller.EliminarPlantilla(1);

            Assert.Equal<Boolean>(expected, response.Success);
        }


        // [Fact(DisplayName = "Obtener lista de Plantillas")]
        // public async void GetPlantillasControllerTest()
        // {
        //     // preparacion de los datos
        //     _servicesMock.Setup(x => x.ObtenerPlantillasDAO()).ReturnsAsync(new List<Plantilla> { new Plantilla() { id = 1, titulo = "Plantilla 1", cuerpo = "Descripcion de la plantilla 1", tipo = "Solicitud" } });
        //     //probar metodo get
        //     var result = await _controller.Get();
        //     var listPlantillas = result.Value!;
        //     // validar cantidad de elementos
        //     Assert.Equal(1, listPlantillas.Count);
        // }

        // [Fact(DisplayName = "Id menor a 0 Obtener Plantilla")]
        // public async void GetIdMenor0PlantillaControllerTest()
        // {
        //     // preparacion de los datos
        //     _servicesMock.Setup(x => x.ObtenerPlantillaDAO(0)).ReturnsAsync(new Plantilla() { id = 1, titulo = "Plantilla 1", cuerpo = "Descripcion de la plantilla 1", tipo = "Solicitud" });
        //     //probar metodo get
        //     var result = await _controller.Get(0);
        //     // validar statusCode

        //     Assert.IsType<BadRequestObjectResult>(result.Result);
        // }

        // [Fact(DisplayName = "Obtener Plantilla")]
        // public async void GetPlantillaControllerTest()
        // {
        //     // preparacion de los datos
        //     _servicesMock.Setup(x => x.ObtenerPlantillaDAO(1)).ReturnsAsync(new Plantilla() { id = 1, titulo = "Plantilla 1", cuerpo = "Descripcion de la plantilla 1", tipo = "Solicitud" });
        //     //probar metodo get
        //     var result = await _controller.Get(1);
        //     // validar statusCode

        //     Assert.IsType<OkObjectResult>(result.Result);
        // }

        // [Fact(DisplayName = "No existe Plantilla")]
        // public async void GetNoExistePlantillaControllerTest()
        // {
        //     // preparacion de los datos
        //     _servicesMock.Setup(x => x.ObtenerPlantillaDAO(5)).ReturnsAsync(new ActionResult<Plantilla>(new NotFoundResult()));
        //     //probar metodo get
        //     var result = await _controller.Get(5);
        //     // validar statusCode

        //     Assert.IsType<NotFoundObjectResult>(result.Result);
        // }

        // [Fact(DisplayName = "Actualizar Plantilla")]
        // public async void PutPlantillaControllerTest()
        // {
        //     // preparacion de los datos
        //     _servicesMock.Setup(x => x.ActualizarPlantillaDAO(It.IsAny<Plantilla>(), 1)).ReturnsAsync(new OkResult());
        //     //probar metodo put
        //     var result = await _controller.ActualizarPlantilla(new PlantillaDTOCreate() { Titulo = "Plantilla 1", Cuerpo = "Descripcion de la plantilla 1", Tipo = "Solicitud" }, 1);
        //     // validar statusCode

        //     Assert.IsType<OkObjectResult>(result);
        // }

        // [Fact(DisplayName = "Id menor a 0 Actualizar Plantilla")]
        // public async void PutIdMenor0PlantillaControllerTest()
        // {
        //     // preparacion de los datos
        //     //probar metodo put
        //     var result = await _controller.ActualizarPlantilla(PlantillaDtoCreate, 0);
        //     // validar statusCode

        //     Assert.IsType<BadRequestObjectResult>(result);
        // }


        // [Fact(DisplayName = "Id menor a 0 Eliminar Plantilla")]
        // public async void DeleteIdMenor0PlantillaControllerTest()
        // {
        //     // preparacion de los datos
        //     //probar metodo delete
        //     var result = await _controller.EliminarPlantilla(0);
        //     // validar statusCode

        //     Assert.IsType<BadRequestObjectResult>(result);
        // }

        // [Fact(DisplayName = "Eliminar Plantilla")]
        // public async void DeletePlantillaControllerTest()
        // {
        //     // preparacion de los datos
        //     _servicesMock.Setup(x => x.EliminarPlantillaDAO(1)).ReturnsAsync(new OkResult());
        //     //probar metodo delete
        //     var result = await _controller.EliminarPlantilla(1);
        //     // validar statusCode

        //     Assert.IsType<OkResult>(result);
        // }

        // [Fact(DisplayName = "No existe Plantilla Eliminar")]
        // public async void DeleteNoExistePlantillaControllerTest()
        // {
        //     // preparacion de los datos
        //     _servicesMock.Setup(x => x.EliminarPlantillaDAO(5)).ReturnsAsync(new NotFoundResult());
        //     //probar metodo delete
        //     var result = await _controller.EliminarPlantilla(5);
        //     // validar statusCode

        //     Assert.IsType<NotFoundResult>(result);
        // }


    }
}