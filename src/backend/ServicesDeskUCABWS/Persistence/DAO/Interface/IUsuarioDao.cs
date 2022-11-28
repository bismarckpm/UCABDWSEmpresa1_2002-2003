using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using System.Collections;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface
{
    public interface IUsuarioDao
    {
        ICollection<Usuario> GetUsuarios();
        ICollection<Empleado> GetEmpleados();
        ICollection<administrador> GetAdministradores();
        ICollection<Cliente> GetClientes();

     
        Usuario ChangePassword(string usuarname, string newpassword, string confirmationpassword);       
        bool CreateUsuario(Usuario usuario, int cargoid, int Departamentoid);
        Usuario GetUsuarioTrimToUpper(RegistroDTO administratorDTO);
         ICollection<UsuarioDTO> GetUsuariosPorDepartamento(int departamentoid);
        bool Save();

        Usuario CreatePasswordHash(Usuario usuario, string clave);

        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}