using Bogus;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.DAO.Implementations;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Database;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.Test.DataSeed;
using PrioridadDAO = ServicesDeskUCABWS.Persistence.DAO.Implementations.PrioridadDAO;

namespace ServicesDeskUCABWS.Test.DAOs
{
    public class PrioridadDAOTest
    {
        private readonly PrioridadDAO _dao;
        private readonly Mock<IMigrationDbContext> _contextMock;
        private readonly Mock<IPrioridadDAO> _servicesMock;
        public PrioridadDAOTest()
        {
            var faker = new Faker();
            _contextMock = new Mock<IMigrationDbContext>();
            _dao = new PrioridadDAO(_contextMock.Object);
            _servicesMock = new Mock<IPrioridadDAO>();
            _contextMock.SetupDbContextData();
        }

        [Fact(DisplayName = "Crear Prioridad")]
        public Task CrearPrioridadTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);

            var prioridad = new Prioridad()
            {
                id = 1,
                nombre = "Alta"
            };

            var result = _dao.AgregarPrioridadDAO(prioridad);

            Assert.IsType<PrioridadDTO>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Valida create prioridad con Excepcion")]
        public Task CrearPrioridadExceptionTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Throws(new DbUpdateConcurrencyException());
            var prioridad = new Prioridad();

            Assert.Throws<Exception>(() => _dao!.AgregarPrioridadDAO(prioridad));
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Consultar lista Prioridades")]
        public Task ConsultarPrioridadTest()
        {
            List<PrioridadDTO> listaDto = _dao.ConsultarTodosPrioridadesDAO();
            var result = listaDto;

            Assert.IsType<List<PrioridadDTO>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Validar consulta lista prioridad excepcion")]
        public Task ConsultarPrioridadExceptionTest()
        {
            _contextMock.Setup(c => c.Prioridades).Throws(new Exception());

            Assert.Throws<Exception>(() => _dao!.ConsultarTodosPrioridadesDAO());
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Valida modificacion prioridad")]
        public Task ActualizarPrioridadTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);

            var prioridad = new Prioridad()
            {
                id = 1,
                nombre = "Alta"
            };

            var result = _dao.ActualizarPrioridadDAO(prioridad);

            Assert.IsType<PrioridadDTO>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Valida modificacion prioridad excepcion")]
        public Task ActualizarPrioridadTestException()
        {
            _servicesMock.Setup(c => c.ActualizarPrioridadDAO(It.IsAny<Prioridad>()))
            .Throws(new Exception());

            Assert.Throws<Exception>(() => _dao.ActualizarPrioridadDAO(null!));
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Valida Eliminar Prioridad")]
        public Task EliminarPrioridadTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);

            var result = _dao.EliminarPrioridadDAO(1);

            Assert.IsType<PrioridadDTO>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Valida eliminar prioridad excepcion")]
        public Task EliminarPrioridadTestException()
        {
            _servicesMock.Setup(c => c.EliminarPrioridadDAO(It.IsAny<int>()))
             .Throws(new Exception());

            Assert.Throws<Exception>(() => _dao.EliminarPrioridadDAO(-1));
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Consultar prioridad por id")]
        public Task ConsultarPrioridadIdTest()
        {
            PrioridadDTO dto = _dao.ConsultaPrioridadDAO(1);
            var result = dto;

            Assert.IsType<PrioridadDTO>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Valida consulta prioridad por id Excepcion")]
        public Task ConsultarPrioridadIdTestException()
        {
            _servicesMock.Setup(c => c.ConsultaPrioridadDAO(It.IsAny<int>()))
                .Throws(new Exception());
            var result = _dao.ConsultarTodosPrioridadesDAO();

            Assert.Throws<Exception>(() => _dao.ConsultaPrioridadDAO(-1));
            return Task.CompletedTask;
        }

    }
}
