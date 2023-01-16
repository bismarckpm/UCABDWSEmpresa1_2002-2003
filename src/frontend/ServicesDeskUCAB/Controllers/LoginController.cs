using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServicesDeskUCAB.DTO;
using ServicesDeskUCAB.ResponseHandler;
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
            AplicationResponseHandler<UsuarioDTO> userResponse = new AplicationResponseHandler<UsuarioDTO>();
            LoginDTO user = new LoginDTO();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");
 
                using (var response = await httpClient.PostAsync("https://localhost:7198/Usuario/Login", content))
                {
                    var response2 = await response.Content.ReadAsStringAsync();
                    JObject json_respuesta = JObject.Parse(response2);
                    Console.WriteLine(json_respuesta["success"].ToString());
                    if (json_respuesta["success"].ToString() == "True")
                    {
                        string stringDataRespuesta = json_respuesta["data"].ToString();
                        user = JsonConvert.DeserializeObject<LoginDTO>(stringDataRespuesta);
                        HttpContext.Session.SetInt32("userid",user.id);
                        HttpContext.Session.SetString("email", user.Email);
                        HttpContext.Session.SetString("rol", user.Discriminator);
                        return RedirectToAction("Index", "Home");
                    }
                     ViewBag.Error = json_respuesta["message"].ToString();

                }
            }
            return View();
        }

        public ViewResult Registrarse() => View();
        [HttpPost]
        public async Task<IActionResult> Registrarse(RegistroDTO usuario)
        {
            using (var httpClient = new HttpClient())
            {
                 if (!ModelState.IsValid)
                 {
                    return View();
                }       
                StringContent content = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");
 
                using (var response = await httpClient.PostAsync("https://localhost:7198/Usuario/Registrar", content))
                {
                    var response2 = await response.Content.ReadAsStringAsync();
                    JObject json_respuesta = JObject.Parse(response2);
                    Console.WriteLine(json_respuesta["success"].ToString());
                    if (json_respuesta["success"].ToString() == "True")
                    {
                         return RedirectToAction("VerificarUsuario");
                    }
                    if(json_respuesta["exception"].ToString().Contains("No se puede insertar una fila de clave duplicada")){
                    ViewBag.Error ="Usuario duplicado" ;
                    }else{
                        ViewBag.Error = json_respuesta["message"].ToString() ;
                    }
                    

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

                    var response2 = await response.Content.ReadAsStringAsync();
                    JObject json_respuesta = JObject.Parse(response2);
                    Console.WriteLine(json_respuesta["success"].ToString());
                    if (json_respuesta["success"].ToString() == "True")
                    {
                       return RedirectToAction("Index");
                    }
                    
                    ViewBag.Error = json_respuesta["message"].ToString() + json_respuesta["exception"].ToString();

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
                     var response2 = await response.Content.ReadAsStringAsync();
                    JObject json_respuesta = JObject.Parse(response2);
                    Console.WriteLine(json_respuesta["success"].ToString());
                    if (json_respuesta["success"].ToString() == "True")
                    {
                         return RedirectToAction("ResetPassword");
                    }
                    
                 ViewBag.Error = json_respuesta["message"].ToString() + json_respuesta["exception"].ToString();


                }
            }
           
            return View();
        }
          public ViewResult ResetPassword() => View();
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO usuario)
        {
              if (!ModelState.IsValid)
                 {
                    return View();
                }  
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");
 
                using (var response = await httpClient.PostAsync("https://localhost:7198/Usuario/Reset-Password", content))
                {
                   var response2 = await response.Content.ReadAsStringAsync();
                    JObject json_respuesta = JObject.Parse(response2);
                    Console.WriteLine(json_respuesta["success"].ToString());
                    if (json_respuesta["success"].ToString() == "True")
                    {
                         return RedirectToAction("Index");
                    }
                    
                ViewBag.Error = json_respuesta["message"].ToString() + json_respuesta["exception"].ToString();


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
