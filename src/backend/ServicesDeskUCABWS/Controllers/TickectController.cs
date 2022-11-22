using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Entity;

namespace ServicesDeskUCABWS.Controllers
{

    [ApiController]
    [Route("/Ticket")]
    public class TicketController : Controller
    {
        public readonly ITicketDao _ticketDao;
        public readonly IMapper _mapper;

        public TicketController(ITicketDao ticketDao, IMapper mapper)
        {
            _ticketDao=ticketDao;
            _mapper=mapper;

        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Ticket>))]
        public IActionResult GetCollection()
        {
            var tickets =_ticketDao.GetTikects();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(tickets);
        }

    }
}
