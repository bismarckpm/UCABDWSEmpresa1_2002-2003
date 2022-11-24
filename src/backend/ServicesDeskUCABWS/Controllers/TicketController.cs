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
    [Route("/Ticket")]
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
        [ProducesResponseType(200, Type = typeof(IEnumerable<Ticket>))]
        public IActionResult GetCollection()
        {
            var tickets =_ticketDao.GetTickets();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(tickets);
        }

        [HttpPost]
        [Route("CreateTicket")]
        public TicketDTO CreateTicket([FromBody] TicketDTO dto)
        {
            try
            {
                var data =  _ticketDao.AgregarTicketDAO(TicketMapper.DtoToEntity(dto));
                return (TicketDTO)data;

            }
            catch (Exception e)
            {
                _log.LogError(e.Message);
                throw e;
            }
        }
        [HttpPut]
        [Route("Actualizar/")]
        public TicketDTO ActualizarTicket([Required][FromBody] TicketDTO dto)
        {
            try
            {
                return _ticketDao.ModificarTicketDAO(TicketMapper.DtoToEntity(dto));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw ex.InnerException!;
            }
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public TicketDTO EliminarTicket([Required][FromRoute] int id)
        {
            try
            {

                return _ticketDao.EliminarTicketDAO(id);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw ex.InnerException!;
            }
        }

    }
}
