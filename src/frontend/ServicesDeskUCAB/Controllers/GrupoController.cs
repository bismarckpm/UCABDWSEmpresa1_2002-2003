using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCAB.DTO;
using System.Text.Json;


namespace ServicesDeskUCAB.Controllers
{
    public class GrupoController : Controller
    {
        public async Task<IActionResult> GestionGrupos()
        {
            try
            {
                List<GrupoDTO> listaGrupos = new List<GrupoDTO>();
                HttpClient client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7198/Grupo/ConsultaGrupo");
                var _client = await client.SendAsync(request);

                if (_client.IsSuccessStatusCode)
                {
                    var responseStream = await _client.Content.ReadAsStreamAsync();
                    listaGrupos = await JsonSerializer.DeserializeAsync<List<GrupoDTO>>(responseStream);
                }

                return View(listaGrupos);

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

        public async Task<IActionResult> AgregarGrupo(GrupoDTO grupo)
        {
            try
            {
                grupo.id = 0;
                HttpClient client = new HttpClient();
                var _client = await client.PostAsJsonAsync<GrupoDTO>("https://localhost:7198/Grupo/CreateGrupo/", grupo);
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
                HttpClient client = new HttpClient();
                var _client = await client.DeleteAsync("https://localhost:7198/Grupo/Eliminar/" + id.ToString());
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
                GrupoDTO Grupo = new GrupoDTO();
                HttpClient client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7198/Grupo/ActualizarGrupo" + id.ToString());
                var _client = await client.SendAsync(request);
                if (_client.IsSuccessStatusCode)
                {
                    var responseStream = await _client.Content.ReadAsStreamAsync();
                    Grupo = await JsonSerializer.DeserializeAsync<GrupoDTO>(responseStream);
                }
                return View(Grupo);


            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }

        public async Task<IActionResult> EditarGrupo(GrupoDTO grupo)
        {
            try
            {
                HttpClient client = new HttpClient();
                //var _client = await client.PutAsJsonAsync("https://localhost:7198/Grupo/Actualizar", grupo);
                var _client = await client.PutAsJsonAsync("https://localhost:7198/Grupo/ActualizarGrupo?id=" + grupo.id.ToString(), grupo);
                return RedirectToAction("GestionGrupos");
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }
    }
}
