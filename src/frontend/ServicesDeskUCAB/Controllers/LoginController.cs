using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServicesDeskUCAB.DTO;
using ServicesDeskUCAB.Services.Login;
using System.Text;

namespace ServicesDeskUCAB.Controllers
{
    public class loginController : Controller
    {
        public ViewResult Index() => View();
        [HttpPost]
        public async Task<IActionResult> Index(UserLoginDTO usuario)
        {
         
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");
 
                using (var response = await httpClient.PostAsync("https://localhost:7198/Usuario/Login", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                      return RedirectToAction("Index", "Home");
                    }
                    
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    dynamic json  = JsonConvert.DeserializeObject(apiResponse);
                    ViewBag.Result = apiResponse;

                }
            }
            return View();
        }
        public ViewResult Registrarse() => View();
        [HttpPost]
        public async Task<IActionResult> Registrarse(RegistroDTO usuario)
        {
             ErrotsDTO errors = new ErrotsDTO();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");
 
                using (var response = await httpClient.PostAsync("https://localhost:7198/Usuario/Registrar?cargoid=0&tipousuario=3", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                         return RedirectToAction("VerificarUsuario");
                    }
                    
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    dynamic json  = JsonConvert.DeserializeObject(apiResponse);
                    ViewBag.Result = json.errors;

                }
            }
           
            return View();
        }
      
    
        public async Task<IActionResult> VerificarUsuario(string token)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7198/Usuario/Verificar?token="+token))
                {
                    if (response.IsSuccessStatusCode)
                    {
                         return RedirectToAction("Index");
                    }
                    
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    dynamic json  = JsonConvert.DeserializeObject(apiResponse);
                    ViewBag.Result = json;

                }
            }
           
            return View();
        }
       

        public async Task<IActionResult> Olvidocontrasena(string email)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7198/Usuario/olvido-contrasena?email="+email))
                {
                    if (response.IsSuccessStatusCode)
                    {
                         return RedirectToAction("ResetPassword");
                    }
                    
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    dynamic json  = JsonConvert.DeserializeObject(apiResponse);
                    ViewBag.Result = json;

                }
            }
           
            return View();
        }
          public ViewResult ResetPassword() => View();
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO usuario)
        {
             ErrotsDTO errors = new ErrotsDTO();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");
 
                using (var response = await httpClient.PostAsync("https://localhost:7198/Usuario/Reset-Password", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                         return RedirectToAction("Index");
                    }
                    
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    dynamic json  = JsonConvert.DeserializeObject(apiResponse);
                    ViewBag.Result = json;

                }
            }
           
            return View();
        }
    }
}