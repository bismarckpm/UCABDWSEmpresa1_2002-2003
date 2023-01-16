using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServicesDeskUCAB.DTO;
using System.Dynamic;
using System.Text;

namespace ServicesDeskUCAB.Controllers
{
    public class TicketController : Controller
    {
        public async Task<IActionResult> GestionTickets()
        {

            List<TicketCDTO> listaTickets = new List<TicketCDTO>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7198/Tickets/Tickects/" + HttpContext.Session.GetInt32("userid")))
                {

                    var response2 = await response.Content.ReadAsStringAsync();
                    JObject json_respuesta = JObject.Parse(response2);
                    if (json_respuesta["success"].ToString() == "True")
                    {
                        listaTickets = JsonConvert.DeserializeObject<List<TicketCDTO>>(json_respuesta["data"].ToString());
                        return View(listaTickets);
                    }
                    ViewBag.Error = json_respuesta["message"].ToString();

                }
            }
            return View();

        }

        public async Task<IActionResult> VerTodos()
        {

            List<TicketCDTO> listaTickets = new List<TicketCDTO>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7198/Tickets/"))
                {

                    var response2 = await response.Content.ReadAsStringAsync();
                    JObject json_respuesta = JObject.Parse(response2);
                    if (json_respuesta["success"].ToString() == "True")
                    {
                        listaTickets = JsonConvert.DeserializeObject<List<TicketCDTO>>(json_respuesta["data"].ToString());
                        return View(listaTickets);
                    }
                    ViewBag.Error = json_respuesta["message"].ToString() + json_respuesta["exception"].ToString();

                }
            }
            return View();

        }
        public async Task<IActionResult> TicketsAsignados()
        {

            List<TicketCDTO> listaTickets = new List<TicketCDTO>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7198/Tickets/Tickect/asginado/" + HttpContext.Session.GetInt32("userid")))
                {

                    var response2 = await response.Content.ReadAsStringAsync();
                    JObject json_respuesta = JObject.Parse(response2);
                    if (json_respuesta["success"].ToString() == "True")
                    {
                        listaTickets = JsonConvert.DeserializeObject<List<TicketCDTO>>(json_respuesta["data"].ToString());
                        return View(listaTickets);
                    }
                    ViewBag.Error = json_respuesta["message"].ToString() + json_respuesta["exception"].ToString();

                }
            }
            return View();

        }

        public async Task<IActionResult> CrearTicket()
        {
            List<DepartamentoDTO> listDep = new List<DepartamentoDTO>();
            List<CategoriaDTO> ListCategoria = new List<CategoriaDTO>();
            using (var httpClient = new HttpClient())
            {
                var departamento = await httpClient.GetAsync("https://localhost:7198/Departamento/ConsultaDepartamentos");
                var categoria = await httpClient.GetAsync("https://localhost:7198/Categoria/ConsultaCategorias");
                string apiResponse = await departamento.Content.ReadAsStringAsync();
                string apiResponse2 = await categoria.Content.ReadAsStringAsync();
                listDep = JsonConvert.DeserializeObject<List<DepartamentoDTO>>(apiResponse);
                ListCategoria = JsonConvert.DeserializeObject<List<CategoriaDTO>>(apiResponse2);
                dynamic mymodel = new ExpandoObject();
                mymodel.Departamentos = listDep;
                mymodel.categoria = ListCategoria;
                ViewBag.id = HttpContext.Session.GetInt32("userid");
                return View(mymodel);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CrearTicket(TicketDTO ticket)
        {
            using (var httpClient = new HttpClient())
            {
                ticket.creadopor = HttpContext.Session.GetInt32("userid");
                ticket.fecha = DateTime.Now;
                if (!ModelState.IsValid)
                {
                    return View();
                }
               
                StringContent content = new StringContent(JsonConvert.SerializeObject(ticket), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:7198/Tickets/CreateTicket", content))
                {
                    var response2 = await response.Content.ReadAsStringAsync();
                    JObject json_respuesta = JObject.Parse(response2);
                    Console.WriteLine(json_respuesta["success"].ToString());
                    if (json_respuesta["success"].ToString() == "True")
                    {
                        return RedirectToAction("GestionTickets");
                    }

                    ViewBag.Error = json_respuesta["message"].ToString() + json_respuesta["exception"].ToString();

                }
            }

            return View();
        }


 public async Task<IActionResult> AsignarTicket(int iddept, int Tickectid)
        {
            List<EmpleadosDTO> listemp = new List<EmpleadosDTO>();
            List<PrioridadDTO> listPri = new List<PrioridadDTO>();
            using (var httpClient = new HttpClient())
            {
                var prioridades = await httpClient.GetAsync("https://localhost:7198/Prioridad/ConsultaPrioridades");
                var usuarios = await httpClient.GetAsync("https://localhost:7198/Usuario/Empleados/Departamento/" + iddept );
                var response = await usuarios.Content.ReadAsStringAsync();
                var response2 = await prioridades.Content.ReadAsStringAsync();
                    JObject json_respuesta = JObject.Parse(response2);
                    JObject json_Usuarios = JObject.Parse(response);
                    if (json_respuesta["success"].ToString() == "True")
                    {
                        listPri = JsonConvert.DeserializeObject<List<PrioridadDTO>>(json_respuesta["data"].ToString());
                        listemp = JsonConvert.DeserializeObject<List<EmpleadosDTO>>(json_Usuarios["data"].ToString());
                        dynamic mymodel = new ExpandoObject();
                        mymodel.Prioridades = listPri;
                        mymodel.Usuarios = listemp;
                        mymodel.id= Tickectid;
                        return View(mymodel);
                    }
                    ViewBag.Error = json_respuesta["message"].ToString() + json_respuesta["exception"].ToString();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AsignarTicket(AsginarDTO ticket)
        {
            using (var httpClient = new HttpClient())
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
               
                StringContent content = new StringContent(JsonConvert.SerializeObject(ticket), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:7198/Tickets/AsignarTicket", content))
                {
                    var response2 = await response.Content.ReadAsStringAsync();
                    JObject json_respuesta = JObject.Parse(response2);
                    Console.WriteLine(json_respuesta["success"].ToString());
                    if (json_respuesta["success"].ToString() == "True")
                    {
                        return RedirectToAction("GestionTickets");
                    }
                    ViewBag.Error = json_respuesta["message"].ToString() + json_respuesta["exception"].ToString();

                }
            }

            return View();
        }

         public async Task<IActionResult> Delegar(int Tickectid)
        {
            List<EmpleadosDTO> listemp = new List<EmpleadosDTO>();
            using (var httpClient = new HttpClient())
            {
                var usuarios = await httpClient.GetAsync("https://localhost:7198/Usuario/Empleados/Departamento/" + 0 );
                var response = await usuarios.Content.ReadAsStringAsync();
                    JObject json_Usuarios = JObject.Parse(response);
                    if (json_Usuarios["success"].ToString() == "True")
                    {
                        listemp = JsonConvert.DeserializeObject<List<EmpleadosDTO>>(json_Usuarios["data"].ToString());
                        dynamic mymodel = new ExpandoObject();
                        mymodel.Usuarios = listemp;
                        mymodel.id= Tickectid;
                        return View(mymodel);
                    }
                    ViewBag.Error = json_Usuarios["message"].ToString();
            }
            return View();
        }

         [HttpPost]
        public async Task<IActionResult> Delegar(DelegarDTO ticket)
        {
            using (var httpClient = new HttpClient())
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
               
                StringContent content = new StringContent(JsonConvert.SerializeObject(ticket), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:7198/Tickets/DelegarTicket/" + ticket.idticket, content))
                {
                    
                    var response2 = await response.Content.ReadAsStringAsync();
                    JObject json_respuesta = JObject.Parse(response2);
                   
                    if (json_respuesta["success"].ToString() == "True")
                    {
                        return RedirectToAction("GestionTickets");
                    }
                    ViewBag.Error = json_respuesta["message"].ToString() + json_respuesta["exception"].ToString();

                }
            }

            return View();
        }

        public async Task<IActionResult> Mergear(int Tickectid)
        {
            List<TicketCDTO> listaTickets = new List<TicketCDTO>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7198/Tickets/"))
                {

                    var response2 = await response.Content.ReadAsStringAsync();
                    JObject json_respuesta = JObject.Parse(response2);
                    if (json_respuesta["success"].ToString() == "True")
                    {
                        listaTickets = JsonConvert.DeserializeObject<List<TicketCDTO>>(json_respuesta["data"].ToString());
                        dynamic mymodel = new ExpandoObject();
                        mymodel.Tickects = listaTickets;
                        mymodel.id= Tickectid;
                        return View(mymodel);
                    }
                    ViewBag.Error = json_respuesta["message"].ToString() + json_respuesta["exception"].ToString();

                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Mergear(MergearTicketDTO ticket)
        {
            using (var httpClient = new HttpClient())
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
               
                StringContent content = new StringContent(JsonConvert.SerializeObject(ticket), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:7198/Tickets/MergearTickets/" , content))
                {
                     
                    var response2 = await response.Content.ReadAsStringAsync();
                    JObject json_respuesta = JObject.Parse(response2);
                   
                    if (json_respuesta["success"].ToString() == "True")
                    {
                        return RedirectToAction("GestionTickets");
                    }
                    ViewBag.Error = json_respuesta["message"].ToString() + json_respuesta["exception"].ToString();

                }
            }

            return View();
        }



        public async Task<IActionResult> CambiarEstado(int Tickectid)
        {
            List<EstadoDTO> listaTickets = new List<EstadoDTO>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7198/api/estados"))
                {

                    var response2 = await response.Content.ReadAsStringAsync();
                    JObject json_respuesta = JObject.Parse(response2);
                    if (json_respuesta["success"].ToString() == "True")
                    {
                        listaTickets  = JsonConvert.DeserializeObject<List<EstadoDTO>>(json_respuesta["data"].ToString());
                        dynamic mymodel = new ExpandoObject();
                        mymodel.Estados = listaTickets;
                        mymodel.id= Tickectid;
                        return View(mymodel);
                    }
                    ViewBag.Error = json_respuesta["message"].ToString() + json_respuesta["exception"].ToString();

                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CambiarEstado(TicketEstadoDTO ticket)
        {
            using (var httpClient = new HttpClient())
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
               
                StringContent content = new StringContent(JsonConvert.SerializeObject(ticket), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:7198/Tickets/Estado/" + ticket.idticket, content))
                {
                    
                    var response2 = await response.Content.ReadAsStringAsync();
                    JObject json_respuesta = JObject.Parse(response2);
                   
                    if (json_respuesta["success"].ToString() == "True")
                    {
                        return RedirectToAction("GestionTickets");
                    }
                    ViewBag.Error = json_respuesta["message"].ToString() + json_respuesta["exception"].ToString();

                }
            }

            return View();
        }

    }


}




