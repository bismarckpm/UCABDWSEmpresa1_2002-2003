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
    public class CargoController : Controller
    {
        public async Task<IActionResult> GestionCargos()
        {
            try
            {
                List<CargoDTO> listaCargos = new List<CargoDTO>();
                HttpClient client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7198/Cargo/ConsultaCargo");
                var _client = await client.SendAsync(request);

                if (_client.IsSuccessStatusCode)
                {
                    var responseStream = await _client.Content.ReadAsStreamAsync();
                    listaCargos = await JsonSerializer.DeserializeAsync<List<CargoDTO>>(responseStream);
                }

                return View(listaCargos);

            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }

        public IActionResult VentanaAgregarCargo()
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

        public async Task<IActionResult> AgregarCargo(CargoDTO cargo)
        {
            try
            {
                cargo.id = 0;
                HttpClient client = new HttpClient();
                var _client = await client.PostAsJsonAsync<CargoDTO>("https://localhost:7198/Cargo/CreateCargo/", cargo);
                return RedirectToAction("GestionCargos");
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }

        public async Task<IActionResult> VentanaEditarCargo(int id)
        {
            try
            {
                CargoDTO Cargo = new CargoDTO();
                HttpClient client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7198/Cargo/ActualizarCargo" + id.ToString());
                var _client = await client.SendAsync(request);
                if (_client.IsSuccessStatusCode)
                {
                    var responseStream = await _client.Content.ReadAsStreamAsync();
                    Cargo = await JsonSerializer.DeserializeAsync<CargoDTO>(responseStream);
                }
                return View(Cargo);
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }

        public async Task<IActionResult> EditarCargo(CargoDTO cargo)
        {
            try
            {
                HttpClient client = new HttpClient();
                var _client = await client.PutAsJsonAsync("https://localhost:7198/Cargo/ActualizarCargo?id=" + cargo.id.ToString(), cargo);
                return RedirectToAction("GestionCargos");
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }

        public IActionResult VentanaEliminarCargo(int id)
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

        public async Task<IActionResult> EliminarCargo(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                var _client = await client.DeleteAsync("https://localhost:7198/Cargo/" + id.ToString());
                return RedirectToAction("GestionCargos");
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }
    }
}