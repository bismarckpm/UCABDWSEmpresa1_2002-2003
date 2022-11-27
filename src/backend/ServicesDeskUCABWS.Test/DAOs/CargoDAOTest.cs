using Bogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Controllers;
using ServicesDeskUCABWS.Exceptions;
using ServicesDeskUCABWS.Persistence.DAO.Implementations;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Database;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.Test.Configuraciones;
using ServicesDeskUCABWS.Test.DataSeed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace ServicesDeskUCABWS.Test.DAOs
{
    public class CargoDAOTest : BasePrueba
    {
        private readonly CargoDAO _dao;
        private readonly Mock<IMigrationDbContext> _contextMock;
        private readonly Mock<ICargoDAO> _servicesMock;


        public CargoDAOTest()
        {
            var faker = new Faker();
            _contextMock = new Mock<IMigrationDbContext>();
            var _logger = new NullLogger<CargoDAO>();
            var _mapper = ConfigurarAutoMapper();
            _dao = new CargoDAO(_mapper, _logger, _contextMock.Object);
            _servicesMock = new Mock<ICargoDAO>();
            _contextMock.SetupDbContextData();
        }

        #region CASOS DE EXITOS
        
        /// <summary>
        /// Crear cargo
        /// </summary>
        /// <returns></returns>
        [Fact(DisplayName = "Crear un Cargo")]
        public async Task CrearCargoTest()
        {
            // preparacion de los datos
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            var cargo = new Cargo()
            {
                id = 1,
                nombre = "Gerente",
                tipoCargoId = 1
            };

            var result = await _dao.AgregarCargoDAO(cargo);
            var cargoResult = result.Value;

            Assert.IsType<CargoDTO>(cargoResult);
        }

        /// <summary>
        /// Consultar lista de cargos
        /// </summary>
        /// <returns></returns>
        [Fact(DisplayName = "Consultar lista de cargos")]
        public async Task ConsultarListaCargosTest()
        {
            var result = await _dao.ConsultarCargoDAO();

            Assert.IsType<List<Cargo>>(result);
            Assert.Equal(2, result.Count);
        }

        /// <summary>
        /// Consultar un cargo especifico
        /// </summary>
        /// <returns></returns>
        [Fact(DisplayName = "Consultar Cargo por Id")]
        public async Task ConsultarCargoByIdTest()
        {
            _contextMock.Setup(e => e.Cargos.FindAsync(It.IsAny<int>()))
            .ReturnsAsync(new Cargo()
            {
                id = 1,
                nombre = "Gerente",
                tipoCargoId = 1
            });

            var id = 1;
            var result = await _dao.ObtenerCargoByIdDAO(id);
            var cargoResult = result.Value;

            Assert.IsType<Cargo>(cargoResult);
            Assert.Equal(id, cargoResult!.id);
        }

        /// <summary>
        /// Actualizar cargo
        /// </summary>
        /// <returns></returns>
        [Fact(DisplayName = "Actualizar un Cargo")]
        public async Task ActualizarCargoTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            _contextMock.Setup(e => e.Cargos.FindAsync(It.IsAny<int>())).ReturnsAsync(new Cargo()
            {
                id = 1,
                nombre = "Prueba",
                tipoCargoId = 1
            });
            var cargo = new Cargo()
            {
                id = 1,
                nombre = "Modificado",
                tipoCargoId = 1
            };

            var result = await _dao.ActualizarCargoDAO(cargo, cargo.id);
            var cargoResult = result.Value;

            Assert.IsType<Cargo>(cargoResult);
        }

        /// <summary>
        /// Eliminar cargo
        /// </summary>
        /// <returns></returns>
        [Fact(DisplayName = "Eliminar un Cargo")]
        public async Task EliminarCargoTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            _contextMock.Setup(e => e.Cargos.FindAsync(It.IsAny<int>())).ReturnsAsync(new Cargo()
            {
                id = 1,
                nombre = "Probar",
                tipoCargoId = 1
            });
            var id = 1;
            var result = await _dao.EliminarCargoDAO(id);

            Assert.IsType<OkResult>(result);

        }

        #endregion


        #region CASOS PARTICULARES
        
        /// <summary>
        /// Crear cargo con excepcion
        /// </summary>
        /// <returns></returns>
        [Fact(DisplayName = "Crear un Cargo con Excepcion")]
        public async Task CrearCargoTestException()
        {
            
            _contextMock.Setup(x => x.DbContext.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            
            await Assert.ThrowsAsync<Exception>(() => _dao!.AgregarCargoDAO(new Cargo()));
        }

        /// <summary>
        /// Consulta de lista de cargos, con excepcion
        /// </summary>
        /// <returns></returns>
        [Fact(DisplayName = "Consultar lista de Cargos con Excepcion")]
        public async Task ConsultarListaCargosTestException()
        {
            _contextMock.Setup(c => c.Cargos).Throws(new NullReferenceException());

            await Assert.ThrowsAsync<NullReferenceException>(() => _dao.ConsultarCargoDAO());
        }


        /// <summary>
        /// Consultar Cargo que no existe
        /// </summary>
        /// <returns></returns>
        [Fact(DisplayName = "Consultar Cargo que no existe")]
        public async Task ConsultarCargoIdNoExisteTest()
        {
            _contextMock.Setup(e => e.Cargos.FindAsync(It.IsAny<int>())).ReturnsAsync(null as Cargo);

            var id = 20;
            var result = await _dao.ObtenerCargoByIdDAO(id);
            var cargoResult = result.Value;

            Assert.Equal(0, cargoResult!.id);
        }

        /// <summary>
        /// Consultar Cargo por Id con Excepcion
        /// </summary>
        /// <returns></returns>
        [Fact(DisplayName = "Consultar Cargo por Id con Excepcion")]
        public async Task ConsultarCargoIdTestException()
        {
            _contextMock.Setup(c => c.Cargos.FindAsync(It.IsAny<int>())).ThrowsAsync(new Exception());

            await Assert.ThrowsAsync<Exception>(() => _dao.ObtenerCargoByIdDAO(-1));
        }


        /// <summary>
        /// Actualiza un cargo que no existe
        /// </summary>
        /// <returns></returns>
        [Fact(DisplayName = "No existe Cargo para actualizar")]
        public async Task ActualizaCargoNoExisteTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            _contextMock.Setup(c => c.Cargos.FindAsync(It.IsAny<int>())).ReturnsAsync(null as Cargo);
            
            var cargo = new Cargo();
            var result = await _dao.ActualizarCargoDAO(cargo, cargo.id);
            var cargoResult = result.Value;

            Assert.IsType<Cargo>(cargoResult);
        }

        



        /// <summary>
        /// Actualiza un cargo con execpcion
        /// </summary>
        /// <returns></returns>
        [Fact(DisplayName = "Actualizar un Cargo con Excepcion")]
        public async Task ActualizarCargoTestException()
        {
            var id = 1;
            _contextMock.Setup(x => x.DbContext.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());
            _contextMock.Setup(c => c.Cargos.FindAsync(It.IsAny<int>())).ThrowsAsync(new Exception());

            await Assert.ThrowsAsync<Exception>(() => _dao!.ActualizarCargoDAO(new Cargo(), id));
        }

        /// <summary>
        /// No existe Cargo para eliminar
        /// </summary>
        /// <returns></returns>
        [Fact(DisplayName = "No existe Cargo para eliminar")]
        public async Task EliminarCargoNoExisteTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            _contextMock.Setup(e => e.Cargos.FindAsync(It.IsAny<int>())).ReturnsAsync(null as Cargo);
            
            var id = 1;
            var result = await _dao.EliminarCargoDAO(id);

            Assert.IsType<NotFoundResult>(result);
        }

        /// <summary>
        /// Elimina un Cargo con Excepcion
        /// </summary>
        /// <returns></returns>
        [Fact(DisplayName = "Eliminar una Cargo con Excepcion")]
        public async Task EliminarCargoTestException()
        {
            var id = 1;
            _contextMock.Setup(x => x.DbContext.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());
            _contextMock.Setup(c => c.Cargos.FindAsync(It.IsAny<int>())).ThrowsAsync(new Exception());


            await Assert.ThrowsAsync<Exception>(() => _dao!.EliminarCargoDAO(id));
        }

        #endregion
    }
}
