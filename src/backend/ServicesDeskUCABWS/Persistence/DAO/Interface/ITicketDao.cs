﻿using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Entity;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface
{
    public interface ITicketDao
    {
        ICollection<TicketCDTO>GetTickets();
        TicketCDTO GetTicket(int ticketid);

        bool Save();
        bool AgregarTicketDAO(Ticket ticket,int creadopor, int asignadaa, int prioridad, int estatud);
      bool Update(Ticket ticket, int asignadoaid, int prioridadid, int Estadoid);
    }
}
