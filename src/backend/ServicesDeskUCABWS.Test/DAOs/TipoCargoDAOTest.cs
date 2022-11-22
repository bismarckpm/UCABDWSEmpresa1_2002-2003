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
using TipoCargoDAO = ServicesDeskUCABWS.Persistence.DAO.Implementations.TipoCargoDAO;

namespace ServicesDeskUCABWS.Test.DAOs
{
    public class TipoCargoDAOTest
    {
            private readonly TipoCargoDAO _dao;
            private readonly Mock<IMigrationDbContext> _contextMock;
            private readonly Mock<ITipoCargoDAO> _servicesMock;
            public TipoCargoDAOTest()
            {
                var faker = new Faker();
                _contextMock = new Mock<IMigrationDbContext>();
                _dao = new TipoCargoDAO(_contextMock.Object);
               _servicesMock = new Mock<ITipoCargoDAO>();
                _contextMock.SetupDbContextData();
            }

        [Theory (DisplayName ="Crear un Tipo de Cargo")]
        [InlineData("Prueba")]
        public Task CrearTipoCargoTest(string Nombre)
        {
            _contextMock.Setup(x=>x.DbContext.SaveChanges()).Returns(1);
            
            var tipocargo = new TipoCargo()
            {
                id = 1,
                nombre = Nombre
            };
            
            var result = _dao.AgregarTipoCargoDAO(tipocargo);
           
            Assert.IsType<TipoCargoDTO>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName ="Valida una excepcion CrearTipoCargo")]
        public Task CrearTipoCargoExceptionTest()
        {
            _contextMock.Setup(x=>x.DbContext.SaveChanges()).Throws(new DbUpdateConcurrencyException());
            var tipo = new TipoCargo();
            
            Assert.Throws<Exception>(()=> _dao!.AgregarTipoCargoDAO(tipo));
            return Task.CompletedTask;
        }

         [Fact(DisplayName= "Consultar lista Tipo de Cargos")]
         public Task ConsultarListTipoCargoTest()
         {
             List<TipoCargoDTO> listaDto = _dao.ConsultarTipoCargoDAO();
                 var result = listaDto;

                 Assert.IsType<List<TipoCargoDTO>>(result);
                 return Task.CompletedTask;
         }

        [Fact(DisplayName= "Validar lista fuera de rango")]
         public Task ConsultarListTipoCargoExceptionTest()
         {
            _servicesMock.Setup(c=>c.ConsultarTipoCargoDAO()).Throws(new Exception("", new Exception()));
            var result = _dao.ConsultarTipoCargoDAO();

                 Assert.Throws<System.ArgumentOutOfRangeException>(()=>result[6]);
                 return Task.CompletedTask;
         }

        [Theory(DisplayName = "Valida Actualizar Tipo Cargo")]
        [InlineData("Junior")]
         public Task ActualizarTipoCargoTest(string nombre)
         {
            _contextMock.Setup(x=>x.DbContext.SaveChanges()).Returns(1);
            
            var tipocargo = new TipoCargo()
            {
                id = 1,
                nombre = nombre
            };
            
            var result = _dao.ActualizarTipoCargoDAO(tipocargo);

            Assert.NotEqual<string>(nombre,result.Nombre!);
            return Task.CompletedTask;
         }

        [Fact(DisplayName="Valida no Actualizar Tipo Cargo")]
         public Task ActualizarTipoCargoTestException()
         {
             _servicesMock.Setup(c=>c.ActualizarTipoCargoDAO(It.IsAny<TipoCargo>()))
             .Throws(new Exception());
           
            Assert.Throws<NullReferenceException>(()=>_dao.ActualizarTipoCargoDAO(null!));
            return Task.CompletedTask;
         }

        [Theory(DisplayName = "Valida Eliminar Tipo Cargo")]
        [InlineData(1)]
        public Task EliminarTipoCargoTest(int id)
        {
            _contextMock.Setup(x=>x.DbContext.SaveChanges()).Returns(1);

            var result = _dao.EliminarTipoCargoDAO(id);

            Assert.NotEqual(null,result);
            return Task.CompletedTask;
        } 

        [Fact(DisplayName="Valida no Eliminar Tipo Cargo")]
        public Task EliminarTipoCargoTestException()
        {
            _servicesMock.Setup(c=>c.ActualizarTipoCargoDAO(It.IsAny<TipoCargo>()))
             .Throws(new Exception());

            Assert.Throws<Exception>(()=>_dao.EliminarTipoCargoDAO(-1));
            return Task.CompletedTask;
        }
    }
}