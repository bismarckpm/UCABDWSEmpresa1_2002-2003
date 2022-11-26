using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System;
using ServicesDeskUCAB.DTO;
using ServicesDeskUCAB.Models;
using System.Reflection;

namespace ServicesDeskUCAB.Controllers
{
    public class PrioridadController : Controller
    {
        public async Task<IActionResult> GestionPrioridades()
        {
            try
            {
                List<PrioridadDTO> listPrioridades = new List<PrioridadDTO>();
                HttpClient client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7198/Prioridad/ConsultaPrioridades");
                var _client = await client.SendAsync(request);
                if (_client.IsSuccessStatusCode)
                {
                    var responseStream = await _client.Content.ReadAsStreamAsync();
                    listPrioridades = await JsonSerializer.DeserializeAsync<List<PrioridadDTO>>(responseStream);
                }
                return View(listPrioridades);
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
                var _client = await client.PostAsJsonAsync<PrioridadDTO>("https://localhost:7198/Prioridad/CreatePrioridad", prioridad);
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
                PrioridadDTO prioridad = new PrioridadDTO();
                HttpClient client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7198/Prioridad/ConsultaPrioridad/"+ id.ToString());
                var _client = await client.SendAsync(request);
                if (_client.IsSuccessStatusCode)
                {
                    var responseStream = await _client.Content.ReadAsStreamAsync();
                    prioridad = await JsonSerializer.DeserializeAsync<PrioridadDTO>(responseStream);
                }
                return View(prioridad);
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
