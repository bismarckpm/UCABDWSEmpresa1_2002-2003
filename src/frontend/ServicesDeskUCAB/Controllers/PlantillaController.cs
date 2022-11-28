using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ServicesDeskUCAB.DTO;
using System.Net.Http;


namespace ServicesDeskUCAB.Controllers
{
    public class PlantillaController : Controller
    {
        public async Task<IActionResult> GestionPlantillas()
        {
            try
            {
                List<PlantillaDTO> Plantillas = new List<PlantillaDTO>();
                HttpClient client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7198/api/plantillas");
                var _client = await client.SendAsync(request);
                if (_client.IsSuccessStatusCode)
                {
                    var responseStream = await _client.Content.ReadAsStreamAsync();
                    Plantillas = await JsonSerializer.DeserializeAsync<List<PlantillaDTO>>(responseStream);
                }
                return View(Plantillas);
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
                HttpClient client = new HttpClient();
                var _client = await client.PostAsJsonAsync<PlantillaDTO>("https://localhost:7198/api/plantillas", Plantilla);
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
                PlantillaDTO Plantilla = new PlantillaDTO();
                HttpClient client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7198/api/plantillas/" + id.ToString());
                var _client = await client.SendAsync(request);
                if (_client.IsSuccessStatusCode)
                {
                    var responseStream = await _client.Content.ReadAsStreamAsync();
                    Plantilla = await JsonSerializer.DeserializeAsync<PlantillaDTO>(responseStream);
                }
                return View(Plantilla);
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }

        public async Task<IActionResult> EditarPlantilla(PlantillaDTO Plantilla)
        {
            try
            {
                HttpClient client = new HttpClient();
                var _client = await client.PutAsJsonAsync("https://localhost:7198/api/plantillas/" + Plantilla.id.ToString(), Plantilla);
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

        public async Task<IActionResult> EliminarPlantilla(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                var _client = await client.DeleteAsync("https://localhost:7198/api/plantillas/" + id.ToString());
                return RedirectToAction("GestionPlantillas");
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }



    }
}