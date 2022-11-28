using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCAB.DTO;
using ServicesDeskUCAB.Models;
using System.Text.Json;

namespace ServicesDeskUCAB.Controllers
{
    public class TicketController : Controller
    {
        public async Task<IActionResult> GestionTickets()
        {
            try
            {
                List<TicketCDTO> listaTickets = new List<TicketCDTO>();
                HttpClient client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7198/Tickets");
                var _client = await client.SendAsync(request);
                if (_client.IsSuccessStatusCode)
                {
                    var responseStream = await _client.Content.ReadAsStreamAsync();
                    listaTickets = await JsonSerializer.DeserializeAsync<List<TicketCDTO>>(responseStream);
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
                AgrupacionTKDtos agrupacionDTOS = new AgrupacionTKDtos();
                agrupacionDTOS.listaPrioridades= new List<PrioridadDTO>();
                HttpClient client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7198/Prioridad/ConsultaPrioridades");
                var _client = await client.SendAsync(request);
                if (_client.IsSuccessStatusCode)
                {
                    var responseStream = await _client.Content.ReadAsStreamAsync();
                    agrupacionDTOS.listaPrioridades = await JsonSerializer.DeserializeAsync<List<PrioridadDTO>>(responseStream);
                }
                return View(agrupacionDTOS);
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }

        public async Task<IActionResult> AgregarTicket(AgrupacionTKDtos infoTK)
        {
            try
            {               
                infoTK.tk.fecha= DateTime.Now;
                //int userId = Int32.Parse(HttpContext.Session.GetString("userid"));
                //var userId = HttpContext.Session.GetString("userid");
                infoTK.creadopor = 1;
                HttpClient client = new HttpClient();
                var _client = await client.PostAsJsonAsync<TicketDTO>("https://localhost:7198/Ticket/CreateTicket?creadopor="+infoTK.creadopor+"&asignadaa="+infoTK.asginadoa+"&prioridad="+infoTK.prioridad+"&estatud="+infoTK.estado+"&categoriaid="+infoTK.categoria, infoTK.tk);
                return RedirectToAction("GestionTickets");
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }

    }
}
