using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Entity;

namespace ServicesDeskUCABWS.Controllers
{

    [ApiController]
    [Route("/Tickect")]
    public class TickectController : Controller
    {
        public readonly ITicketcDao _ticketcDao;
        public readonly IMapper _mapper;

        public TickectController(ITicketcDao ticketcDao, IMapper mapper)
        {
            _ticketcDao=ticketcDao;
            _mapper=mapper;

        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Ticket>))]
        public IActionResult GetCollection()
        {
            var tickets =_ticketcDao.GetTikects();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(tickets);
        }

    }
}
