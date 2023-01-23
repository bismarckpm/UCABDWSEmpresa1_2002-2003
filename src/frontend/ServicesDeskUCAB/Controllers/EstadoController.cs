using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ServicesDeskUCAB.DTO;
using System.Net.Http;
using ServicesDeskUCAB.ResponseHandler;
using ServicesDeskUCAB.Factory;
using Newtonsoft.Json;

namespace ServicesDeskUCAB.Controllers
{
    public class EstadoController : Controller
    {
        public const string URL = "https://localhost:7198/api/estados";
        public string responseString = string.Empty;
        public async Task<IActionResult> GestionEstados()
        {
            try
            {
                AplicationResponseHandler<List<EstadoDTO>> apiResponse = new AplicationResponseHandler<List<EstadoDTO>>();
                HttpClient client = FactoryHttp.CreateClient();
                HttpResponseMessage response = await client.GetAsync(URL);
                responseString = await response.Content.ReadAsStringAsync();
                apiResponse = JsonConvert.DeserializeObject<AplicationResponseHandler<List<EstadoDTO>>>(responseString);
                if (apiResponse.Success) return View(apiResponse.Data);
                //redireccionar a una pagina de error
                return RedirectToAction("Error", "Error");
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }

        public IActionResult VentanaAgregarEstado()
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

        public async Task<IActionResult> AgregarEstado(EstadoDTO Estado)
        {
            try
            {
                Estado.id = 0;
                HttpClient client =  FactoryHttp.CreateClient();
                var _client = await client.PostAsJsonAsync<EstadoDTO>(URL, Estado);
                // Si el estado se agrega correctamente, se redirige a la vista de gestión de estados
                return RedirectToAction("GestionEstados");

            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }

        public async Task<IActionResult> VentanaEditarEstado(int id)
        {
            try
            {
                AplicationResponseHandler<EstadoDTO> apiResponse = new AplicationResponseHandler<EstadoDTO>();
                HttpClient client =  FactoryHttp.CreateClient();
                var response = await client.GetAsync( URL +"/"+id.ToString());
                if (response.IsSuccessStatusCode)
                {
                    responseString = await response.Content.ReadAsStringAsync();
                    apiResponse =  JsonConvert.DeserializeObject<AplicationResponseHandler<EstadoDTO>>(responseString);
                }
                return View(apiResponse.Data);
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }

        public async Task<IActionResult> EditarEstado(EstadoDTO Estado)
        {
            try
            {
                HttpClient client =  FactoryHttp.CreateClient();
                var _client = await client.PutAsJsonAsync(URL+"/" + Estado.id.ToString(), Estado);
                return RedirectToAction("GestionEstados");
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }

        public IActionResult VentanaEliminarEstado(int id)
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

        public async Task<IActionResult> EliminarEstado(int id)
        {
            try
            {
                HttpClient client = FactoryHttp.CreateClient();
                var _client = await client.DeleteAsync(URL+"/" + id.ToString());
                return RedirectToAction("GestionEstados");
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }





    }
}