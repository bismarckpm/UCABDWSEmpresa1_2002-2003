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
    public class TipoCargoController : Controller
    {
        public async Task<IActionResult> GestionTipoCargo()
        {
            try
            {
                List<TipoCargoDTO> listTCargo = new List<TipoCargoDTO>();
                
                HttpClient clientTCargo = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7198/TipoCargo/ConsultaTCargo/");
                    var _client = await clientTCargo.SendAsync(request);

                    if(_client.IsSuccessStatusCode)
                    {
                        var responseStream = await _client.Content.ReadAsStreamAsync();
                        listTCargo = await JsonSerializer.DeserializeAsync<List<TipoCargoDTO>>(responseStream);

                    } else 
                    {
                        BadRequest();
                    }


                return View(listTCargo);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message +" || "+ ex.StackTrace);
                throw new Exception(ex.Message, ex.InnerException);
            }
            
        }
        public IActionResult VentanaAgregarTipoCargo()
        {
            try
            {
                return View();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message + " || "+ex.StackTrace, ex.InnerException);
            }
        }

        public async Task<IActionResult> AgregarTCargo(TipoCargoDTO tipoCargo)
        {
            try
            {
                tipoCargo.id = 0;
                HttpClient client = new HttpClient();
                var _client = await client.PostAsJsonAsync<TipoCargoDTO>("https://localhost:7198/TipoCargo/CreateTCargo/", tipoCargo);
                    return RedirectToAction("GestionTipoCargo");
                    
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message + " || "+ex.StackTrace);
                throw new Exception(ex.Message + " || "+ex.StackTrace, ex.InnerException);
            }            
        }

        public async Task<IActionResult> VentanaEditarTipoCargo(int id)
        {
            try
            {
               TipoCargoDTO dto = new TipoCargoDTO();
                HttpClient client = new HttpClient();
                
                var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7198/TipoCargo/ConsultaTCargo/"+id.ToString());
                var _client = await client.SendAsync(request);

                if(_client.IsSuccessStatusCode)
                {
                    var responseStream = await _client.Content.ReadAsStreamAsync();
                    dto = await JsonSerializer.DeserializeAsync<TipoCargoDTO>(responseStream);
                }
                return View(dto);
                
            }catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public async Task<IActionResult> ActualizarTCargo(TipoCargoDTO tipoCargo)
        {
            try
            {
              HttpClient client = new HttpClient();
              var _client = await client.PutAsJsonAsync<TipoCargoDTO>("https://localhost:7198/TipoCargo/ActualizarTCargo/",tipoCargo);      

              return RedirectToAction("GestionTipoCargo");      
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message + " || "+ex.StackTrace);
                throw new Exception(ex.Message + " || "+ex.StackTrace, ex.InnerException);
            }
        }

        public async Task<IActionResult> VentanaEliminarTipoCargo(int id)
        {
            try
            {
                return View(id);
                
            }catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public async Task<IActionResult> EliminarTCargo(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                var _client = await client.DeleteAsync("https://localhost:7198/TipoCargo/EliminarTCargo/"+id.ToString());
                return RedirectToAction("GestionTipoCargo");
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message + " || "+ex.StackTrace);
                throw new Exception(ex.Message + " || "+ex.StackTrace, ex.InnerException);
            }
        }
    }
}