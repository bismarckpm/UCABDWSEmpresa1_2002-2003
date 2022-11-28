using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Controllers;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Database;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.Test.Configuraciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesDeskUCABWS.Test.Controllers
{
    public class CargoControllerTest : BasePrueba
    {
        private readonly CargoController _controller;
        private readonly Mock<ICargoDAO> _servicesMock;
        public CargoDTO cargoDto = It.IsAny<CargoDTO>();
        public Cargo cargo = It.IsAny<Cargo>();
        private readonly Mock<IMigrationDbContext> _contextMock;

        /// <summary>
        /// Constructor
        /// </summary>
        public CargoControllerTest()
        {
            _contextMock = new Mock<IMigrationDbContext>();
            var _logger = new NullLogger<CargoController>();
            var _mapper = ConfigurarAutoMapper();
            _servicesMock = new Mock<ICargoDAO>();
            _controller = new CargoController(_servicesMock.Object, _mapper, _logger);
            _controller.ControllerContext = new ControllerContext();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.ActionDescriptor = new ControllerActionDescriptor();
        }

        #region CASOS DE EXITOS

        /// <summary>
        /// Agregar Cargo
        /// </summary>
        [Fact(DisplayName = "Agregar un Cargo")]
        public async void CreateCargoControllerTest()
        {
            var dto = new CargoDTO() { Nombre = "Gerente", TipoCargoId = 1 };
            
            _servicesMock.Setup(x => x.AgregarCargoDAO(new Cargo())).ReturnsAsync(new CargoDTO() { Id = 1, Nombre = "Gerente", TipoCargoId = 1 });
            
            var result = await _controller.Post(dto);

            Assert.IsType<OkObjectResult>(result);
        }

        /// <summary>
        /// Consultar lista de cargos
        /// </summary>
        [Fact(DisplayName = "Obtener lista de cargos")]
        public async void GetListCargosControllerTest()
        {
            
            _servicesMock.Setup(x => x.ConsultarCargoDAO()).ReturnsAsync(new List<Cargo> { new Cargo() { id = 1, nombre = "Gerente", tipoCargoId = 1 } });
            
            var result = await _controller.Get();
            
            Assert.IsType<OkObjectResult>(result.Result);
        }

        /// <summary>
        /// Consultar un cargo en especifico
        /// </summary>
        [Fact(DisplayName = "Obtener cargo")]
        public async void GetCargoControllerTest()
        {
            _servicesMock.Setup(x => x.ObtenerCargoByIdDAO(1)).ReturnsAsync(new Cargo() { id = 1, nombre = "Gerente", tipoCargoId = 1});
            
            var result = await _controller.Get(1);
            
            Assert.IsType<OkObjectResult>(result.Result);
        }


        //[Fact(DisplayName = "Actualizar Cargo")]
        //public async void PutCargoControllerTest()
        //{

        //    var dto = new CargoDTO() { Id = 1, Nombre = "Presidente", TipoCargoId = 1 };
        //    var prueba = new Cargo() { id = 1, nombre = "Gerente", tipoCargoId = 1 };

        //    _servicesMock.Setup(x => x.ActualizarCargoDAO(prueba, 1)).ReturnsAsync(prueba);
        //    // _servicesMock.Setup(x => x.ActualizarCargoDAO(prueba, 1)).ReturnsAsync(new ActionResult<Cargo> (prueba));

        //    var result = await _controller.ActualizarCargo(dto, 1);

        //    Assert.IsType<OkObjectResult>(result);
        //}


        /// <summary>
        /// Eliminar cargo
        /// </summary>
        [Fact(DisplayName = "Eliminar Cargo")]
        public async void DeleteCargoControllerTest()
        {
            _servicesMock.Setup(x => x.EliminarCargoDAO(1)).ReturnsAsync(new OkResult());
            
            var result = await _controller.EliminarCargo(1);
            
            Assert.IsType<OkResult>(result);
        }

        #endregion

        #region CASOS PARTICULARES

        /// <summary>
        /// Agregar Cargo con Excepcion
        /// </summary>
        /// <returns></returns>
        [Fact(DisplayName = "Agregar Cargo con Excepcion")]
        public async Task AgregarCargoControllerTestException()
        {
            _servicesMock.Setup(t => t.AgregarCargoDAO(cargo))
            .Throws(new NullReferenceException());

            await Assert.ThrowsAsync<NullReferenceException>(() => _controller.Post(cargoDto));
        }

        /// <summary>
        /// Consultar un lista de cargos con excepcion
        /// </summary>
        [Fact(DisplayName = "Consultar cargo con excepciones")]
        public async void GetCargoExceptionControllerTest()
        {

            _servicesMock.Setup(c => c.ConsultarCargoDAO()).Throws(new NullReferenceException());

            await Assert.ThrowsAsync<NullReferenceException>(() => _controller.Get());

        }

        /// <summary>
        /// Consultar un cargo con Id = 0
        /// </summary>
        [Fact(DisplayName = "Id menor a 0 al consular un cargo")]
        public async void GetIdMenor0CargoControllerTest()
        {
            _servicesMock.Setup(x => x.ObtenerCargoByIdDAO(0)).ReturnsAsync(new Cargo() { id = 1, nombre = "Gerente", tipoCargoId = 1 });
            
            var result = await _controller.Get(0);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        /// <summary>
        /// Consultar cargo con excepciones
        /// </summary>
        [Fact(DisplayName = "Consultar Cargo ID exception")]
        public async void GetCargoIdExceptionControllerTest()
        {
            _servicesMock.Setup(x => x.ObtenerCargoByIdDAO(20)).Throws(new NullReferenceException());

            await Assert.ThrowsAsync<NullReferenceException>(() => _controller.Get(20));

        }

        /// <summary>
        /// No existe el id del cargo 
        /// </summary>
        [Fact(DisplayName = "No existe Cargo")]
        public async void GetNoExisteCargoControllerTest()
        {
            _servicesMock.Setup(x => x.ObtenerCargoByIdDAO(5)).ReturnsAsync(new Cargo());
            
            var result = await _controller.Get(5);
            
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }



        /// <summary>
        /// Actualizar un cargo con Id = 0
        /// </summary>
        [Fact(DisplayName = "Id menor a 0 Actualizar Cargo")]
        public async void PutIdMenor0CargoControllerTest()
        {
            var result = await _controller.ActualizarCargo(cargoDto, 0);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// NO existe el cargo a actualizar
        /// </summary>
        [Fact(DisplayName = "No existe Cargo al Actualizar")]
        public async void PutNoExisteCargoControllerTest()
        {
            // preparacion de los datos
            _servicesMock.Setup(x => x.ActualizarCargoDAO(cargo, 5)).ReturnsAsync(new Cargo());
            //probar metodo put
            var result = await _controller.ActualizarCargo(cargoDto, 5);
            // validar statusCode

            Assert.IsType<NotFoundObjectResult>(result);
        }

        /// <summary>
        /// Actualizar Cargo con Excepciones
        /// </summary>
        [Fact(DisplayName = "Actualizar Cargo con Excepciones")]
        public async void PutCargoExceptionControllerTest()
        {

            var dto = new CargoDTO() { Id = 1, Nombre = "Presidente", TipoCargoId = 1 };
            var prueba = new Cargo() { id = 1, nombre = "Gerente", tipoCargoId = 1 };

            _servicesMock.Setup(x => x.ActualizarCargoDAO(prueba, 1)).ReturnsAsync(prueba);

            await Assert.ThrowsAsync<NullReferenceException>(() => _controller.ActualizarCargo(dto, 1));
        }


        /// <summary>
        /// Eliminar un cargo con id = 0
        /// </summary>
        [Fact(DisplayName = "Id menor a 0 Eliminar Cargo")]
        public async void DeleteIdMenor0CargoControllerTest()
        {
            var result = await _controller.EliminarCargo(0);

            Assert.IsType<BadRequestObjectResult>(result);
        }


        /// <summary>
        /// No existe el cargo a eliminar
        /// </summary>
        [Fact(DisplayName = "No existe Cargo a Eliminar")]
        public async void DeleteNoExisteCargoControllerTest()
        {
            _servicesMock.Setup(x => x.EliminarCargoDAO(5)).ReturnsAsync(new NotFoundResult());

            var result = await _controller.EliminarCargo(5);

            Assert.IsType<NotFoundResult>(result);
        }


        ///// <summary>
        ///// Eliminar cargo con excepciones
        ///// </summary>
        [Fact(DisplayName = "Eliminar cargo con excepciones")]
        public async void DeleteCargoExceptionControllerTest()
        {
            _servicesMock.Setup(x => x.EliminarCargoDAO(5)).Throws(new NullReferenceException());

            await Assert.ThrowsAsync<NullReferenceException>(() => _controller.EliminarCargo(5));

        }

        #endregion



    }
}
