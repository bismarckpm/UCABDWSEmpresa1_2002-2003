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
using static ServicesDeskUCABWS.Reponses.AplicationResponse;
using ServicesDeskUCABWS.Exceptions;

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

    #region  Casos Exitosos

            [Fact(DisplayName = "Agregar Tipo Cargo")]
            public Task CreateTipoCargoControllerTest()
            {   var dto = new TipoCargoDTO(){Id = 3, Nombre = "Senior"};
                
                _servicesMock.Setup(t=>t.AgregarTipoCargoDAO(tipo))
                .Returns(tipoCargo);
                
                var result = _controller.AgregarTipoCargo(dto);
                
                Assert.IsType<ApplicationResponse<TipoCargoDTO>>(result);
                return Task.CompletedTask;
            }

            [Fact(DisplayName="Consultar Lista Tipo Cargo")]
            public Task ConsultarTipoCargoControllerTest()
            {
                _servicesMock.Setup(t=>t.ConsultarTipoCargoDAO())
                .Returns(new List<TipoCargoDTO>());

                var result = _controller.ConsultaTipoCargo();

                Assert.IsType<ApplicationResponse<List<TipoCargoDTO>>>(result);
                return Task.CompletedTask;
            }

            [Fact (DisplayName ="Actualizar Tipo de Cargo")]
            public Task ActualizarTipoCargoControllerTest()
            {
                var tipo1 = new TipoCargoDTO(){Id = 2, Nombre = "Semi Senior"};

                _servicesMock.Setup(t=>t.ActualizarTipoCargoDAO(tipo))
                    .Returns(new TipoCargoDTO());

                    var result = _controller.ActualizarTipoCargo(tipo1);

                    Assert.IsType<ApplicationResponse<TipoCargoDTO>>(result);
                return Task.CompletedTask;
            }	

             [Fact (DisplayName ="Elimina un Tipo de Cargo")]
             public Task EliminarTipoCargoControllerTest()
             {
                var codigo = 1;
                 _servicesMock.Setup(t=>t.EliminarTipoCargoDAO(It.IsAny<int>())).Returns(It.IsAny<TipoCargoDTO>());

                 var result = _controller.EliminarTipoCargo(codigo);

                 Assert.IsType<ApplicationResponse<TipoCargoDTO>>(result);
                 return Task.CompletedTask;
             }

    #endregion

    #region  Casos Particulares
                            //FALLA
            [Fact(DisplayName = "Agregar Tipo Cargo con Excepcion")]
            public Task CreateTipoCargoControllerTestException()
            {var dto = new TipoCargoDTO(){Id = 3, Nombre = "Senior"};
                
                _servicesMock.Setup(t=>t.AgregarTipoCargoDAO(tipo))
                .Throws(new ServicesDeskUcabWsException("", new NullReferenceException()));               
                var result = _controller.AgregarTipoCargo(dto);
                
                Assert.NotNull(result);        
                Assert.False(result.Success);
                return Task.CompletedTask;
            }

            [Fact(DisplayName="Consulta Lista Tipo Cargo con Excepcion")]    
            public Task ConsultarTipoCargoControllerTestException()
            {
                _servicesMock
                    .Setup(t=>t.ConsultarTipoCargoDAO())
                    .Throws(new ServicesDeskUcabWsException("", new Exception()));

                        var result = _controller.ConsultaTipoCargo();

                Assert.NotNull(result);        
                Assert.False(result.Success);
                return Task.CompletedTask;
            }

                    //Por Corregir
             [Fact(DisplayName="Actualiza Tipo Cargo con Excepcion")]
             public Task ActualizarTipoCargoControllerTestException()
             {
                 _servicesMock.Setup(t=>t.ActualizarTipoCargoDAO(NewTipoCargo())).
                 Throws(new ServicesDeskUcabWsException("", new Exception()));
            
                var resultEx = _controller.ActualizarTipoCargo(tipoCargo);

                 Assert.NotNull(resultEx);
                 Assert.False(resultEx.Success);
                 return Task.CompletedTask;
             }

            [Fact(DisplayName= "Elimina un Tipo de Cargo con excepcion")]
             public Task EliminarTipoCargoControllerTestException()
             {
                    _servicesMock.Setup(t=>t.EliminarTipoCargoDAO(It.IsAny<int>()))
                    .Throws(new ServicesDeskUcabWsException("", new Exception()));

                    var resultEx = _controller.EliminarTipoCargo(It.IsAny<int>());

                Assert.NotNull(resultEx);
                Assert.False(resultEx.Success);    
                return Task.CompletedTask;
             }

    #endregion

    #region Metodo Privado
             private TipoCargo NewTipoCargo()
             {
                return new TipoCargo()
                {
                    id = -1,
                    nombre = string.Empty
                };
             }

    #endregion
    }
}
