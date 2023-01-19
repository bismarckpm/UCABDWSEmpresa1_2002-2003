using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ServicesDeskUCAB.DTO;
using System.Dynamic;
using ServicesDeskUCAB.Factory;

namespace ServicesDeskUCAB.Controllers
{
    public class ModeloParaleloController : Controller
    {
        public async Task<IActionResult> GestionMParalelo()
        {
            try
            {
                List<ModeloParaleloDTO> listDto = new List<ModeloParaleloDTO>();
                HttpClient clientMParalelo = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7198/ModeloAprobacion/GetModeloParalelo/");
                    var _client = await clientMParalelo.SendAsync(request);

                    if(_client.IsSuccessStatusCode)
                    {
                        var responseStream = await _client.Content.ReadAsStreamAsync();
                        listDto = await JsonSerializer.DeserializeAsync<List<ModeloParaleloDTO>>(responseStream);
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

        public async Task<IActionResult> VentanaAgregarModeloParalelo()
        {
            try
            {
                List<CategoriaDTO> categorias = new List<CategoriaDTO>();
                    
                    var client = FactoryHttp.CreateClient();
                    var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7198/Categoria/ConsultaCategorias");
                    var _clientC = await client.SendAsync(request);

                    if(_clientC.IsSuccessStatusCode)
                    {
                        var response = await _clientC.Content.ReadAsStreamAsync();
                        categorias = await JsonSerializer.DeserializeAsync<List<CategoriaDTO>>(response!);                        
                        dynamic model = new ExpandoObject();
                        model.Categorias = categorias;
                        return View(model);
                    }

                    return View();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message + " || "+ex.StackTrace, ex.InnerException);
            }
        } 

        public async Task<IActionResult> AgregarMParalelo(ModeloParaleloDTO modeloParalelo)
        {
            try
            {

                modeloParalelo.id = 0;
               var client = FactoryHttp.CreateClient();

               var _client = await client.PostAsJsonAsync<ModeloParaleloDTO>("https://localhost:7198/ModeloAprobacion/Paralelo/", modeloParalelo);

                return RedirectToAction("GestionMParalelo");

            }catch(Exception ex)
            {
                throw new Exception(ex.Message + " || "+ex.StackTrace, ex.InnerException);
            }
        }

        public async Task<IActionResult> VentanaEditarModeloParalelo(int id)
        {
            try
            {
                var modelDto = FactoryMParalelo.CreateModeloParalelo();
                var client = FactoryHttp.CreateClient();

                var request = new HttpRequestMessage(HttpMethod.Get,"https://localhost:7198/ModeloAprobacion/Paralelo" + id.ToString());
                var _client = await client.SendAsync(request);

                if(_client.IsSuccessStatusCode)
                {
                    var responseStream = await _client.Content.ReadAsStreamAsync();
                    modelDto = await JsonSerializer.DeserializeAsync<ModeloParaleloDTO>(responseStream);

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

        public async Task<IActionResult> ActualizarModeloParalelo(ModeloParaleloDTO modeloParalelo)
        {
            try
            {
                    var client = FactoryHttp.CreateClient();
                    var _client = await client.PutAsJsonAsync<ModeloParaleloDTO>("https://localhost:7198/ModeloAprobacion/ActualizaModeloParalelo/", modeloParalelo);

                    return RedirectToAction("GestionMParalelo");
            }catch(Exception ex)
            {
                throw new Exception(ex.Message+""+ex.StackTrace, ex);
            }
        }

        public async Task<IActionResult> VentanaEliminarModeloParalelo(int id)
        {
            try
            {
                return View(id);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message + " || "+ex.StackTrace, ex.InnerException);
            }
        }      

        public async Task<IActionResult> EliminarMParalelo(int id)
        {
            try
            {
                var client = FactoryHttp.CreateClient();
                var _client = await client.DeleteAsync("https://localhost:7198/ModeloAprobacion/DeleteModeloParalelo/"+id.ToString());

                return RedirectToAction("GestionMParalelo");
                
            }catch(Exception ex)
            {
                throw new Exception(ex.Message + " || "+ex.StackTrace, ex.InnerException);
            }
        }
    }
}