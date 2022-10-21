namespace ServicesDeskUCABWS.BussinessLogic.DTO
{
    public class CargoDTO
    {
        public int Id {get; set;}
        public string? Nombre {get; set;}

        public TipoCargoDTO? tipoCargo {get; set;}

        public UsuarioDTO? usuario {get; set;}
    }
}