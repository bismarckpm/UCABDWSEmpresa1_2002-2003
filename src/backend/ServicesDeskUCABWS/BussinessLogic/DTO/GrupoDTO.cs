namespace ServicesDeskUCABWS.BussinessLogic.DTO
{
    public class GrupoDTO
    {
        public int id { get; set; } 

        public string? nombre { get; set; }

        public ICollection<UsuarioDTO>? usuarios { get; set; }
    }
}
