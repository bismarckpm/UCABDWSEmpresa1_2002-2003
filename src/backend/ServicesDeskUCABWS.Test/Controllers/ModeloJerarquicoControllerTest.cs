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

namespace ServicesDeskUCABWS.Test.Controllers;

public class ModeloJerarquicoControllerTest : BasePrueba
{
    private readonly ModeloJerarquicoController _controller;
    private readonly Mock<IModeloJerarquicoDAO> _servicesMock;
    public ModeloJerarquicoDTO modeloJerarquicoDTO = It.IsAny<ModeloJerarquicoDTO>();
    public ModeloJerarquicoCreateDTO modeloJerarquicoCreateDTO = It.IsAny<ModeloJerarquicoCreateDTO>();
    public ModeloJerarquico modeloJerarquico = It.IsAny<ModeloJerarquico>();
    private readonly Mock<IMigrationDbContext> _contextMock;
    public ModeloJerarquicoControllerTest()
    {
        _contextMock = new Mock<IMigrationDbContext>();
        var _logger = new NullLogger<ModeloJerarquicoController>();
        var _mapper = ConfigurarAutoMapper();
        _servicesMock = new Mock<IModeloJerarquicoDAO>();
        _controller = new ModeloJerarquicoController(_logger, _servicesMock.Object, _mapper);
        _controller.ControllerContext = new ControllerContext();
        _controller.ControllerContext.HttpContext = new DefaultHttpContext();
        _controller.ControllerContext.ActionDescriptor = new ControllerActionDescriptor();
    }

    [Fact(DisplayName = "Agregar una Modelo Jerarquico")]
    public async void CreateModeloJerarquicoControllerTest()
    {
        TipoCargo tCargo = new TipoCargo { id = 1, nombre = "Prueba"};
        List<TipoCargo>  lista = new List<TipoCargo>();
        lista.Add(tCargo);
        var dto = new ModeloJerarquicoCreateDTO() { Nombre = "ModeloJerarquico1", orden = lista, CategoriaId = 1 };
        // preparacion de los datos
        _servicesMock.Setup(x => x.AgregarModeloJerarquicoDAO(new ModeloJerarquico())).ReturnsAsync(new ModeloJerarquicoDTO() { id = 1, Nombre = "ModeloJerarquico1", orden = lista, CategoriaId = 1 });
        //probar metodo post
        var result = await _controller.Post(dto);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact(DisplayName = "Obtener lista de modelos jerarquicos")]
    public async void GetModelosJerarquicosControllerTest()
    {
        // preparacion de los datos
        TipoCargo tCargo = new TipoCargo { id = 1, nombre = "Prueba"};
        List<TipoCargo>  lista = new List<TipoCargo>();
        lista.Add(tCargo);
        _servicesMock.Setup(x => x.ConsultarModeloJerarquicosDAO()).ReturnsAsync(new List<ModeloJerarquico> { new ModeloJerarquico() { Id = 1, Nombre = "ModeloJerarquico1", orden = lista , categoria = null } });
        //probar metodo get
        var result = await _controller.Get();
        // validar statusCode

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact(DisplayName = "Id menor a 0 Obtener modelo jerarquico")]
    public async void GetIdMenor0ModelosJerarquicosControllerTest()
    {
        // preparacion de los datos
        TipoCargo tCargo = new TipoCargo { id = 1, nombre = "Prueba"};
        List<TipoCargo>  lista = new List<TipoCargo>();
        lista.Add(tCargo);
        _servicesMock.Setup(x => x.ObtenerModeloJerarquicoDAO(0)).ReturnsAsync(new ModeloJerarquico() { Id = 1, Nombre = "ModeloJerarquico1", orden = lista , categoria = null });
        //probar metodo get
        var result = await _controller.Get(0);
        // validar statusCode

        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact(DisplayName = "Obtener modelo jerarquico")]
    public async void GetModeloJerarquicoControllerTest()
    {
        // preparacion de los datos
        TipoCargo tCargo = new TipoCargo { id = 1, nombre = "Prueba"};
        List<TipoCargo>  lista = new List<TipoCargo>();
        lista.Add(tCargo);
        _servicesMock.Setup(x => x.ObtenerModeloJerarquicoDAO(1)).ReturnsAsync(new ModeloJerarquico() { Id = 1, Nombre = "ModeloJerarquico1", orden = lista , categoria = null });
        //probar metodo get
        var result = await _controller.Get(1);
        // validar statusCode
        Assert.IsType<OkObjectResult>(result.Result);
    }
    
    [Fact(DisplayName = "No existe modelo jerarquico")]
    public async void GetNoExisteModeloJerarquicoControllerTest()
    {
        // preparacion de los datos
        _servicesMock.Setup(x => x.ObtenerModeloJerarquicoDAO(5)).ReturnsAsync(new ModeloJerarquico());
        //probar metodo get
        var result = await _controller.Get(5);
        // validar statusCode
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact(DisplayName = "Id menor a 0 Actualiczar modelo jerarquico")]
    public async void PutIdMenor0ModeloJerarquicoControllerTest()
    {
        // preparacion de los datos
        //probar metodo put
        var result = await _controller.ActualizarModeloJerarquico(modeloJerarquicoCreateDTO, 0);
        // validar statusCode
        Assert.IsType<BadRequestObjectResult>(result);
    }

    /*
    [Fact(DisplayName = "Actualizar modelo jerarquico")]
    public async void PutModeloJerarquicoControllerTest()
    {
        // preparacion de los datos
        TipoCargo tCargo = new TipoCargo { id = 1, nombre = "Prueba"};
        List<TipoCargo>  lista = new List<TipoCargo>();
        lista.Add(tCargo);
        _servicesMock.Setup(x => x.ActualizarModeloJerarquicoDAO(modeloJerarquicoCreateDTO, 1)).ReturnsAsync(new OkObjectResult(new ModeloJerarquico() { Id = 1, Nombre = "ModeloJerarquico1", orden = lista, CategoriaId = 1 }));
        //probar metodo put
        var result = await _controller.ActualizarModeloJerarquico(modeloJerarquicoCreateDTO, 1);
        // validar statusCode
        Assert.IsType<OkObjectResult>(result);
    }*/

    [Fact(DisplayName = "No existe modelo jerarquico Actualizar")]
    public async void PutNoExisteModeloJerarquicoControllerTest()
    {
        // preparacion de los datos
        _servicesMock.Setup(x => x.ActualizarModeloJerarquicoDAO(modeloJerarquicoCreateDTO, 5)).ReturnsAsync(new ModeloJerarquico());
        //probar metodo put
        var result = await _controller.ActualizarModeloJerarquico(modeloJerarquicoCreateDTO, 5);
        // validar statusCode
        Assert.IsType<NotFoundObjectResult>(result);
    }
    
    [Fact(DisplayName = "Id menor a 0 Eliminar modelo jerarquico")]
    public async void DeleteIdMenor0ModeloJerarquicoControllerTest()
    {
        // preparacion de los datos
        //probar metodo delete
        var result = await _controller.EliminarModeloJerarquico(-1);
        // validar statusCode
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact(DisplayName = "No existe modelo paraleol Eliminar")]
    public async void DeleteNoExisteModeloJerarquicoControllerTest()
    {
        // preparacion de los datos
        _servicesMock.Setup(x => x.EliminarModeloJerarquicoDAO(5)).ReturnsAsync(new NotFoundResult());
        //probar metodo delete
        var result = await _controller.EliminarModeloJerarquico(5);
        // validar statusCode
        Assert.IsType<NotFoundResult>(result);
    }
}