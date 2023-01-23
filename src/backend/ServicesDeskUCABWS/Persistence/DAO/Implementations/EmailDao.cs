using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.DAO.Interface;

namespace ServicesDeskUCABWS.Persistence.DAO.Implementations
{
    public class EmailDao : IEmailDao
    {
        private readonly IConfiguration _configuration;

        public EmailDao(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        // SE REALIZA EL ENVÍO DEL CORREO EN EL SERVICIO DE CORREOS
        public void SendEmail(EmailDTO emailDTO)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(emailDTO.para));
            email.Subject = emailDTO.asunto;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = emailDTO.Cuerpo };
            using var smtp = new SmtpClient();
            smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
            smtp.Connect(_configuration.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration.GetSection("EmailUsername").Value, _configuration.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);

        }
    }
}