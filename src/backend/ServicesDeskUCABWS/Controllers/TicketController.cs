using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.BussinessLogic.Mapper;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Entity;
using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCABWS.Controllers
{

    [ApiController]
    [Route("/Tickets")]
    public class TicketController : Controller
    {
        public readonly ITicketDao _ticketDao;
        public readonly IMapper _mapper;
        private readonly ILogger<TicketController> _log;

        public TicketController(ITicketDao ticketDao, IMapper mapper, ILogger<TicketController> logger)
        {
            _ticketDao=ticketDao;
            _mapper=mapper;
            _log = logger;

        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TicketCDTO>))]
        public IActionResult GetCollection()
        {
            var tickets =_ticketDao.GetTickets();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(tickets);
        }

        [HttpGet("Tickect/{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TicketCDTO>))]
        public IActionResult GetTicket([FromRoute] int id)
        {
            var tickets =_ticketDao.GetTicket(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(tickets);
        }

        [HttpPut("{ticketid}")]
        public IActionResult UpdateTickect(int ticketid, 
            [FromQuery] int asignadoaid, [FromQuery] int prioridadid,
            [FromBody] TickeUDTO ticketupdate, [FromQuery] int Estadoid)
        {
            if (ticketupdate == null)
                return BadRequest(ModelState);

            if (ticketid != ticketupdate.Id)
                return BadRequest(ModelState);

            if (_ticketDao.GetTicket(ticketid) == null)
                return NotFound();


            if (!ModelState.IsValid)
                return BadRequest();

            var ticketmap = _mapper.Map<Ticket>(ticketupdate);

            if (!_ticketDao.Update(ticketmap,asignadoaid,prioridadid,Estadoid))
            {
                ModelState.AddModelError("", "Hubo un error ");
                return StatusCode(500, ModelState);
            }
            return Ok("Ticket Actualizado");
        }

        [HttpPost]
        [Route("CreateTicket")]
         public IActionResult CreateTicket([FromQuery] int creadopor,[FromQuery] int asignadaa, [FromBody] TicketDTO ticket, [FromQuery] int prioridad,[FromQuery] int estatud)
        {
            if (ticket == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var Tickectmap = _mapper.Map<Ticket>(ticket);
            if (!_ticketDao.AgregarTicketDAO(Tickectmap,creadopor,asignadaa,prioridad,estatud))
            {
                ModelState.AddModelError("", "Error al guardar");
                return StatusCode(500, ModelState);
            }
            return Ok("Ticket Creado");
        }
    }
}
