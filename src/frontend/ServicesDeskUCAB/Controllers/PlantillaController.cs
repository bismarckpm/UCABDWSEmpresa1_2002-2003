using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ServicesDeskUCAB.DTO;
using System.Net.Http;
using ServicesDeskUCAB.ResponseHandler;
using ServicesDeskUCAB.Factory;
using Newtonsoft.Json;

namespace ServicesDeskUCAB.Controllers
{
    public class PlantillaController : Controller
    {
        public const string URL = "https://localhost:7198/api/plantillas";
        public string responseString = string.Empty;
        public async Task<IActionResult> GestionPlantillas()
        {
            try
            {
                AplicationResponseHandler<List<PlantillaDTO>> apiResponse = new AplicationResponseHandler<List<PlantillaDTO>>();
                HttpClient client = FactoryHttp.CreateClient();
                HttpResponseMessage response = await client.GetAsync(URL);
                responseString = await response.Content.ReadAsStringAsync();
                apiResponse = JsonConvert.DeserializeObject<AplicationResponseHandler<List<PlantillaDTO>>>(responseString);
                if (apiResponse.Success) return View(apiResponse.Data);
                //redireccionar a una pagina de error
                return RedirectToAction("Error", "Error");
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }

        public IActionResult VentanaAgregarPlantilla()
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

        public async Task<IActionResult> AgregarPlantilla(PlantillaDTO Plantilla)
        {
            try
            {
                Plantilla.id = 0;
                HttpClient client =  FactoryHttp.CreateClient();
                var _client = await client.PostAsJsonAsync<PlantillaDTO>(URL, Plantilla);
                // Si el estado se agrega correctamente, se redirige a la vista de gestión de estados
                return RedirectToAction("GestionPlantillas");
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }

        public async Task<IActionResult> VentanaEditarPlantilla(int id)
        {
            try
            {
                AplicationResponseHandler<PlantillaDTO> apiResponse = new AplicationResponseHandler<PlantillaDTO>();
                HttpClient client =  FactoryHttp.CreateClient();
                var response = await client.GetAsync( URL +"/"+id.ToString());
                if (response.IsSuccessStatusCode)
                {
                    responseString = await response.Content.ReadAsStringAsync();
                    apiResponse =  JsonConvert.DeserializeObject<AplicationResponseHandler<PlantillaDTO>>(responseString);
                }
                return View(apiResponse.Data);
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }

        // PARA EDUTAR LAS PLANTILLAS
        public async Task<IActionResult> EditarPlantilla(PlantillaDTO Plantilla)
        {
            try
            {
                HttpClient client =  FactoryHttp.CreateClient();
                var _client = await client.PutAsJsonAsync(URL+"/" + Plantilla.id.ToString(), Plantilla);
                return RedirectToAction("GestionPlantillas");
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }

        public IActionResult VentanaEliminarPlantilla(int id)
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

        // PARA ELIMINAR PLANTILLAS
        public async Task<IActionResult> EliminarPlantilla(int id)
        {
            try
            {
               HttpClient client = FactoryHttp.CreateClient();
                var _client = await client.DeleteAsync(URL+"/" + id.ToString());
                return RedirectToAction("GestionPlantillas");
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }



    }
}