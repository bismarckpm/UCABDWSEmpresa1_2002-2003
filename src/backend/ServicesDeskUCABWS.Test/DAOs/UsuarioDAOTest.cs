using Bogus;
using Microsoft.EntityFrameworkCore;
using Moq;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.DAO.Implementations;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Database;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.Test.DataSeed;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ServicesDeskUCABWS.Test.DAOs
{
    public class UsuarioDAOTest
    {

        private readonly UsuarioDAO _dao;
        private readonly Mock<IMigrationDbContext> _contextMock;
        private readonly Mock<IUsuarioDao> _servicesMock;

        public UsuarioDAOTest()
        {
            var faker = new Faker();
            _contextMock = new Mock<IMigrationDbContext>();
            _dao = new UsuarioDAO(_contextMock.Object);
            _servicesMock = new Mock<IUsuarioDao>();
            _contextMock.SetupDbContextData();
        }

        [Fact(DisplayName = "Obtiene listado de Usuarios")]
        public Task GetUserTest()
        {
            var lista = _dao.GetUsuarios();
            Assert.IsType<List<Usuario>>(lista);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Obtiene Usuario por Email")]
        public Task GetUserByEmailTest()
        {
            var email = "prueba@gmail.com";
            var result = _dao.GetUsuario(email);

            Assert.Equal(email, result.email);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Valida usuario existe")]
        public Task UserExistTest()
        {
            var correo = "prueba@gmail.com";
            var pwd = "123";

            var result = _dao.UsuarioExists(correo, pwd);
            Assert.True(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Trim to Upper")]
        public Task GetUserTrimToUpperTest()
        {
            var dto = new RegistroDTO()
            {
                Email = "prueba@gmail.com",
                Password= "prueba",
                confirmationpassword = "prueba"
            };

            var result = _dao.GetUsuarioTrimToUpper(dto);
            Assert.IsType<Cliente>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Cambio de clave validado")]
        public Task ChangePasswordTest()
        {
            var correo = "prueba@gmail.com";
            var newPassword = "prueba";
            var confirmationpassword = "prueba";

            var result = _dao.ChangePassword(correo, newPassword, confirmationpassword);

            Assert.IsType<Cliente>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Cambio de clave Fallido")]
        public Task ChangePasswordTestFail()
        {
            var correo = "prueba@gmail.com";
            var newPassword = "prueba";
            var confirmationpassword = "diferente";

            var result = _dao.ChangePassword(correo, newPassword, confirmationpassword);

            Assert.Null(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Crear Usuario")]
        public Task CreateUserTest()
        {

            var user = new Cliente();
            var cargoid = 1;
            var departamento = 1;

            var result = _dao.CreateUsuario(user, cargoid, departamento);

            Assert.True(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Crea Contraseña hash")]
        public Task CreatePaswordHashTest()
        {
            var user = new Cliente();
            var pwd = "prueba";

            var result = _dao.CreatePasswordHash(user, pwd);

            Assert.IsType<Cliente>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Verifica Contraseña hash")]
        public Task VerifyPaswordHashTest()
        {
            var user = new Cliente();
            var pwd = "prueba";

            var hash = new HMACSHA512();
            user.passwordSalt = hash.Key;
            user.passwordHash = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(pwd));

            var result = _dao.VerifyPasswordHash(pwd, user.passwordHash, user.passwordSalt);

            Assert.True(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Guardar context")]
        public Task SaveTest()
        {

            var result = _dao.Save();

            Assert.True(result);
            return Task.CompletedTask;

        }
    }
}
