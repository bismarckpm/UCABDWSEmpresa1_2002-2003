namespace ServicesDeskUCABWS.BussinessLogic.DTO
{
    public class NotificacionDTO
    {
        public int Id { get; set;}

        public string? Titulo { get; set;}

        public string? Fecha { get; set; }

        public string? Descripcion { get; set;}

        public UsuarioDTO? usuarioDestino {get; set;}
    }
}