using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.BussinessLogic.DTO;

namespace ServicesDeskUCABWS.Controllers
{
    public class EmailController : Controller

    {
        private readonly IEmailDao _emailrepository;

        public EmailController(IEmailDao emailrepository)
        {
            this._emailrepository = emailrepository;
        }
         [HttpPost("Email")]
        public IActionResult SendEmail([FromBody] EmailDTO emailDTO)
        {
            
         _emailrepository.SendEmail(emailDTO);

            return Ok();
            
           
        }

   
    }
}