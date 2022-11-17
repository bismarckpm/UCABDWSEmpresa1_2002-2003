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
            // public TipoCargoDTO tipo = It.IsAny<TipoCargoDTO>();

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
            {
                TipoCargo tipo = new TipoCargo(){
                    id = 1,
                    nombre = "Prueba"
                };
                _servicesMock.Setup(t=>t.AgregarTipoCargoDAO(tipo)).Returns(tipoCargo);
                var result = _controller.AgregarTipoCargo(tipoCargo);
                Assert.IsType<TipoCargoDTO>(result);
                return Task.CompletedTask;
            }
    }
}