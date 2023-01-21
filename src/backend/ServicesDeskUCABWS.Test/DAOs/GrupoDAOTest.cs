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

        // public GrupoDAOTest()
        // {
        //     var faker = new Faker();
        //     _contextMock = new Mock<IMigrationDbContext>();
        //     _dao = new GrupoDAO(_contextMock.Object);
        //     _serviceMock = new Mock<IGrupoDAO>();
        //     _contextMock.SetupDbContextData();
        // }

        // [Fact(DisplayName ="Crear Grupo")]
        // public Task CrearGrupoTest()
        // {
        //     _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);

        //     var grupo = new Grupo()
        //     {
        //         id = 1,
        //         nombre = "Grupo5",
        //         departamentoid = 1
        //     };

        //     var result = _dao.AgregarGrupo(grupo);

        //    Assert.IsType<GrupoDTO>(result);
        //     return Task.CompletedTask;
        // }
        // [Fact(DisplayName ="Valida create Grupo con excepcion")]

        // public Task CrearGrupoExceptionTest()
        // {
        //     _contextMock.Setup(x => x.DbContext.SaveChanges()).Throws(new DbUpdateConcurrencyException());
        //     var grupo = new Grupo();

        //     Assert.Throws<NullReferenceException>(() => _dao.ActualizarGrupo(null!));
        //     return Task.CompletedTask;                        
        // }

        //  [Fact(DisplayName ="Consultar Lista de Grupo")]
        // public Task ConsultarGrupoTest()
        // {
        //     List<GrupoDTO> listaDto = _dao.ConsultarGrupo();
                
        //         var result = listaDto;

        //     Assert.IsType<List<GrupoDTO>>(result);
        //     return Task.CompletedTask;
        // }

        // [Fact(DisplayName = "Validar consulta lista Grupo excepcion")]
        // public Task ConsultarGrupoExceptionTest()
        // {
        //     _contextMock.Setup(c => c.Grupo).Throws(new Exception());

        //     Assert.Throws<Exception>(() => _dao!.ConsultarGrupo());
        //     return Task.CompletedTask;
        // }

        // [Fact(DisplayName = "Valida modificacion Grupo")]
        // public Task ActualizarGrupoTest()
        // {
        //     _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);

        //     var grupo = new Grupo()
        //     {
        //         id = 1,
        //         nombre = "Grupo5",
        //         departamentoid = 1                
        //     };

        //     var result = _dao.ActualizarGrupo(grupo);

        //     Assert.IsType<GrupoDTO>(result);
        //     return Task.CompletedTask;
        // }

        // [Fact(DisplayName = "Valida Actualizar Grupo excepcion")]
        // public Task ActualizarGrupoTestException()
        // {
        //     _serviceMock.Setup(c => c.ActualizarGrupo(It.IsAny<Grupo>()))
        //     .Throws(new Exception());


        //     Assert.Throws<NullReferenceException>(() => _dao.ActualizarGrupo(null!));
        //     return Task.CompletedTask;
        // }

        // [Fact(DisplayName = "Valida Eliminar Grupo")]
        // public Task EliminarGrupoTest()
        // {
        //     _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);

        //     var result = _dao.EliminarGrupo(1);

        //     Assert.IsType<GrupoDTO>(result);
        //     return Task.CompletedTask;
        // }

        // [Fact(DisplayName = "Valida eliminar Grupo excepcion")]
        // public Task EliminarGrupoTestException()
        // {
        //     _serviceMock.Setup(c => c.EliminarGrupo(It.IsAny<int>()))
        //      .Throws(new Exception());

        //     Assert.Throws<Exception>(() => _dao.EliminarGrupo(-1));
        //     return Task.CompletedTask;
        // }

       
    }
}
