using Bogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Controllers;
using ServicesDeskUCABWS.Exceptions;
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
using Xunit.Sdk;

namespace ServicesDeskUCABWS.Test.DAOs
{
    public class GrupoDAOTest : BasePrueba
    {
        private readonly GrupoDAO _dao;
        private readonly Mock<IMigrationDbContext> _contextMock;
        private readonly Mock<IGrupoDAO> _servicesMock;


        public GrupoDAOTest()
        {
            var faker = new Faker();
            _contextMock = new Mock<IMigrationDbContext>();
            var _logger = new NullLogger<GrupoDAO>();
            var _mapper = ConfigurarAutoMapper();
            _dao = new GrupoDAO(_mapper, _logger, _contextMock.Object);
            _servicesMock = new Mock<IGrupoDAO>();
            _contextMock.SetupDbContextData();
        }

        #region CASOS DE EXITOS

        //Test para Crear Grupo
        [Fact(DisplayName = "Crear un Grupo")]
        public async Task CrearGrupoTest()
        {
            // preparacion de los datos
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            var grupo = new Grupo()
            {
                id = 1,
                nombre = "Grupo de Finanzas",
                departamentoid = 1
            };

            var result = await _dao.AgregarGrupoDAO(grupo);
            var grupoResult = result.Value;

            Assert.IsType<GrupoDTO>(grupoResult);
        }

        //Test para Consultar todos los Grupos
        [Fact(DisplayName = "Consultar todos los grupos")]
        public async Task ConsultarListaGruposTest()
        {
            var result = await _dao.ConsultarGrupoDAO();

            Assert.IsType<List<Grupo>>(result);
            Assert.Equal(3, result.Count);
        }

        //Test para consultar un grupo por su Id
        [Fact(DisplayName = "Consultar Grupo por Id")]
        public async Task ConsultarGrupoByIdTest()
        {
            _contextMock.Setup(e => e.Grupo.FindAsync(It.IsAny<int>()))
            .ReturnsAsync(new Grupo()
            {
                id = 1,
                nombre = "Grupo de Finanzas",
                departamentoid = 1
            });

            var id = 1;
            var result = await _dao.ConsultarGrupoByIdDAO(id);
            var grupoResult = result.Value;

            Assert.IsType<Grupo>(grupoResult);
            Assert.Equal(id, grupoResult!.id);
        }

        //Test para Actualizar un Grupo
        [Fact(DisplayName = "Actualizar un Grupo")]
        public async Task ActualizarGrupoTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            _contextMock.Setup(e => e.Grupo.FindAsync(It.IsAny<int>())).ReturnsAsync(new Grupo()
            {
                id = 1,
                nombre = "Prueba",
                departamentoid = 1
            });
            var grupo = new Grupo()
            {
                id = 1,
                nombre = "Modificado",
                departamentoid = 1
            };

            var result = await _dao.ActualizarGrupoDAO(grupo, grupo.id);
            var grupoResult = result.Value;

            Assert.IsType<Grupo>(grupoResult);
        }

        //Test para Eliminar un Grupo
        [Fact(DisplayName = "Eliminar un Grupo")]
        public async Task EliminarGrupoTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            _contextMock.Setup(e => e.Grupo.FindAsync(It.IsAny<int>())).ReturnsAsync(new Grupo()
            {
                id = 1,
                nombre = "Probar",
                departamentoid = 1
            });
            var id = 1;
            var result = await _dao.EliminarGrupoDAO(id);

            Assert.IsType<OkResult>(result);

        }

        #endregion


        #region CASOS PARTICULARES

        //Test para crear un Grupo con excepcion
        [Fact(DisplayName = "Crear un Grupo con Excepcion")]
        public async Task CrearGrupoTestException()
        {

            _contextMock.Setup(x => x.DbContext.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());


            await Assert.ThrowsAsync<Exception>(() => _dao!.AgregarGrupoDAO(new Grupo()));
        }

        //Test para consultar los grupos con excepcion
        [Fact(DisplayName = "Consultar lista de Grupos con Excepcion")]
        public async Task ConsultarListaGruposTestException()
        {
            _contextMock.Setup(c => c.Grupo).Throws(new NullReferenceException());

            await Assert.ThrowsAsync<NullReferenceException>(() => _dao.ConsultarGrupoDAO());
        }

        //Test para consultar un grupo que no existe
        [Fact(DisplayName = "Consultar Grupo que no existe")]
        public async Task ConsultarGrupoIdNoExisteTest()
        {
            _contextMock.Setup(e => e.Grupo.FindAsync(It.IsAny<int>())).ReturnsAsync(null as Grupo);

            var id = 20;
            var result = await _dao.ConsultarGrupoByIdDAO(id);
            var grupoResult = result.Value;

            Assert.Equal(0, grupoResult!.id);
        }

        /// <summary>
        //Test para consultar un grupo por Id con Excepcion
        [Fact(DisplayName = "Consultar Grupo por Id con Excepcion")]
        public async Task ConsultarGrupoIdTestException()
        {
            _contextMock.Setup(c => c.Grupo.FindAsync(It.IsAny<int>())).ThrowsAsync(new Exception());

            await Assert.ThrowsAsync<Exception>(() => _dao.ConsultarGrupoByIdDAO(-1));
        }


        //Test para actualizar un grupo que no existe
        [Fact(DisplayName = "No existe Grupo para actualizar")]
        public async Task ActualizaGrupoNoExisteTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            _contextMock.Setup(c => c.Grupo.FindAsync(It.IsAny<int>())).ReturnsAsync(null as Grupo);

            var grupo = new Grupo();
            var result = await _dao.ActualizarGrupoDAO(grupo, grupo.id);
            var grupoResult = result.Value;

            Assert.IsType<Grupo>(grupoResult);
        }
  
        //Test para actualizar un grupo con execpcion
        [Fact(DisplayName = "Actualizar un Grupo con Excepcion")]
        public async Task ActualizarGrupoTestException()
        {
            var id = 1;
            _contextMock.Setup(x => x.DbContext.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());
            _contextMock.Setup(c => c.Grupo.FindAsync(It.IsAny<int>())).ThrowsAsync(new Exception());

            await Assert.ThrowsAsync<Exception>(() => _dao!.ActualizarGrupoDAO(new Grupo(), id));
        }

        //Test cuando no existe un grupo para eliminar
        [Fact(DisplayName = "No existe Grupo para eliminar")]
        public async Task EliminarGrupoNoExisteTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            _contextMock.Setup(e => e.Grupo.FindAsync(It.IsAny<int>())).ReturnsAsync(null as Grupo);

            var id = 1;
            var result = await _dao.EliminarGrupoDAO(id);

            Assert.IsType<NotFoundResult>(result);
        }

        //Test para eliminar un grupo con Excepcion
        [Fact(DisplayName = "Eliminar una Grupo con Excepcion")]
        public async Task EliminarGrupoTestException()
        {
            var id = 1;
            _contextMock.Setup(x => x.DbContext.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());
            _contextMock.Setup(c => c.Grupo.FindAsync(It.IsAny<int>())).ThrowsAsync(new Exception());


            await Assert.ThrowsAsync<Exception>(() => _dao!.EliminarGrupoDAO(id));
        }

        #endregion
    }
}


//using Bogus;
//using Microsoft.Data.SqlClient;
//using Microsoft.EntityFrameworkCore;
//using Moq;
//using ServicesDeskUCABWS.BussinessLogic.DTO;
//using ServicesDeskUCABWS.Persistence.DAO.Implementations;
//using ServicesDeskUCABWS.Persistence.DAO.Interface;
//using ServicesDeskUCABWS.Persistence.Database;
//using ServicesDeskUCABWS.Persistence.Entity;
//using ServicesDeskUCABWS.Test.DataSeed;
//using GrupoDAO = ServicesDeskUCABWS.Persistence.DAO.Implementations.GrupoDAO;

//namespace ServicesDeskUCABWS.Test.DAOs
//{
//public class GrupoDAOTest
//{
//private readonly GrupoDAO _dao;
//private readonly Mock<IMigrationDbContext> _contextMock;
//private readonly Mock<IGrupoDAO> _serviceMock;

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
//
//       
//    }
//}
