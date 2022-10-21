using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;

namespace ServicesDeskUCABWS.BussinessLogic.Mapper
{
    public class NotificacionMapper
    {
        public Notification DtoToEntity(NotificacionDTO dTO)
        {
            UsuarioMapper map = new UsuarioMapper();
            var data = map.DtoToEntity(dTO.usuarioDestino!);
            
            return new Notification()
            {
                id = dTO.Id,
                titulo = dTO.Titulo,
                fecha = dTO.Fecha,
                descripcion = dTO.Descripcion,
                usuario = data
            };
        }

    public NotificacionDTO EntityToDto(Notification nft)
    {
        return new NotificacionDTO()
        {
            Id = nft.id,
            Titulo = nft.titulo,
            Fecha = nft.fecha,
            Descripcion = nft.descripcion
        };
    }
    }
}