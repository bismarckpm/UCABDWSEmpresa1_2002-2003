using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServicesDeskUCAB.DTO;
using System.Dynamic;
using System.Text;
using ServicesDeskUCAB.Factory;
using ServicesDeskUCAB.ResponseHandler;

namespace ServicesDeskUCAB.Controllers
{
    public class ModeloJerarquicoController : Controller
    {
        public async Task<IActionResult> GestionMJerarquico()
        {
            try
            {
                AplicationResponseHandler<List<ModeloJerarquicoDTO>> apiResponseH = new AplicationResponseHandler<List<ModeloJerarquicoDTO>>();
                 List<ModeloJerarquicoDTO> listDto = new List<ModeloJerarquicoDTO>();
                HttpClient clientMJerarquico = new HttpClient();

                    var response = await clientMJerarquico.GetAsync("https://localhost:7198/ModeloAprobacion/GetModeloJerarquico/");
                    
                    if(response.IsSuccessStatusCode)
                    {
                        var responseStream = await response.Content.ReadAsStringAsync();
                        apiResponseH = JsonConvert.DeserializeObject<AplicationResponseHandler<List<ModeloJerarquicoDTO>>>(responseStream);
                        listDto = apiResponseH!.Data;
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

        public async Task<IActionResult> VentanaAgregarModeloJerarquico()
        {
            try
            {
                AplicationResponseHandler<List<TipoCargoDTO>> ApiResponseH = new AplicationResponseHandler<List<TipoCargoDTO>>();
                List<CategoriaDTO> categorias = new List<CategoriaDTO>();
                List<TipoCargoDTO> tipoCargos = new List<TipoCargoDTO>();
                    
                using(var clientMJ = new HttpClient())
                {
                   var categoria = await clientMJ.GetAsync("https://localhost:7198/Categoria/ConsultaCategorias");
                   var tipoCargo = await clientMJ.GetAsync("https://localhost:7198/TipoCargo/ConsultaTCargo/");

                        string response = await categoria.Content.ReadAsStringAsync();
                        string response2 = await tipoCargo.Content.ReadAsStringAsync();

                        categorias = JsonConvert.DeserializeObject<List<CategoriaDTO>>(value: response);
                        ApiResponseH = JsonConvert.DeserializeObject<AplicationResponseHandler<List<TipoCargoDTO>>>(value: response2);
                        tipoCargos = ApiResponseH!.Data;
                        dynamic model = new ExpandoObject();
                        model.Categorias = categorias;
                        model.TipoCargos = tipoCargos;

                        return View(model);
                    
                }

                    // return View();
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
                AplicationResponseHandler<List<TipoCargoDTO>> apiTipoCargo = new AplicationResponseHandler<List<TipoCargoDTO>>();
                ModeloJerarquicoDTO  dto = new  ModeloJerarquicoDTO();
                List<TipoCargoDTO> listTipoCargos = new List<TipoCargoDTO>();
                List<CategoriaDTO> listCategorias = new List<CategoriaDTO>();
                
                using(var client = FactoryHttp.CreateClient())
                {
                    var response = await client.GetAsync("https://localhost:7198/ModeloAprobacion/Jerarquico" + id.ToString());
                    var responseStream = await response.Content.ReadAsStringAsync();
                    ApiResponseH = JsonConvert.DeserializeObject<AplicationResponseHandler<ModeloJerarquicoDTO>>(responseStream);
                    dto = ApiResponseH!.Data;

                   var categoria = await client.GetAsync("https://localhost:7198/Categoria/ConsultaCategorias");
                   var tipoCargo = await client.GetAsync("https://localhost:7198/TipoCargo/ConsultaTCargo/");

                        string response2 = await categoria.Content.ReadAsStringAsync();
                        string response3 = await tipoCargo.Content.ReadAsStringAsync();

                        listCategorias = JsonConvert.DeserializeObject<List<CategoriaDTO>>(value: response2);
                        apiTipoCargo = JsonConvert.DeserializeObject<AplicationResponseHandler<List<TipoCargoDTO>>>(value: response3);
                        listTipoCargos = apiTipoCargo!.Data;


                    dynamic model = new ExpandoObject();
                        model.ModeloJerarquicos = dto;
                        model.Categorias = listCategorias;
                        model.TipoCargos = listTipoCargos;

                return View(model);
                }

            }catch(Exception ex)
            {
                throw new Exception(ex.Message + " || "+ ex.StackTrace, ex);
            }
        } 

        public async Task<IActionResult> ActualizarModeloJerarquico(ModeloJerarquicoDTO modeloJerarquico)
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