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
    public class JerarquicoTipoCargoDAOTest
    {
        private readonly ModeloJerarquicoTipoCargoDAO _dao;
        private readonly Mock<IMigrationDbContext> _contextMock;
        private readonly Mock<IModeloJerarquicoTipoCargo> _services;
        public JerarquicoTipoCargoDAOTest()
        {
            var faker = new Faker();
            _contextMock = new Mock<IMigrationDbContext>();
            _dao = new ModeloJerarquicoTipoCargoDAO(_contextMock.Object);
            _services = new Mock<IModeloJerarquicoTipoCargo>();
            _contextMock.SetupDbContextData();
        }

        #region  Casos Existosos

        [Fact(DisplayName = "Agregar Jerarquico Tipo Cargo")]
        public Task CreateJerarquicoTipoCargoDAOTest()
        {
            _contextMock.Setup(D => D.DbContext.SaveChanges())
                        .Returns(1);

            var result = _dao.CreateJerarquicoTipoCargoDAO(JerarquicoTest());

            Assert.IsType<JerarquicoTipoCargoDTO>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName= "Consular lista de Jerarquico Tipo Cargo")]
        public Task ConsultarJerarquicoTCargoDAOTest()
        {
            var result = _dao.ListadoJerarquicoTipoCargoDAO();

            Assert.IsType<List<JerarquicoTCargoCDTO>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Consultar Jerarquico Tipo Cargo por Id")]
        public Task ConsultarJerarquicoTCargoByIdDAOTest()
        {
            var id = 1;
            var result = _dao.ObtenerJerarquicoTipoCargoDAO(id);

            Assert.IsType<JerarquicoTCargoCDTO>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Actualizar Jerarquico Tipo Cargo")]
        public Task ActualizarJerarquicoTCargoDAOTest()
        {
            _contextMock.Setup(D => D.DbContext.SaveChanges())
                        .Returns(1);

            var result = _dao.ActualizarJerarquicoTipoCargoDAO(UpdateTest());

            Assert.IsType<JerarquicoTipoCargoDTO>(result);                        
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Eliminar Jerarquico Tipo Cargo")]
        public Task EliminarJerarquicoTCargoDAOTest()
        {
            _contextMock.Setup(D=> D.DbContext.SaveChanges())
                        .Returns(1);

            var id = 1;
            var result = _dao.EliminarJerarquicoTipoCargoDAO(id);

            Assert.IsType<JerarquicoTipoCargoDTO>(result);
            return Task.CompletedTask;
        }

        #endregion

        #region  Casos Particulares

        [Fact(DisplayName = "Agregar Jerarquico Tipo Cargo con excepcion")]
        public Task AgregarJerarquicoTCargoDAOExcepcionTest()
        {
            _contextMock.Setup(e => e.DbContext.SaveChanges())
                        .Throws(new Exception());

            Assert.Throws<ServicesDeskUcabWsException>(()=> _dao.CreateJerarquicoTipoCargoDAO(It.IsAny<ModeloJerarquicoCargos>()));
            return Task.CompletedTask;
        }

        [Fact(DisplayName ="Consultar listado de Jerarquico Tipo Cargo con excepcion")]
        public Task ConsultarJerarquicoTCargoDAOExceptionTest()
        {
            _contextMock.Setup(e => e.ModeloJerarquicoCargos)
                        .Throws(new Exception());

            Assert.Throws<ServicesDeskUcabWsException>(()=> _dao.ListadoJerarquicoTipoCargoDAO());                        
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Consultar Jerarquico Tipo Cargo por id con excepcion")]
        public Task ConsultarJerarquicoTCargoByIdDAOExceptionTest()
        {
            _contextMock.Setup(e => e.ModeloJerarquicoCargos.Find(It.IsAny<int>()))
                        .Throws(new Exception());

            Assert.Throws<ServicesDeskUcabWsException>(()=> _dao.ObtenerJerarquicoTipoCargoDAO(-1));
            return Task.CompletedTask;            
        }

        [Fact(DisplayName = "Actualizar Jerarquico Tipo Cargo con excepcion")]
        public Task ActualizarJerarquicoTCargoDAOExceptionTest()
        {
            _contextMock.Setup(e => e.DbContext.SaveChanges())
                        .Throws(new Exception());

            Assert.Throws<ServicesDeskUcabWsException>(() => _dao.ActualizarJerarquicoTipoCargoDAO(It.IsAny<ModeloJerarquicoCargos>()));            
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Eliminar Jerarquico Tipo Cargo con excepcion")]
        public Task EliminarJerarquicoTCargoDAOExceptionTest()
        {
            _contextMock.Setup(e => e.DbContext.SaveChanges())
                        .Throws(new Exception());

            Assert.Throws<ServicesDeskUcabWsException>(() => _dao.EliminarJerarquicoTipoCargoDAO(It.IsAny<int>()));
            return Task.CompletedTask;
        }

        #endregion

        #region  Metodo Privados

        private ModeloJerarquicoCargos JerarquicoTest()
        {
            return new ModeloJerarquicoCargos()
            {
                Id = 1,
                orden = 1,
                jerarquico = new ModeloJerarquico(),
                modelojerarquicoid = 1,
                TipoCargo = new TipoCargo(),
                TipoCargoid = 1

            };
        }

            private ModeloJerarquicoCargos UpdateTest()
            {
                return new ModeloJerarquicoCargos()
                {
                    Id = 2,
                    modelojerarquicoid = 3,
                    jerarquico = new ModeloJerarquico(),
                    orden = 3,
                    TipoCargoid = 3,
                    TipoCargo = new TipoCargo()
                };
            }
        #endregion

    }
}