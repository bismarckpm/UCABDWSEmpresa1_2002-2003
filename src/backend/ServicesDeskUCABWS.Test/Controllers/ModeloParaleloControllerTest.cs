using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Controllers;
using ServicesDeskUCABWS.Exceptions;
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
    private readonly Mock<ILogger<ModeloParaleloController>>? _logger;
    private readonly Mock<IMapper>? _mapper;
    public ModeloParaleloControllerTest()
    {
        _contextMock = new Mock<IMigrationDbContext>();
        var _logger = new Mock<ILogger<ModeloParaleloController>>();
        var _mapper = new Mock<IMapper>();
        _servicesMock = new Mock<IModeloParaleloDAO>();
        _controller = new ModeloParaleloController(_logger.Object, _servicesMock.Object, _mapper.Object);
        _controller.ControllerContext = new ControllerContext();
        _controller.ControllerContext.HttpContext = new DefaultHttpContext();
        _controller.ControllerContext.ActionDescriptor = new ControllerActionDescriptor();
    }

    private ModeloParalelo ModelModeloParaleloTest()
        {
            return new ModeloParalelo()
            {
                id = 1,
                        nombre = "Prueba Modelo",
                        categoriaid = 1,
                        categoria = new Categoria()
                        {
                            id = 1,
                            nombre = "Guardado"
                        },
                        cantidaddeaprobacion = 5
            };
        }

        private ModeloParaleloDTO ModelDTO()
        {
            return new ModeloParaleloDTO()
            {
                Id = 1,
                nombre = "Prueba Modelo",
                categoriaId = 1,
                cantidaddeaprobacion = 5
            };
        }

        private ModeloParaleloCreateDTO ModelCreateDTO()
        {
            return new ModeloParaleloCreateDTO()
            {
                nombre = "Prueba Modelo",
                categoriaId = 1,
                cantidaddeaprobacion = 5
            };
        }

        private ModeloParaleloDTO  ErrorModelDTO()
        {
            return new ModeloParaleloDTO();
        }

    //Agregar un modelo Paralelo
    [Fact(DisplayName = "Agrega un Modelo Paralelo")]
    public Task CreateModeloParaleloControllerTest()
    {
        _servicesMock.Setup(m => m.AgregarModeloParaleloDAO(modeloParalelo))
        .Returns(new ModeloParaleloCreateDTO());
        var result = _controller.Post(ModelCreateDTO());
        Assert.IsType<ModeloParaleloCreateDTO>(result);
        return Task.CompletedTask;
    }

    //Consultar un modelo Paralelo por su id
    [Fact(DisplayName = "Consulta de modelo paralelo por ID")]
    public Task ConsultaModeloParaleloPorIdControllerTest()
    {
        _servicesMock.Setup(j => j.ObtenerModeloParaleloDAO(It.IsAny<int>()))
        .Returns(ModelDTO());
        var result = _controller.ConsultarMParaleloPorId(It.IsAny<int>());
        Assert.IsType<ModeloParaleloDTO>(result);
        return Task.CompletedTask;
    }

    //Consulta una lista de objetos de Modelo Paralelo
    [Fact(DisplayName = "Consulta lista de modelos paralelos")]
    public Task GetModeloParaleloControllerTest()
    {
        _servicesMock.Setup(j => j.ConsultarModelosParalelosDAO())
        .Returns(new List<ModeloParaleloDTO>());
        var result = _controller.GetModeloParalelo();
        Assert.IsType<List<ModeloParaleloDTO>>(result);
        return Task.CompletedTask;
    }

    //Actualiza el modelo Paralelo
    [Fact(DisplayName = "Actualiza un Modelo Paralelo")]
    public Task ActualizarModeloParaleloControllerTest()
    {
        _servicesMock.Setup(m =>m.ActualizarModeloParaleloDAO(modeloParalelo))
        .Returns(new ModeloParaleloDTO());
        var result = _controller.ActualizarModeloParalelo(ModelDTO());
        Assert.IsType<ModeloParaleloDTO>(result);
        return Task.CompletedTask;
    }

    //Elimina un modelo Paralelo
    [Fact(DisplayName = "Elimina un modelo Paralelo")]
    public Task EliminarModeloParaleloControllerTest()
    {
        _servicesMock.Setup(j => j.EliminarModeloParaleloDAO(It.IsAny<int>()))
        .Returns(new ModeloParaleloDTO());
        var result = _controller.EliminarModeloParalelo(It.IsAny<int>());
        Assert.IsType<ModeloParaleloDTO>(result);
        return Task.CompletedTask;
    }

    //Agregar un modelo paralelo con excepcion
    [Fact(DisplayName = "Agregar modelo paralelo con Excepcion")]
    public Task CreateModeloParaleloControllerExceptionTest()
    {
        _servicesMock.Setup(e => e.AgregarModeloParaleloDAO(modeloParalelo))
            .Throws(new Exception());
            var dto = new ModeloParaleloCreateDTO()
                        {
                            nombre = "Prueba de excepcion",
                        };
        Assert.Throws<Exception>(() => _controller.Post(dto));
        return Task.CompletedTask;
    }


    //Consulta un listado de modelo paralelo con excepcion
    [Fact(DisplayName = "Consulta listado de modelo paralelo con excepcion")]
    public Task ConsultarModeloParaleloControllerExceptionTest()
    {
        _servicesMock.Setup(e => e.ConsultarModelosParalelosDAO())
        .Throws(new Exception("",new ArgumentOutOfRangeException()));
        Assert.Throws<Exception>(() => _controller.GetModeloParalelo());
        return Task.CompletedTask;
    }

    //Consulta un modelo paralelo por id con excepcion
    [Fact(DisplayName = "Consultar Modelo Paralelo por Id con excepcion")]
    public Task ConsultarModeloParaleloIdControllerExceptionTest()
    {
        _servicesMock.Setup(e => e.ObtenerModeloParaleloDAO(It.IsAny<int>()))
                    .Throws(new ServicesDeskUcabWsException("", new Exception()));
        var buscarModelo = -1;
        Assert.Throws<ServicesDeskUcabWsException>(() => _controller.ConsultarMParaleloPorId(buscarModelo));
        return Task.CompletedTask;
    }

    //Actualiza un modelo paralelo con excepcion
    [Fact(DisplayName = "Actualizar modelo jerarquico con excepcion")]
    public Task ActualizarModeloParaleloControllerExceptionTest()
    {
        _servicesMock.Setup(e => e.ActualizarModeloParaleloDAO(modeloParalelo))
                    .Throws(new Exception());
        Assert.Throws<Exception>(() => _controller.ActualizarModeloParalelo(ErrorModelDTO()));
        return Task.CompletedTask;
    }

    //Elimina un modelo paralelo con excepcion
    [Fact(DisplayName = "Eliminar un modelo jerarquico con excepcion")]
    public Task EliminarModeloParaleloExceptionControllerTest()
    {
        _servicesMock.Setup(e => e.EliminarModeloParaleloDAO(It.IsAny<int>()))
        .Throws(new ServicesDeskUcabWsException("", new Exception()));
        Assert.Throws<ServicesDeskUcabWsException>(() => _controller.EliminarModeloParalelo(-1));
        return Task.CompletedTask;
    }
}