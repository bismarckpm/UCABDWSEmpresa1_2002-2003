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
    public class ModeloParaleloDAOTest : BasePrueba
    {

        private readonly ModeloParaleloDAO _dao;
        private readonly Mock<IMigrationDbContext> _contextMock;
        private readonly Mock<IModeloParaleloDAO> _servicesMock;

        public ModeloParaleloDAOTest()
        {
            // preparacion de los mocks
            var faker = new Faker();
            _contextMock = new Mock<IMigrationDbContext>();
            var mapper = ConfigurarAutoMapper();
            _dao = new ModeloParaleloDAO(_contextMock.Object, mapper);
            _servicesMock = new Mock<IModeloParaleloDAO>();
            _contextMock.SetupDbContextData();
        }

        // [Fact(DisplayName = "Crear un modelo paralelo")]
        // public async Task CrearModeloParaleloTest()
        // {
        //     // preparacion de los datos
        //     _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
        //     var mp = new ModeloParalelo()
        //     {
        //         paraid = 1,
        //         nombre = "paralelo1",
        //         cantidadAprobaciones = 3,
        //         categoriaId=2
        //     };

        //     // prueba de la funcion
        //     var result = await _dao.AgregarModeloParaleloDAO(mp);
        //     var mpResult = result.Value;
        //     // verificacion de la prueba
        //     Assert.IsType<ModeloParaleloDTO>(mpResult);
        // }

        // [Fact(DisplayName = "Crear un modelo paralelo excepcion no categoria")]
        // public async Task CrearModeloParaleloNoCategoriaTestException()
        // {
        //     // preparacion de los datos
        //     _contextMock.Setup(x => x.DbContext.SaveChangesAsync(It.IsAny<CancellationToken>()))
        //         .ThrowsAsync(new DbUpdateException());

        //     // prueba de la funcion
        //     await Assert.ThrowsAsync<ModeloParaleloException>(() => _dao!.AgregarModeloParaleloDAO(new ModeloParalelo { categoriaId=1 }));
        // }

        // [Fact(DisplayName = "Crear un modelo paralelo con excepcion")]
        // public async Task CrearModeloParaleloTestException()
        // {
        //     // preparacion de los datos
        //     _contextMock.Setup(x => x.DbContext.SaveChangesAsync(It.IsAny<CancellationToken>()))
        //         .ThrowsAsync(new DbUpdateException());

        //     // prueba de la funcion
        //     await Assert.ThrowsAsync<Exception>(() => _dao!.AgregarModeloParaleloDAO(new ModeloParalelo()));
        // }

        // [Fact(DisplayName = "Consultar todos los modelos paralelos")]
        // public async Task ConsultarModelosParalelosTest()
        // {

        //     // prueba de la funcion
        //     var result = await _dao.ConsultarModelosParalelosDAO();

        //     // verificacion de la prueba
        //     Assert.IsType<List<ModeloParalelo>>(result);
        //     Assert.Equal(2, result.Count);
        // }

        // [Fact(DisplayName = "Consultar todos los modelos paralelos con Excepcion")]
        // public async Task ConsultarModelosParalelosTestException()
        // {
        //     // preparacion de los datos
        //     _contextMock.Setup(c => c.ModeloParalelos).Throws(new Exception());

        //     // prueba de la funcion
        //     await Assert.ThrowsAsync<ModeloParaleloException>(() => _dao.ConsultarModelosParalelosDAO());
        // }

        // [Fact(DisplayName = "Consultar modelo paralelo por Id")]
        // public async Task ConsultarModeloParaleloIdTest()
        // {
        //     // preparacion de los datos
        //     _contextMock.Setup(e => e.Categorias.FindAsync(It.IsAny<int>()))
        //     .ReturnsAsync(new Categoria ()
        //     {
        //         id = 1,
        //         nombre = "Prueba"
        //     });
        //     _contextMock.Setup(e => e.ModeloParalelos.FindAsync(It.IsAny<int>()))
        //     .ReturnsAsync(new ModeloParalelo()
        //     {
        //         paraid = 1,
        //         nombre = "ModeloParalelo1",
        //         cantidadAprobaciones = 3,
        //         categoriaId = 1
        //     });


        //     var id = 1;
        //     // prueba de la funcion
        //     var result = await _dao.ConsultaModeloParaleloDAO(id);
        //     var mpResult = result.Value;

        //     // verificacion de la prueba
        //     Assert.IsType<ModeloParalelo>(mpResult);
        //     Assert.Equal(id, mpResult!.paraid);
        // }

        // [Fact(DisplayName = "Consultar modelo paralelo por Id que no existe")]
        // public async Task ConsultarModeloParaleloIdNoExisteTest()
        // {
        //     // preparacion de los datos
        //     // no obtener ningun modelo paralelo
        //     _contextMock.Setup(e => e.ModeloParalelos.FindAsync(It.IsAny<int>())).ReturnsAsync(null as ModeloParalelo);

        // // prueba de la funcion
        //     var id = 4;
        // // verificacion de la prueba           
        //     await Assert.ThrowsAsync<ModeloParaleloException>(() => _dao.ConsultaModeloParaleloDAO(id));    
        // }

        // [Fact(DisplayName = "Consultar modelo paralelo por Id con Excepcion")]
        // public async Task ConsultarModeloParaleloIdTestException()
        // {
        //     // preparacion de los datos
        //     _contextMock.Setup(t => t.ModeloParalelos).Throws(new Exception());

        //     // prueba de la funcion
        //     await Assert.ThrowsAsync<ModeloParaleloException>(() => _dao.ConsultaModeloParaleloDAO(-1));
        // }

        // [Fact(DisplayName = "Actualizar un modelo paralelo")]
        // public async Task ActualizarModeloParaleloTest()
        // {
        //     // preparacion de los datos
        //     _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
        //     _contextMock.Setup(e => e.ModeloParalelos.FindAsync(It.IsAny<int>())).ReturnsAsync(new ModeloParalelo()
        //     {
        //         paraid = 1,
        //         nombre = "paralelo1",
        //         cantidadAprobaciones = 3,
        //         categoriaId=2
        //     });
        //     var paraid = 1;
        //     var modeloParalelo = new ModeloParaleloCreateDTO()
        //     {
        //         nombre = "Modificada",
        //         cantidadAprobaciones = 3,
        //         categoriaId=2
        //     };
        //     // prueba de la funcion
        //     var result = await _dao.ActualizarModeloParaleloDAO(paraid, modeloParalelo);
        //     var mpResult = result.Value;
        //     // verificacion de la prueba
        //     Assert.IsType<ModeloParalelo>(mpResult);
        // }

        // [Fact(DisplayName = "Actualizar un modelo paralelo sin categoria")]
        // public async Task ActualizarModeloParaleloSinCategoriaTest()
        // {
        //     // preparacion de los datos
        //     _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
        //     _contextMock.Setup(e => e.ModeloParalelos.FindAsync(It.IsAny<int>())).ReturnsAsync(new ModeloParalelo()
        //     {
        //         paraid = 1,
        //         nombre = "paralelo1",
        //         cantidadAprobaciones = 3
        //     });
        //     var paraid = 1;
        //     var modeloParalelo = new ModeloParaleloCreateDTO()
        //     {
        //         nombre = "Modificada",
        //         cantidadAprobaciones = 3
        //     };
        //     // prueba de la funcion
        //     // verificacion de la prueba
        //     Assert.ThrowsAsync<NullReferenceException>(async () => await _dao.ActualizarModeloParaleloDAO(paraid, modeloParalelo));
        // }

        // [Fact(DisplayName = "No existe modelo paralelo para actualizar")]
        // public async Task ActualizarModeloParaleloNoExisteTest()
        // {
        //     // preparacion de los datos
        //     _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
        //     _contextMock.Setup(e => e.ModeloParalelos.FindAsync(It.IsAny<int>())).ReturnsAsync(null as ModeloParalelo);
        //     var paraid = 5;
        //     var modeloParalelo = new ModeloParaleloCreateDTO();
        //     // verificacion de la prueba
        //     Assert.ThrowsAsync<ModeloParaleloException>(async () => await _dao.ActualizarModeloParaleloDAO(paraid, modeloParalelo));
        // }

        // [Fact(DisplayName = "Actualizar un modelo paralelo con Excepcion")]
        // public async Task ActualizarModeloParaleloTestException()
        // {
        //     // preparacion de los datos
        //     var id = 1;
        //     _contextMock.Setup(x => x.DbContext.SaveChangesAsync(It.IsAny<CancellationToken>()))
        //         .ThrowsAsync(new Exception());

        //     // prueba de la funcion
        //     await Assert.ThrowsAsync<ModeloParaleloException>(() => _dao!.ActualizarModeloParaleloDAO(id, new ModeloParaleloCreateDTO()));
        // }

        // [Fact(DisplayName = "Eliminar un modelo paralelo")]
        // public async Task EliminarModeloParaleloTest()
        // {
        //     // preparacion de los datos
        //     _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
        //     _contextMock.Setup(e => e.ModeloParalelos.FindAsync(It.IsAny<int>())).ReturnsAsync(new ModeloParalelo()
        //     {
        //         paraid = 1,
        //         nombre = "paralelo1",
        //         cantidadAprobaciones = 3,
        //         categoriaId=2
        //     });
        //     var id = 1;
        //     // prueba de la funcion
        //     var result = await _dao.EliminarModeloParaleloDAO(id);

        //     // verificacion de result Ok
        //     Assert.IsType<OkResult>(result);

        // }

        // [Fact(DisplayName = "No existe modelo paralelo a eliminar")]
        // public async Task EliminarModeloParaleloNoExisteTest()
        // {
        //     // preparacion de los datos
        //     _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
        //     _contextMock.Setup(e => e.ModeloParalelos.FindAsync(It.IsAny<int>())).ReturnsAsync(null as ModeloParalelo);
        //     var id = 1;
        //     // prueba de la funcion
        //     var result = await _dao.EliminarModeloParaleloDAO(id);

        //     // verificacion de result NotFound
        //     Assert.IsType<NotFoundResult>(result);
        // }

        // [Fact(DisplayName = "Eliminar una modelo paralelo con Excepcion")]
        // public async void EliminarModeloParaleloTestException()
        // {
        //     // preparacion de los datos
        //     var id = 1;
        //     _contextMock.Setup(x => x.DbContext.SaveChangesAsync(It.IsAny<CancellationToken>()))
        //         .ThrowsAsync(new Exception());

        //     // prueba de la funcion
        //     await Assert.ThrowsAsync<ModeloParaleloException>(() => _dao.EliminarModeloParaleloDAO(id));
        // }
    }
}