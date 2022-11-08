using ServicesDeskUCABWS.BussinessLogic.DTO;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface
{
    public interface IEmailDao
    {
         public void SendEmail( EmailDTO emailDto);
    }
}