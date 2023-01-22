using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.BussinessLogic.Mapper;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.Controllers;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Moq;
using Xunit;
using ServicesDeskUCABWS.Reponses;
using static ServicesDeskUCABWS.Reponses.AplicationResponse;
using ServicesDeskUCABWS.Exceptions;

namespace ServicesDeskUCABWS.Test.Controllers
{
    public class CategoriaControllerTest
    {
        private readonly CategoriaController _controller;
        private readonly Mock<ICategoriaDAO> _servicesMock;
        private readonly Mock<ILogger<CategoriaController>> _log;
        public CategoriaDTO categoria = It.IsAny<CategoriaDTO>();
        public Categoria cat = It.IsAny<Categoria>();

        public CategoriaControllerTest()
        {
            _log = new Mock<ILogger<CategoriaController>>();
            _servicesMock = new Mock<ICategoriaDAO>();
            _controller = new CategoriaController(_servicesMock.Object, _log.Object);
            _controller.ControllerContext = new ControllerContext();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.ActionDescriptor = new ControllerActionDescriptor();
        }


        #region Casos Exitosos
        [Fact(DisplayName = "Agregar Categoria")]
        public Task CreateCategoriaControllerTest()
        {
            var dto = new CategoriaDTO() { Id = It.IsAny<int>(), Nombre = It.IsAny<String>() };

            _servicesMock.Setup(t => t.AgregarCategoriaDAO(cat))
            .Returns(categoria);

            var result = _controller.CreateCategoria(dto);

            Assert.IsType<ApplicationResponse<CategoriaDTO>>(result);
            return Task.CompletedTask;
        }


        [Fact(DisplayName = "Consultar Lista Categorias")]
        public Task ConsultarCategoriasControllerTest()
        {
            _servicesMock.Setup(t => t.ConsultarTodosCategoriasDAO())
            .Returns(new List<CategoriaDTO>());

            var result = _controller.ConsultaCategorias();

            Assert.IsType<ApplicationResponse<List<CategoriaDTO>>>(result);
            return Task.CompletedTask;
        }


        [Fact(DisplayName = "Actualizar Categoria")]
        public Task ActualizarCategoriaControllerTest()
        {
            var dto = new CategoriaDTO() { Id = 2, Nombre = "Cate" };

            _servicesMock.Setup(t => t.ActualizarCategoriaDAO(cat))
                .Returns(categoria);

            var result = _controller.ActualizarCategoria(dto);

            Assert.IsType<ApplicationResponse<CategoriaDTO>>(result);
            return Task.CompletedTask;
        }


        [Fact(DisplayName = "Elimina una Categoria")]
        public Task EliminarTipoCargoControllerTest()
        {
            var codigo = 1;
            _servicesMock.Setup(t => t.EliminarCategoriaDAO(It.IsAny<int>()))
                .Returns(categoria);

            var result = _controller.EliminarCategoria(codigo);

            Assert.IsType<ApplicationResponse<CategoriaDTO>>(result);
            return Task.CompletedTask;
        }


        [Fact(DisplayName = "Consultar Categoria por id")]
        public Task ConsultarCategoriaIdControllerTest()
        {
            _servicesMock.Setup(t => t.ConsultaCategoriaDAO(It.IsAny<int>()))
            .Returns(categoria);

            var result = _controller.ConsultaCategoria(1);

            Assert.IsType<ApplicationResponse<CategoriaDTO>>(result);
            return Task.CompletedTask;
        }

        #endregion



        #region Casos Particulares
        [Fact(DisplayName = "Exception: Crear categoria")]
        public Task CreateCategoriaControllerTestException()
        {
            _servicesMock.Setup(t => t.AgregarCategoriaDAO(cat))
            .Throws(new Exception());

            var result = _controller.CreateCategoria(categoria);

            Assert.NotNull(result);
            Assert.False(result.Success);
            return Task.CompletedTask;
        }

        

        [Fact(DisplayName = "Exception: Consultar lista de categorias")]
        public Task ConsultarCategoriasControllerTestException()
        {
            _servicesMock
                .Setup(t => t.ConsultarTodosCategoriasDAO())
                .Throws(new ServicesDeskUcabWsException("", new Exception()));

            var result = _controller.ConsultaCategorias();

            Assert.NotNull(result);
            Assert.False(result.Success);
            return Task.CompletedTask;
        }

        

        [Fact(DisplayName = "Exception: Actualizar categoria")]
        public Task ActualizarCategoriaControllerTestException()
        {
            _servicesMock.Setup(t => t.ActualizarCategoriaDAO(cat))
                .Throws(new Exception());

            var result = _controller.ActualizarCategoria(categoria);

            Assert.NotNull(result);
            Assert.False(result.Success);
            return Task.CompletedTask;
        }


        [Fact(DisplayName = "Exception: Eliminar categoria")]
        public Task EliminarTipoCargoControllerTestException()
        {
            _servicesMock.Setup(t => t.EliminarCategoriaDAO(It.IsAny<int>()))
            .Throws(new ServicesDeskUcabWsException("", new Exception()));

            var result = _controller.EliminarCategoria(It.IsAny<int>());

            Assert.NotNull(result);
            Assert.False(result.Success);
            return Task.CompletedTask;
        }


        [Fact(DisplayName = "Exception: Consultar categoria con ID")]
        public Task ConsultarCategoriaIdControllerTestException()
        {
            _servicesMock.Setup(t => t.ConsultaCategoriaDAO(It.IsAny<int>()))
            .Throws(new ServicesDeskUcabWsException("", new Exception()));

            var result = _controller.ConsultaCategoria(It.IsAny<int>());

            Assert.NotNull(result);
            Assert.False(result.Success);
            return Task.CompletedTask;
        }
        #endregion
    }
}
