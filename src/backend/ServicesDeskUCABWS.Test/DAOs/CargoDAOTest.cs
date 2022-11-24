using Bogus;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Controllers;
using ServicesDeskUCABWS.Persistence.DAO.Implementations;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Database;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.Test.Configuraciones;
using ServicesDeskUCABWS.Test.DataSeed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesDeskUCABWS.Test.DAOs
{
    public class CargoDAOTest : BasePrueba
    {
        private readonly CargoDAO _dao;
        private readonly Mock<IMigrationDbContext> _contextMock;
        private readonly Mock<ICargoDAO> _servicesMock;


        public CargoDAOTest()
        {
            var faker = new Faker();
            _contextMock = new Mock<IMigrationDbContext>();
            var _logger = new NullLogger<CargoDAO>();
            var _mapper = ConfigurarAutoMapper();
            _dao = new CargoDAO(_mapper, _logger, _contextMock.Object);
            _servicesMock = new Mock<ICargoDAO>();
            _contextMock.SetupDbContextData();
        }
    }

    [Fact(DisplayName = "Crear un Cargo")]
    public async Task CrearCargoTest()
    {
        // preparacion de los datos
        _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
        var cargo = new Cargo()
        {
            id = 1,
            nombre = "Gerente",
            tipoCargoId = 1
        };

        // prueba de la funcion
        var result = await _dao.AgregarCargoDAO(cargo);
        var cargoResult = result.Value;

        // verificacion de la prueba
        Assert.IsType<CargoDTO>(cargoResult);
    }

    
}
