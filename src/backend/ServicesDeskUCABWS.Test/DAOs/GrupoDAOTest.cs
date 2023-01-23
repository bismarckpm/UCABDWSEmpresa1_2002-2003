using Microsoft.EntityFrameworkCore;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Database;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using Bogus;
using Moq;
using GrupoDAO = ServicesDeskUCABWS.Persistence.DAO.Implementations.GrupoDAO;
using Microsoft.Extensions.Logging.Abstractions;
using ServicesDeskUCABWS.Test.Configuraciones;
using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCABWS.Exceptions;
using ServicesDeskUCABWS.Test.DataSeed;

namespace ServicesDeskUCABWS.Test.DAOs
{

    public class GrupoDAOTest : BasePrueba
    {
        private readonly GrupoDAO _dao;
        private readonly Mock<IMigrationDbContext> _contextMock;
        private readonly Mock<IGrupoDAO> _servicesMock;




        public GrupoDAOTest()
        {
            // preparacion de los mocks
            var faker = new Faker();
            _contextMock = new Mock<IMigrationDbContext>();
            var _logger = new NullLogger<GrupoDAO>();
            var _mapper = ConfigurarAutoMapper();
            _dao = new GrupoDAO(_mapper, _contextMock.Object, _logger);
            _servicesMock = new Mock<IGrupoDAO>();
            _contextMock.SetupDbContextData();
        }

        [Fact(DisplayName = "Crear un Grupo")]
        public async Task CrearGrupoTest()
        {
            // preparacion de los datos
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            _contextMock.Setup(e => e.Departamentos.FindAsync(It.IsAny<int>()))
            .ReturnsAsync(new Departamento()
            {
                id = 1,
                nombre = "Prueba",
            });
            var Grupo = new Grupo()
            {
                id = 1,
                nombre = "Nueva",
                departamentoid = 1
            };

            //prueba de la funcion
            var result = await _dao.AgregarGrupoDAO(Grupo);

            //verificacion de la prueba
            Assert.IsType<GrupoDTO>(result);
        }

        [Fact(DisplayName = "El departamento no existe")]
        public async Task DepartamentoNoExisteTest()
        {
            // preparacion de los datos
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            _contextMock.Setup(e => e.Departamentos.FindAsync(It.IsAny<int>()))
            .ReturnsAsync(null as Departamento);
            var Grupo = new Grupo()
            {
                id = 1,
                nombre = "Nueva",
                departamentoid = 4
            };

            //verificacion de la prueba
            await Assert.ThrowsAsync<GrupoException>(() => _dao.AgregarGrupoDAO(Grupo));
        }

        [Fact(DisplayName = "Crear Grupo con excepcion")]
        public async Task CrearGrupoExcepcionTest()
        {
            // preparacion de los datos
            _contextMock.Setup(e => e.Departamentos.FindAsync(It.IsAny<int>()))
            .ReturnsAsync(new Departamento()
            {
                id = 1,
                nombre = "Prueba"
            });
            // preparacion de los datos
            _contextMock.Setup(x => x.DbContext.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new DbUpdateException());

            // prueba de la funcion
            await Assert.ThrowsAsync<GrupoException>(() => _dao!.AgregarGrupoDAO(new Grupo()));
        }

        [Fact(DisplayName = "Consultar lista Grupos")]
        public async Task ConsultarListGruposTest()
        {

            //prueba de la funcion
            var result = await _dao.ObtenerGruposDAO();

            //verificacion de la prueba
            Assert.IsType<List<GrupoResponseDTO>>(result);
        }

        [Fact(DisplayName = "Consultar lista Grupos con Excepcion")]
        public async Task ConsultarListGruposTestException()
        {
            // preparacion de los datos
            _contextMock.Setup(c => c.Grupo).Throws(new Exception());

            // prueba de la funcion
            await Assert.ThrowsAsync<GrupoException>(() => _dao.ObtenerGruposDAO());
        }

        [Fact(DisplayName = "Consultar Grupo por Id")]
        public async Task ConsultarGrupoIdTest()
        {
            // preparacion de los datos
            _contextMock.Setup(e => e.Grupo.FindAsync(It.IsAny<int>()))
            .ReturnsAsync(It.IsAny<Grupo>());


            var id = 1;
            // prueba de la funcion
            var result = await _dao.ObtenerGrupoByIdDAO(id);


            // verificacion de la prueba
            Assert.IsType<GrupoResponseDTO>(result);
            Assert.Equal(id, result.id);
        }

        [Fact(DisplayName = "Consultar Grupo por Id que no existe")]
        public async Task ConsultarGrupoIdNoExisteTest()
        {
            // preparacion de los datos
            // no obtener ninguna Grupo
            _contextMock.Setup(e => e.Grupo.FindAsync(It.IsAny<int>())).ReturnsAsync(null as Grupo);
            var id = 4;
            // verificacion de la prueba
            await Assert.ThrowsAsync<GrupoException>(() => _dao.ObtenerGrupoByIdDAO(id));
        }

        [Fact(DisplayName = "Consultar Grupo por Id con Excepcion")]
        public async Task ConsultarGrupoIdExcepcionTest()
        {
            // preparacion de los datos
            _servicesMock.Setup(c => c.ObtenerGrupoByIdDAO(It.IsAny<int>()))
                 .Throws(new Exception());


            // prueba de la funcion
            await Assert.ThrowsAsync<GrupoException>(() => _dao.ObtenerGrupoByIdDAO(-1));
        }


        [Fact(DisplayName = "Actualizar un Grupo")]
        public async Task ActualizarGrupoTest()
        {
            // preparacion de los datos
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            _contextMock.Setup(e => e.Grupo.FindAsync(It.IsAny<int>())).ReturnsAsync(new Grupo()
            {
                id = 1,
                nombre = "Prueba",
                departamentoid = 1
            });
            _contextMock.Setup(e => e.Departamentos.FindAsync(It.IsAny<int>())).ReturnsAsync(new Departamento()
            {
                id = 1,
                nombre = "Prueba"
            });
            var Grupo = new Grupo()
            {
                id = 1,
                nombre = "Test",
                departamentoid = 1
            };
            // prueba de la funcion
            var result = await _dao.ActualizarGrupoDAO(Grupo, Grupo.id);
            // verificacion de la prueba
            Assert.IsType<GrupoDTO>(result);
        }

        [Fact(DisplayName = "Actualizar Grupo con excepcion")]
        public async Task ActualizarGrupoExcepcionTest()
        {
            // preparacion de los datos
            _contextMock.Setup(e => e.Grupo.FindAsync(It.IsAny<int>())).ReturnsAsync(new Grupo()
            {
                id = 1,
                nombre = "Prueba",
                departamentoid = 1
            });
            _contextMock.Setup(e => e.Departamentos.FindAsync(It.IsAny<int>())).ReturnsAsync(new Departamento()
            {
                id = 1,
                nombre = "Prueba"
            });
            // preparacion de los datos
            _contextMock.Setup(x => x.DbContext.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new DbUpdateException());

            // prueba de la funcion
            await Assert.ThrowsAsync<GrupoException>(() => _dao!.ActualizarGrupoDAO(new Grupo(), 1));
        }

        [Fact(DisplayName = "Grupo no encontrado para actualizar")]
        public async Task ActualizarGrupoNoEncontradoTest()
        {
            // preparacion de los datos
            // no obtener ninguna Grupo
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            _contextMock.Setup(e => e.Grupo.FindAsync(It.IsAny<int>())).ReturnsAsync(null as Grupo);
            Grupo grupo = new Grupo();
            // prueba de la funcion
            //var result = await _dao.ActualizarGrupoDAO(grupo, grupo.id);
            // verificacion de la prueba
            await Assert.ThrowsAsync<GrupoException>(() => _dao.ActualizarGrupoDAO(grupo, grupo.id));
        }

        [Fact(DisplayName = "Eliminar Grupo")]
        public async Task EliminarGrupoTest()
        {
            // preparacion de los datos
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            _contextMock.Setup(e => e.Grupo.FindAsync(It.IsAny<int>())).ReturnsAsync(new Grupo()
            {
                id = 1,
                nombre = "Prueba",
                departamentoid = 1
            });
            Boolean expected = true;
            var id = 1;
            // prueba de la funcion
            Boolean result = await _dao.EliminarGrupoDAO(id);
            // verificacion de la prueba
            Assert.Equal<Boolean>(expected, result);
        }

        [Fact(DisplayName = "Eliminar Grupo con excepcion")]
        public async Task EliminarGrupoExcepcionTest()
        {
            // preparacion de los datos
            _contextMock.Setup(e => e.Grupo.FindAsync(It.IsAny<int>())).ReturnsAsync(new Grupo()
            {
                id = 1,
                nombre = "Prueba",
                departamentoid = 1
            });
            // preparacion de los datos
            _contextMock.Setup(x => x.DbContext.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new DbUpdateException());

            // prueba de la funcion
            await Assert.ThrowsAsync<GrupoException>(() => _dao!.EliminarGrupoDAO(1));
        }

        [Fact(DisplayName = "Grupo no encontrado para eliminar")]
        public async Task EliminarGrupoNoEncontradoTest()
        {
            // preparacion de los datos
            // no obtener ninguna Grupo
            _contextMock.Setup(e => e.Grupo.FindAsync(It.IsAny<int>())).ReturnsAsync(null as Grupo);
            var id = 4;
            Boolean expected = false;
            Boolean result = await _dao.EliminarGrupoDAO(id);
            // prueba de la funcion
            Assert.Equal<Boolean>(expected, result);
        }



    }
}
