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
                    ViewBag.Error = json_respuesta["message"].ToString() + json_respuesta["Exception"].ToString();

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
                    ViewBag.Error = json_respuesta["message"].ToString() + json_respuesta["Exception"].ToString();

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
                    ViewBag.Error = json_respuesta["message"].ToString() + json_respuesta["Exception"].ToString();

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

                    ViewBag.Error = json_respuesta["message"].ToString() + json_respuesta["Exception"].ToString();

                }
            }

            return View();
        }


 public async Task<IActionResult> AsignarTicket(int iddept)
        {
            List<EmpleadosDTO> listemp = new List<EmpleadosDTO>();
            List<PrioridadDTO> listPri = new List<PrioridadDTO>();
            using (var httpClient = new HttpClient())
            {
                var prioridades = await httpClient.GetAsync("https://localhost:7198/Prioridad/ConsultaPrioridades");
                var usuarios = await httpClient.GetAsync("https://localhost:7198/Usuario/Empleados/Departamento/" + iddept );
                listPri = JsonConvert.DeserializeObject<List<PrioridadDTO>>(await prioridades.Content.ReadAsStringAsync());
                listemp = JsonConvert.DeserializeObject<List<EmpleadosDTO>>(await usuarios.Content.ReadAsStringAsync());
                dynamic mymodel = new ExpandoObject();
                mymodel.Prioridades = listPri;
                mymodel.Usuarios = listemp;
                return View(mymodel);
            }
            return View();
        }


    }
}




