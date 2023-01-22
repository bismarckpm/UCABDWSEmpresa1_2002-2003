using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServicesDeskUCAB.DTO;
using System.Dynamic;
using System.Text;
using ServicesDeskUCAB.Factory;
using ServicesDeskUCAB.ResponseHandler;

namespace ServicesDeskUCAB.Controllers
{
    public class JerarquicoTipoCargoController : Controller
    {
        public async Task<IActionResult> GestionJerarquicoTCargo()
        {
            try
            {
                    AplicationResponseHandler<List<JerarquicoTipoCargoDTO>> jResponse = new AplicationResponseHandler<List<JerarquicoTipoCargoDTO>>();
                    HttpClient JClient = FactoryHttp.CreateClient();
                    
                    var response = await JClient.GetAsync("https://localhost:7198/JerarquicoTipoCargo/ListJerarquicoTCargo/");

                        if(response.IsSuccessStatusCode)
                        {
                            var responseStream = await response.Content.ReadAsStringAsync();
                            jResponse = JsonConvert.DeserializeObject<AplicationResponseHandler<List<JerarquicoTipoCargoDTO>>>(responseStream);

                        }
                    return View(jResponse.Data);

            }catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<IActionResult> VentanaAgregarJerarquicoTCargo()
        {
            try
            {
                AplicationResponseHandler<List<TipoCargoDTO>> ApiResponseH = new AplicationResponseHandler<List<TipoCargoDTO>>();
                AplicationResponseHandler<List<ModeloJerarquicoDTO>> mjResponse = new AplicationResponseHandler<List<ModeloJerarquicoDTO>>();

                using(var mjClient = FactoryHttp.CreateClient())
                {
                        var mJerarquico = await mjClient.GetAsync("https://localhost:7198/ModeloAprobacion/GetModeloJerarquico/");
                        var tCargo      = await mjClient.GetAsync("https://localhost:7198/TipoCargo/ConsultaTCargo/");

                        string response = await mJerarquico.Content.ReadAsStringAsync();
                        string response2 = await tCargo.Content.ReadAsStringAsync();

                        ApiResponseH = JsonConvert.DeserializeObject<AplicationResponseHandler<List<TipoCargoDTO>>>(response2);
                        mjResponse = JsonConvert.DeserializeObject<AplicationResponseHandler<List<ModeloJerarquicoDTO>>>(response);

                        dynamic model = new ExpandoObject();
                        model.TipoCargo = ApiResponseH.Data;
                        model.ModeloJerarquico = mjResponse.Data;

                        return View(model);
                }

            }catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AgregarJerarquicoTCargo(JerarquicoTipoCargoDTO dto)
        {
            try{
                    dto.id = 0;
                    var client = FactoryHttp.CreateClient();

                    var _client = await client.PostAsJsonAsync<JerarquicoTipoCargoDTO>("https://localhost:7198/JerarquicoTipoCargo/JerarquicoTCargo/",dto);

                    return RedirectToAction("GestionJerarquicoTCargo");
            }catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<IActionResult> VentanaEditarJerarquicoTCargo(int id)
        {
            try
            {
                AplicationResponseHandler<JerarquicoTipoCargoDTO> jcResponse = new AplicationResponseHandler<JerarquicoTipoCargoDTO>();
                AplicationResponseHandler<TipoCargoDTO> tcResponse = new AplicationResponseHandler<TipoCargoDTO>();

                using(var client = FactoryHttp.CreateClient())
                {
                    var response1 = await client.GetAsync("https://localhost:7198/JerarquicoTipoCargo/JerarquicoTCargo/" + id.ToString());
                    var respStream = await response1.Content.ReadAsStringAsync();

                    jcResponse = JsonConvert.DeserializeObject<AplicationResponseHandler<JerarquicoTipoCargoDTO>>(respStream);

                    return View(jcResponse.Data);
                }

            }catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<IActionResult> ActualizarJerarquicoTCargo(JerarquicoTipoCargoDTO jtcDto)
        {
            try
            {
                var client = FactoryHttp.CreateClient();
                var _client = await client.PutAsJsonAsync<JerarquicoTipoCargoDTO>("https://localhost:7198/JerarquicoTipoCargo/UpdateJerarquicoTC/", jtcDto);

                return RedirectToAction("GestionJerarquicoTCargo");
            }catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<IActionResult> VentanaEliminarJerarquicoTCargo(int id)
        {
            try
            {
                return View(id);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<IActionResult> EliminarJerarquicoTCargo(int id)
        {
            try
            {
                var client = FactoryHttp.CreateClient();
                var _client = await client.DeleteAsync("https://localhost:7198/JerarquicoTipoCargo/DeleteJerarquicoTC/"+id.ToString());                

                return RedirectToAction("GestionJerarquicoTCargo");


            }catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }


    }
}