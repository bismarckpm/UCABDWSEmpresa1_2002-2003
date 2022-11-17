using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ServicesDeskUCAB.DTO;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;


namespace ServicesDeskUCAB.Controllers
{
    public class EtiquetaController : Controller
    {
        public async Task<IActionResult> GestionEtiquetas()
        {
            try
            {
                List<EtiquetaDTO> etiquetas = new List<EtiquetaDTO>();
                HttpClient client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7198/api/etiquetas");
                var _client = await client.SendAsync(request);
                if (_client.IsSuccessStatusCode)
                {
                    var responseStream = await _client.Content.ReadAsStreamAsync();
                    etiquetas = await JsonSerializer.DeserializeAsync<List<EtiquetaDTO>>(responseStream);
                }
                return View(etiquetas);
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
                HttpClient client = new HttpClient();
                var _client = await client.PostAsJsonAsync<EtiquetaDTO>("https://localhost:7198/api/etiquetas", etiqueta);
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
                EtiquetaDTO Etiqueta = new EtiquetaDTO();
                HttpClient client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7198/api/etiquetas/" + id.ToString());
                var _client = await client.SendAsync(request);
                if (_client.IsSuccessStatusCode)
                {
                    var responseStream = await _client.Content.ReadAsStreamAsync();
                    Etiqueta = await JsonSerializer.DeserializeAsync<EtiquetaDTO>(responseStream);
                }
                return View(Etiqueta);
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
                HttpClient client = new HttpClient();
                var _client = await client.PutAsJsonAsync("https://localhost:7198/api/etiquetas/" + etiqueta.id.ToString(), etiqueta);
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
                HttpClient client = new HttpClient();
                var _client = await client.DeleteAsync("https://localhost:7198/api/etiquetas/" + id.ToString());
                return RedirectToAction("GestionEtiquetas");
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }



    }
}