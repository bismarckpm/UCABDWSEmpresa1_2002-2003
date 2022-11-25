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
    public async void CreateEtiquetaControllerTest()
    {
        var dto = new ModeloParaleloCreateDTO() { nombre = "ModeloParalelo1", cantidadAprobaciones = 3, categoriaId = 1 };
        // preparacion de los datos
        _servicesMock.Setup(x => x.AgregarModeloParaleloDAO(new ModeloParalelo())).ReturnsAsync(new ModeloParaleloDTO() { paraid = 1, nombre = "Paralelo1", cantidadAprobaciones = 3, categoriaId = 1 });
        //probar metodo post
        //var result = await _controller.Post(dto);

        //Assert.IsType<OkObjectResult>(result);
    }
}