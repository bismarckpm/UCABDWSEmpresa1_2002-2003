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
        //DECLARACION DE VARIABLES
        public readonly ITicketDao _ticketDao;
        public readonly IMapper _mapper;
        private readonly ILogger<TicketController> _log;

        //CONSTRUCTOR
        public TicketController(ITicketDao ticketDao, IMapper mapper, ILogger<TicketController> logger)
        {
            _ticketDao=ticketDao;
            _mapper=mapper;
            _log = logger;

        }

        //ENDPOINT PARA MOSTRAR LOS TICKETS
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

        //ENDPOINT PARA MOSTRAR LOS TICKETS QUE TIENEN UN USUARIO DE UN DEPARTAMENTO DADO EL ID
        [HttpGet("Tickects/{idusuario}")]
        public ApplicationResponse<ICollection<TicketCDTO>> GetTicketsPorDept([FromRoute] int idusuario)
        {
            var response = new ApplicationResponse<ICollection<TicketCDTO>>();
             try{
             response.Data =_ticketDao.GetTicketsDept(idusuario);
           
             } catch (TickectExeception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }


        //ENDPOINT PARA CONSULTAR UN TICKET DADO EL ID
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

        //ENDPOINT PARA MOSTRAR LOS TICKETS QUE FUERON MERGEADOS DADO EL ID
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

        //ENDPOINT PARA MOSTRAR LOS TICKETS QUE FUERON ASIGNADOS DANDO EL ID DEL USUARIO
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
        //ENDPOINT PARA MOSTRAR QUIÉN CREÓ LOS TICKETS DADO EL ID
        [HttpGet("Tickect/creado/{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TicketCDTO>))]
        public ApplicationResponse<ICollection<TicketCDTO>> GetTicketCreado([FromRoute] int id)
        {
            var response = new ApplicationResponse<ICollection<TicketCDTO>>();
             try{
             response.Data =_ticketDao.GetTicketCreadopor(id);
           
             } catch (TickectExeception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
           
        }

        //ENDPOINT PARA CAMBIAR EL ESTADO DE  UN TICKET DADO EL ID
        [HttpPut("Estado/{ticketid}")]
        public ApplicationResponse<string> UpdateTickect(int ticketid, TickectEstadoDTO tickectEstado)
        {
            var response = new ApplicationResponse<string>();
             try
            {
                response.Data= _ticketDao.CambiarEstado(tickectEstado);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.ToString();
            }
            return response;
        }

        //ENDPOINT PARA CREAR UN TICKET
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
        
        //ENDPOINT PARA MERGEAR UN TICKET CON OTRO 
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

        //ENDPOINT PARA ELIMINAR UN MERGE DE TICKETS 
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

        //ENDPOINT PARA ASIGNAR UN TICKET EN EL SISTEMA
        [HttpPut("AsignarTicket")]
        public ApplicationResponse<string> AsignarTicket(AsignarTicketDTO asignarTicket)
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

        //ENDPOINT PARA DELEGAR UN TICKET DADO EL ID
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
