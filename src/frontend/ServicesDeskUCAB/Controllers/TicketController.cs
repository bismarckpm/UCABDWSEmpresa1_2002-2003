using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServicesDeskUCAB.DTO;
using System.Dynamic;
using System.Text;

namespace ServicesDeskUCAB.Controllers
{
    public class TicketController : Controller
    {
        public async Task<IActionResult> GestionTickets()
        {
            try
            {
                List<TicketCDTO> listaTickets = new List<TicketCDTO>();
                HttpClient httpClient = new HttpClient();
               var _client = await httpClient.GetAsync("https://localhost:7198/Tickets");
                if (_client.IsSuccessStatusCode)
                {
                    var responseStream = await _client.Content.ReadAsStringAsync();
                    listaTickets =  JsonConvert.DeserializeObject<List<TicketCDTO>>(responseStream);
                }
                return View(listaTickets);
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }

        public async Task<IActionResult> VentanaAgregarTicket()
        {
            try
            {
                List<PrioridadDTO> listPri = new List<PrioridadDTO>();
                List<EmpleadosDTO> ListEmpl = new List<EmpleadosDTO>();
                 List<CategoriaDTO> listCate = new List<CategoriaDTO>();
                 List<EstadoDTO> listEst = new List<EstadoDTO>();
                
         using (var httpClient = new HttpClient())
            {
                using (var prioridad = await httpClient.GetAsync("https://localhost:7198/Prioridad/ConsultaPrioridades"))
                {
                    if (prioridad.IsSuccessStatusCode)
                    {
                    var empleados = await httpClient.GetAsync("https://localhost:7198/Usuario/Empleados");
                    var categorias = await httpClient.GetAsync("https://localhost:7198/Categoria/ConsultaCategorias");
                    var estados = await httpClient.GetAsync("https://localhost:7198/api/estados");
                    string apiResponse = await prioridad.Content.ReadAsStringAsync();
                    string apiResponse2 = await empleados.Content.ReadAsStringAsync();
                    string apiResponse3 = await categorias.Content.ReadAsStringAsync();
                    string apiResponse4 = await estados.Content.ReadAsStringAsync();
                      listPri = JsonConvert.DeserializeObject<List<PrioridadDTO>>(apiResponse);
                      ListEmpl = JsonConvert.DeserializeObject<List<EmpleadosDTO>>(apiResponse2);
                      listCate = JsonConvert.DeserializeObject<List<CategoriaDTO>>(apiResponse3);
                        listEst = JsonConvert.DeserializeObject<List<EstadoDTO>>(apiResponse4);

                      dynamic mymodel = new ExpandoObject();
                      mymodel.Empleados = ListEmpl;
                      mymodel.Prioridades = listPri;
                      mymodel.Categorias = listCate;
                      mymodel.Estados = listEst;
                     return View(mymodel);
                    }
                    
                   

                }
          return View();}}
            
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }

 [HttpPost]
        public async Task<IActionResult> VentanaAgregarTicket(int prioridad, int categoria, int estatus, int asignadoa , string nombre, string descripcion)
        {
            try
            {               
             TicketDTO ticket = new TicketDTO();
             ticket.fecha = DateTime.Now;
             ticket.descripcion= descripcion;
             ticket.nombre = nombre;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(ticket), Encoding.UTF8, "application/json");
 
                using (var response = await httpClient.PostAsync("https://localhost:7198/Ticket/CreateTicket?creadopor="+1+"&asignadaa="+asignadoa+"&prioridad="+prioridad+"&estatud="+estatus+"&categoriaid="+categoria, content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                           return RedirectToAction("GestionTickets");
                    }
                    
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    dynamic json  = JsonConvert.DeserializeObject(apiResponse);
                    ViewBag.Result = json.errors;
                    return View();
                }
            }
         
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }

    }
}
