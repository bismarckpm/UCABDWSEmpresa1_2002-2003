using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;

namespace ServicesDeskUCABWS.BussinessLogic.Mapper
{
 public class UsuarioMapper
 {
    public Usuario DtoToEntity(UsuarioDTO dto)
    {
        return new Usuario()
        {
            id = dto.Id,
            username = dto.Username,
            password = dto.Password,
            email = dto.Email
        };
    }

    public UsuarioDTO EntityToDto(Usuario user)
    {
        return new UsuarioDTO()
        {
            Id = user.id,
            Username = user.username!,
            Password = user.password!,
            Email = user.email!
        };
    }
 }   
}