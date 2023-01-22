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

        [Fact(DisplayName = "Agregar Ticket")]
        public Task AgregarTicketDAOTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            _emailMock.Setup(x => x.SendEmail(It.IsAny<EmailDTO>()));
            _contextMock.Setup(x => x.Estados.Where(e => e.nombre == "En espera")).Returns(new List<Estado>() { new Estado() { id = 1, nombre = "En espera" } }.AsQueryable());
            _contextMock.Setup(x => x.Plantillas.Find()).Returns(new Plantilla() { id = 1, titulo = "Plantilla 1", cuerpo = "Cuerpo de la plantilla 1" });

            var tk = new TickectCreateDTO()
            {
                nombre = "nombre",
                creadopor = It.IsAny<int>(),
                descripcion = "descripcion",
                fecha = It.IsAny<DateTime>(),
                categoriaid = It.IsAny<int>(),
                Departamentoid = It.IsAny<int>()
            };

            var result = _dao.AgregarTicketDAO(tk);

            Assert.IsType<string>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Actualizar Ticket")]
        public Task UpdateTicketDAOTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);

            var tk = new Ticket()
            {
                id = 1,
                nombre = "nombre",
                asginadoa = new Empleado() { id = 5, email = "test1@gmail.com" },
                creadopor = new Empleado() { id = 6, email = "test2@gmail.com" },
                descripcion = "descripcion",
                fecha = It.IsAny<DateTime>(),
                Estado = new Estado(),
                prioridad = new Prioridad(),
                categoria = new Categoria(),
                FlujoAprobacion = new FlujoAprobacion()
            };

            // var result = _dao.Update(tk, 1, 1, 1, 1);

            // Assert.True(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Get Lista Ticket")]
        public Task GetTicketsDAOTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);

            var tk = new Ticket();

            var result = _dao.GetTickets();

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

        [Fact(DisplayName = "Get Tickets por Estado")]
        public Task GetTicketPorEstadoTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);

            // var result = _dao.GetTicketporestado(1);

            // Assert.IsType<List<TicketCDTO>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Get Tickets por Departamento")]
        public Task GetTicketPorDepartamentoTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);

            // var result = _dao.GetTicketsPorDepartamento(1);

            // Assert.IsType<List<TicketCDTO>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Get Tickets por Categoria")]
        public Task GetTicketPorCategoriaTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);

            // var result = _dao.GetTicketsPorCategoria(1);

            // Assert.IsType<List<TicketCDTO>>(result);
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
    }
}
