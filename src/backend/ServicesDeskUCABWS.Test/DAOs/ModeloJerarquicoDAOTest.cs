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
        const int IdModeloJerarquico = 1;
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

    #region  Caso Existosos

        /// <summary>
        /// Crear Modelo Jerarquico
        /// </summary>
        /// <returns></returns>
        [Fact(DisplayName = "Agregar nuevo modelo Jerarquico")]
        public Task CreateModeloJerarquicoDAOTest()
        {
            _contextMock.Setup(j => j.DbContext.SaveChanges()).Returns(1);

            var result = _dao.AgregarModeloJerarquicoDAO(NewModeloJerarquico());

            Assert.IsType<ModeloJerarquicoDTO>(result);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Consultar todos los modelos jerarquicos 
        /// </summary>
        /// <returns></returns>
        [Fact(DisplayName = "Consultar todos los modelos jerarquicos")]
        public Task ConsultarModelosJerarquicosTest()
        {
            // prueba de la funcion
            var result =  _dao.ConsultarModeloJerarquicosDAO();

            // verificacion de la prueba
            Assert.IsType<List<ModeloJCDTO>>(result);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Consultar un modelo jerarquico por id 
        /// </summary>
        /// <returns></returns>
        [Fact(DisplayName = "Consultar modelo jerarquico por Id")]
        public Task ConsultarModeloJerarquicoIdTest()
        {
            var id = 1;
            // prueba de la funcion
            var result =  _dao.ObtenerModeloJerarquicoDAO(id);
            // verificacion de la prueba
            Assert.IsType<ModeloJCDTO>(result);
            return Task.CompletedTask;
        }

// Problema con la prueba al actualizar
        /// <summary>
        /// Actualiza Un modelo Jerarquico 
        /// </summary>
        /// <returns></returns>
    [Fact(DisplayName = "Actualizar ModeloJerarquico")]
    public Task ActualizarModeloJerarquicoTest()
    {
            _contextMock.Setup(m=> m.DbContext.SaveChanges()).Returns(1);
            var dtoModel = new ModeloJerarquico()
            {id = 3, 
            nombre = "Prueba.", 
            categoriaid = 4,
            Jeraruia = new List<ModeloJerarquicoCargos>()
            {
                new ModeloJerarquicoCargos()
                {
                    Id = 1,
                    orden = 2,
                    TipoCargoid = 1,
                    modelojerarquicoid =3
                }
            },
            categoria = new Categoria()
            {
                id = 4,
                nombre = "prueba 4"
            }
            };

                var result = _dao.ActualizarModeloJerarquicoDAO(dtoModel);

        Assert.IsType<ModeloJerarquicoDTO>(result);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Elimina un modelo jerarquico por id 
    /// </summary>
    /// <returns></returns>
    [Fact(DisplayName = "Eliminar un Modelo Jerarquico")]
    public Task EliminarModeloJerarquicoTest()
    {
        var id = 1;
        _contextMock.Setup(m => m.DbContext.SaveChanges()).Returns(1);
        
        var result = _dao.EliminarModeloJerarquicoDAO(id);

        Assert.IsType<ModeloJerarquicoDTO>(result);
        return Task.CompletedTask;
    }

    #endregion

    #region  Caso Particulares

        /// <summary>
        /// Agregar Modelo Jerarquico con Excepcion 
        /// </summary>
        /// <returns></returns>
        [Fact(DisplayName = "Agregar un modelo jerarquico con excepcion ")]
        public Task CrearModeloJerarquicoTestException()
        {
            _contextMock.Setup(m => m.DbContext.SaveChanges())
            .Throws(new ServicesDeskUcabWsException("", new NullReferenceException()));

            Assert.Throws<ServicesDeskUcabWsException>(()=>_dao.AgregarModeloJerarquicoDAO(NewModeloJerarquico()));
            return Task.CompletedTask;
        }

        /// <summary>
        /// Consultar todos los modelos jerarquicos con excepcion 
        /// </summary>
        /// <returns></returns>
        [Fact(DisplayName = "Consultar todos los modelos jerarquicos con Excepcion")]
        public Task ConsultarModelosJerarquicosTestException()
        {
            // preparacion de los datos
            _contextMock.Setup(c => c.ModeloJerarquicos).Throws(new Exception());

            // prueba de la funcion
            Assert.Throws<ServicesDeskUcabWsException>(() =>_dao.ConsultarModeloJerarquicosDAO());
            return Task.CompletedTask;
        }

        /// <summary>
        /// Consultar un modelo jerarquico por id con Excepcion
        /// </summary>
        /// <returns></returns>
        [Fact(DisplayName = "Consultar Modelo Jerarquico por Id con Excepcion")]
        public Task ConsultarModeloJerarquicoByIdValidarExceptionTest()
        {
            _contextMock.Setup(m => m.ModeloJerarquicos.Find(It.IsAny<int>())).Throws(new Exception());

            Assert.Throws<ServicesDeskUcabWsException>(()=> _dao.ObtenerModeloJerarquicoDAO(-1));
            return Task.CompletedTask;
        }

        /// <summary>
        /// Actualiza un modelo jerarquico con Excepcion
        /// </summary>
        /// <returns></returns>
    [Fact(DisplayName = "Validar Excepcion al actualizar")]
    public Task ActualizarModeloJerarquicoTestException()
    {
        _contextMock.Setup(m=>m.DbContext.SaveChanges())
        .Throws(new ServicesDeskUcabWsException("", new Exception()));

        Assert.Throws<ServicesDeskUcabWsException>(()=>_dao.ActualizarModeloJerarquicoDAO(It.IsAny<ModeloJerarquico>()));
        return Task.CompletedTask;
    }

    [Fact(DisplayName = "Eliminar un Modelo Jerarquico con Excepcion")]
    public Task EliminarModeloJerarquicoExceptionTest()
    {
        _contextMock.Setup(m => m.ModeloJerarquicos.Find(It.IsAny<int>()))
            .Throws(new ServicesDeskUcabWsException("", new Exception()));

        Assert.Throws<ServicesDeskUcabWsException>(()=> _dao.EliminarModeloJerarquicoDAO(-1));
        return Task.CompletedTask;
    }
    
    #endregion

    #region  Metodo Privados
        private ModeloJerarquico NewModeloJerarquico()
        {
            return new ModeloJerarquico{
                    id = 1,
                    nombre = "Prueba Modelo",
                    categoriaid = 1,
                    categoria = new Categoria()
                    {
                        id = 1,
                        nombre = "Guardado"
                    },
                    Jeraruia = new List<ModeloJerarquicoCargos>()
                    };
        }

        private ModeloJerarquicoCargos NewModelJerarquicCargos()
        {
            return new ModeloJerarquicoCargos()
            {
                Id = 1,
                orden = 2,
                modelojerarquicoid = 1,
                TipoCargoid = 1
            };
        }
        #endregion
    }
}