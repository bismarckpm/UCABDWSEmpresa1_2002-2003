using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface
{
    public interface IUsuarioDao
    {
        public UsuarioDTO AgregarUsuario(Usuario user);
    }
}