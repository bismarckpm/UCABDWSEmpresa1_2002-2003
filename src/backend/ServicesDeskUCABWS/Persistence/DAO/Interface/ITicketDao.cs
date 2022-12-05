using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Entity;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface
{
    public interface ITicketDao
    {
        ICollection<TicketCDTO>GetTickets();
        TicketCDTO GetTicket(int ticketid);
        ICollection<TicketCDTO> GetTicketporestado(int estado);
        ICollection<TicketCDTO> GetTicketsPorDepartamento(int departamento);
         ICollection<TicketCDTO> GetTicketsPorCategoria(int categoriaid);
        ICollection<TicketCDTO> GetTicketporusuarioasignado(int usuarioasignado);
        bool Save();
        bool AgregarTicketDAO(Ticket ticket,int creadopor,int grupoid, int categoriaid);
        bool AsignarTicket( AsignarTicketDTO asignarTicket);
        bool Update(Ticket ticket, int asignadoaid, int prioridadid, int Estadoid, int categoriaid);
    }
}
