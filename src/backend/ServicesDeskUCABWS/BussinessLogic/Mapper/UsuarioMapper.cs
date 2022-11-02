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
            CreateMap<administrador, AdministratorDTO>();
            CreateMap<AdministratorDTO, administrador>();
        }
    }
}