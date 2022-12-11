using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.BussinessLogic.Mapper;
using ServicesDeskUCABWS.Exceptions;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Entity;
using System.ComponentModel.DataAnnotations;
using static ServicesDeskUCABWS.Reponses.AplicationResponse;

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
        public ApplicationResponse<ICollection<TicketCDTO>> GetCollection()
        {
            var response = new ApplicationResponse<ICollection<TicketCDTO>>();
             try{
             response.Data =_ticketDao.GetTickets();
           
             } catch (TickectExeception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }
        

        [HttpGet("Tickect/{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TicketCDTO>))]
        public ApplicationResponse<TicketCDTO> GetTicket([FromRoute] int id)
        {
            var response = new ApplicationResponse<TicketCDTO>();
             try{
             response.Data =_ticketDao.GetTicket(id);
           
             } catch (TickectExeception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }
        [HttpGet("TickectMergeados/{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Ticket>))]
        public ApplicationResponse<ICollection<TicketCDTO>> GetTicketMergeados([FromRoute] int id)
        {
            var response = new ApplicationResponse<ICollection<TicketCDTO>>();
             try{
             response.Data =_ticketDao.TicketsMergeados(id);
           
             } catch (TickectExeception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }


        [HttpGet("Tickect/asginado/{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TicketCDTO>))]
        public ApplicationResponse<ICollection<TicketCDTO>> GetTicketasignados([FromRoute] int id)
        {
            var response = new ApplicationResponse<ICollection<TicketCDTO>>();
             try{
             response.Data =_ticketDao.GetTicketporusuarioasignado(id);
           
             } catch (TickectExeception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
           
        }
        

        [HttpPut("Estado/{ticketid}")]
        public ApplicationResponse<string> UpdateTickect(int ticketid, TickectEstadoDTO tickectEstado)
        {
            var response = new ApplicationResponse<string>();
             try
            {
                response.Data= _ticketDao.CambiarEstado(tickectEstado);
            }
            catch (TickectExeception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TickectCreateDTO>))]
        [Route("CreateTicket")]
         public ApplicationResponse<string> CreateTicket( [FromBody] TickectCreateDTO ticket)
        {
            var response = new ApplicationResponse<string>();
             try
            {
                response.Data= _ticketDao.AgregarTicketDAO(ticket);
            }
            catch (TickectExeception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }
        [HttpPost]
        [Route("MergearTickets")]
         public ApplicationResponse<string> MergeTicket( [FromBody] TicketsRelacionadosDTO ticket)
        {
            var response = new ApplicationResponse<string>();
             try
            {
                response.Data= _ticketDao.TikcetsRelacionados(ticket);
            }
            catch (TickectExeception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }
        [HttpDelete]
        [Route("EliminarMerge")]
         public ApplicationResponse<string> EliminarMerge([FromBody] TicketsRelacionadosDTO tickects )
        {
            var response = new ApplicationResponse<string>();
             try
            {
                response.Data= _ticketDao.EliminarRelacionMerge(tickects);
            }
            catch (TickectExeception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }

        [HttpPut("AsignarTicket/{ticketid}")]
        public ApplicationResponse<string> AsignarTicket(int ticketid, AsignarTicketDTO asignarTicket)
        {
              var response = new ApplicationResponse<string>();
             try
            {
                response.Data= _ticketDao.AsignarTicket(asignarTicket);
            }
            catch (TickectExeception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
          
        }
         [HttpPut("DelegarTicket/{ticketid}")]
        public ApplicationResponse<string> DelegarTIcket(int ticketid, TickectDelegadoDTO delegadoDTO)
        {
              var response = new ApplicationResponse<string>();
             try
            {
                response.Data= _ticketDao.DelegarTicket(delegadoDTO);
            }
            catch (TickectExeception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
          
        }
    }
}
