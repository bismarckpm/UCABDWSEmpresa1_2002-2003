using Bogus;
using Microsoft.EntityFrameworkCore;
using Moq;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Exceptions;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Database;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.Test.DataSeed;
using TicketDao = ServicesDeskUCABWS.Persistence.DAO.Implementations.TicketDao;

namespace ServicesDeskUCABWS.Test.DAOs
{
    public class TicketDAOTest
    {
        private readonly TicketDao _dao;
        private readonly Mock<IMigrationDbContext> _contextMock;
        private readonly Mock<ITicketDao> _servicesMock;
        private readonly Mock<IEmailDao> _emailMock;

        public TickectCreateDTO tk = new TickectCreateDTO
        {
            nombre = "nombre",
            fecha = It.IsAny<DateTime>(),
            descripcion = "descripcion",
            creadopor = 1,
            categoriaid = 1,
            Departamentoid = 1
        };

        public TickectEstadoDTO tkEstado = new TickectEstadoDTO
        {
            idestado=1,
            idticket=1
        };

        public AsignarTicketDTO tkAsignar = new AsignarTicketDTO
        {
            asginadoa=1,
            prioridadid=1,
            ticketid=1
        };

        public TickectDelegadoDTO tkDelgado = new TickectDelegadoDTO
        {
            idticket=1,
            idAsignadoa=1
        };

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
            var result = _dao.AgregarTicketDAO(tk);

            Assert.IsType<string>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Cambiar Estado")]
        public Task CambiarEstadoDAOTest()
        {
            var result = _dao.CambiarEstado(tkEstado);

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

            var result = _dao.AsignarTicket(tkAsignar);

            Assert.IsType<string>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Delegar Ticket")]
        public Task DelegarTicketTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);

            var result = _dao.DelegarTicket(tkDelgado);

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
        #endregion



        #region Casos Particulares

        [Fact(DisplayName = "Exception: Agregar Ticket")]
        public Task AgregarTicketExceptionTest()
        {

            _contextMock.Setup(x => x.DbContext.SaveChanges()).Throws(new DbUpdateConcurrencyException());

            Assert.Throws<TickectExeception>(() => _dao!.AgregarTicketDAO(null!));
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Excepcion: Asignar Ticket")]
        public Task AsignarTicketExcepcionTest()
        {
            _contextMock.Setup(x => x.Tickets)
                .Throws(new Exception());

            Assert.Throws<TickectExeception>(() => _dao.AsignarTicket(null!));
            return Task.CompletedTask;
        }


        [Fact(DisplayName = "Excepcion: Delegar Ticket")]
        public Task DelegarTicketExcepcionTest()
        {
            _contextMock.Setup(x => x.Tickets)
                .Throws(new Exception());

            Assert.Throws<TickectExeception>(() => _dao.DelegarTicket(null!));
            return Task.CompletedTask;
        }


        [Fact(DisplayName = "Excepcion: Tickets Relacionados")]
        public Task GetTicketsExceptionTest()
        {
            _contextMock.Setup(x => x.Tickets)
                .Throws(new Exception());

            Assert.Throws<TickectExeception>(() => _dao.GetTickets());
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Excepcion: Tickets Departamento")]
        public Task GetTicketsDepartamentoExceptionTest()
        {
            _contextMock.Setup(x => x.Tickets)
                .Throws(new Exception());

            Assert.Throws<TickectExeception>(() => _dao.GetTicketsDept(-1));
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Excepcion: Tickets Mergeados")]
        public Task TicketsMergeadosExceptionTest()
        {
            _contextMock.Setup(x => x.Tickets)
                .Throws(new Exception());

            Assert.Throws<TickectExeception>(() => _dao.TicketsMergeados(-1));
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Excepcion: Get Tickets")]
        public Task EliminarRelacionMergeExcepcionTest()
        {
            _contextMock.Setup(x => x.Tickets)
                .Throws(new Exception());

            Assert.Throws<TickectExeception>(() => _dao.TikcetsRelacionados(null!));
            return Task.CompletedTask;
        }


        [Fact(DisplayName = "Excepcion: Eliminar Relacion Merge")]
        public Task TicketsRelacionadosExcepcionTest()
        {
            _contextMock.Setup(x => x.TickectsRelacionados)
                .Throws(new Exception());

            Assert.Throws<TickectExeception>(() => _dao.EliminarRelacionMerge(null!));
            return Task.CompletedTask;
        }


        [Fact(DisplayName = "Exception: Get Ticket")]
        public Task GetTicketExceptionTest()
        {

            _contextMock.Setup(x => x.Tickets)
                .Throws(new Exception());

            Assert.Throws<TickectExeception>(() => _dao.GetTicket(-1));
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

            _contextMock.Setup(x => x.Usuario)
            .Throws(new Exception());

            Assert.Throws<TickectExeception>(() => _dao.GetTicketporusuarioasignado(-1));
            return Task.CompletedTask;
        }


        [Fact(DisplayName = "Exception: Get Ticket creado por")]
        public Task GetTicketCreadoPorExceptionTest()
        {

            _contextMock.Setup(x => x.Usuario)
            .Throws(new Exception());

            Assert.Throws<TickectExeception>(() => _dao.GetTicketCreadopor(-1));
            return Task.CompletedTask;
        }


        #endregion
    }
}
