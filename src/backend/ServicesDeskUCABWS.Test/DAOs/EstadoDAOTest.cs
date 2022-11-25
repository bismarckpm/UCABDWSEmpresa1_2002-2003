
using Microsoft.EntityFrameworkCore;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Database;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using Bogus;
using Moq;
using EstadoDAO = ServicesDeskUCABWS.Persistence.DAO.Implementations.EstadoDAO;
using Microsoft.Extensions.Logging.Abstractions;
using ServicesDeskUCABWS.Test.Configuraciones;
using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCABWS.Exceptions;
using ServicesDeskUCABWS.Test.DataSeed;

namespace ServicesDeskUCABWS.Test.DAOs
{

    public class EstadoDAOTest : BasePrueba
    {
        private readonly EstadoDAO _dao;
        private readonly Mock<IMigrationDbContext> _contextMock;
        private readonly Mock<IEstadoDAO> _servicesMock;




        public EstadoDAOTest()
        {
            // preparacion de los mocks
            var faker = new Faker();
            _contextMock = new Mock<IMigrationDbContext>();
            var _logger = new NullLogger<EstadoDAO>();
            var _mapper = ConfigurarAutoMapper();
            _dao = new EstadoDAO(_mapper, _contextMock.Object, _logger);
            _servicesMock = new Mock<IEstadoDAO>();
            _contextMock.SetupDbContextData();
        }

        [Fact(DisplayName = "Crear un Estado")]
        public async Task CrearEstadoTest()
        {
            // preparacion de los datos
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            _contextMock.Setup(e => e.Etiquetas.FindAsync(It.IsAny<int>()))
            .ReturnsAsync(new Etiqueta()
            {
                id = 1,
                nombre = "Prueba",
                descripcion = "Creada"
            });
            var Estado = new Estado()
            {
                id = 1,
                nombre = "Nueva",
                EtiquetaId = 1
            };

            // prueba de la funcion
            var result = await _dao.AgregarEstadoDAO(Estado);
            var EstadoResult = result.Value;

            // verificacion de la prueba
            Assert.IsType<EstadoDTO>(EstadoResult);
        }

        [Fact(DisplayName = "Etiqueta no existe")]
        public async Task EtiquetaNoExisteTest()
        {
            // preparacion de los datos
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            _contextMock.Setup(e => e.Etiquetas.FindAsync(It.IsAny<int>()))
            .ReturnsAsync(null as Etiqueta);
            var Estado = new Estado()
            {
                id = 1,
                nombre = "Nueva",
                EtiquetaId = 1
            };

            // verificacion de la prueba
            await Assert.ThrowsAsync<EstadoException>(() => _dao.AgregarEstadoDAO(Estado));
        }

        [Fact(DisplayName = "Crear Estado con excepcion")]
        public async Task CrearEstadoExcepcionTest()
        {
            // preparacion de los datos
            _contextMock.Setup(e => e.Etiquetas.FindAsync(It.IsAny<int>()))
            .ReturnsAsync(new Etiqueta()
            {
                id = 1,
                nombre = "Prueba",
                descripcion = "Creada"
            });
            // preparacion de los datos
            _contextMock.Setup(x => x.DbContext.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new DbUpdateException());

            // prueba de la funcion
            await Assert.ThrowsAsync<EstadoException>(() => _dao!.AgregarEstadoDAO(new Estado()));
        }

        [Fact(DisplayName = "Consultar lista Estados")]
        public async Task ConsultarListEstadosTest()
        {

            // prueba de la funcion
            var result = await _dao.GetEstadosDAO();
            var EstadoResult = result.Value!;


            // verificacion de la prueba
            Assert.IsType<List<EstadoDTO>>(EstadoResult);
            Assert.Equal(2, EstadoResult.Count);
        }

        [Fact(DisplayName = "Consultar lista Estados con Excepcion")]
        public async Task ConsultarListEstadosTestException()
        {
            // preparacion de los datos
            _contextMock.Setup(c => c.Estados).Throws(new Exception());

            // prueba de la funcion
            await Assert.ThrowsAsync<EstadoException>(() => _dao.GetEstadosDAO());
        }

        [Fact(DisplayName = "Consultar Estado por Id")]
        public async Task ConsultarEstadoIdTest()
        {
            // preparacion de los datos
            _contextMock.Setup(e => e.Estados.FindAsync(It.IsAny<int>()))
            .ReturnsAsync(It.IsAny<Estado>());


            var id = 1;
            // prueba de la funcion
            var result = await _dao.GetEstadoDAO(id);
            var EstadoResult = result.Value;

            // verificacion de la prueba
            Assert.IsType<EstadoDTO>(EstadoResult);
            Assert.Equal(id, EstadoResult!.id);
        }

        [Fact(DisplayName = "Consultar Estado por Id que no existe")]
        public async Task ConsultarEstadoIdNoExisteTest()
        {
            // preparacion de los datos
            // no obtener ninguna Estado
            _contextMock.Setup(e => e.Estados.FindAsync(It.IsAny<int>())).ReturnsAsync(null as Estado);
            var id = 4;
            // prueba de la funcion
            await Assert.ThrowsAsync<EstadoException>(() => _dao.GetEstadoDAO(id));
        }


        [Fact(DisplayName = "Actualizar una Estado")]
        public async Task ActualizarEstadoTest()
        {
            // preparacion de los datos
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            _contextMock.Setup(e => e.Estados.FindAsync(It.IsAny<int>())).ReturnsAsync(new Estado()
            {
                id = 1,
                nombre = "Prueba",
                EtiquetaId = 1
            });
            _contextMock.Setup(e => e.Etiquetas.FindAsync(It.IsAny<int>())).ReturnsAsync(new Etiqueta()
            {
                id = 1,
                nombre = "Prueba",
                descripcion = "Creada"
            });
            var Estado = new Estado()
            {
                id = 1,
                nombre = "Modificada",
                EtiquetaId = 1
            };
            // prueba de la funcion
            var result = await _dao.ActualizarEstadoDAO(Estado, Estado.id);
            // verificacion de la prueba
            Assert.IsType<OkResult>(result);
        }

        [Fact(DisplayName = "Actualizar Estado con excepcion")]
        public async Task ActualizarEstadoExcepcionTest()
        {
            // preparacion de los datos
            _contextMock.Setup(e => e.Estados.FindAsync(It.IsAny<int>())).ReturnsAsync(new Estado()
            {
                id = 1,
                nombre = "Prueba",
                EtiquetaId = 1
            });
            _contextMock.Setup(e => e.Etiquetas.FindAsync(It.IsAny<int>())).ReturnsAsync(new Etiqueta()
            {
                id = 1,
                nombre = "Prueba",
                descripcion = "Creada"
            });
            // preparacion de los datos
            _contextMock.Setup(x => x.DbContext.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new DbUpdateException());

            // prueba de la funcion
            await Assert.ThrowsAsync<EstadoException>(() => _dao!.ActualizarEstadoDAO(new Estado(), 1));
        }

        [Fact(DisplayName = "Estado no encontrado para actualizar")]
        public async Task ActualizarEstadoNoEncontradoTest()
        {
            // preparacion de los datos
            // no obtener ninguna Estado
            _contextMock.Setup(e => e.Estados.FindAsync(It.IsAny<int>())).ReturnsAsync(null as Estado);
            var id = 4;
            // prueba de la funcion
            await Assert.ThrowsAsync<EstadoException>(() => _dao.ActualizarEstadoDAO(new Estado(), id));
        }

        [Fact(DisplayName = "Etiqueta no encontrada para actualizar")]
        public async Task ActualizarEtiquetaNoEncontradaTest()
        {
            // preparacion de los datos
            // no obtener ninguna Estado
            _contextMock.Setup(e => e.Estados.FindAsync(It.IsAny<int>())).ReturnsAsync(new Estado()
            {
                id = 1,
                nombre = "Prueba",
                EtiquetaId = 1
            });
            _contextMock.Setup(e => e.Etiquetas.FindAsync(It.IsAny<int>())).ReturnsAsync(null as Etiqueta);
            var id = 1;
            var Estado = new Estado()
            {
                id = 1,
                nombre = "Cambio",
                EtiquetaId = 2
            };
            // prueba de la funcion
            await Assert.ThrowsAsync<EstadoException>(() => _dao.ActualizarEstadoDAO(Estado, id));
        }

        [Fact(DisplayName = "Eliminar Estado")]
        public async Task EliminarEstadoTest()
        {
            // preparacion de los datos
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            _contextMock.Setup(e => e.Estados.FindAsync(It.IsAny<int>())).ReturnsAsync(new Estado()
            {
                id = 1,
                nombre = "Prueba",
                EtiquetaId = 1
            });
            var id = 1;
            // prueba de la funcion
            var result = await _dao.EliminarEstadoDAO(id);
            // verificacion de la prueba
            Assert.IsType<OkResult>(result);
        }

        [Fact(DisplayName = "Eliminar Estado con excepcion")]
        public async Task EliminarEstadoExcepcionTest()
        {
            // preparacion de los datos
            _contextMock.Setup(e => e.Estados.FindAsync(It.IsAny<int>())).ReturnsAsync(new Estado()
            {
                id = 1,
                nombre = "Prueba",
                EtiquetaId = 1
            });
            // preparacion de los datos
            _contextMock.Setup(x => x.DbContext.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new DbUpdateException());

            // prueba de la funcion
            await Assert.ThrowsAsync<EstadoException>(() => _dao!.EliminarEstadoDAO(1));
        }

        [Fact(DisplayName = "Estado no encontrado para eliminar")]
        public async Task EliminarEstadoNoEncontradoTest()
        {
            // preparacion de los datos
            // no obtener ninguna Estado
            _contextMock.Setup(e => e.Estados.FindAsync(It.IsAny<int>())).ReturnsAsync(null as Estado);
            var id = 4;
            var result = await _dao.EliminarEstadoDAO(id);
            // prueba de la funcion
            Assert.IsType<NotFoundResult>(result);
        }

        // [Fact(DisplayName = "Eliminar una Estado")]
        // public async Task EliminarEstadoTest()
        // {
        //     // preparacion de los datos
        //     _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
        //     _contextMock.Setup(e => e.Estados.FindAsync(It.IsAny<int>())).ReturnsAsync(new Estado()
        //     {
        //         id = 1,
        //         nombre = "Prueba",
        //         descripcion = "Creada"
        //     });
        //     var id = 1;
        //     // prueba de la funcion
        //     var result = await _dao.EliminarEstadoDAO(id);

        //     // verificacion de result Ok
        //     Assert.IsType<OkResult>(result);

        // }

        // [Fact(DisplayName = "No existe Estado para eliminar")]
        // public async Task EliminarEstadoNoExisteTest()
        // {
        //     // preparacion de los datos
        //     _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
        //     _contextMock.Setup(e => e.Estados.FindAsync(It.IsAny<int>())).ReturnsAsync(null as Estado);
        //     var id = 1;
        //     // prueba de la funcion
        //     var result = await _dao.EliminarEstadoDAO(id);

        //     // verificacion de result NotFound
        //     Assert.IsType<NotFoundResult>(result);
        // }

        // [Fact(DisplayName = "Eliminar una Estado con Excepcion")]
        // public async Task EliminarEstadoTestException()
        // {
        //     // preparacion de los datos
        //     var id = 1;
        //     _contextMock.Setup(x => x.DbContext.SaveChangesAsync(It.IsAny<CancellationToken>()))
        //         .ThrowsAsync(new Exception());

        //     // prueba de la funcion
        //     await Assert.ThrowsAsync<EstadoException>(() => _dao!.EliminarEstadoDAO(id));
        // }


    }
}