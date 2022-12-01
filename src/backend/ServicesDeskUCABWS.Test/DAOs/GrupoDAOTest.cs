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
using GrupoDAO = ServicesDeskUCABWS.Persistence.DAO.Implementations.GrupoDAO;

namespace ServicesDeskUCABWS.Test.DAOs
{
   public class GrupoDAOTest
    {
        private readonly GrupoDAO _dao;
        private readonly Mock<IMigrationDbContext> _contextMock;
        private readonly Mock<IGrupoDAO> _serviceMock;

        public GrupoDAOTest()
        {
            var faker = new Faker();
            _contextMock = new Mock<IMigrationDbContext>();
            _dao = new GrupoDAO(_contextMock.Object);
            _serviceMock = new Mock<IGrupoDAO>();
            _contextMock.SetupDbContextData();
        }

        [Fact(DisplayName ="Crear Grupo")]
        public Task CrearGrupoTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);

            var grupo = new Grupo()
            {
                id = 1,
                nombre = "Grupo5",
                departamentoid = 1
            };

            var result = _dao.AgregarGrupoDAO(grupo);

           Assert.IsType<GrupoDTO>(result);
            return Task.CompletedTask;
        }
        [Fact(DisplayName ="Valida create Grupo con excepcion")]

        public Task CrearGrupoExceptionTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Throws(new DbUpdateConcurrencyException());
            var grupo = new Grupo();

            Assert.Throws<Exception>(() => _dao!.AgregarGrupoDAO(grupo));
            return Task.CompletedTask;
        }

         [Fact(DisplayName ="Consultar Lista de Grupo")]
        public Task ConsultarGrupoTest()
        {
            List<GrupoDTO> listaDto = _dao.ConsultarGrupoDAO();
                
                var result = listaDto;

            Assert.IsType<List<GrupoDTO>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Validar consulta lista Grupo excepcion")]
        public Task ConsultarGrupoExceptionTest()
        {
            _contextMock.Setup(c => c.Grupo).Throws(new Exception());

            Assert.Throws<Exception>(() => _dao!.ConsultarGrupoDAO());
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Valida modificacion Grupo")]
        public Task ActualizarGrupoTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);

            var grupo = new Grupo()
            {
                id = 1,
                nombre = "Grupo5",
                departamentoid = 1                
            };

            var result = _dao.ActualizarGrupoDAO(grupo);

            Assert.IsType<GrupoDTO>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Valida Actualizar Grupo excepcion")]
        public Task ActualizarGrupoTestException()
        {
            _serviceMock.Setup(c => c.ActualizarGrupoDAO(It.IsAny<Grupo>()))
            .Throws(new Exception());

            Assert.Throws<Exception>(() => _dao.ActualizarGrupoDAO(null!));
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Valida Eliminar Grupo")]
        public Task EliminarGrupoTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);

            var result = _dao.EliminarGrupoDAO(1);

            Assert.IsType<GrupoDTO>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Valida eliminar Grupo excepcion")]
        public Task EliminarGrupoTestException()
        {
            _serviceMock.Setup(c => c.EliminarGrupoDAO(It.IsAny<int>()))
             .Throws(new Exception());

            Assert.Throws<Exception>(() => _dao.EliminarGrupoDAO(-1));
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Consultar Grupo por id")]
        public Task ConsultarGrupoIdTest()
        {
            GrupoDTO dto = _dao.ConsultaGrupoIdDAO(1);
            var result = dto;

            Assert.IsType<GrupoDTO>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Valida consulta Grupo por id Excepcion")]
        public Task ConsultarGrupoIdTestException()
        {
            _serviceMock.Setup(c => c.ConsultaGrupoIdDAO(It.IsAny<int>()))
                .Throws(new Exception());
            var result = _dao.ConsultarGrupoDAO();

            Assert.Throws<Exception>(() => _dao.ConsultaGrupoIdDAO(-1));
            return Task.CompletedTask;
        }

    }
}
