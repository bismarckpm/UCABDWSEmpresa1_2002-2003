using Microsoft.EntityFrameworkCore;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Database;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using Bogus;
using Moq;
using EmailDAO = ServicesDeskUCABWS.Persistence.DAO.Implementations.EmailDao;
using Microsoft.Extensions.Logging.Abstractions;
using ServicesDeskUCABWS.Test.Configuraciones;
using Microsoft.Extensions.Configuration;

namespace ServicesDeskUCABWS.Test.DAOs
{
    public class EmailDAOTest
    {

        private readonly EmailDAO _dao;
        private readonly Mock<IEmailDao> _servicesMock;

        private readonly Mock<IConfiguration> _configuration;

        public EmailDAOTest()
        {

            _servicesMock = new Mock<IEmailDao>();
            _configuration = new Mock<IConfiguration>();
            _dao = new EmailDAO(_configuration.Object);
        }

        [Fact(DisplayName = "Enviar un Email")]
        public Task SendEmailDAOTest()
        {
            // arrange
            EmailDTO dto = new EmailDTO() { para = "test@gmail.com", asunto = "test", Cuerpo = "test" };
            _configuration.Setup(x => x.GetSection("EmailUsername").Value).Returns("vito59@ethereal.email");
            _configuration.Setup(x => x.GetSection("EmailHost").Value).Returns("smtp.ethereal.email");
            _configuration.Setup(x => x.GetSection("EmailPassword").Value).Returns("w2nBcus53Wv8ykQb89");
            _servicesMock.Setup(x => x.SendEmail(It.IsAny<EmailDTO>()));

            // act
            _dao.SendEmail(dto);

            // assert
            return Task.CompletedTask;
        }
    }
}