using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ServicesDeskUCAB.DTO;
using System.Net.Http;
using ServicesDeskUCAB.Factory;
using ServicesDeskUCAB.ResponseHandler;
using Newtonsoft.Json;

namespace ServicesDeskUCAB.Controllers
{
    public class EtiquetaController : Controller
    {
        public const string URL = "https://localhost:7198/api/etiquetas";
        public string responseString = string.Empty;

        
        public async Task<IActionResult> GestionEtiquetas()
        {
            try
            {
                AplicationResponseHandler<List<EtiquetaDTO>> apiResponse = new AplicationResponseHandler<List<EtiquetaDTO>>();
                HttpClient client = FactoryHttp.CreateClient();
                HttpResponseMessage response = await client.GetAsync(URL);
                responseString = await response.Content.ReadAsStringAsync();
                apiResponse = JsonConvert.DeserializeObject<AplicationResponseHandler<List<EtiquetaDTO>>>(responseString);
                if (apiResponse.Success) return View(apiResponse.Data);
                //redireccionar a una pagina de error
                return RedirectToAction("Error", "Error");
                
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }

        public IActionResult VentanaAgregarEtiqueta()
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

        public async Task<IActionResult> AgregarEtiqueta(EtiquetaDTO etiqueta)
        {
            try
            {
                etiqueta.id = 0;
                HttpClient client =  FactoryHttp.CreateClient();
                var _client = await client.PostAsJsonAsync<EtiquetaDTO>(URL, etiqueta);
                return RedirectToAction("GestionEtiquetas");
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }

        public async Task<IActionResult> VentanaEditarEtiqueta(int id)
        {
            try
            {
                AplicationResponseHandler<EtiquetaDTO> apiResponse = new AplicationResponseHandler<EtiquetaDTO>();
                HttpClient client =  FactoryHttp.CreateClient();
                var response = await client.GetAsync( URL +"/"+id.ToString());
                if (response.IsSuccessStatusCode)
                {
                    responseString = await response.Content.ReadAsStringAsync();
                    apiResponse =  JsonConvert.DeserializeObject<AplicationResponseHandler<EtiquetaDTO>>(responseString);
                }
                return View(apiResponse.Data);
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }

        public async Task<IActionResult> EditarEtiqueta(EtiquetaDTO etiqueta)
        {
            try
            {
                HttpClient client =  FactoryHttp.CreateClient();
                var _client = await client.PutAsJsonAsync(URL+"/" + etiqueta.id.ToString(), etiqueta);
                return RedirectToAction("GestionEtiquetas");
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }

        public IActionResult VentanaEliminarEtiqueta(int id)
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

        public async Task<IActionResult> EliminarEtiqueta(int id)
        {
            try
            {
                HttpClient client = FactoryHttp.CreateClient();
                var _client = await client.DeleteAsync(URL+"/" + id.ToString());
                return RedirectToAction("GestionEtiquetas");
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }



    }
}