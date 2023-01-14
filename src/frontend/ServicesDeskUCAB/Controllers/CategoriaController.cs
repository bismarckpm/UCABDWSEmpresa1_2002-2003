using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using ServicesDeskUCAB.DTO;
using ServicesDeskUCAB.Models;
using System.Reflection;
using ServicesDeskUCAB.ResponseHandler;
using Newtonsoft.Json;


namespace ServicesDeskUCAB.Controllers
{
    public class CategoriaController : Controller
    {
        public async Task<IActionResult> GestionCategorias()
        {
            try
            {
                AplicationResponseHandler<List<CategoriaDTO>> apiResponse = new AplicationResponseHandler<List<CategoriaDTO>>();
                HttpClient client = new HttpClient();
                var response = await client.GetAsync("https://localhost:7198/Categoria/ConsultaCategorias");
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    apiResponse = JsonConvert.DeserializeObject<AplicationResponseHandler<List<CategoriaDTO>>>(responseString);
                }
                return View(apiResponse.Data);
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
                HttpClient client = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(categoria), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://localhost:7198/Categoria/CreateCategoria", content);
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
                var _client = await client.DeleteAsync("https://localhost:7198/Categoria/Eliminar/" + id.ToString());
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
                AplicationResponseHandler<CategoriaDTO> apiResponse = new AplicationResponseHandler<CategoriaDTO>();
                HttpClient client = new HttpClient();
                var response = await client.GetAsync("https://localhost:7198/Categoria/ConsultaCategoria/" + id.ToString());
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    apiResponse = JsonConvert.DeserializeObject <AplicationResponseHandler<CategoriaDTO>>(responseString);
                }
                return View(apiResponse);
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
                var _client = await client.PutAsJsonAsync("https://localhost:7198/Categoria/Actualizar", categoria);
                return RedirectToAction("GestionCategorias");
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }
    }
}
