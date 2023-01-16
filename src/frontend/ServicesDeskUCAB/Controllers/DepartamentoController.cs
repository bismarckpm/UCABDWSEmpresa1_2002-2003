using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCAB.DTO;
using ServicesDeskUCAB.ResponseHandler;
using Newtonsoft.Json;
using System.Text;

namespace ServicesDeskUCAB.Controllers
{
    public class DepartamentoController : Controller
    {
        public async Task<IActionResult> GestionDepartamentos()
        {
            try
            {
                AplicationResponseHandler<List<DepartamentoDTO>> apiResponse = new AplicationResponseHandler<List<DepartamentoDTO>>();
                HttpClient client = new HttpClient();
                var response = await client.GetAsync("https://localhost:7198/Departamento/ConsultaDepartamentos");
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    apiResponse = JsonConvert.DeserializeObject<AplicationResponseHandler<List<DepartamentoDTO>>>(responseString);
                }
                return View(apiResponse.Data);
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
                HttpClient client = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(departamento), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://localhost:7198/Departamento/CreateDepartamento", content);
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
                AplicationResponseHandler<DepartamentoDTO> apiResponse = new AplicationResponseHandler<DepartamentoDTO>();
                HttpClient client = new HttpClient();
                var response = await client.GetAsync("https://localhost:7198/Departamento/ConsultaDepartamento/" + id.ToString());
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    apiResponse = JsonConvert.DeserializeObject<AplicationResponseHandler<DepartamentoDTO>>(responseString);
                }
                return View(apiResponse.Data);
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