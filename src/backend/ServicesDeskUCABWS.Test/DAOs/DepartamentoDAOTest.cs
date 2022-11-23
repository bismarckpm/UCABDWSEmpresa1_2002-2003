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
using DepartamentoDAO = ServicesDeskUCABWS.Persistence.DAO.Implementations.DepartamentoDAO;

namespace ServicesDeskUCABWS.Test.DAOs
{
    public class DepartamentoDAOTest
    {
        private readonly DepartamentoDAO _dao;
        private readonly Mock<IMigrationDbContext> _contextMock;
        private readonly Mock<IDepartamentoDAO> _servicesMock;
        public DepartamentoDAOTest()
        {
            var faker = new Faker();
            _contextMock = new Mock<IMigrationDbContext>();
            _dao = new DepartamentoDAO(_contextMock.Object);
            _servicesMock = new Mock<IDepartamentoDAO>();
            _contextMock.SetupDbContextData();
        }

        [Fact(DisplayName = "Crear Departamento")]
        public Task CrearDepartamentoTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);

            var departamento = new Departamento()
            {
                id = 1,
                nombre = "departamento1"
            };

            var result = _dao.AgregarDepartamentoDAO(departamento);

            Assert.IsType<DepartamentoDTO>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Valida create departamento con Excepcion")]
        public Task CrearDepartamentoExceptionTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Throws(new DbUpdateConcurrencyException());
            var departamento = new Departamento();

            Assert.Throws<Exception>(() => _dao!.AgregarDepartamentoDAO(departamento));
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Consultar lista Departamentos")]
        public Task ConsultarDepartamentoTest()
        {
            List<DepartamentoDTO> listaDto = _dao.ConsultarDepartamentosDAO();
            var result = listaDto;

            Assert.IsType<List<DepartamentoDTO>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Validar consulta lista departamento excepcion")]
        public Task ConsultarDepartamentoExceptionTest()
        {
            _contextMock.Setup(c => c.Departamentos).Throws(new Exception());

            Assert.Throws<Exception>(() => _dao!.ConsultarDepartamentosDAO());
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Valida modificacion departamento")]
        public Task ActualizarDepartamentoTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);

            var departamento = new Departamento()
            {
                id = 1,
                nombre = "departamento1"
            };

            var result = _dao.ModificarDepartamentoDAO(departamento);

            Assert.IsType<DepartamentoDTO>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Valida modificacion departamento excepcion")]
        public Task ActualizarDepartamentoTestException()
        {
            _servicesMock.Setup(c => c.ModificarDepartamentoDAO(It.IsAny<Departamento>()))
            .Throws(new Exception());

            Assert.Throws<Exception>(() => _dao.ModificarDepartamentoDAO(null!));
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Valida Eliminar Departamento")]
        public Task EliminarDepartamentoTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);

            var result = _dao.EliminarDepartamentoDAO(1);

            Assert.IsType<DepartamentoDTO>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Valida eliminar departamento excepcion")]
        public Task EliminarDepartamentoTestException()
        {
            _servicesMock.Setup(c => c.EliminarDepartamentoDAO(It.IsAny<int>()))
             .Throws(new Exception());

            Assert.Throws<Exception>(() => _dao.EliminarDepartamentoDAO(-1));
            return Task.CompletedTask;
        }

    }
}
