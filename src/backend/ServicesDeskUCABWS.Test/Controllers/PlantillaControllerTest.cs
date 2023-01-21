// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.Controllers;
// using Microsoft.Extensions.Logging;
// using Microsoft.Extensions.Logging.Abstractions;
// using Moq;
// using ServicesDeskUCABWS.BussinessLogic.DTO;
// using ServicesDeskUCABWS.Controllers;
// using ServicesDeskUCABWS.Persistence.DAO.Interface;
// using ServicesDeskUCABWS.Persistence.Database;
// using ServicesDeskUCABWS.Persistence.Entity;
// using ServicesDeskUCABWS.Test.Configuraciones;


// namespace ServicesDeskUCABWS.Test.Controllers
// {
//     public class PlantillaControllerTest : BasePrueba
//     {

//         private readonly PlantillaController _controller;
//         private readonly Mock<IPlantillaDAO> _servicesMock;
//         public PlantillaDTO PlantillaDto = It.IsAny<PlantillaDTO>();

//         public PlantillaDTOCreate PlantillaDtoCreate = It.IsAny<PlantillaDTOCreate>();
//         public Plantilla plantilla = It.IsAny<Plantilla>();

//         private readonly Mock<IMigrationDbContext> _contextMock;

//         public PlantillaControllerTest()
//         {
//             _contextMock = new Mock<IMigrationDbContext>();
//             var _logger = new NullLogger<PlantillaController>();
//             var _mapper = ConfigurarAutoMapper();
//             _servicesMock = new Mock<IPlantillaDAO>();
//             _controller = new PlantillaController(_logger, _servicesMock.Object, _mapper);
//             _controller.ControllerContext = new ControllerContext();
//             _controller.ControllerContext.HttpContext = new DefaultHttpContext();
//             _controller.ControllerContext.ActionDescriptor = new ControllerActionDescriptor();
//         }

//         [Fact(DisplayName = "Agregar una Plantilla")]
//         public async void CreatePlantillaControllerTest()
//         {
//             var dto = new PlantillaDTOCreate() { Titulo = "Plantilla 1", Cuerpo = "Descripcion de la plantilla 1", Tipo = "Solicitud" };
//             // preparacion de los datos
//             _servicesMock.Setup(x => x.AgregarPlantillaDAO(new Plantilla())).ReturnsAsync(new PlantillaDTO() { id = 1, Titulo = "Plantilla 1", Cuerpo = "Descripcion de la plantilla 1", Tipo = "Solicitud" });
//             //probar metodo post
//             var result = await _controller.Post(dto);

//             Assert.IsType<OkObjectResult>(result);
//         }

//         [Fact(DisplayName = "Obtener lista de Plantillas")]
//         public async void GetPlantillasControllerTest()
//         {
//             // preparacion de los datos
//             _servicesMock.Setup(x => x.ObtenerPlantillasDAO()).ReturnsAsync(new List<Plantilla> { new Plantilla() { id = 1, titulo = "Plantilla 1", cuerpo = "Descripcion de la plantilla 1", tipo = "Solicitud" } });
//             //probar metodo get
//             var result = await _controller.Get();
//             var listPlantillas = result.Value!;
//             // validar cantidad de elementos
//             Assert.Equal(1, listPlantillas.Count);
//         }

//         [Fact(DisplayName = "Id menor a 0 Obtener Plantilla")]
//         public async void GetIdMenor0PlantillaControllerTest()
//         {
//             // preparacion de los datos
//             _servicesMock.Setup(x => x.ObtenerPlantillaDAO(0)).ReturnsAsync(new Plantilla() { id = 1, titulo = "Plantilla 1", cuerpo = "Descripcion de la plantilla 1", tipo = "Solicitud" });
//             //probar metodo get
//             var result = await _controller.Get(0);
//             // validar statusCode

//             Assert.IsType<BadRequestObjectResult>(result.Result);
//         }

//         [Fact(DisplayName = "Obtener Plantilla")]
//         public async void GetPlantillaControllerTest()
//         {
//             // preparacion de los datos
//             _servicesMock.Setup(x => x.ObtenerPlantillaDAO(1)).ReturnsAsync(new Plantilla() { id = 1, titulo = "Plantilla 1", cuerpo = "Descripcion de la plantilla 1", tipo = "Solicitud" });
//             //probar metodo get
//             var result = await _controller.Get(1);
//             // validar statusCode

//             Assert.IsType<OkObjectResult>(result.Result);
//         }

//         [Fact(DisplayName = "No existe Plantilla")]
//         public async void GetNoExistePlantillaControllerTest()
//         {
//             // preparacion de los datos
//             _servicesMock.Setup(x => x.ObtenerPlantillaDAO(5)).ReturnsAsync(new ActionResult<Plantilla>(new NotFoundResult()));
//             //probar metodo get
//             var result = await _controller.Get(5);
//             // validar statusCode

//             Assert.IsType<NotFoundObjectResult>(result.Result);
//         }

//         [Fact(DisplayName = "Actualizar Plantilla")]
//         public async void PutPlantillaControllerTest()
//         {
//             // preparacion de los datos
//             _servicesMock.Setup(x => x.ActualizarPlantillaDAO(It.IsAny<Plantilla>(), 1)).ReturnsAsync(new OkResult());
//             //probar metodo put
//             var result = await _controller.ActualizarPlantilla(new PlantillaDTOCreate() { Titulo = "Plantilla 1", Cuerpo = "Descripcion de la plantilla 1", Tipo = "Solicitud" }, 1);
//             // validar statusCode

//             Assert.IsType<OkObjectResult>(result);
//         }

//         [Fact(DisplayName = "Id menor a 0 Actualizar Plantilla")]
//         public async void PutIdMenor0PlantillaControllerTest()
//         {
//             // preparacion de los datos
//             //probar metodo put
//             var result = await _controller.ActualizarPlantilla(PlantillaDtoCreate, 0);
//             // validar statusCode

//             Assert.IsType<BadRequestObjectResult>(result);
//         }


//         [Fact(DisplayName = "Id menor a 0 Eliminar Plantilla")]
//         public async void DeleteIdMenor0PlantillaControllerTest()
//         {
//             // preparacion de los datos
//             //probar metodo delete
//             var result = await _controller.EliminarPlantilla(0);
//             // validar statusCode

//             Assert.IsType<BadRequestObjectResult>(result);
//         }

//         [Fact(DisplayName = "Eliminar Plantilla")]
//         public async void DeletePlantillaControllerTest()
//         {
//             // preparacion de los datos
//             _servicesMock.Setup(x => x.EliminarPlantillaDAO(1)).ReturnsAsync(new OkResult());
//             //probar metodo delete
//             var result = await _controller.EliminarPlantilla(1);
//             // validar statusCode

//             Assert.IsType<OkResult>(result);
//         }

//         [Fact(DisplayName = "No existe Plantilla Eliminar")]
//         public async void DeleteNoExistePlantillaControllerTest()
//         {
//             // preparacion de los datos
//             _servicesMock.Setup(x => x.EliminarPlantillaDAO(5)).ReturnsAsync(new NotFoundResult());
//             //probar metodo delete
//             var result = await _controller.EliminarPlantilla(5);
//             // validar statusCode

//             Assert.IsType<NotFoundResult>(result);
//         }


//     }
// }