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
    public class CategoriaController : Controller
    {
        public async Task<IActionResult> GestionCategorias()
        {
            try
            {
                List<CategoriaDTO> listCategorias = new List<CategoriaDTO>();
                HttpClient client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7267/Categoria/ConsultaCategorias");
                var _client = await client.SendAsync(request);
                if (_client.IsSuccessStatusCode)
                {
                    var responseStream = await _client.Content.ReadAsStreamAsync();
                    listCategorias = await JsonSerializer.DeserializeAsync<List<CategoriaDTO>>(responseStream);
                }
                return View(listCategorias);
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }

        public IActionResult VentanaAgregarCategoria()
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

        public async Task<IActionResult> AgregarCategoria(CategoriaDTO categoria)
        {
            try
            {
                categoria.id = 0;
                HttpClient client = new HttpClient();
                var _client = await client.PostAsJsonAsync<CategoriaDTO>("https://localhost:7267/Categoria/CreateCategoria", categoria);
                return RedirectToAction("GestionCategorias");
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }

        public IActionResult VentanaEliminarCategoria(int id)
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

        public async Task<IActionResult> EliminarCategoria(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                var _client = await client.DeleteAsync("https://localhost:7267/Categoria/Eliminar/" + id.ToString());
                return RedirectToAction("GestionCategorias");
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }

        public async Task<IActionResult> VentanaEditarCategoria(int id)
        {
            try
            {
                CategoriaDTO categoria = new CategoriaDTO();
                HttpClient client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7267/Categoria/ConsultaCategoria/" + id.ToString());
                var _client = await client.SendAsync(request);
                if (_client.IsSuccessStatusCode)
                {
                    var responseStream = await _client.Content.ReadAsStreamAsync();
                    categoria = await JsonSerializer.DeserializeAsync<CategoriaDTO>(responseStream);
                }
                return View(categoria);
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }

        public async Task<IActionResult> EditarCategoria(CategoriaDTO categoria)
        {
            try
            {
                HttpClient client = new HttpClient();
                var _client = await client.PutAsJsonAsync("https://localhost:7267/Categoria/Actualizar", categoria);
                return RedirectToAction("GestionCategorias");
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }
    }
}
