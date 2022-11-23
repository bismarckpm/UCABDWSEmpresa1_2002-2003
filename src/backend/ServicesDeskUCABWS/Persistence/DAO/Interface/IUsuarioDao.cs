using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using System.Collections;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface
{
    public interface IUsuarioDao
    {
        ICollection<Usuario> GetUsuarios();

        Usuario GetUsuario(string username);
        bool UsuarioExists(string usuarname, string password);   
        Usuario ChangePassword(string usuarname, string newpassword, string confirmationpassword);       
        bool CreateUsuario(Usuario usuario, int cargoid, int Departamentoid);
        Usuario GetUsuarioTrimToUpper(RegistroDTO administratorDTO);
        bool Save();

        Usuario CreatePasswordHash(Usuario usuario, string clave);

        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}