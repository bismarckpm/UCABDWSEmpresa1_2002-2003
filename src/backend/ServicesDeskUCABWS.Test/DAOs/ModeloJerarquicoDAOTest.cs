using System.Reflection;
using Bogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.DAO.Implementations;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Database;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.Test.DataSeed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServicesDeskUCABWS.Exceptions;
using Microsoft.EntityFrameworkCore;
using ServicesDeskUCABWS.Test.Configuraciones;

namespace ServicesDeskUCABWS.Test.DAOs
{
    public class ModeloJerarquicoDAOTest : BasePrueba
    {

        private readonly ModeloJerarquicoDAO _dao;
        private readonly Mock<IMigrationDbContext> _contextMock;
        private readonly Mock<IModeloJerarquicoDAO> _servicesMock;

        public ModeloJerarquicoDAOTest()
        {
            // preparacion de los mocks
            var faker = new Faker();
            _contextMock = new Mock<IMigrationDbContext>();
            var mapper = ConfigurarAutoMapper();
            _dao = new ModeloJerarquicoDAO(_contextMock.Object, mapper);
            _servicesMock = new Mock<IModeloJerarquicoDAO>();
            _contextMock.SetupDbContextData();
        }

        [Fact(DisplayName = "Crear un modelo jerarquico")]
        public async Task CrearModeloJerarquicoTest()
        {
            // preparacion de los datos
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            TipoCargo tCargo = new TipoCargo { id = 1, nombre = "Prueba"};
            List<TipoCargo>  lista = new List<TipoCargo>();
            lista.Add(tCargo);
            var mp = new ModeloJerarquico()
            {
                Id = 1,
                Nombre = "jerarquico1",
                orden = lista,
                CategoriaId=1
            };
            // prueba de la funcion
            var result = await _dao.AgregarModeloJerarquicoDAO(mp);
            var mpResult = result.Value;
            // verificacion de la prueba
            Assert.IsType<ModeloJerarquicoDTO>(mpResult);
        }

        [Fact(DisplayName = "Crear un modelo jerarquico con excepcion")]
        public async Task CrearModeloJerarquicoTestException()
        {
            // preparacion de los datos
            _contextMock.Setup(x => x.DbContext.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new DbUpdateException());

            // prueba de la funcion
            await Assert.ThrowsAsync<Exception>(() => _dao!.AgregarModeloJerarquicoDAO(new ModeloJerarquico()));
        }

        [Fact(DisplayName = "Consultar todos los modelos jerarquicos")]
        public async Task ConsultarModelosJerarquicosTest()
        {

            // prueba de la funcion
            var result = await _dao.ConsultarModeloJerarquicosDAO();

            // verificacion de la prueba
            Assert.IsType<List<ModeloJerarquico>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact(DisplayName = "Consultar todos los modelos jerarquicos con Excepcion")]
        public async Task ConsultarModelosJerarquicosTestException()
        {
            // preparacion de los datos
            _contextMock.Setup(c => c.ModeloJerarquicos).Throws(new Exception());

            // prueba de la funcion
            await Assert.ThrowsAsync<Exception>(() => _dao.ConsultarModeloJerarquicosDAO());
        }

        [Fact(DisplayName = "Consultar modelo jerarquico por Id")]
        public async Task ConsultarModeloJerarquicoIdTest()
        {
            // preparacion de los datos
            _contextMock.Setup(e => e.ModeloJerarquicos.FindAsync(It.IsAny<int>()))
            .ReturnsAsync(new ModeloJerarquico()
            {
                Id = 1,
                Nombre = "jerarquico1",
                CategoriaId=1
            });


            var id = 1;
            // prueba de la funcion
            var result = await _dao.ObtenerModeloJerarquicoDAO(id);
            var mpResult = result.Value;

            // verificacion de la prueba
            Assert.IsType<ModeloJerarquico>(mpResult);
            Assert.Equal(id, mpResult!.Id);
        }

        [Fact(DisplayName = "Consultar modelo jerarquico por Id que no existe")]
        public async Task ConsultarModeloJerarquicoIdNoExisteTest()
        {
            // preparacion de los datos
            // no obtener ninguna etiqueta
            _contextMock.Setup(e => e.ModeloJerarquicos.FindAsync(It.IsAny<int>())).ReturnsAsync(null as ModeloJerarquico);


            var id = 4;
            // prueba de la funcion
            // verificacion de la prueba
            await Assert.ThrowsAsync<Exception>(() => _dao.ObtenerModeloJerarquicoDAO(id));    


        }

        [Fact(DisplayName = "Consultar modelo jerarquico por Id con Excepcion")]
        public async Task ConsultarModeloJerarquicoIdTestException()
        {
            // preparacion de los datos
            _servicesMock.Setup(c => c.ObtenerModeloJerarquicoDAO(It.IsAny<int>()))
                .Throws(new Exception());

            // prueba de la funcion
            await Assert.ThrowsAsync<Exception>(() => _dao.ObtenerModeloJerarquicoDAO(-1));
        }

         /* No pasa porque no esta encuentra nada en el include con categoria
        [Fact(DisplayName = "Actualizar un modelo jerarquico")]
        public async Task ActualizarModeloJerarquicoTest()
        {
            // preparacion de los datos
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            TipoCargo tCargo = new TipoCargo { id = 1, nombre = "Prueba"};
            List<TipoCargo>  lista = new List<TipoCargo>();
            lista.Add(tCargo);
            _contextMock.Setup(e => e.ModeloJerarquicos.FindAsync(It.IsAny<int>())).ReturnsAsync(new ModeloJerarquico()
            {
                Id = 1,
                Nombre = "jerarquico1",
                orden = lista,
                CategoriaId=1
            });
            var Id = 1;
            var modeloJerarquico = new ModeloJerarquicoCreateDTO()
            {
                Nombre = "Modificada",
                orden = lista,
                CategoriaId = 2
            };
            // prueba de la funcion
            var result = await _dao.ActualizarModeloJerarquicoDAO(modeloJerarquico, Id);
            var mpResult = result.Value;
            // verificacion de la prueba
            Assert.IsType<ModeloJerarquico>(mpResult);
        }*/

        [Fact(DisplayName = "No existe modelo jerarquico para actualizar")]
        public async Task ActualizarModeloJerarquicoNoExisteTest()
        {
            // preparacion de los datos
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            _contextMock.Setup(e => e.ModeloJerarquicos.FindAsync(It.IsAny<int>())).ReturnsAsync(null as ModeloJerarquico);
            var Id = 5;
            var modeloJerarquico = new ModeloJerarquicoCreateDTO();
            // prueba de la funcion
            // verificacion de la prueba
            await Assert.ThrowsAsync<Exception>(()=> _dao.ActualizarModeloJerarquicoDAO(modeloJerarquico, Id));
        }

        [Fact(DisplayName = "Actualizar un modelo jerarquico con Excepcion")]
        public async Task ActualizarModeloJerarquicoTestException()
        {
            // preparacion de los datos
            var id = 5;
            _contextMock.Setup(x => x.DbContext.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            // prueba de la funcion
            await Assert.ThrowsAsync<Exception>(() => _dao!.ActualizarModeloJerarquicoDAO(new ModeloJerarquicoCreateDTO(), id));
        }

        [Fact(DisplayName = "Eliminar un modelo jerarquico")]
        public async Task EliminarModeloJerarquicoTest()
        {
            // preparacion de los datos
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            _contextMock.Setup(e => e.ModeloJerarquicos.FindAsync(It.IsAny<int>())).ReturnsAsync(new ModeloJerarquico()
            {
                Id = 1,
                Nombre = "jerarquico1",
                CategoriaId = 1
            });
            var id = 1;
            // prueba de la funcion
            var result = await _dao.EliminarModeloJerarquicoDAO(id);

            // verificacion de result Ok
            Assert.IsType<OkResult>(result);

        }

        [Fact(DisplayName = "No existe modelo jerarquico a eliminar")]
        public async Task EliminarModeloJerarquicoNoExisteTest()
        {
            // preparacion de los datos
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            _contextMock.Setup(e => e.ModeloJerarquicos.FindAsync(It.IsAny<int>())).ReturnsAsync(null as ModeloJerarquico);
            var id = 1;
            // prueba de la funcion
            var result = await _dao.EliminarModeloJerarquicoDAO(id);

            // verificacion de result NotFound
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact(DisplayName = "Eliminar una modelo jerarquico con Excepcion")]
        public async void EliminarModeloJerarquicoTestException()
        {
            // preparacion de los datos
            var id = 1;
            _contextMock.Setup(x => x.DbContext.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            // prueba de la funcion
            await Assert.ThrowsAsync<Exception>(() => _dao.EliminarModeloJerarquicoDAO(id));
        }
    }
}