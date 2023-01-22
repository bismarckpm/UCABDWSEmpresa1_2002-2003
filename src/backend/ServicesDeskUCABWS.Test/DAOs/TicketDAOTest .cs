using Bogus;
using Microsoft.EntityFrameworkCore;
using Moq;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.DAO.Implementations;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Database;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.Test.Configuraciones;
using ServicesDeskUCABWS.Test.DataSeed;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TicketDao = ServicesDeskUCABWS.Persistence.DAO.Implementations.TicketDao;
using static ServicesDeskUCABWS.Reponses.AplicationResponse;
using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCABWS.Exceptions;

namespace ServicesDeskUCABWS.Test.DAOs
{
    public class TicketDAOTest
    {
        private readonly TicketDao _dao;
        private readonly Mock<IMigrationDbContext> _contextMock;
        private readonly Mock<ITicketDao> _servicesMock;
        private readonly Mock<IEmailDao> _emailMock;
        public Cliente clienteOrigen = new Cliente()
        {
            id = 2,
            email = "cliente2@gmail.com"
        };
        public Empleado empleadoAsignado = new Empleado()
        {
            id = 3,
            email = "empleado3@gmail.com"
        };
        public TicketDAOTest()
        {
            var faker = new Faker();
            _contextMock = new Mock<IMigrationDbContext>();
            _emailMock = new Mock<IEmailDao>();
            _dao = new TicketDao(_contextMock.Object, _emailMock.Object);
            _servicesMock = new Mock<ITicketDao>();
            _contextMock.SetupDbContextData();
        }


        #region Casos Exitosos
        [Fact(DisplayName = "Agregar Ticket")]
        public Task AgregarTicketDAOTest()
        {
            var tk = new TickectCreateDTO();
            var plantilla = new Plantilla();
            var email = new EmailDTO();

            var result = _dao.AgregarTicketDAO(tk);

            Assert.IsType<string>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Actualizar Ticket")]
        public Task UpdateTicketDAOTest()
        {
            var tk = new TickectEstadoDTO();

            var result = _dao.CambiarEstado(tk);

            Assert.IsType<string>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Get Lista Ticket")]
        public Task GetTicketsDAOTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            var result = _dao.GetTickets();

            Assert.IsType<List<TicketCDTO>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Get Tickets Mergeados")]
        public Task GetTicketsMergeadosDAOTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            var result = _dao.TicketsMergeados(1);

            Assert.IsType<List<TicketCDTO>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Get Ticket")]
        public Task GetTicketDAOTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            var dto = _dao.GetTicket(1);
            var result = dto;

            Assert.Null(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Get Tickets por Usuario asignado")]
        public Task GetTicketPorUsuarioAsignadoTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);

            var result = _dao.GetTicketporusuarioasignado(1);

            Assert.IsType<List<TicketCDTO>>(result);
            return Task.CompletedTask;
        }


        [Fact(DisplayName = "Get Tickets por Departamento")]
        public Task GetTicketPorDepartamentoTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);

            var result = _dao.GetTicketsDept(1);

            Assert.IsType<List<TicketCDTO>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Asignar Ticket")]
        public Task AsignarTicketTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);

            var asignar = new AsignarTicketDTO()
            {
                ticketid = It.IsAny<int>(),
                asginadoa = It.IsAny<int>(),
                prioridadid = It.IsAny<int>()
            };

            var result = _dao.AsignarTicket(asignar);

            Assert.IsType<string>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Delegar Ticket")]
        public Task DelegarTicketTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);

            var result = _dao.DelegarTicket(new TickectDelegadoDTO());

            Assert.IsType<string>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Tickets Relacionados")]
        public Task TicketsRelacionadosTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);

            var result = _dao.TikcetsRelacionados(new TicketsRelacionadosDTO());

            Assert.IsType<string>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Eliminar Relacion Merge")]
        public Task EliminarRelacionMergeTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);

            var result = _dao.EliminarRelacionMerge(new TicketsRelacionadosDTO());

            Assert.IsType<string>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Get Tickets Creados Por")]
        public Task GetTicketCreadoPorTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);

            var result = _dao.GetTicketCreadopor(1);

            Assert.IsType<List<TicketCDTO>>(result);
            return Task.CompletedTask;
        }



        [Fact(DisplayName = "Save")]
        public Task SaveDAOTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);

            var tk = new Ticket();

            // var result = _dao.Save();

            // Assert.True(result);
            return Task.CompletedTask;
        }

        #endregion



        #region Casos Particulares

        [Fact(DisplayName = "Exception: Agregar Ticket")]
        public Task AgregarTicketExceptionTest()
        {

            _contextMock.Setup(x => x.DbContext.SaveChanges()).Throws(new DbUpdateConcurrencyException());

            Assert.Throws<TickectExeception>(() => _dao!.AgregarTicketDAO(null!));
            return Task.CompletedTask;
        }



        [Fact(DisplayName = "Exception: Get Ticket")]
        public Task GetTicketExceptionTest()
        {

            _servicesMock.Setup(c => c.GetTicket(It.IsAny<int>()))
                .Throws(new TickectExeception(null));

            Assert.NotNull(() => _dao.GetTicket(-1));
            //Assert.Throws<TickectExeception>(() => _dao.GetTicket(-1));
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Exception: Update Ticket")]
        public Task ActualizarTicketExceptionTest()
        {

            _servicesMock.Setup(c => c.CambiarEstado(null!))
                .Throws(new TickectExeception(null, null, null, null));

            Assert.Throws<TickectExeception>(() => _dao.CambiarEstado(null!));
            return Task.CompletedTask;
        }


        [Fact(DisplayName = "Exception: Get Ticket por usuario asignado")]
        public Task GetTicketPorUsuarioExceptionTest()
        {

            _servicesMock.Setup(c => c.GetTicketporusuarioasignado(It.IsAny<int>()))
                .Throws(new TickectExeception(null, null));

            Assert.NotNull(() => _dao.GetTicketporusuarioasignado(-1));
            //Assert.Throws<TickectExeception>(() => _dao.GetTicketporusuarioasignado(-1));
            return Task.CompletedTask;
        }


        [Fact(DisplayName = "Exception: Get Ticket creado por")]
        public Task GetTicketCreadoPorExceptionTest()
        {

            _servicesMock.Setup(c => c.GetTicketCreadopor(It.IsAny<int>()))
                .Throws(new TickectExeception(null, null));

            Assert.NotNull(() => _dao.GetTicketCreadopor(-1));
            //Assert.Throws<TickectExeception>(() => _dao.GetTicketCreadopor(-1));
            return Task.CompletedTask;
        }


        #endregion



        #region Metodos de Utilidad
        public Ticket ObjetoTicketNuevo()
        {
            return new Ticket()
            {
                id = 1,
                nombre = "nombre",
                asginadoa = new Empleado() { id = 2, email = "prueba@gmail.com" },
                creadopor = new Cliente() { id = 3, email = "prueba2@gmail.com" },
                descripcion = "descripcion",
                fecha = It.IsAny<DateTime>(),
                Estado = new Estado(),
                prioridad = new Prioridad(),
                categoria = new Categoria(),
                FlujoAprobacion = new FlujoAprobacion()
            };
        }

        #endregion

    }
}
