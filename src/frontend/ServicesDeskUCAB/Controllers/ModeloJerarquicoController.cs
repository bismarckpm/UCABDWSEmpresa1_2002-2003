using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServicesDeskUCAB.DTO;
using System.Dynamic;
using System.Text;
using ServicesDeskUCAB.Factory;
using ServicesDeskUCAB.ResponseHandler;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServicesDeskUCAB.Controllers
{
    public class ModeloJerarquicoController : Controller
    {
        public async Task<IActionResult> GestionMJerarquico()
        {
            try
            {
                AplicationResponseHandler<List<ModeloJCDTO>> apiResponseH = new AplicationResponseHandler<List<ModeloJCDTO>>();
                
                using(var clientMJerarquico = FactoryHttp.CreateClient())
                {
                       var ModeloJ = await clientMJerarquico.GetAsync("https://localhost:7198/ModeloAprobacion/GetModeloJerarquico/");

                        var response = await ModeloJ.Content.ReadAsStringAsync();

                        apiResponseH = JsonConvert.DeserializeObject<AplicationResponseHandler<List<ModeloJCDTO>>>(response);

                    return View(apiResponseH.Data);
                }  
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message +" || "+ ex.StackTrace);
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public async Task<IActionResult> VentanaAgregarModeloJerarquico()
        {
            try
            {
                AplicationResponseHandler<List<CategoriaDTO>> ApiResponse = new AplicationResponseHandler<List<CategoriaDTO>>();

                List<CategoriaDTO> categorias = new List<CategoriaDTO>();
                   
                using(var clientMJ = FactoryHttp.CreateClient())
                {
                   var categoria = await clientMJ.GetAsync("https://localhost:7198/Categoria/ConsultaCategorias");

                        string response = await categoria.Content.ReadAsStringAsync();

                       ApiResponse = JsonConvert.DeserializeObject<AplicationResponseHandler<List<CategoriaDTO>>>(value: response);

                        dynamic model = new ExpandoObject();
                        model.Categorias = ApiResponse!.Data;

                        return View(model);
                    
                }
            }catch(Exception ex)
            {
                throw new (ex.Message + " || "+ex.StackTrace, ex);
            }
        } 

        public async Task<IActionResult> AgregarMJerarquico(ModeloJerarquicoDTO modeloJerarquico, JerarquicoTipoCargoDTO jerarquicoTcargo)
        {
            try
            {
                modeloJerarquico.id = 0;
                modeloJerarquico.orden!.Add(jerarquicoTcargo);
               var client = FactoryHttp.CreateClient();

               var _client = await client.PostAsJsonAsync<ModeloJerarquicoDTO>("https://localhost:7198/ModeloAprobacion/Jerarquico/", modeloJerarquico);

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
                AplicationResponseHandler<ModeloJerarquicoDTO> ApiResponseH = new AplicationResponseHandler<ModeloJerarquicoDTO>();
                AplicationResponseHandler<List<CategoriaDTO>> apiCategoria = new AplicationResponseHandler<List<CategoriaDTO>>();
                
                using(var client = FactoryHttp.CreateClient())
                {
                    var response = await client.GetAsync("https://localhost:7198/ModeloAprobacion/Jerarquico/" + id.ToString());
                    var responseStream = await response.Content.ReadAsStringAsync();
                    ApiResponseH = JsonConvert.DeserializeObject<AplicationResponseHandler<ModeloJerarquicoDTO>>(responseStream);
                   
                    var categoria = await client.GetAsync("https://localhost:7198/Categoria/ConsultaCategorias");
        
                    string response2 = await categoria.Content.ReadAsStringAsync();
        
                    apiCategoria = JsonConvert.DeserializeObject<AplicationResponseHandler<List<CategoriaDTO>>>(value: response2);
        
                    List<SelectListItem> listItemsCategoria = crearCategoriaDropDown(apiCategoria!.Data);

                    var tupla = new Tuple<ModeloJerarquicoDTO,List<SelectListItem>>(ApiResponseH!.Data, listItemsCategoria);

                    return View(tupla);
                }

            }catch(Exception ex)
            {
                throw new Exception(ex.Message + " || "+ ex.StackTrace, ex);
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


        public async Task<IActionResult> ActualizarModeloJerarquico([Bind(Prefix ="Item1")] ModeloJerarquicoDTO modeloJerarquico)
        {
            try
            {
                
                    var client = FactoryHttp.CreateClient();
                    var _client = await client.PutAsJsonAsync<ModeloJerarquicoDTO>("https://localhost:7198/ModeloAprobacion/ActualizaModeloJerarquico/", modeloJerarquico);

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

        public async Task<IActionResult> EliminarMJerarquico(int id)
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