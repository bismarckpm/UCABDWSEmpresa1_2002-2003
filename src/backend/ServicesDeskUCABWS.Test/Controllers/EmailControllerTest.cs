
using Microsoft.AspNetCore.Mvc;
using Moq;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Controllers;
using ServicesDeskUCABWS.Persistence.DAO.Interface;


namespace ServicesDeskUCABWS.Test.Controllers
{
    public class EmailControllerTest
    {

        private readonly EmailController _controller;
        private readonly Mock<IEmailDao> _servicesMock;

        public EmailControllerTest()
        {

            _servicesMock = new Mock<IEmailDao>();
            _controller = new EmailController(_servicesMock.Object);
        }

        [Fact(DisplayName = "Enviar un Email")]
        public void SendEmailControllerTest()
        {
            // arrange
            EmailDTO dto = new EmailDTO();
            IActionResult expected = new OkResult();

            _servicesMock.Setup(x => x.SendEmail(It.IsAny<EmailDTO>()));

            // act
            IActionResult actual = _controller.SendEmail(dto);

            // assert
            Assert.Equal(expected.GetType(), actual.GetType());
        }
    }
}