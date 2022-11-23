using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ServicesDeskUCAB.DTO;
using System.Net.Http;


namespace ServicesDeskUCAB.Controllers
{
    public class EstadoController : Controller
    {
        public async Task<IActionResult> GestionEstados()
        {
            try
            {
                List<EstadoDTO> Estados = new List<EstadoDTO>();
                HttpClient client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7198/api/estados");
                var _client = await client.SendAsync(request);
                if (_client.IsSuccessStatusCode)
                {
                    var responseStream = await _client.Content.ReadAsStreamAsync();
                    Estados = await JsonSerializer.DeserializeAsync<List<EstadoDTO>>(responseStream);
                }
                return View(Estados);
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
                HttpClient client = new HttpClient();
                var _client = await client.PostAsJsonAsync<EstadoDTO>("https://localhost:7198/api/estados", Estado);
                // Si el estado se agrega correctamente, se redirige a la vista de gestión de estados
                if (_client.IsSuccessStatusCode)
                {
                    return RedirectToAction("GestionEstados");
                }
                else
                {
                    // Si el estado no se agrega correctamente, se redirige a la vista de agregar estado
                    //return RedirectToAction("VentanaAgregarEstado");
                    return RedirectToAction("GestionEstados");
                }

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
                EstadoDTO Estado = new EstadoDTO();
                HttpClient client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7198/api/estados/" + id.ToString());
                var _client = await client.SendAsync(request);
                if (_client.IsSuccessStatusCode)
                {
                    var responseStream = await _client.Content.ReadAsStreamAsync();
                    Estado = await JsonSerializer.DeserializeAsync<EstadoDTO>(responseStream);
                }
                return View(Estado);
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
                HttpClient client = new HttpClient();
                var _client = await client.PutAsJsonAsync("https://localhost:7198/api/estados/" + Estado.id.ToString(), Estado);
                // Si el estado se edita correctamente, se redirige a la vista de gestión de estados
                if (_client.IsSuccessStatusCode)
                {
                    return RedirectToAction("GestionEstados");
                }
                else
                {
                    // Si el estado no se edita correctamente, se redirige a la vista de editar estado
                    //return RedirectToAction("VentanaEditarEstado", new { id = Estado.id });
                    return RedirectToAction("GestionEstados");
                }
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
                HttpClient client = new HttpClient();
                var _client = await client.DeleteAsync("https://localhost:7198/api/estados/" + id.ToString());
                return RedirectToAction("GestionEstados");
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }





    }
}