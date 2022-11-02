using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface
{
    public interface IUsuarioDao
    {
        ICollection<Usuario> GetUsuarios();

        Usuario GetUsuario(string username);
        bool UsuarioExists(string usuarname, string password);   
        Usuario ChangePassword(string usuarname, string newpassword, string confirmationpassword);       
        bool CreateUsuario(Usuario usuario);
        bool Save();
    }
}