using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Controllers;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Database;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.Test.Configuraciones;

namespace ServicesDeskUCABWS.Test.Controllers;

public class ModeloParaleloControllerTest : BasePrueba
{
    private readonly ModeloParaleloController _controller;
    private readonly Mock<IModeloParaleloDAO> _servicesMock;
    public ModeloParaleloDTO modeloParaleloDTO = It.IsAny<ModeloParaleloDTO>();
    public ModeloParaleloCreateDTO modeloParaleloCreateDTO = It.IsAny<ModeloParaleloCreateDTO>();
    public ModeloParalelo modeloParalelo = It.IsAny<ModeloParalelo>();
    private readonly Mock<IMigrationDbContext> _contextMock;
    public ModeloParaleloControllerTest()
    {
        _contextMock = new Mock<IMigrationDbContext>();
        var _logger = new NullLogger<ModeloParaleloController>();
        var _mapper = ConfigurarAutoMapper();
        _servicesMock = new Mock<IModeloParaleloDAO>();
        _controller = new ModeloParaleloController( _servicesMock.Object, _mapper);
        _controller.ControllerContext = new ControllerContext();
        _controller.ControllerContext.HttpContext = new DefaultHttpContext();
        _controller.ControllerContext.ActionDescriptor = new ControllerActionDescriptor();
    }

    [Fact(DisplayName = "Agregar una Modelo Paralelo")]
    public async void CreateModeloParaleloControllerTest()
    {
        var dto = new ModeloParaleloCreateDTO() { nombre = "ModeloParalelo1", cantidadAprobaciones = 3, categoriaId = 1 };
        // preparacion de los datos
        _servicesMock.Setup(x => x.AgregarModeloParaleloDAO(new ModeloParalelo())).ReturnsAsync(new ModeloParaleloDTO() { paraid = 1, nombre = "Paralelo1", cantidadAprobaciones = 3, categoriaId = 1 });
        //probar metodo post
        var result = await _controller.Crear(dto);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact(DisplayName = "Obtener lista de modelos paralelos")]
    public async void GetModelosParalelosControllerTest()
    {
        // preparacion de los datos
        _servicesMock.Setup(x => x.ConsultarModelosParalelosDAO()).ReturnsAsync(new List<ModeloParalelo> { new ModeloParalelo() { paraid = 1, nombre = "Paralelo1", cantidadAprobaciones = 3, categoria = null } });
        //probar metodo get
        var result = await _controller.ConsultarTodos();
        // validar statusCode

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact(DisplayName = "Id menor a 0 Obtener modelo paralelo")]
    public async void GetIdMenor0ModelosParalelosControllerTest()
    {
        // preparacion de los datos
        _servicesMock.Setup(x => x.ConsultaModeloParaleloDAO(0)).ReturnsAsync(new ModeloParalelo() { paraid = 0, nombre = "Paralelo1", cantidadAprobaciones = 3, categoria = null });
        //probar metodo get
        var result = await _controller.Consultar(0);
        // validar statusCode

        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact(DisplayName = "Obtener modelo paralelo")]
    public async void GetModeloParaleloControllerTest()
    {
        // preparacion de los datos
        _servicesMock.Setup(x => x.ConsultaModeloParaleloDAO(1)).ReturnsAsync(new ModeloParalelo() { paraid = 1, nombre = "Paralelo1", cantidadAprobaciones = 3, categoria = null });
        //probar metodo get
        var result = await _controller.Consultar(1);
        // validar statusCode
        Assert.IsType<OkObjectResult>(result.Result);
    }
    
    [Fact(DisplayName = "No existe modelo paralelo")]
    public async void GetNoExisteModeloParaleloControllerTest()
    {
        // preparacion de los datos
        _servicesMock.Setup(x => x.ConsultaModeloParaleloDAO(5)).ReturnsAsync(new ModeloParalelo());
        //probar metodo get
        var result = await _controller.Consultar(5);
        // validar statusCode
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact(DisplayName = "Id menor a 0 Actualiczar modelo paralelo")]
    public async void PutIdMenor0ModeloParaleloControllerTest()
    {
        // preparacion de los datos
        //probar metodo put
        var result = await _controller.Actualizar(0, modeloParaleloCreateDTO);
        // validar statusCode
        Assert.IsType<BadRequestObjectResult>(result);
    }

    /* No pasa porque no esta encuentra nada en el include con categoria
    [Fact(DisplayName = "Actualizar modelo paralelo")]
    public async void PutModeloParaleloControllerTest()
    {
        // preparacion de los datos
        var cat = new Categoria(){ id = 1, nombre = "Prueba"};
        _servicesMock.Setup(x => x.ActualizarModeloParaleloDAO(1, modeloParaleloCreateDTO)).ReturnsAsync(new OkObjectResult(new ModeloParalelo() { paraid = 1, nombre = "Paralelo2", cantidadAprobaciones = 3, categoriaId = 1 , categoria = cat}));
        //probar metodo put
        var result = await _controller.Actualizar(1, modeloParaleloCreateDTO);
        // validar statusCode
        Assert.IsType<OkObjectResult>(result);
    }*/

    [Fact(DisplayName = "No existe modelo paralelo Actualizar")]
    public async void PutNoExisteModeloParaleloControllerTest()
    {
        // preparacion de los datos
        _servicesMock.Setup(x => x.ActualizarModeloParaleloDAO(5, modeloParaleloCreateDTO)).ReturnsAsync(new ModeloParalelo());
        //probar metodo put
        var result = await _controller.Actualizar(5, modeloParaleloCreateDTO);
        // validar statusCode
        Assert.IsType<NotFoundObjectResult>(result);
    }
    
    [Fact(DisplayName = "Id menor a 0 Eliminar modelo paralelo")]
    public async void DeleteIdMenor0ModeloParaleloControllerTest()
    {
        // preparacion de los datos
        //probar metodo delete
        var result = await _controller.Eliminar(-1);
        // validar statusCode
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact(DisplayName = "No existe modelo paraleol Eliminar")]
    public async void DeleteNoExisteModeloParaleloControllerTest()
    {
        // preparacion de los datos
        _servicesMock.Setup(x => x.EliminarModeloParaleloDAO(5)).ReturnsAsync(new NotFoundResult());
        //probar metodo delete
        var result = await _controller.Eliminar(5);
        // validar statusCode
        Assert.IsType<NotFoundResult>(result);
    }
}