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

        public IActionResult ventanaAgregarPrioridad()
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
                //cambiar, EL GUID SE GENERARA AUTOMATICO
                prioridad.id = 0;
                HttpClient client = new HttpClient();
                var _client = await client.PostAsJsonAsync<PrioridadDTO>("https://localhost:7198/Prioridad/CreatePrioridad", prioridad);
                return RedirectToAction("GestionPrioridades");
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }
    }
}
