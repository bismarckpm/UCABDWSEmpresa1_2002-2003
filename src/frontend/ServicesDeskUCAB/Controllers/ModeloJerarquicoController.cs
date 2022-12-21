using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ServicesDeskUCAB.DTO;
using System.Dynamic;
using System.Text;
using ServicesDeskUCAB.Factory;

namespace ServicesDeskUCAB.Controllers
{
    public class ModeloJerarquicoController : Controller
    {
        public async Task<IActionResult> GestionMJerarquico()
        {
            try
            {
                List<ModeloJerarquicoDTO> listDto = new List<ModeloJerarquicoDTO>();
                HttpClient clientMJerarquico = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7198/ModeloAprobacion/GetModeloJerarquico/");
                    var _client = await clientMJerarquico.SendAsync(request);

                    if(_client.IsSuccessStatusCode)
                    {
                        var responseStream = await _client.Content.ReadAsStreamAsync();
                        listDto = await JsonSerializer.DeserializeAsync<List<ModeloJerarquicoDTO>>(responseStream);
                    } else 
                    {
                        BadRequest();
                    }

                return View(listDto);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message +" || "+ ex.StackTrace);
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public IActionResult VentanaAgregarModeloJerarquico()
        {
            try
            {
                    return View();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message + " || "+ex.StackTrace, ex.InnerException);
            }
        } 

        public async Task<IActionResult> AgregarMJerarquico(ModeloJerarquicoDTO modeloDto)
        {
            try
            {
                modeloDto.id = 0;
               var client = FactoryHttp.CreateClient();

               var _client = await client.PostAsJsonAsync<ModeloJerarquicoDTO>("https://localhost:7198/ModeloAprobacion/Jerarquico/", modeloDto);

                return RedirectToAction("GestionMJerarquico");

            }catch(Exception ex)
            {
                throw new Exception(ex.Message + " || "+ex.StackTrace, ex.InnerException);
            }
        }

        public async Task<IActionResult> VentanaEditarModeloJerarquico(int id)
        {
            try
            {
                var modelDto = FactoryMJerarquico.CreateModeloJerarquico();
                var client = FactoryHttp.CreateClient();

                var request = new HttpRequestMessage(HttpMethod.Get,"https://localhost:7198/ModeloAprobacion/Jerarquico" + id.ToString());
                var _client = await client.SendAsync(request);

                if(_client.IsSuccessStatusCode)
                {
                    var responseStream = await _client.Content.ReadAsStreamAsync();
                    modelDto = await JsonSerializer.DeserializeAsync<ModeloJerarquicoDTO>(responseStream);

                } else 
                {
                    BadRequest();
                }

                return View(modelDto);

            }catch(Exception ex)
            {
                throw new Exception(ex.Message + "|| "+ ex.StackTrace, ex);
            }
        } 

        public async Task<IActionResult> ActualizarModeloJerarquico(ModeloJerarquicoDTO modeloDto)
        {
            try
            {
                    var client = FactoryHttp.CreateClient();
                    var _client = await client.PutAsJsonAsync<ModeloJerarquicoDTO>("https://localhost:7198/ModeloAprobacion/ActualizaModeloJerarquico/", modeloDto);

                    return RedirectToAction("GestionMJerarquico");
            }catch(Exception ex)
            {
                throw new Exception(ex.Message+""+ex.StackTrace, ex);
            }
        }

        public async Task<IActionResult> VentanaEliminarModeloJerarquico(int id)
        {
            try
            {
                return View(id);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message + " || "+ex.StackTrace, ex.InnerException);
            }
        }      

        public async Task<IActionResult> EliminarModeloJerarquico(int id)
        {
            try
            {
                var client = FactoryHttp.CreateClient();
                var _client = await client.DeleteAsync("https://localhost:7198/ModeloAprobacion/DeleteModeloJerarquico/"+id.ToString());

                

                return RedirectToAction("GestionMJerarquico");
            }catch(Exception ex)
            {
                throw new Exception(ex.Message + " || "+ex.StackTrace, ex.InnerException);
            }
        }
    }
}