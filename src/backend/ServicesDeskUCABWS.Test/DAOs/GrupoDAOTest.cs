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
            var faker = new Faker();
            _contextMock = new Mock<IMigrationDbContext>();
            var _logger = new NullLogger<GrupoDAO>();
            var _mapper = ConfigurarAutoMapper();
            _dao = new GrupoDAO(_mapper, _contextMock.Object, _logger);
            _servicesMock = new Mock<IGrupoDAO>();
            _contextMock.SetupDbContextData();
        }

        //Test para crear un grupo
        [Fact(DisplayName = "Crear un Grupo")]
        public async Task CrearGrupoTest()
        {
            
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

            var result = await _dao.AgregarGrupoDAO(Grupo);

            Assert.IsType<GrupoDTO>(result);
        }

        //Test para cuando el departamento del grupo no existe
        [Fact(DisplayName = "El departamento no existe")]
        public async Task DepartamentoNoExisteTest()
        {
            
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            _contextMock.Setup(e => e.Departamentos.FindAsync(It.IsAny<int>()))
            .ReturnsAsync(null as Departamento);
            var Grupo = new Grupo()
            {
                id = 1,
                nombre = "Nueva",
                departamentoid = 4
            };

            await Assert.ThrowsAsync<GrupoException>(() => _dao.AgregarGrupoDAO(Grupo));
        }

        //Test para crear un grupo con Exception
        [Fact(DisplayName = "Crear Grupo con excepcion")]
        public async Task CrearGrupoExcepcionTest()
        {
            
            _contextMock.Setup(e => e.Departamentos.FindAsync(It.IsAny<int>()))
            .ReturnsAsync(new Departamento()
            {
                id = 1,
                nombre = "Prueba"
            });
            
            _contextMock.Setup(x => x.DbContext.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new DbUpdateException());

            await Assert.ThrowsAsync<GrupoException>(() => _dao!.AgregarGrupoDAO(new Grupo()));
        }

        //Test para consultar todos los grupos
        [Fact(DisplayName = "Consultar lista Grupos")]
        public async Task ConsultarListGruposTest()
        {

            var result = await _dao.ObtenerGruposDAO();

            Assert.IsType<List<GrupoResponseDTO>>(result);
        }

        //Test para consultar todos los grupos con Exception
        [Fact(DisplayName = "Consultar lista Grupos con Excepcion")]
        public async Task ConsultarListGruposTestException()
        {
            
            _contextMock.Setup(c => c.Grupo).Throws(new Exception());

            await Assert.ThrowsAsync<GrupoException>(() => _dao.ObtenerGruposDAO());
        }

        //Test para consultar un grupo mediante su ID
        [Fact(DisplayName = "Consultar Grupo por Id")]
        public async Task ConsultarGrupoIdTest()
        {
            
            _contextMock.Setup(e => e.Grupo.FindAsync(It.IsAny<int>()))
            .ReturnsAsync(It.IsAny<Grupo>());


            var id = 1;
            
            var result = await _dao.ObtenerGrupoByIdDAO(id);


            Assert.IsType<GrupoResponseDTO>(result);
            Assert.Equal(id, result.id);
        }

        //Test para cuando el Grupo al que buscamos mediante su ID no existe
        [Fact(DisplayName = "Consultar Grupo por Id que no existe")]
        public async Task ConsultarGrupoIdNoExisteTest()
        {
            
            _contextMock.Setup(e => e.Grupo.FindAsync(It.IsAny<int>())).ReturnsAsync(null as Grupo);
            var id = 4;
            
            await Assert.ThrowsAsync<GrupoException>(() => _dao.ObtenerGrupoByIdDAO(id));
        }

        //Test para consultar un grupo mediante su ID con exception
        [Fact(DisplayName = "Consultar Grupo por Id con Excepcion")]
        public async Task ConsultarGrupoIdExcepcionTest()
        {
           
            _servicesMock.Setup(c => c.ObtenerGrupoByIdDAO(It.IsAny<int>()))
                 .Throws(new Exception());


            await Assert.ThrowsAsync<GrupoException>(() => _dao.ObtenerGrupoByIdDAO(-1));
        }

        //Test para actualizar un grupo
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
            
            var result = await _dao.ActualizarGrupoDAO(Grupo, Grupo.id);
            
            Assert.IsType<GrupoDTO>(result);
        }

        [Fact(DisplayName = "Actualizar No existe departamento")]
        public async Task ActualizarGrupoNoExisteDepartamentoTest()
        {

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
                departamentoid = 9
            };
           _contextMock.Setup(e => e.Etiquetas.FindAsync(It.IsAny<int>())).ReturnsAsync((Etiqueta)null!);

            await Assert.ThrowsAsync<GrupoException>(() => _dao.ActualizarGrupoDAO(Grupo, Grupo.id));
        }

        //Test para actualizar un grupo con exception
        [Fact(DisplayName = "Actualizar Grupo con excepcion")]
        public async Task ActualizarGrupoExcepcionTest()
        {
            
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
            
            _contextMock.Setup(x => x.DbContext.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new DbUpdateException());

            await Assert.ThrowsAsync<GrupoException>(() => _dao!.ActualizarGrupoDAO(new Grupo(), 1));
        }

        //Test para cuando no se encuentra el grupo que se desea actualizar
        [Fact(DisplayName = "Grupo no encontrado para actualizar")]
        public async Task ActualizarGrupoNoEncontradoTest()
        {
            
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            _contextMock.Setup(e => e.Grupo.FindAsync(It.IsAny<int>())).ReturnsAsync(null as Grupo);
            Grupo grupo = new Grupo();
            
            //var result = await _dao.ActualizarGrupoDAO(grupo, grupo.id);
            
            await Assert.ThrowsAsync<GrupoException>(() => _dao.ActualizarGrupoDAO(grupo, grupo.id));
        }

        //Test para eliminar un grupo
        [Fact(DisplayName = "Eliminar Grupo")]
        public async Task EliminarGrupoTest()
        {
            
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            _contextMock.Setup(e => e.Grupo.FindAsync(It.IsAny<int>())).ReturnsAsync(new Grupo()
            {
                id = 1,
                nombre = "Prueba",
                departamentoid = 1
            });
            Boolean expected = true;
            var id = 1;
            
            Boolean result = await _dao.EliminarGrupoDAO(id);
            
            Assert.Equal<Boolean>(expected, result);
        }

        //Test para eliminar un grupo con exception
        [Fact(DisplayName = "Eliminar Grupo con excepcion")]
        public async Task EliminarGrupoExcepcionTest()
        {
            
            _contextMock.Setup(e => e.Grupo.FindAsync(It.IsAny<int>())).ReturnsAsync(new Grupo()
            {
                id = 1,
                nombre = "Prueba",
                departamentoid = 1
            });
            
            _contextMock.Setup(x => x.DbContext.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new DbUpdateException());

            
            await Assert.ThrowsAsync<GrupoException>(() => _dao!.EliminarGrupoDAO(1));
        }

        //Test para cuando no se encuentra el grupo que se desea eliminar
        [Fact(DisplayName = "Grupo no encontrado para eliminar")]
        public async Task EliminarGrupoNoEncontradoTest()
        {
            
            _contextMock.Setup(e => e.Grupo.FindAsync(It.IsAny<int>())).ReturnsAsync(null as Grupo);
            var id = 4;
            Boolean expected = false;
            Boolean result = await _dao.EliminarGrupoDAO(id);
            
            Assert.Equal<Boolean>(expected, result);
        }



    }
}
