using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ServicesDeskUCAB.DTO;
using System.Net.Http;
using ServicesDeskUCAB.ResponseHandler;
using ServicesDeskUCAB.Factory;
using Newtonsoft.Json;

namespace ServicesDeskUCAB.Controllers
{
    public class GrupoController : Controller
    {
        public const string URL = "https://localhost:7198/api/grupos";
        public string responseString = string.Empty;
        public async Task<IActionResult> GestionGrupos()
        {
            try
            {
                AplicationResponseHandler<List<GrupoDTO>> apiResponse = new AplicationResponseHandler<List<GrupoDTO>>();
                HttpClient client = FactoryHttp.CreateClient();
                HttpResponseMessage response = await client.GetAsync(URL);
                responseString = await response.Content.ReadAsStringAsync();
                apiResponse = JsonConvert.DeserializeObject<AplicationResponseHandler<List<GrupoDTO>>>(responseString);
                if (apiResponse.Success) return View(apiResponse.Data);
                
                return RedirectToAction("Error", "Error");
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }

        public IActionResult VentanaAgregarGrupo()
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

        public async Task<IActionResult> AgregarGrupo(GrupoDTO Grupo)
        {
            try
            {
                Grupo.id = 0;
                HttpClient client = FactoryHttp.CreateClient();
                var _client = await client.PostAsJsonAsync<GrupoDTO>(URL, Grupo);
                
                return RedirectToAction("GestionGrupos");

            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }

        public async Task<IActionResult> VentanaEditarGrupo(int id)
        {
            try
            {
                AplicationResponseHandler<GrupoDTO> apiResponse = new AplicationResponseHandler<GrupoDTO>();
                HttpClient client = FactoryHttp.CreateClient();
                var response = await client.GetAsync(URL + "/" + id.ToString());
                if (response.IsSuccessStatusCode)
                {
                    responseString = await response.Content.ReadAsStringAsync();
                    apiResponse = JsonConvert.DeserializeObject<AplicationResponseHandler<GrupoDTO>>(responseString);
                }
                return View(apiResponse.Data);
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }

        public async Task<IActionResult> EditarGrupo(GrupoDTO Grupo)
        {
            try
            {
                HttpClient client = FactoryHttp.CreateClient();
                var _client = await client.PutAsJsonAsync(URL + "/" + Grupo.id.ToString(), Grupo);
                return RedirectToAction("GestionGrupos");
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }

        public IActionResult VentanaEliminarGrupo(int id)
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

        public async Task<IActionResult> EliminarGrupo(int id)
        {
            try
            {
                HttpClient client = FactoryHttp.CreateClient();
                var _client = await client.DeleteAsync(URL + "/" + id.ToString());
                return RedirectToAction("GestionGrupos");
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }





    }
}

