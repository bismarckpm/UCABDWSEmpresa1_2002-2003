using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ServicesDeskUCAB.DTO;
using System.Dynamic;
using ServicesDeskUCAB.Factory;
using Newtonsoft.Json;
using ServicesDeskUCAB.ResponseHandler;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace ServicesDeskUCAB.Controllers
{
    public class ModeloParaleloController : Controller
    {
        public async Task<IActionResult> GestionMParalelo()
        {
            try
            {
                List<ModeloParaleloDTO> listDto = new List<ModeloParaleloDTO>();
                HttpClient clientMParalelo = FactoryHttp.CreateClient();
                var request = await clientMParalelo.GetAsync("https://localhost:7198/ModeloAprobacion/GetModeloParalelo/");
                if(request.IsSuccessStatusCode)
                {
                    var responseStream = await request.Content.ReadAsStringAsync();
                    listDto = JsonConvert.DeserializeObject<List<ModeloParaleloDTO>>(responseStream);
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
                AplicationResponseHandler<List<CategoriaDTO>> apiCategoria = new AplicationResponseHandler<List<CategoriaDTO>>();
                using (var client = FactoryHttp.CreateClient())
                {
                    var categoria = await client.GetAsync("https://localhost:7198/Categoria/ConsultaCategorias");
                    string response2 = await categoria.Content.ReadAsStringAsync();
                    apiCategoria = JsonConvert.DeserializeObject<AplicationResponseHandler<List<CategoriaDTO>>>(value: response2);

                    List<SelectListItem> listItemsCategoria = crearCategoriaDropDown(apiCategoria!.Data);

                    var tuple = new Tuple<ModeloParaleloDTO, List<SelectListItem>>(new ModeloParaleloDTO(),listItemsCategoria);
                    return View(tuple);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " || " + ex.StackTrace, ex.InnerException);
            }
        }

        private static List<SelectListItem> crearCategoriaDropDown(List<CategoriaDTO> lista)
        {
            List<SelectListItem> listItems = new List<SelectListItem>();

            foreach (var item in lista)
            {
                listItems.Add(new SelectListItem
                {
                    Text = item.nombre,
                    Value = item.id.ToString(),
                });
            }

            return listItems;
        }

        public async Task<IActionResult> AgregarMParalelo([Bind(Prefix = "Item1")] ModeloParaleloDTO modeloParalelo)
        {
            try
            {
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
                ModeloParaleloDTO response = new ModeloParaleloDTO();
                AplicationResponseHandler<List<CategoriaDTO>> apiCategoria = new AplicationResponseHandler<List<CategoriaDTO>>();
                using(var client = FactoryHttp.CreateClient())
                {
                    var request = await client.GetAsync("https://localhost:7198/ModeloAprobacion/Paralelo/" + id.ToString());
                    var responseStream = await request.Content.ReadAsStringAsync();
                    response = JsonConvert.DeserializeObject<ModeloParaleloDTO>(responseStream);

                    var categoria = await client.GetAsync("https://localhost:7198/Categoria/ConsultaCategorias");
                    string response2 = await categoria.Content.ReadAsStringAsync();
                    apiCategoria = JsonConvert.DeserializeObject<AplicationResponseHandler<List<CategoriaDTO>>>(value: response2);
                    List<SelectListItem> listItemsCategoria = crearCategoriaDropDown(apiCategoria!.Data);

                    var tuple = new Tuple<ModeloParaleloDTO, List<SelectListItem>>(response, listItemsCategoria);
                    return View(tuple);
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message + "|| "+ ex.StackTrace, ex);
            }
        } 

        public async Task<IActionResult> ActualizarModeloParalelo([Bind(Prefix = "Item1")] ModeloParaleloDTO modeloParalelo)
        {
            try
            {
                    var client = FactoryHttp.CreateClient();
                    var _client = await client.PutAsJsonAsync<ModeloParaleloDTO>("https://localhost:7198/ModeloAprobacion/ActualizarModeloParalelo/", modeloParalelo);

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