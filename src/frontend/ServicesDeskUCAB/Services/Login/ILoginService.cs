namespace ServicesDeskUCAB.Services.Login
{
    public interface ILoginService
    {
        public Task<List<DTO.UsuarioDTO>> GetUsuarios();
    }
}