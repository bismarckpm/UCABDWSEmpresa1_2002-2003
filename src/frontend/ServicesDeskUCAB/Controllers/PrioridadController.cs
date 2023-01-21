using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCAB.DTO;
using ServicesDeskUCAB.ResponseHandler;
using Newtonsoft.Json;
using System.Text;

namespace ServicesDeskUCAB.Controllers
{
    public class PrioridadController : Controller
    {
        public async Task<IActionResult> GestionPrioridades()
        {
            try
            {
                AplicationResponseHandler<List<PrioridadDTO>> apiResponse = new AplicationResponseHandler<List<PrioridadDTO>>();
                HttpClient client = new HttpClient();
                var response = await client.GetAsync("https://localhost:7198/Prioridad/ConsultaPrioridades");
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    apiResponse = JsonConvert.DeserializeObject<AplicationResponseHandler<List<PrioridadDTO>>>(responseString);
                }
                return View(apiResponse.Data);
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }
        
        public IActionResult VentanaAgregarPrioridad()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }
        
        public async Task<IActionResult> AgregarPrioridad(PrioridadDTO prioridad)
        {
            try
            {
                HttpClient client = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(prioridad), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://localhost:7198/Prioridad/CreatePrioridad", content);
                return RedirectToAction("GestionPrioridades");
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }
        
        public IActionResult VentanaEliminarPrioridad(int id)
        {
            try
            {
                return View(id);
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }
        
        public async Task<IActionResult> EliminarPrioridad(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                var _client = await client.DeleteAsync("https://localhost:7198/Prioridad/Eliminar/" + id.ToString());
                return RedirectToAction("GestionPrioridades");
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }
        
        public async Task<IActionResult> VentanaEditarPrioridad(int id)
        {
            try
            {
                AplicationResponseHandler<PrioridadDTO> apiResponse = new AplicationResponseHandler<PrioridadDTO>();
                HttpClient client = new HttpClient();
                var response = await client.GetAsync("https://localhost:7198/Prioridad/ConsultaPrioridad/" + id.ToString());
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    apiResponse = JsonConvert.DeserializeObject<AplicationResponseHandler<PrioridadDTO>>(responseString);
                }
                return View(apiResponse.Data);
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }
        
        public async Task<IActionResult> EditarPrioridad(PrioridadDTO prioridad)
        {
            try
            {
                HttpClient client = new HttpClient();
                var _client = await client.PutAsJsonAsync("https://localhost:7198/Prioridad/Actualizar",prioridad);
                return RedirectToAction("GestionPrioridades");
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }
    }
}
