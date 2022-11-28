using Microsoft.AspNetCore.Mvc;
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
    public class GrupoController : Controller
    {
        public async Task<IActionResult> GestionGrupo()
        {
            try
            {
                List<GrupoDTO> listGrupo = new List<GrupoDTO>();
                HttpClient client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7198/Grupo/ConsultaGrupo");
                var _client = await client.SendAsync(request);
                if (_client.IsSuccessStatusCode)
                {
                    var responseStream = await _client.Content.ReadAsStreamAsync();
                    listGrupo = await JsonSerializer.DeserializeAsync<List<GrupoDTO>>(responseStream);
                }
                return View(listGrupo);
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }

        public IActionResult VentanaAgregarGrupos()
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
                HttpClient client = new HttpClient();
                var _client = await client.PostAsJsonAsync<GrupoDTO>("https://localhost:7198/Grupo/CrearGrupos", grupo);
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
                var _client = await client.DeleteAsync("https://localhost:7198/Grupo/EliminarGrupo/" + id.ToString());
                return RedirectToAction("GestionGrupo");
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
                GrupoDTO grupo = new GrupoDTO();
                HttpClient client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7198/Grupo/ConsultarGrupo/" + id.ToString());
                var _client = await client.SendAsync(request);
                if (_client.IsSuccessStatusCode)
                {
                    var responseStream = await _client.Content.ReadAsStreamAsync();
                    grupo = await JsonSerializer.DeserializeAsync<GrupoDTO>(responseStream);
                }
                return View(grupo);
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
                var _client = await client.PutAsJsonAsync("https://localhost:7198/Grupo/ActualizarGrupo", grupo);
                return RedirectToAction("GestionGrupo");
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }


    }
}