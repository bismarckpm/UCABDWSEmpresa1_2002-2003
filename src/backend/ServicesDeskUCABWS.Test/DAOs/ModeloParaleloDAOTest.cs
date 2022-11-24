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

namespace ServicesDeskUCABWS.Test.DAOs
{
    public class ModeloParaleloDAOTest
    {

        private readonly ModeloParaleloDAO _dao;
        private readonly Mock<IMigrationDbContext> _contextMock;
        private readonly Mock<IModeloParaleloDAO> _servicesMock;

        public ModeloParaleloDAOTest()
        {
            // preparacion de los mocks
            var faker = new Faker();
            _contextMock = new Mock<IMigrationDbContext>();
            _dao = new ModeloParaleloDAO(_contextMock.Object);
            _servicesMock = new Mock<IModeloParaleloDAO>();
            _contextMock.SetupDbContextData();
        }

        [Fact(DisplayName = "Crear un modelo paralelo")]
        public async Task CrearModeloParaleloTest()
        {
            // preparacion de los datos
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            var mp = new ModeloParalelo()
            {
                categoriaId=2
            };

            // prueba de la funcion
            var result = await _dao.AgregarModeloParaleloDAO(mp);

            // verificacion de la prueba
            Assert.IsType<OkResult>(result);
        }

    }
}
