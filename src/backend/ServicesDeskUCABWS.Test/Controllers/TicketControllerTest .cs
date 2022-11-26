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

            [Fact(DisplayName = "Get Collection Ticket")]
            public Task GetCollectionTicketControllerTest()
            {
                _servicesMock.Setup(t=>t.GetTickets())
                .Returns(new List<TicketCDTO>());
                
                var result = _controller.GetCollection();
                
                Assert.IsType<OkObjectResult>(result);
                return Task.CompletedTask;
            }

            [Fact(DisplayName = "Get Tickets asignados")]
            public Task GetTicketsAsignadosControllerTest()
            {
                _servicesMock.Setup(t => t.GetTicketporusuarioasignado(1))
                .Returns(new List<TicketCDTO>());

                var result = _controller.GetTicketasignados(1);

                Assert.IsType<OkObjectResult>(result);
                return Task.CompletedTask;
            }

            [Fact(DisplayName = "Get Tickets por Estado")]
            public Task GetTicketsPorEstadosControllerTest()
            {
                _servicesMock.Setup(t => t.GetTicketporestado(1))
                .Returns(new List<TicketCDTO>());

                var result = _controller.GetTicketEstados(1);

                Assert.IsType<OkObjectResult>(result);
                return Task.CompletedTask;
            }

            [Fact(DisplayName = "Get Tickets por Departamento")]
            public Task GetTicketsPorDepartamentoControllerTest()
            {
                _servicesMock.Setup(t => t.GetTicketsPorDepartamento(1))
                .Returns(new List<TicketCDTO>());

                var result = _controller.GetTicketDepartamento(1);

                Assert.IsType<OkObjectResult>(result);
                return Task.CompletedTask;
            }

            [Fact(DisplayName = "Get Tickets por Categoria")]
            public Task GetTicketsPorCategoriaControllerTest()
            {
                _servicesMock.Setup(t => t.GetTicketsPorCategoria(1))
                .Returns(new List<TicketCDTO>());

                var result = _controller.GetTicketPorCategoria(1);

                Assert.IsType<OkObjectResult>(result);
                return Task.CompletedTask;
            }

            [Fact(DisplayName = "Get Ticket")]
            public Task GetTicketControllerTest()
            {
                _servicesMock.Setup(t => t.GetTicket(1))
                .Returns(new TicketCDTO());

                var result = _controller.GetTicket(1);

                Assert.IsType<OkObjectResult>(result);
                return Task.CompletedTask;
            }

            [Fact(DisplayName = "Update Ticket")]
            public Task UpdateTicketControllerTest()
            {
                var tk = new TickeUDTO() {
                    Id = 2,
                    nombre = "nombreticket",
                    fecha = It.IsAny<DateTime>(),
                    descripcion = "descripcion"
                };


                _mapper.Setup(m => m.Map<Ticket>(ticketU))
                .Returns(new Ticket());

                _servicesMock.Setup(r => r.GetTicket(It.IsAny<int>()))
                .Returns(new TicketCDTO());

                _servicesMock.Setup(t => t.Update(tick, It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);
                
                var result = _controller.UpdateTickect(2, It.IsAny<int>(), It.IsAny<int>(), tk, It.IsAny<int>(), It.IsAny<int>());

                Assert.IsType<OkObjectResult>(result);
                return Task.CompletedTask;
            }

            [Fact(DisplayName = "Update Ticket Nulo")]
            public Task UpdateTicketNullControllerTest()
            {
                _mapper.Setup(m => m.Map<Ticket>(ticketU))
                .Returns(new Ticket());

                _servicesMock.Setup(r => r.GetTicket(It.IsAny<int>()))
                .Returns(new TicketCDTO());

                _servicesMock.Setup(t => t.Update(tick, It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);

                var result = _controller.UpdateTickect(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), null, It.IsAny<int>(), It.IsAny<int>());

                Assert.IsType<BadRequestObjectResult>(result);
                return Task.CompletedTask;
            }

        //        PENDING: FORZAR UN VALOR NULO EN GETTICKET(ID)
            [Fact(DisplayName = "Update Ticket No Existe")]
            public Task UpdateTicketNoExisteControllerTest()
            {
                var tk = new TickeUDTO()
                {
                    Id = 3,
                    nombre = "nombre3",
                    descripcion = "descripcion3",
                    fecha = It.IsAny<DateTime>()
                };  

                _mapper.Setup(m => m.Map<Ticket>(ticketU))
                .Returns(new Ticket());

                _servicesMock.Setup(r => r.GetTicket(3))
                .Returns<Ticket>(null);

                var result = _controller.UpdateTickect(3, It.IsAny<int>(), It.IsAny<int>(), tk, It.IsAny<int>(), It.IsAny<int>());

                Assert.IsType<NotFoundObjectResult>(result);
                return Task.CompletedTask;
            }

            [Fact(DisplayName = "Update Ticket Diferente a TicketU Id")]
            public Task UpdateTicketErrorGuardarControllerTest()
            {
                var ticke = new TickeUDTO()
                {
                    Id = 2,
                    nombre = "nombreticket",
                    fecha = It.IsAny<DateTime>(),
                    descripcion = "descripcion"
                };
                _mapper.Setup(m => m.Map<Ticket>(ticketU))
                .Returns(new Ticket());

                _servicesMock.Setup(r => r.GetTicket(It.IsAny<int>()))
                .Returns(new TicketCDTO());

                _servicesMock.Setup(t => t.Update(tick, It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(false);

                var result = _controller.UpdateTickect(2, It.IsAny<int>(), It.IsAny<int>(), ticke, It.IsAny<int>(), It.IsAny<int>());

                Assert.IsType<ObjectResult>(result);
                return Task.CompletedTask;
            }

            [Fact(DisplayName = "Update Ticket No se Guardo")]
            public Task UpdateTicketDiferenteControllerTest()
            {
                var ticke = new TickeUDTO()
                {
                    Id = 2,
                    nombre = "nombreticket",
                    fecha = It.IsAny<DateTime>(),
                    descripcion = "descripcion"
                };
                _mapper.Setup(m => m.Map<Ticket>(ticketU))
                .Returns(new Ticket());

                _servicesMock.Setup(r => r.GetTicket(It.IsAny<int>()))
                .Returns(new TicketCDTO());

                _servicesMock.Setup(t => t.Update(tick, It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);

                var result = _controller.UpdateTickect(1, It.IsAny<int>(), It.IsAny<int>(), ticke, It.IsAny<int>(), It.IsAny<int>());

                Assert.IsType<BadRequestObjectResult>(result);
                return Task.CompletedTask;
            }

            [Fact(DisplayName = "Crear Ticket")]
            public Task CreateTicketControllerTest()
            {
                var ticke = new TicketDTO()
                {
                    nombre = "nombreticket",
                    fecha = It.IsAny<DateTime>(),
                    descripcion = "descripcion"
                };
                _mapper.Setup(m => m.Map<Ticket>(ticketU))
                .Returns(new Ticket());

                _servicesMock.Setup(r => r.AgregarTicketDAO(tick, It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);

                var result = _controller.CreateTicket(It.IsAny<int>(), It.IsAny<int>(), ticke, It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>());

                Assert.IsType<OkObjectResult>(result);
                return Task.CompletedTask;
            }

            [Fact(DisplayName = "Crear Ticket Nulo")]
            public Task CreateTicketNullControllerTest()
            {
                _mapper.Setup(m => m.Map<Ticket>(ticketU))
                .Returns(new Ticket());

                _servicesMock.Setup(r => r.AgregarTicketDAO(tick, It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);

                var result = _controller.CreateTicket(It.IsAny<int>(), It.IsAny<int>(), ticket, It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>());

                Assert.IsType<BadRequestObjectResult>(result);
                return Task.CompletedTask;
            }

            [Fact(DisplayName = "Crear Ticket Error al guardar")]
            public Task CreateTicketErrorGuardarControllerTest()
            {
                var ticke = new TicketDTO()
                {
                    nombre = "nombreticket",
                    fecha = It.IsAny<DateTime>(),
                    descripcion = "descripcion"
                };

                _mapper.Setup(m => m.Map<Ticket>(ticketU))
                .Returns(new Ticket());

                _servicesMock.Setup(r => r.AgregarTicketDAO(tick, It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(false);

                var result = _controller.CreateTicket(It.IsAny<int>(), It.IsAny<int>(), ticke, It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>());

                Assert.IsType<ObjectResult>(result);
                return Task.CompletedTask;
            }
    }
}
