using Microsoft.AspNetCore.Mvc;
using System.Text;
using Newtonsoft.Json;
using ServicesDeskUCAB.DTO;
using ServicesDeskUCAB.ResponseHandler;

namespace ServicesDeskUCAB.Controllers
{
    public class TipoCargoController : Controller
    {
        public async Task<IActionResult> GestionTipoCargo()
        {
            try
            {
                AplicationResponseHandler<List<TipoCargoDTO>> ApiResponseH = new AplicationResponseHandler<List<TipoCargoDTO>>();
                
                HttpClient clientTCargo = new HttpClient();
                    
                   var response = await clientTCargo.GetAsync("https://localhost:7198/TipoCargo/ConsultaTCargo/");

                    if(response.IsSuccessStatusCode)
                    {
                        var responseStream = await response.Content.ReadAsStringAsync();
                        ApiResponseH = JsonConvert.DeserializeObject<AplicationResponseHandler<List<TipoCargoDTO>>>(responseStream);
                    } else 
                    {
                        BadRequest();
                    }


                return View(ApiResponseH!.Data);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message +" || "+ ex.StackTrace);
                throw new Exception(ex.Message, ex.InnerException);
            }
            
        }
        public IActionResult VentanaAgregarTipoCargo()
        {
            try
            {
                return View();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message + " || "+ex.StackTrace);
                throw new Exception(ex.Message + " || "+ex.StackTrace, ex.InnerException);
            }
        }

        public async Task<IActionResult> AgregarTCargo(TipoCargoDTO tipoCargo)
        {
            try
            {
                tipoCargo.id = 0;
                HttpClient client = new HttpClient();
                var _client = await client.PostAsJsonAsync<TipoCargoDTO>("https://localhost:7198/TipoCargo/CreateTCargo/", tipoCargo);
                    return RedirectToAction("GestionTipoCargo");
                    
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message + " || "+ex.StackTrace);
                throw new Exception(ex.Message + " || "+ex.StackTrace, ex.InnerException);
            }            
        }

        public async Task<IActionResult> VentanaEditarTipoCargo(int id)
        {
            try
            {
                AplicationResponseHandler<TipoCargoDTO> ResponseTipoCargo = new AplicationResponseHandler<TipoCargoDTO>();
                var dto = new TipoCargoDTO();
                HttpClient client = new HttpClient();

                var response = await client.GetAsync("https://localhost:7198/TipoCargo/ConsultaTCargo/" + id.ToString());    

                if(response.IsSuccessStatusCode)
                {
                    var responseStream = await response.Content.ReadAsStringAsync();
                    ResponseTipoCargo = JsonConvert.DeserializeObject<AplicationResponseHandler<TipoCargoDTO>>(responseStream);
                    dto = ResponseTipoCargo!.Data;
                }
                return View(dto);
                
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message + " || "+ex.StackTrace);
                throw new (ex.Message, ex);
            }
        }
        public async Task<IActionResult> ActualizarTCargo(TipoCargoDTO tipoCargo)
        {
            try
            {
              HttpClient client = new HttpClient();
              var _client = await client.PutAsJsonAsync("https://localhost:7198/TipoCargo/ActualizarTCargo/",tipoCargo);      

              return RedirectToAction("GestionTipoCargo");      
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message + " || "+ex.StackTrace);
                throw new (ex.Message + " || "+ex.StackTrace, ex.InnerException);
            }
        }

        public async Task<IActionResult> VentanaEliminarTipoCargo(int id)
        {
            try
            {
                return View(id);
                
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message + " || "+ex.StackTrace);
                throw new Exception(ex.Message, ex);
            }
        }
        public async Task<IActionResult> EliminarTCargo(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                var _client = await client.DeleteAsync("https://localhost:7198/TipoCargo/EliminarTCargo/"+id.ToString());
                return RedirectToAction("GestionTipoCargo");
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message + " || "+ex.StackTrace);
                throw new Exception(ex.Message + " || "+ex.StackTrace, ex.InnerException);
            }
        }
    }
}