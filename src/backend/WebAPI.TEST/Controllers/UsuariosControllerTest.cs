using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Controllers;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WebAPI.TEST.Controllers
{
    public class UsuariosControllerTest
    {
        private readonly IUsuarioDao _usuarioRepository;
        private readonly ICargoDAO _cargorepository;
        private readonly IMapper _mapper;
        private readonly IEmailDao email;
        public UsuariosControllerTest() 
        {
            _usuarioRepository = A.Fake<IUsuarioDao>();
            _cargorepository = A.Fake<ICargoDAO>();
            _mapper = A.Fake<IMapper>();
            email = A.Fake<IEmailDao>();
        }

        [Fact]
        public void PokemonController_GetUsuario() 
        {
            //Arranges
            var usuarios = A.Fake<ICollection<UsuarioDTO>>();
            var usuariosList= A.Fake<List<UsuarioDTO>>();
            A.CallTo(() => _mapper.Map<List<UsuarioDTO>>(usuarios)).Returns(usuariosList);
            var controller = new UsuarioController(_usuarioRepository, _cargorepository, _mapper, email);


            //Act

            var resutl = controller.GetCollection();


            //Assert 
            resutl.Should().NotBeNull();
            resutl.Should().BeOfType(typeof(OkObjectResult));

        }
    
       



    }
}
