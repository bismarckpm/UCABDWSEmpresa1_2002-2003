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



    }
}
