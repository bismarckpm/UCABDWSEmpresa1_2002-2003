using AutoMapper;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Entity;

namespace ServicesDeskUCABWS.BussinessLogic.Mapper
{
    public class UsuarioMapper :Profile 
    {
        public UsuarioMapper()
        {
            CreateMap<Usuario, UsuarioDTO>();
            CreateMap<UsuarioDTO, Usuario>();
            CreateMap<administrador, RegistroDTO>();
            CreateMap<RegistroDTO, administrador>();
            CreateMap<Empleado, RegistroDTO>();
            CreateMap<RegistroDTO, Empleado>();
            CreateMap<Cliente, RegistroDTO>();
            CreateMap<RegistroDTO, Cliente>();
            CreateMap<Usuario, ResetPasswordDTO>();
            CreateMap<ResetPasswordDTO, Usuario >();
        
        }
    }
}