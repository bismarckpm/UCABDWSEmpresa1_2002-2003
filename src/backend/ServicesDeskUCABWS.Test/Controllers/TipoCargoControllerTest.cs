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

namespace ServicesDeskUCABWS.Test.Controllers
{
    public class TipoCargoControllerTest
    {       
            private readonly TipoCargoController _controller;
            private readonly Mock<ITipoCargoDAO> _servicesMock;
            private readonly Mock<ILogger<TipoCargoController>> _log;
            public TipoCargoDTO tipoCargo = It.IsAny<TipoCargoDTO>();
            public TipoCargo tipo = It.IsAny<TipoCargo>();
            
            public TipoCargoControllerTest()
            {
                _log = new Mock<ILogger<TipoCargoController>>();
                _servicesMock = new Mock<ITipoCargoDAO>();
                _controller = new TipoCargoController(_servicesMock.Object,_log.Object);
                _controller.ControllerContext = new ControllerContext();
                _controller.ControllerContext.HttpContext = new DefaultHttpContext();
                _controller.ControllerContext.ActionDescriptor = new ControllerActionDescriptor();
            }

            [Fact(DisplayName = "Agregar Tipo Cargo")]
            public Task CreateTipoCargoControllerTest()
            {   var dto = new TipoCargoDTO(){Id = 3, Nombre = "Senior"};
                
                _servicesMock.Setup(t=>t.AgregarTipoCargoDAO(tipo))
                .Returns(tipoCargo);
                
                var result = _controller.AgregarTipoCargo(dto);
                
                Assert.IsType<ActionResult<TipoCargoDTO>>(result);
                return Task.CompletedTask;
            }

            [Fact(DisplayName = "Tipo Cargo con Excepcion")]
            public Task CreateTipoCargoControllerTestException()
            {
                _servicesMock.Setup(t=>t.AgregarTipoCargoDAO(tipo))
                .Throws(new NullReferenceException());

                Assert.Throws<NullReferenceException>(()=>_controller.AgregarTipoCargo(tipoCargo));
                return Task.CompletedTask;
            }

            [Fact(DisplayName="Consultar Lista Tipo Cargo")]
            public Task ConsultarTipoCargoControllerTest()
            {
                _servicesMock.Setup(t=>t.ConsultarTipoCargoDAO())
                .Returns(new List<TipoCargoDTO>());

                var result = _controller.ConsultaTipoCargo();

                Assert.IsType<ActionResult<List<TipoCargoDTO>>>(result);
                return Task.CompletedTask;
            }

            [Fact(DisplayName="Consulta Lista Tipo Cargo con Excepcion")]    
            public Task ConsultarTipoCargoControllerTestException()
            {
                _servicesMock
                    .Setup(t=>t.ConsultarTipoCargoDAO())
                    .Throws(new Exception("",new NullReferenceException()));

                Assert.Throws<NullReferenceException>(()=>_controller.ConsultaTipoCargo());
                return Task.CompletedTask;
            }

            [Theory (DisplayName ="Actualizar Tipo de Cargo")]
            [InlineData("Semi Senior")]
            public Task ActualizarTipoCargoControllerTest(string nombre)
            {
                var tipo1 = new TipoCargoDTO(){Id = 2, Nombre = "Semi Senior"};

                _servicesMock.Setup(t=>t.ActualizarTipoCargoDAO(tipo))
                    .Returns(new TipoCargoDTO());

                    var result = _controller.ActualizarTipoCargo(tipo1);
                    Assert.NotEqual<ActionResult<TipoCargoDTO>>(It.IsAny<TipoCargoDTO>(), result);
                return Task.CompletedTask;
            }	

             [Fact(DisplayName="Actualiza Tipo Cargo con Excepcion")]
             public Task ActualizarTipoCargoControllerTestException()
             {
                 _servicesMock.Setup(t=>t.ActualizarTipoCargoDAO(tipo)).Throws(new Exception("", new NullReferenceException()));
            
                 Assert.Throws<NullReferenceException>(()=>_controller.ActualizarTipoCargo(tipoCargo));
                 return Task.CompletedTask;
             }

             [Theory (DisplayName ="Elimina un Tipo de Cargo")]
             [InlineData(1)]
             public Task EliminarTipoCargoControllerTest(int id)
             {
                 _servicesMock.Setup(t=>t.EliminarTipoCargoDAO(It.IsAny<int>())).Returns(It.IsAny<TipoCargoDTO>());

                 var result = _controller.EliminarTipoCargo(id);

                 Assert.IsType<ActionResult<TipoCargoDTO>>(result);
                 return Task.CompletedTask;
             }

            [Fact(DisplayName= "Elimina un Tipo de Cargo con excepcion")]
             public Task EliminarTipoCargoControllerTestException()
             {
                    _servicesMock.Setup(t=>t.EliminarTipoCargoDAO(It.IsAny<int>()))
                    .Throws(new Exception("", new NullReferenceException()));

                    Assert.Throws<NullReferenceException>(()=>_controller.EliminarTipoCargo(It.IsAny<int>()));
                return Task.CompletedTask;
             }
    }
}