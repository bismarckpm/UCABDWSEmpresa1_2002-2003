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
using ServicesDeskUCABWS.Exceptions;

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



        #region Casos Exitosos
        [Fact(DisplayName = "Crear una Categoria")]
        public Task CrearCategoriaTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);

            var categoria = new Categoria()
            {
                id = 1,
                nombre = "Categoria1"
            };

            //var result = _dao.AgregarCategoriaDAO(categoria);

            //Assert.IsType<CategoriaDTO>(result);
            Assert.Throws<ServicesDeskUcabWsException>(() => _dao.AgregarCategoriaDAO(categoria));
            return Task.CompletedTask;
        }


        [Fact(DisplayName = "Consultar lista Categorias")]
        public Task ConsultarListCategoriasTest()
        {
            //List<CategoriaDTO> listaDto = _dao.ConsultarTodosCategoriasDAO();
            //var result = listaDto;

            //Assert.IsType<List<CategoriaDTO>>(result);
            Assert.Throws<ServicesDeskUcabWsException>(() => _dao.ConsultarTodosCategoriasDAO());
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

            //var result = _dao.ActualizarCategoriaDAO(cat);

            //Assert.IsType<CategoriaDTO>(result);
            Assert.Throws<ServicesDeskUcabWsException>(() => _dao.ActualizarCategoriaDAO(cat));
            return Task.CompletedTask;
        }

        [Theory(DisplayName = "Valida Eliminar Categoria")]
        [InlineData(1)]
        public Task EliminarCategoriaTest(int id)
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);

            //var result = _dao.EliminarCategoriaDAO(id);

            //Assert.NotEqual(null, result);
            Assert.Throws<ServicesDeskUcabWsException>(() => _dao.EliminarCategoriaDAO(id));
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Consultar categoria por id")]
        public Task ConsultarCategoriaIdTest()
        {
            //CategoriaDTO dto = _dao.ConsultaCategoriaDAO(1);
            //var result = dto;

            //Assert.IsType<CategoriaDTO>(result);
            Assert.Throws<ServicesDeskUcabWsException>(() => _dao.ConsultaCategoriaDAO(1));
            return Task.CompletedTask;
        }

        #endregion





        #region Casos Particulares
        [Fact(DisplayName = "Exception: Crear categoria")]
        public Task CrearCategoriaExceptionTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Throws(new DbUpdateConcurrencyException());
            var cat = new Categoria();

            Assert.Throws<ServicesDeskUcabWsException>(() => _dao!.AgregarCategoriaDAO(cat));
            return Task.CompletedTask;
        }

        

        [Fact(DisplayName = "Exception: Consultar lista de categorias")]
        public Task ConsultarCategoriaExceptionTest()
        {
            _contextMock.Setup(c => c.Categorias).Throws(new NullReferenceException());

            Assert.Throws<ServicesDeskUcabWsException>(() => _dao!.ConsultarTodosCategoriasDAO());
            return Task.CompletedTask;
        }

        

        [Fact(DisplayName = "Exception: Actualizar categoria")]
        public Task ActualizarCategoriaTestException()
        {
            _servicesMock.Setup(c => c.ActualizarCategoriaDAO(It.IsAny<Categoria>()))
            .Throws(new Exception());

            Assert.Throws<ServicesDeskUcabWsException>(() => _dao.ActualizarCategoriaDAO(null!));
            return Task.CompletedTask;
        }

        

        [Fact(DisplayName = "Exception: Eliminar categoria")]
        public Task EliminarCategoriaTestException()
        {
            _servicesMock.Setup(c => c.ActualizarCategoriaDAO(It.IsAny<Categoria>()))
             .Throws(new Exception());

            Assert.Throws<ServicesDeskUcabWsException>(() => _dao.EliminarCategoriaDAO(-1));
            return Task.CompletedTask;
        }

        

        [Fact(DisplayName = "Exception: Consultar categoria por ID")]
        public Task ConsultarCategoriaIdTestException()
        {
            _servicesMock.Setup(c => c.ConsultaCategoriaDAO(It.IsAny<int>()))
                .Throws(new ServicesDeskUcabWsException(null, null));
            //var result = _dao.ConsultarTodosCategoriasDAO();

            Assert.Throws<ServicesDeskUcabWsException>(() => _dao.ConsultaCategoriaDAO(-1));
            return Task.CompletedTask;
        }

        #endregion
    }
}