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
    public class DepartamentoController : Controller
    {
        public async Task<IActionResult> GestionDepartamentos()
        {
            try
            {
                List<DepartamentoDTO> listDepartamentos = new List<DepartamentoDTO>();
                HttpClient client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7198/Departamento/ConsultaDepartamentos");
                var _client = await client.SendAsync(request);
                if (_client.IsSuccessStatusCode)
                {
                    var responseStream = await _client.Content.ReadAsStreamAsync();
                    listDepartamentos = await JsonSerializer.DeserializeAsync<List<DepartamentoDTO>>(responseStream);
                }
                return View(listDepartamentos);
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }

        public IActionResult VentanaAgregarDepartamento()
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

        public async Task<IActionResult> AgregarDepartamento(DepartamentoDTO departamento)
        {
            try
            {
                departamento.id = 0;
                HttpClient client = new HttpClient();
                var _client = await client.PostAsJsonAsync<DepartamentoDTO>("https://localhost:7198/Departamento/CreateDepartamento", departamento);
                return RedirectToAction("GestionDepartamentos");
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }

        public IActionResult VentanaEliminarDepartamento(int id)
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

        public async Task<IActionResult> EliminarDepartamento(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                var _client = await client.DeleteAsync("https://localhost:7198/Departamento/Eliminar/" + id.ToString());
                return RedirectToAction("GestionDepartamentos");
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }

        public async Task<IActionResult> VentanaEditarDepartamento(int id)
        {
            try
            {
                DepartamentoDTO departamento = new DepartamentoDTO();
                HttpClient client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7198/Departamento/ConsultaDepartamento/" + id.ToString());
                var _client = await client.SendAsync(request);
                if (_client.IsSuccessStatusCode)
                {
                    var responseStream = await _client.Content.ReadAsStreamAsync();
                    departamento = await JsonSerializer.DeserializeAsync<DepartamentoDTO>(responseStream);
                }
                return View(departamento);
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }

        public async Task<IActionResult> EditarDepartamento(DepartamentoDTO departamento)
        {
            try
            {
                HttpClient client = new HttpClient();
                var _client = await client.PutAsJsonAsync("https://localhost:7198/Departamento/Actualizar", departamento);
                return RedirectToAction("GestionDepartamentos");
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }
    }
}
