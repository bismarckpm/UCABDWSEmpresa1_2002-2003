using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.BussinessLogic.Mapper;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.Controllers;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using AutoMapper;
using Moq;
using Xunit;
using Bogus;
using ServicesDeskUCABWS.Persistence.DAO.Implementations;
using ServicesDeskUCABWS.Persistence.Database;
using ServicesDeskUCABWS.Test.DataSeed;
using Org.BouncyCastle.Crypto.Fpe;
using TicketDao = ServicesDeskUCABWS.Persistence.DAO.Implementations.TicketDao;
using static ServicesDeskUCABWS.Reponses.AplicationResponse;
using ServicesDeskUCABWS.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ServicesDeskUCABWS.Test.Controllers
{
    public class TicketControllerTest
    {       
            private readonly TicketController _controller;
            private readonly Mock<ITicketDao> _servicesMock;
            private readonly Mock<ILogger<TicketController>> _log;
            public TicketCDTO ticketC = It.IsAny<TicketCDTO>();
            public TicketDTO ticket = It.IsAny<TicketDTO>();
            public TickeUDTO ticketU = It.IsAny<TickeUDTO>();
            public Ticket tick = It.IsAny<Ticket>();
            private readonly Mock<IMapper> _mapper;
            private readonly Mock<IMigrationDbContext> _dbMock;
            

        public TicketControllerTest()
            {
                _log = new Mock<ILogger<TicketController>>();
                _servicesMock = new Mock<ITicketDao>();
                _mapper = new Mock<IMapper>();
                _controller = new TicketController(_servicesMock.Object,_mapper.Object,_log.Object);
                _controller.ControllerContext = new ControllerContext();
                _controller.ControllerContext.HttpContext = new DefaultHttpContext();
                _controller.ControllerContext.ActionDescriptor = new ControllerActionDescriptor();
        }


        #region Casos Exitosos
        [Fact(DisplayName = "Create Ticket")]
        public Task CreateTicketTest()
        {
            _servicesMock.Setup(t => t.AgregarTicketDAO(new TickectCreateDTO()))
            .Returns(It.IsAny<string>());

            var result = _controller.CreateTicket(new TickectCreateDTO());

            Assert.IsType<ApplicationResponse<string>>(result);
            return Task.CompletedTask;
        }


        [Fact(DisplayName = "Get Tickets Mergeados")]
        public Task GetTicketsMergeadosTest()
        {
            _servicesMock.Setup(t => t.TicketsMergeados(1))
            .Returns(new List<TicketCDTO>());

            var result = _controller.GetTicketMergeados(1);

            Assert.IsType<ApplicationResponse<ICollection<TicketCDTO>>>(result);
            return Task.CompletedTask;
        }


        [Fact(DisplayName = "Get Tickets Mergeados")]
        public Task GetTicketCreadoPorTest()
        {
            _servicesMock.Setup(t => t.GetTicketCreadopor(1))
            .Returns(new List<TicketCDTO>());

            var result = _controller.GetTicketCreado(1);

            Assert.IsType<ApplicationResponse<ICollection<TicketCDTO>>>(result);
            return Task.CompletedTask;
        }


        [Fact(DisplayName = "Get Collection Ticket")]
            public Task GetCollectionTicketControllerTest()
            {
                _servicesMock.Setup(t=>t.GetTickets())
                .Returns(new List<TicketCDTO>());
                
                var result = _controller.GetCollection();
                
                Assert.IsType<ApplicationResponse<ICollection<TicketCDTO>>>(result);
                return Task.CompletedTask;
            }

            [Fact(DisplayName = "Get Tickets asignados")]
            public Task GetTicketsAsignadosControllerTest()
            {
                _servicesMock.Setup(t => t.GetTicketporusuarioasignado(1))
                .Returns(new List<TicketCDTO>());

                var result = _controller.GetTicketasignados(1);

                Assert.IsType<ApplicationResponse<ICollection<TicketCDTO>>>(result);
                return Task.CompletedTask;
            }
        

            [Fact(DisplayName = "Get Tickets por Departamento")]
            public Task GetTicketsPorDepartamentoControllerTest()
            {
                _servicesMock.Setup(t => t.GetTicketsDept(1))
                 .Returns(new List<TicketCDTO>());

                var result = _controller.GetTicketsPorDept(1);

                Assert.IsType<ApplicationResponse<ICollection<TicketCDTO>>>(result);
                return Task.CompletedTask;
            }

            [Fact(DisplayName = "Get Ticket")]
            public Task GetTicketControllerTest()
            {
                _servicesMock.Setup(t => t.GetTicket(1))
                .Returns(new TicketCDTO()
                {
                    id = It.IsAny<int>(),
                    idasignad = It.IsAny<int>(),
                    idestado = It.IsAny<int>(),
                    idprioridad = It.IsAny<int>(),
                    idcategoria = It.IsAny<int>(),
                    nombre = It.IsAny<string>(),
                    fecha = It.IsAny<DateTime>(),
                    descripcion = It.IsAny<string>(),
                    creadopor = It.IsAny<string>(),
                    asginadoa = It.IsAny<string>(),
                    prioridad = It.IsAny<string>(),
                    estado = It.IsAny<string>(),
                    categoria = It.IsAny<string>(),
                    departamento = It.IsAny<string>(),
                    departamentoid = It.IsAny<int>(),
                });

                var result = _controller.GetTicket(1);

                Assert.IsType<ApplicationResponse<TicketCDTO>>(result);
                return Task.CompletedTask;
            }

            [Fact(DisplayName = "Update Ticket")]
            public Task UpdateTicketControllerTest()
            {

                _servicesMock.Setup(t => t.CambiarEstado(It.IsAny<TickectEstadoDTO>()))
                .Returns(It.IsAny<string>());

                var result = _controller.UpdateTickect(1, new TickectEstadoDTO());

                Assert.IsType<ApplicationResponse<string>>(result);
                return Task.CompletedTask;
            }

            [Fact(DisplayName = "Merge Tickets")]
            public Task MergeTicketsTest()
            {
                _servicesMock.Setup(t => t.TikcetsRelacionados(It.IsAny<TicketsRelacionadosDTO>()))
                .Returns(It.IsAny<string>());

                var result = _controller.MergeTicket(It.IsAny<TicketsRelacionadosDTO>());

                Assert.IsType<ApplicationResponse<string>>(result);
                return Task.CompletedTask;
            }

            [Fact(DisplayName = "Eliminar Merge Tickets")]
            public Task EliminarMergeTicketsTest()
            {
                _servicesMock.Setup(t => t.EliminarRelacionMerge(It.IsAny<TicketsRelacionadosDTO>()))
                .Returns(It.IsAny<string>());

                var result = _controller.EliminarMerge(It.IsAny<TicketsRelacionadosDTO>());

                Assert.IsType<ApplicationResponse<string>>(result);
                return Task.CompletedTask;
            }

            [Fact(DisplayName = "Asignar Tickets")]
            public Task AsignarTicketsTest()
            {
                _servicesMock.Setup(t => t.AsignarTicket(It.IsAny<AsignarTicketDTO>()))
                .Returns(It.IsAny<string>());

                var result = _controller.AsignarTicket(It.IsAny<AsignarTicketDTO>());

                Assert.IsType<ApplicationResponse<string>>(result);
                return Task.CompletedTask;
            }

            [Fact(DisplayName = "Delegar Ticket")]
            public Task DelegarTicketTest()
            {
                _servicesMock.Setup(t => t.DelegarTicket(It.IsAny<TickectDelegadoDTO>()))
                .Returns(It.IsAny<string>());

                var result = _controller.DelegarTIcket(It.IsAny<int>(), It.IsAny<TickectDelegadoDTO>());

                Assert.IsType<ApplicationResponse<string>>(result);
                return Task.CompletedTask;
            }

        #endregion





        #region Casos Particulares
        [Fact(DisplayName = "Exception: Get lista de tickets")]
        public Task GetCollectionControllerTestException()
        {
            _servicesMock.Setup(t => t.GetTickets())
            .Throws(new TickectExeception("", new Exception()));

            var result = _controller.GetCollection();

            Assert.NotNull(result);
            Assert.False(result.Success);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Exception: Get tickets por departamento")]
        public Task GetTicketsDeptControllerTestException()
        {
            _servicesMock.Setup(t => t.GetTicketsDept(1))
            .Throws(new TickectExeception("", new Exception()));

            var result = _controller.GetTicketsPorDept(1);

            Assert.NotNull(result);
            Assert.False(result.Success);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Exception: Get ticket")]
        public Task GetTicketControllerTestException()
        {
            _servicesMock.Setup(t => t.GetTicket(1))
            .Throws(new TickectExeception("", new Exception()));

            var result = _controller.GetTicket(1);

            Assert.NotNull(result);
            Assert.False(result.Success);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Exception: Get tickets mergeados")]
        public Task GetTicketMergeadosControllerTestException()
        {
            _servicesMock.Setup(t => t.TicketsMergeados(1))
            .Throws(new TickectExeception("", new Exception()));

            var result = _controller.GetTicketMergeados(1);

            Assert.NotNull(result);
            Assert.False(result.Success);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Exception: Get tickets asignados")]
        public Task GetTicketAsignadosControllerTestException()
        {
            _servicesMock.Setup(t => t.GetTicketporusuarioasignado(1))
            .Throws(new TickectExeception("", new Exception()));

            var result = _controller.GetTicketasignados(1);

            Assert.NotNull(result);
            Assert.False(result.Success);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Exception: Get tickets creado por")]
        public Task CrearTicketTestException()
        {
            _servicesMock.Setup(t => t.GetTicketCreadopor(1))
            .Throws(new TickectExeception("", new Exception()));

            var result = _controller.GetTicketCreado(1);

            Assert.NotNull(result);
            Assert.False(result.Success);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Exception: Agregar ticket")]
        public Task CreateTicketExcepcion()
        {

            var dto = new TickectCreateDTO();

            _servicesMock.Setup(t => t.AgregarTicketDAO(dto))
            .Throws(new TickectExeception(null!, null!, null!, null!));


            Assert.Throws<NullReferenceException>(() => _controller.CreateTicket(dto));
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Exception: Update ticket")]
        public Task UpdateTicketExcepcion()
        {

            var dto = new TickectEstadoDTO();

            _servicesMock.Setup(t => t.CambiarEstado(dto))
            .Throws(new Exception());

            var result = _controller.UpdateTickect(dto.idticket, dto);

            Assert.NotNull(result);
            Assert.False(result.Success);
            //Assert.Throws<NullReferenceException>(() => _controller.UpdateTickect(dto.idticket, dto));
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Exception: Merge ticket")]
        public Task MergeTicketExcepcion()
        {

            var dto = new TicketsRelacionadosDTO();

            _servicesMock.Setup(t => t.TikcetsRelacionados(dto))
            .Throws(new TickectExeception(null!, null!, null!, null!));


            Assert.Throws<NullReferenceException>(() => _controller.MergeTicket(dto));
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Exception: Eliminar Merge ticket")]
        public Task EliminarMergeTicketExcepcion()
        {

            var dto = new TicketsRelacionadosDTO();

            _servicesMock.Setup(t => t.EliminarRelacionMerge(dto))
            .Throws(new TickectExeception(null!, null!, null!, null!));


            Assert.Throws<NullReferenceException>(() => _controller.EliminarMerge(dto));
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Exception: Asignar ticket")]
        public Task AsignarTicketExcepcion()
        {

            var dto = new AsignarTicketDTO();

            _servicesMock.Setup(t => t.AsignarTicket(dto))
            .Throws(new TickectExeception(null!, null!, null!, null!));


            Assert.Throws<NullReferenceException>(() => _controller.AsignarTicket(dto));
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Exception: Delegar ticket")]
        public Task DelegarTicketExcepcion()
        {

            var dto = new TickectDelegadoDTO();

            _servicesMock.Setup(t => t.DelegarTicket(dto))
            .Throws(new TickectExeception(null!, null!, null!, null!));


            Assert.Throws<NullReferenceException>(() => _controller.DelegarTIcket(It.IsAny<int>(), dto));
            return Task.CompletedTask;
        }

        #endregion
    }
}
