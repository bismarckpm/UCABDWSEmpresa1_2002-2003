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
// En Proceso
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

                Assert.Throws<NullReferenceException>(()=> _controller.ConsultaTipoCargo());
                return Task.CompletedTask;
            }
    }
}