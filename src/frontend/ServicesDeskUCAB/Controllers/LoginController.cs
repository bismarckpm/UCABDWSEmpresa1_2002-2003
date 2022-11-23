using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServicesDeskUCAB.DTO;
using ServicesDeskUCAB.Services.Login;
using System.Dynamic;
using System.Text;


namespace ServicesDeskUCAB.Controllers
{
    public class loginController : Controller
    {
        public ViewResult Index() => View();
        [HttpPost]
        public async Task<IActionResult> Index(UserLoginDTO usuario)
        {
          LoginDTO user = new LoginDTO();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");
 
                using (var response = await httpClient.PostAsync("https://localhost:7198/Usuario/Login", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                         string response2 = await response.Content.ReadAsStringAsync();
                         user = JsonConvert.DeserializeObject<LoginDTO>(response2);
                        HttpContext.Session.SetInt32("userid",user.id);
                        HttpContext.Session.SetString("email", user.email);
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
 
                using (var response = await httpClient.PostAsync("https://localhost:7198/Usuario/Registrar?cargoid=0&tipousuario=3&Departamentoid=0", content))
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
      
        public ViewResult VerificarUsuario() => View();
        [HttpPost]
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
    

      public async Task<IActionResult> RegistrarEmpleado(){
        List<DepartamentoDTO> listDep = new List<DepartamentoDTO>();
        List<CargoDTO> listCargo = new List<CargoDTO>();
         using (var httpClient = new HttpClient())
            {
                using (var departamento = await httpClient.GetAsync("https://localhost:7198/Departamento/ConsultaDepartamentos"))
                {
                    if (departamento.IsSuccessStatusCode)
                    {
                    var cargos = await httpClient.GetAsync("https://localhost:7198/Cargo/ConsultaCargo");
                      string apiResponse = await departamento.Content.ReadAsStringAsync();
                      string apiResponse2 = await cargos.Content.ReadAsStringAsync();
                      listDep = JsonConvert.DeserializeObject<List<DepartamentoDTO>>(apiResponse);
                      listCargo = JsonConvert.DeserializeObject<List<CargoDTO>>(apiResponse2);
                      dynamic mymodel = new ExpandoObject();
                      mymodel.Departamentos = listDep;
                      mymodel.Cargos = listCargo;
                     return View(mymodel);
                    }
                    
                   

                }
          return View();
            }

    }
 [HttpPost]
     public async Task<IActionResult> RegistrarEmpleado(int tipou, int departamentos, int cargos, string email , string password, string confirmationpassword)
        {
             RegistroDTO usuario = new RegistroDTO();
             usuario.Email = email;
             usuario.Password= password;
             usuario.confirmationpassword = confirmationpassword;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");
 
                using (var response = await httpClient.PostAsync("https://localhost:7198/Usuario/Registrar?cargoid="+cargos+"&tipousuario="+tipou+"&Departamentoid=" + departamentos, content))
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


        public IActionResult Cerrar(){
            HttpContext.Session.Remove("userid");
            HttpContext.Session.Remove("email");
            return RedirectToAction("Index");
        }
      
    }
}
