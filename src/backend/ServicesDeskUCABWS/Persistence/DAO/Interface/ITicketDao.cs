using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Entity;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface
{
    public interface ITicketDao
    {
        ICollection<TicketCDTO>GetTickets();
        ICollection<TicketCDTO>GetTicketsDept(int idgrupo);
        
        TicketCDTO GetTicket(int ticketid);
        ICollection<TicketCDTO> GetTicketporusuarioasignado(int usuarioasignado);
        string AgregarTicketDAO(TickectCreateDTO ticket);
        string AsignarTicket( AsignarTicketDTO asignarTicket);
        string CambiarEstado(TickectEstadoDTO tickectEstado);
        string DelegarTicket(TickectDelegadoDTO tickectDelegado);
        string TikcetsRelacionados(TicketsRelacionadosDTO ticketsRelacionados);
        string EliminarRelacionMerge(TicketsRelacionadosDTO ticketsRelacionados);
        ICollection<TicketCDTO> TicketsMergeados(int ticketid);
    }
}
