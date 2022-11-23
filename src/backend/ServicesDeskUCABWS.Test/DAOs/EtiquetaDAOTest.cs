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
using EtiquetaDAO = ServicesDeskUCABWS.Persistence.DAO.Implementations.EtiquetaDAO;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using ServicesDeskUCABWS.Test.Configuraciones;
using Microsoft.AspNetCore.Mvc;

namespace ServicesDeskUCABWS.Test.DAOs
{

    public class EtiquetaDAOTest : BasePrueba
    {
        private readonly EtiquetaDAO _dao;
        private readonly Mock<IMigrationDbContext> _contextMock;
        private readonly Mock<IEtiquetaDAO> _servicesMock;




        public EtiquetaDAOTest()
        {
            // preparacion de los mocks
            var faker = new Faker();
            _contextMock = new Mock<IMigrationDbContext>();
            var _logger = new NullLogger<EtiquetaDAO>();
            var _mapper = ConfigurarAutoMapper();
            _dao = new EtiquetaDAO(_mapper, _contextMock.Object, _logger);
            _servicesMock = new Mock<IEtiquetaDAO>();
            _contextMock.SetupDbContextData();
        }

        [Fact(DisplayName = "Crear una Etiqueta")]
        public async Task CrearEtiquetaTest()
        {
            // preparacion de los datos
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            var etiqueta = new Etiqueta()
            {
                id = 1,
                nombre = "Nueva",
                descripcion = "Creada"
            };

            // prueba de la funcion
            var result = await _dao.AgregarEtiquetaDAO(etiqueta);
            var etiquetaResult = result.Value;

            // verificacion de la prueba
            Assert.IsType<EtiquetaDTO>(etiquetaResult);
        }

        [Fact(DisplayName = "Crear una Etiqueta con Excepcion")]
        public async Task CrearEtiquetaTestException()
        {
            // preparacion de los datos
            _servicesMock.Setup(x => x.AgregarEtiquetaDAO(It.IsAny<Etiqueta>()))
                .ThrowsAsync(new DbUpdateException());

            // prueba de la funcion
            await Assert.ThrowsAsync<DbUpdateException>(() => _servicesMock.Object.AgregarEtiquetaDAO(new Etiqueta()));
        }

        [Fact(DisplayName = "Consultar lista Etiquetas")]
        public async Task ConsultarListEtiquetasTest()
        {

            // prueba de la funcion
            var result = await _dao.ConsultarEtiquetasDAO();


            // verificacion de la prueba
            Assert.IsType<List<Etiqueta>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact(DisplayName = "Consultar lista Etiquetas con Excepcion")]
        public async Task ConsultarListEtiquetasTestException()
        {
            // preparacion de los datos
            _servicesMock.Setup(x => x.ConsultarEtiquetasDAO())
                .ThrowsAsync(new Exception());

            // prueba de la funcion
            await Assert.ThrowsAsync<Exception>(() => _servicesMock.Object.ConsultarEtiquetasDAO());
        }

        [Fact(DisplayName = "Consultar Etiqueta por Id")]
        public async Task ConsultarEtiquetaIdTest()
        {
            // preparacion de los datos
            //_contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            _contextMock.Setup(e => e.Etiquetas.FindAsync(It.IsAny<int>()))
            .ReturnsAsync(new Etiqueta()
            {
                id = 1,
                nombre = "Prueba",
                descripcion = "Creada"
            });


            var id = 1;
            // prueba de la funcion
            var result = await _dao.ObtenerEtiquetaDAO(id);
            var etiquetaResult = result.Value;

            // verificacion de la prueba
            Assert.IsType<Etiqueta>(etiquetaResult);
            Assert.Equal(id, etiquetaResult!.id);
        }

        [Fact(DisplayName = "Consultar Etiqueta por Id que no existe")]
        public async Task ConsultarEtiquetaIdNoExisteTest()
        {
            // preparacion de los datos
            // no obtener ninguna etiqueta
            _contextMock.Setup(e => e.Etiquetas.FindAsync(It.IsAny<int>())).ReturnsAsync(null as Etiqueta);


            var id = 4;
            // prueba de la funcion
            var result = await _dao.ObtenerEtiquetaDAO(id);
            var etiquetaResult = result.Value;

            // verificacion de la prueba
            Assert.Equal(0, etiquetaResult!.id);
        }

        [Fact(DisplayName = "Consultar Etiqueta por Id con Excepcion")]
        public async Task ConsultarEtiquetaIdTestException()
        {
            // preparacion de los datos
            var id = 1;
            _servicesMock.Setup(x => x.ObtenerEtiquetaDAO(id))
                .ThrowsAsync(new Exception());

            // prueba de la funcion
            await Assert.ThrowsAsync<Exception>(() => _servicesMock.Object.ObtenerEtiquetaDAO(id));
        }

        [Fact(DisplayName = "Actualizar una Etiqueta")]
        public async Task ActualizarEtiquetaTest()
        {
            // preparacion de los datos
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            _contextMock.Setup(e => e.Etiquetas.FindAsync(It.IsAny<int>())).ReturnsAsync(new Etiqueta()
            {
                id = 1,
                nombre = "Prueba",
                descripcion = "Creada"
            });
            var etiqueta = new Etiqueta()
            {
                id = 1,
                nombre = "Modificada",
                descripcion = "Creada"
            };
            // prueba de la funcion
            var result = await _dao.ActualizarEtiquetaDAO(etiqueta, etiqueta.id);
            var etiquetaResult = result.Value;
            // verificacion de la prueba
            Assert.IsType<Etiqueta>(etiquetaResult);
        }

        [Fact(DisplayName = "No existe Etiqueta para actualizar")]
        public async Task ActualizarEtiquetaNoExisteTest()
        {
            // preparacion de los datos
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            _contextMock.Setup(e => e.Etiquetas.FindAsync(It.IsAny<int>())).ReturnsAsync(null as Etiqueta);
            var etiqueta = new Etiqueta();
            // prueba de la funcion
            var result = await _dao.ActualizarEtiquetaDAO(etiqueta, etiqueta.id);
            var etiquetaResult = result.Value;
            // verificacion de la prueba
            Assert.IsType<Etiqueta>(etiquetaResult);
        }

        [Fact(DisplayName = "Actualizar una Etiqueta con Excepcion")]
        public async Task ActualizarEtiquetaTestException()
        {
            // preparacion de los datos
            var etiqueta = new Etiqueta()
            {
                id = 1,
                nombre = "Modificada",
                descripcion = "Creada"
            };
            _servicesMock.Setup(x => x.ActualizarEtiquetaDAO(etiqueta, etiqueta.id))
                .ThrowsAsync(new DbUpdateException());

            // prueba de la funcion
            await Assert.ThrowsAsync<DbUpdateException>(() => _servicesMock.Object.ActualizarEtiquetaDAO(etiqueta, etiqueta.id));
        }

        [Fact(DisplayName = "Eliminar una Etiqueta")]
        public async Task EliminarEtiquetaTest()
        {
            // preparacion de los datos
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            _contextMock.Setup(e => e.Etiquetas.FindAsync(It.IsAny<int>())).ReturnsAsync(new Etiqueta()
            {
                id = 1,
                nombre = "Prueba",
                descripcion = "Creada"
            });
            var id = 1;
            // prueba de la funcion
            var result = await _dao.EliminarEtiquetaDAO(id);

            // verificacion de result Ok
            Assert.IsType<OkResult>(result);

        }

        [Fact(DisplayName = "No existe Etiqueta para eliminar")]
        public async Task EliminarEtiquetaNoExisteTest()
        {
            // preparacion de los datos
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            _contextMock.Setup(e => e.Etiquetas.FindAsync(It.IsAny<int>())).ReturnsAsync(null as Etiqueta);
            var id = 1;
            // prueba de la funcion
            var result = await _dao.EliminarEtiquetaDAO(id);

            // verificacion de result NotFound
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact(DisplayName = "Eliminar una Etiqueta con Excepcion")]
        public async Task EliminarEtiquetaTestException()
        {
            // preparacion de los datos
            var id = 1;
            _servicesMock.Setup(x => x.EliminarEtiquetaDAO(id))
                .ThrowsAsync(new DbUpdateException());

            // prueba de la funcion
            await Assert.ThrowsAsync<DbUpdateException>(() => _servicesMock.Object.EliminarEtiquetaDAO(id));
        }


    }
}