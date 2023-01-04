using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using System.Collections;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface
{
    public interface IUsuarioDao
    {
        ICollection<Usuario> GetUsuario(); 
        string CreateUsuario(Usuario usuario, int cargoid, int Grupoid);
        string UpdateU(Usuario usuario);
        ICollection<UsuarioDTO> GetUsuariosPorDepartamento(int departamentoid);
        Usuario CreatePasswordHash(Usuario usuario, string clave);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        UsuarioDTO GetUsuarioPorEmail(string email);
    }
}