using Microsoft.EntityFrameworkCore;
using ServicesDeskUCABWS.Persistence.DAO.Implementations;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Database;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Test.DataSeed;
using System.Threading.Tasks;
using System.Linq;
using Bogus;
using Xunit;
using Moq;
using CategoriaDAO = ServicesDeskUCABWS.Persistence.DAO.Implementations.CategoriaDAO;

namespace ServicesDeskUCABWS.Test.DAOs
{
    public class CategoriaDAOTest
    {
        private readonly CategoriaDAO _dao;
        private readonly Mock<IMigrationDbContext> _contextMock;
        private readonly Mock<ICategoriaDAO> _servicesMock;
        public CategoriaDAOTest()
        {
            var faker = new Faker();
            _contextMock = new Mock<IMigrationDbContext>();
            _dao = new CategoriaDAO(_contextMock.Object);
            _servicesMock = new Mock<ICategoriaDAO>();
            _contextMock.SetupDbContextData();
        }

        [Theory(DisplayName = "Crear una Categoria")]
        [InlineData("Prueba")]
        public Task CrearCategoriaTest(string Nombre)
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);

            var categoria = new Categoria()
            {
                id = 1,
                nombre = Nombre
            };

            var result = _dao.AgregarCategoriaDAO(categoria);

            Assert.IsType<CategoriaDTO>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Valida una excepcion CrearCategoria")]
        public Task CrearCategoriaExceptionTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Throws(new DbUpdateConcurrencyException());
            var cat = new Categoria();

            Assert.Throws<Exception>(() => _dao!.AgregarCategoriaDAO(cat));
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Consultar lista Categorias")]
        public Task ConsultarListCategoriasTest()
        {
            List<CategoriaDTO> listaDto = _dao.ConsultarTodosCategoriasDAO();
            var result = listaDto;

            Assert.IsType<List<CategoriaDTO>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Validar lista fuera de rango")]
        public Task ConsultarListCategoriaExceptionTest()
        {
            _servicesMock.Setup(c => c.ConsultarTodosCategoriasDAO()).Throws(new Exception("", new Exception()));
            var result = _dao.ConsultarTodosCategoriasDAO();

            Assert.Throws<System.ArgumentOutOfRangeException>(() => result[6]);
            return Task.CompletedTask;
        }

        [Theory(DisplayName = "Valida Actualizar Categoria")]
        [InlineData("probado")]
        public Task ActualizarCategoriaTest(string nombre)
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);

            var cat = new Categoria()
            {
                id = 2,
                nombre = nombre
            };

            var result = _dao.ActualizarCategoriaDAO(cat);

            Assert.IsType<CategoriaDTO>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Valida no Actualizar Categoria")]
        public Task ActualizarCategoriaTestException()
        {
            _servicesMock.Setup(c => c.ActualizarCategoriaDAO(It.IsAny<Categoria>()))
            .Throws(new Exception());

            Assert.Throws<NullReferenceException>(() => _dao.ActualizarCategoriaDAO(null!));
            return Task.CompletedTask;
        }

        [Theory(DisplayName = "Valida Eliminar Categoria")]
        [InlineData(1)]
        public Task EliminarCategoriaTest(int id)
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);

            var result = _dao.EliminarCategoriaDAO(id);

            Assert.NotEqual(null, result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Valida no Eliminar Categoria")]
        public Task EliminarCategoriaTestException()
        {
            _servicesMock.Setup(c => c.ActualizarCategoriaDAO(It.IsAny<Categoria>()))
             .Throws(new Exception());

            Assert.Throws<Exception>(() => _dao.EliminarCategoriaDAO(-1));
            return Task.CompletedTask;
        }
    }
}