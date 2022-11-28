using ServicesDeskUCAB.DTO;
using Newtonsoft.Json;

namespace ServicesDeskUCAB.Services.Login
{
    public class LoginService : ILoginService
    {
        private HttpClient _httpclient;
         public async Task<List<UsuarioDTO>> GetUsuarios(){
          
            List<UsuarioDTO> result = null;
            _httpclient =  new HttpClient();
            string url = "https://localhost:7198/Usuarios";
            using(HttpRequestMessage reuest = new HttpRequestMessage(HttpMethod.Get,url)){
                using(HttpResponseMessage response = await _httpclient.SendAsync(reuest) ){
                    using(HttpContent content = response.Content){
                        if(response.IsSuccessStatusCode){
                            string responseStringContent = await content.ReadAsStringAsync();
                            result= JsonConvert.DeserializeObject<List<UsuarioDTO>>(responseStringContent);
                            
                        }
                    }
                }
            }
            return result;
         }
    }
}