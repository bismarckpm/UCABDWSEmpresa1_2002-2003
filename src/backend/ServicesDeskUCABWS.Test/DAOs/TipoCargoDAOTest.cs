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
using ServicesDeskUCABWS.Exceptions;

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

        #region Casos Existosos

            [Fact (DisplayName ="Crear un Tipo de Cargo")]
            public Task CrearTipoCargoTest()
            {
                _contextMock.Setup(x=>x.DbContext.SaveChanges()).Returns(1);
                
                var tipocargo = new TipoCargo()
                {
                    id = 1,
                    nombre = "Carlos"
                };
                
                var result = _dao.AgregarTipoCargoDAO(tipocargo);
            
                Assert.IsType<TipoCargoDTO>(result);
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

        [Fact(DisplayName = "Valida Actualizar Tipo Cargo")]
         public Task ActualizarTipoCargoTest()
         {
            _contextMock.Setup(x=>x.DbContext.SaveChanges()).Returns(1);
            
            var tipocargo = new TipoCargo()
            {
                id = 1,
                nombre = "Pedro"
            };
            
            var result = _dao.ActualizarTipoCargoDAO(tipocargo);

            Assert.IsType<TipoCargoDTO>(result);
            return Task.CompletedTask;
         }

        [Fact(DisplayName = "Eliminar Tipo Cargo")]
        public Task EliminarTipoCargoTest()
        {
            _contextMock.Setup(x=>x.DbContext.SaveChanges()).Returns(1);

            var result = _dao.EliminarTipoCargoDAO(1);

            Assert.IsType<TipoCargoDTO>(result);
            return Task.CompletedTask;
        }         

        #endregion

        #region  Casos Particulares

        [Fact(DisplayName ="Valida una excepcion CrearTipoCargo")]
        public Task CrearTipoCargoExceptionTest()
        {
            _contextMock.Setup(x=>x.DbContext.SaveChanges())
                            .Throws(new Exception());
            var tipo = new TipoCargo();
            
            Assert.Throws<ServicesDeskUcabWsException>(()=> _dao!.AgregarTipoCargoDAO(tipo));
            return Task.CompletedTask;
        }

        [Fact(DisplayName= "Validar lista excepcion")]
         public Task ConsultarListTipoCargoExceptionTest()
         {
            _contextMock.Setup(c => c.TipoCargos).Throws(new Exception());

            Assert.Throws<ServicesDeskUcabWsException>(() => _dao!.ConsultarTipoCargoDAO());
            return Task.CompletedTask;
        }

        [Fact(DisplayName="Actualizar Tipo Cargo con excepcion")]
         public Task ActualizarTipoCargoTestException()
         {
             _contextMock.Setup(e =>e.DbContext.SaveChanges())
                .Throws(new Exception("", new NullReferenceException()));
           
            Assert.Throws<NullReferenceException>(()=>_dao.ActualizarTipoCargoDAO(It.IsAny<TipoCargo>()));
            return Task.CompletedTask;
         }

        [Fact(DisplayName="Valida no Eliminar Tipo Cargo")]
        public Task EliminarTipoCargoTestException()
        {
            _servicesMock.Setup(c=>c.EliminarTipoCargoDAO(It.IsAny<int>()))
             .Throws(new Exception());

            Assert.Throws<ServicesDeskUcabWsException>(()=>_dao.EliminarTipoCargoDAO(-1));
            return Task.CompletedTask;
        }

        #endregion




    }
}