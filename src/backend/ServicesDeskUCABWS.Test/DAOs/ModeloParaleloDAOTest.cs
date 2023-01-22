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

        private ModeloParalelo NewModeloParalelo()
        {
            return new ModeloParalelo{
                    id = 1,
                    nombre = "Prueba Modelo",
                    categoriaid = 1,
                    categoria = new Categoria()
                    {
                        id = 1,
                        nombre = "Guardado"
                    },
                    cantidaddeaprobacion = 5,
                    };
        }
        
        //Crear modelo Paralelo
        [Fact(DisplayName = "Agregar nuevo modelo Paralelo")]
        public Task CreateModeloParaleloDAOTest()
        {
            _contextMock.Setup(j => j.DbContext.SaveChanges()).Returns(1);
            var result = _dao.AgregarModeloParaleloDAO(NewModeloParalelo());
            Assert.IsType<ModeloParaleloCreateDTO>(result);
            return Task.CompletedTask;
        }

        //Consulta una lista de modelos Paralelos
        [Fact(DisplayName = "Consultar todos los modelos paralelos")]
        public Task ConsultarModelosParalelosTest()
        {
            // prueba de la funcion
            var result =  _dao.ConsultarModelosParalelosDAO();

            // verificacion de la prueba
            Assert.IsType<List<ModeloParaleloDTO>>(result);
            return Task.CompletedTask;
        }

        //Consultar un modelo Paralelo
        [Fact(DisplayName = "Consultar modelo paralelo por Id")]
        public Task ConsultarModeloParaleloIdTest()
        {
            var id = 1;
            // prueba de la funcion
                var result =  _dao.ObtenerModeloParaleloDAO(id);
                // verificacion de la prueba
                Assert.IsType<ModeloParaleloDTO>(result);
                return Task.CompletedTask;
            }

        //Actualizar un modelo Paralelo
        [Fact(DisplayName = "Actualizar Modelo Paralelo")]
        public Task ActualizarModeloParaleloTest()
        {
            _contextMock.Setup(m=> m.DbContext.SaveChanges()).Returns(1);
            var dtoModel = new ModeloParalelo()
            {
                id = 2, 
                nombre = "Prueba.", 
                categoriaid = 4,
                categoria = new Categoria()
                {
                    id = 4,
                    nombre = "prueba 4"
                },
                cantidaddeaprobacion = 5 
            };
            var result = _dao.ActualizarModeloParaleloDAO(dtoModel);
            Assert.IsType<ModeloParaleloDTO>(result);
            return Task.CompletedTask;
        }

        //Elimina un modelo Paralelo
        [Fact(DisplayName = "Eliminar un Modelo Paralelo")]
        public Task EliminarModeloParaleloTest()
        {
            var id = 1;
            _contextMock.Setup(m => m.DbContext.SaveChanges()).Returns(1);

            var result = _dao.EliminarModeloParaleloDAO(id);

            Assert.IsType<ModeloParaleloDTO>(result);
            return Task.CompletedTask;
        }

        //Agregar modelo paralelo con excepcion
        [Fact(DisplayName = "Agregar un modelo paralelo con excepcion ")]
        public Task CrearModeloParaleloTestException()
        {
            _contextMock.Setup(m => m.DbContext.SaveChanges())
            .Throws(new ModeloParaleloException("", new NullReferenceException()));

            Assert.Throws<ModeloParaleloException>(()=>_dao.AgregarModeloParaleloDAO(NewModeloParalelo()));
            return Task.CompletedTask;
        }

        //Consultar una lista de modelos paralelos con excepcion
        [Fact(DisplayName = "Consultar todos los modelos paralelos con Excepcion")]
        public Task ConsultarModelosParalelosTestException()
        {
            // preparacion de los datos
            _contextMock.Setup(c => c.ModeloParalelos).Throws(new Exception());
            // prueba de la funcion
            Assert.Throws<ModeloParaleloException>(() =>_dao.ConsultarModelosParalelosDAO());
            return Task.CompletedTask;
        }

        //Consulta un modelo paralelo con excepcion
        [Fact(DisplayName = "Consultar Modelo Paralelo por Id con Excepcion")]
        public Task ConsultarModeloParaleloByIdValidarExceptionTest()
        {
            // preparacion de los datos
            _contextMock.Setup(m => m.ModeloParalelos.Find(It.IsAny<int>())).Throws(new Exception());
            // prueba de la funcion
            Assert.Throws<ModeloParaleloException>(()=> _dao.ObtenerModeloParaleloDAO(-1));
            return Task.CompletedTask;
        }

        //Actualizar un modelo paralelo con excepcion
        [Fact(DisplayName = "Validar Excepcion al actualizar")]
        public Task ActualizarModeloParaleloTestException()
        {
            _contextMock.Setup(m=>m.DbContext.SaveChanges())
            .Throws(new ModeloParaleloException("", new Exception()));

            Assert.Throws<ModeloParaleloException>(()=>_dao.ActualizarModeloParaleloDAO(It.IsAny<ModeloParalelo>()));
            return Task.CompletedTask;
        }

        //Eliminar un modelo paralelo con excepcion
        [Fact(DisplayName = "Eliminar un Modelo Paralelo con Excepcion")]
        public Task EliminarModeloParaleloExceptionTest()
        {
            _contextMock.Setup(m => m.ModeloParalelos.Find(It.IsAny<int>()))
                .Throws(new ModeloParaleloException("", new Exception()));

            Assert.Throws<ModeloParaleloException>(()=> _dao.EliminarModeloParaleloDAO(-1));
            return Task.CompletedTask;
        }
    }
}