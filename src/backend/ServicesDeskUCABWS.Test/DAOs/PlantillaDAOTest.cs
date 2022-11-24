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
using PlantillaDAO = ServicesDeskUCABWS.Persistence.DAO.Implementations.PlantillaDAO;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using ServicesDeskUCABWS.Test.Configuraciones;
using Microsoft.AspNetCore.Mvc;
using PlantillaException = ServicesDeskUCABWS.Exceptions.PlantillaException;

namespace ServicesDeskUCABWS.Test.DAOs
{

    public class PlantillaDAOTest : BasePrueba
    {
        private readonly PlantillaDAO _dao;
        private readonly Mock<IMigrationDbContext> _contextMock;
        private readonly Mock<IPlantillaDAO> _servicesMock;




        public PlantillaDAOTest()
        {
            // preparacion de los mocks
            var faker = new Faker();
            _contextMock = new Mock<IMigrationDbContext>();
            var _logger = new NullLogger<PlantillaDAO>();
            var _mapper = ConfigurarAutoMapper();
            _dao = new PlantillaDAO(_mapper, _contextMock.Object, _logger);
            _servicesMock = new Mock<IPlantillaDAO>();
            _contextMock.SetupDbContextData();
        }

        [Fact(DisplayName = "Crear una Plantilla")]
        public async Task CrearPlantillaTest()
        {
            // preparacion de los datos
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            var Plantilla = new Plantilla()
            {
                id = 1,
                titulo = "Plantilla 1",
                cuerpo = "Descripcion de la plantilla 1",
                tipo = "Solicitud"
            };

            // prueba de la funcion
            var result = await _dao.AgregarPlantillaDAO(Plantilla);
            var PlantillaResult = result.Value;

            // verificacion de la prueba
            Assert.IsType<PlantillaDTO>(PlantillaResult);
        }

        [Fact(DisplayName = "Crear una Plantilla con Excepcion")]
        public async Task CrearPlantillaTestException()
        {
            // preparacion de los datos
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Throws(new DbUpdateException());
            var Plantilla = new Plantilla();



            // prueba de la funcion
            await Assert.ThrowsAsync<PlantillaException>(() => _dao.AgregarPlantillaDAO(Plantilla));
        }

        [Fact(DisplayName = "Consultar lista Plantillas")]
        public async Task ConsultarListPlantillasTest()
        {

            // prueba de la funcion
            var result = await _dao.ObtenerPlantillasDAO();


            // verificacion de la prueba
            Assert.IsType<List<Plantilla>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact(DisplayName = "Consultar lista Plantillas con Excepcion")]
        public async Task ConsultarListPlantillasTestException()
        {
            // preparacion de los datos
            _servicesMock.Setup(x => x.ObtenerPlantillasDAO())
                .ThrowsAsync(new Exception());

            // prueba de la funcion
            await Assert.ThrowsAsync<Exception>(() => _servicesMock.Object.ObtenerPlantillasDAO());
        }

        [Fact(DisplayName = "Consultar Plantilla por Id")]
        public async Task ConsultarPlantillaIdTest()
        {
            // preparacion de los datos
            //_contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            _contextMock.Setup(e => e.Plantillas.FindAsync(It.IsAny<int>()))
            .ReturnsAsync(new Plantilla()
            {
                id = 1,
                titulo = "Plantilla 1",
                cuerpo = "Descripcion de la plantilla 1",
                tipo = "Solicitud"
            });


            var id = 1;
            // prueba de la funcion
            var result = await _dao.ObtenerPlantillaDAO(id);
            var PlantillaResult = result.Value;

            // verificacion de la prueba
            Assert.IsType<Plantilla>(PlantillaResult);
            Assert.Equal(id, PlantillaResult!.id);
        }

        [Fact(DisplayName = "Consultar Plantilla por Id que no existe")]
        public async Task ConsultarPlantillaIdNoExisteTest()
        {
            // preparacion de los datos
            // no obtener ninguna Plantilla
            _contextMock.Setup(e => e.Plantillas.FindAsync(It.IsAny<int>())).ReturnsAsync(null as Plantilla);


            var id = 4;
            // prueba de la funcion
            var result = await _dao.ObtenerPlantillaDAO(id);


            // verificacion de la prueba
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact(DisplayName = "Consultar Plantilla por Id con Excepcion")]
        public async Task ConsultarPlantillaIdTestException()
        {
            // preparacion de los datos
            var id = 1;
            _servicesMock.Setup(x => x.ObtenerPlantillaDAO(id))
                .ThrowsAsync(new Exception());

            // prueba de la funcion
            await Assert.ThrowsAsync<Exception>(() => _servicesMock.Object.ObtenerPlantillaDAO(id));
        }

        [Fact(DisplayName = "Actualizar una Plantilla")]
        public async Task ActualizarPlantillaTest()
        {
            // preparacion de los datos
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            _contextMock.Setup(e => e.Plantillas.FindAsync(It.IsAny<int>())).ReturnsAsync(new Plantilla()
            {
                id = 1,
                titulo = "Plantilla 1",
                cuerpo = "Descripcion de la plantilla 1",
                tipo = "Solicitud"
            });
            var Plantilla = new Plantilla()
            {
                id = 1,
                titulo = "modificada",
                cuerpo = "Descripcion de la plantilla 1",
                tipo = "Solicitud"
            };
            // prueba de la funcion
            var result = await _dao.ActualizarPlantillaDAO(Plantilla, Plantilla.id);

            // verificacion de la prueba
            Assert.IsType<OkResult>(result);
        }

        [Fact(DisplayName = "No existe Plantilla para actualizar")]
        public async Task ActualizarPlantillaNoExisteTest()
        {
            // preparacion de los datos
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            _contextMock.Setup(e => e.Plantillas.FindAsync(It.IsAny<int>())).ReturnsAsync(null as Plantilla);
            var Plantilla = new Plantilla();
            // prueba de la funcion
            var result = await _dao.ActualizarPlantillaDAO(Plantilla, Plantilla.id);
            // verificacion de la prueba
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact(DisplayName = "Actualizar una Plantilla con Excepcion")]
        public async Task ActualizarPlantillaTestException()
        {
            // preparacion de los datos
            var Plantilla = new Plantilla()
            {
                id = 1,
                titulo = "modificada",
                cuerpo = "Descripcion de la plantilla 1",
                tipo = "Solicitud"
            };
            _servicesMock.Setup(x => x.ActualizarPlantillaDAO(Plantilla, Plantilla.id))
                .ThrowsAsync(new DbUpdateException());

            // prueba de la funcion
            await Assert.ThrowsAsync<DbUpdateException>(() => _servicesMock.Object.ActualizarPlantillaDAO(Plantilla, Plantilla.id));
        }

        [Fact(DisplayName = "Eliminar una Plantilla")]
        public async Task EliminarPlantillaTest()
        {
            // preparacion de los datos
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            _contextMock.Setup(e => e.Plantillas.FindAsync(It.IsAny<int>())).ReturnsAsync(new Plantilla()
            {
                id = 1,
                titulo = "Plantilla 1",
                cuerpo = "Descripcion de la plantilla 1",
                tipo = "Solicitud"
            });
            var id = 1;
            // prueba de la funcion
            var result = await _dao.EliminarPlantillaDAO(id);

            // verificacion de result Ok
            Assert.IsType<OkResult>(result);

        }

        [Fact(DisplayName = "No existe Plantilla para eliminar")]
        public async Task EliminarPlantillaNoExisteTest()
        {
            // preparacion de los datos
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            _contextMock.Setup(e => e.Plantillas.FindAsync(It.IsAny<int>())).ReturnsAsync(null as Plantilla);
            var id = 1;
            // prueba de la funcion
            var result = await _dao.EliminarPlantillaDAO(id);

            // verificacion de result NotFound
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact(DisplayName = "Eliminar una Plantilla con Excepcion")]
        public async Task EliminarPlantillaTestException()
        {
            // preparacion de los datos
            var id = 1;
            _servicesMock.Setup(x => x.EliminarPlantillaDAO(id))
                .ThrowsAsync(new DbUpdateException());

            // prueba de la funcion
            await Assert.ThrowsAsync<DbUpdateException>(() => _servicesMock.Object.EliminarPlantillaDAO(id));
        }


    }
}