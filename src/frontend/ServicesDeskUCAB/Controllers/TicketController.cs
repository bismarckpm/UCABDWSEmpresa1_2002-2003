using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCAB.DTO;
using System.Text.Json;

namespace ServicesDeskUCAB.Controllers
{
    public class TicketController : Controller
    {
        public async Task<IActionResult> GestionTickets()
        {
            try
            {
                List<TicketDTO> listaTickets = new List<TicketDTO>();
                HttpClient client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7198/Ticket");
                var _client = await client.SendAsync(request);
                if (_client.IsSuccessStatusCode)
                {
                    var responseStream = await _client.Content.ReadAsStreamAsync();
                    listaTickets = await JsonSerializer.DeserializeAsync<List<TicketDTO>>(responseStream);
                }
                return View(listaTickets);
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }

        public IActionResult VentanaAgregarTicket()
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

        public async Task<IActionResult> AgregarTicket(TicketDTO ticket)
        {
            try
            {
                HttpClient client = new HttpClient();
                var _client = await client.PostAsJsonAsync<TicketDTO>("https://localhost:7198/Ticket/CreateTicket", ticket);
                return RedirectToAction("GestionTickets");
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }

    }
}
