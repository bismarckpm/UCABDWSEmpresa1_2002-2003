using Bogus;
using Microsoft.EntityFrameworkCore;
using Moq;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Exceptions;
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
using static Bogus.Person.CardAddress;

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
        public Task ListaUsuariosTest()
        {
            var lista = _dao.GetUsuario();
            Assert.IsType<List<Usuario>>(lista);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Excepcion Obtiene listado de Usuarios")]
        public Task ListaUsuariosExceptionTest()
        {
            _contextMock.Setup(c => c.Usuario).Throws(new Exception());
            Assert.Throws<UsuarioExepcion>(() => _dao!.GetUsuario());
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Obtiene listado de Usuarios por Departamento id!=0")]
        public Task ListaUsuariosPorDepartamentoNoId0Test()
        {
            var lista = _dao.GetUsuariosPorDepartamento(1);
            Assert.IsType<List<UsuarioDTO>>(lista);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Obtiene listado de Usuarios por Departamento id=0")]
        public Task ListaUsuariosPorDepartamentoId0Test()
        {
            var lista = _dao.GetUsuariosPorDepartamento(0);
            Assert.IsType<List<UsuarioDTO>>(lista);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Excepcion Obtiene listado de Usuarios por Departamento")]
        public Task ListaUsuariosPorDepartamentoExcepcionTest()
        {
            _contextMock.Setup(c => c.Usuario).Throws(new Exception());

            Assert.Throws<UsuarioExepcion>(() => _dao!.GetUsuariosPorDepartamento(1));
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Obtiene Usuario por Email")]
        public Task ObtienerUsuarioPorEmailTest()
        {
            var resp = _dao.GetUsuarioPorEmail("prueba@gmail.com");
            Assert.IsType<UsuarioDTO>(resp);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Crear Usuario")]
        public Task CreateUserTest()
        {

            var user = new Cliente();
            var cargoid = 1;
            var departamento = 1;

            var result = _dao.CreateUsuario(user, cargoid, departamento);

            Assert.IsType<string>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Excepcion Crear Usuario")]
        public Task CreateUserExcepcionTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Throws(new DbUpdateConcurrencyException());
            var user = new Cliente();
            var cargoid = 1;
            var departamento = 1;

            Assert.Throws<UsuarioExepcion>(() => _dao!.CreateUsuario(user, cargoid, departamento));
            return Task.CompletedTask;
        }


        [Fact(DisplayName = "Actualizar Usuario")]
        public Task UpdateUserTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Returns(1);
            var user = new Cliente();

            var result = _dao.UpdateU(user);

            Assert.IsType<string>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Excepcion Actualizar Usuario")]
        public Task UpdateUserExcepcionTest()
        {
            _contextMock.Setup(x => x.DbContext.SaveChanges()).Throws(new DbUpdateConcurrencyException());

            Assert.Throws<UsuarioExepcion>(() => _dao!.UpdateU(null!));
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

        [Fact(DisplayName = "Excepcion Crea Contraseña hash")]
        public Task CreatePaswordHashExcepcionTest()
        {
            Assert.Throws<UsuarioExepcion>(() => _dao!.CreatePasswordHash(null!,null!));
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

        [Fact(DisplayName = "Excepcion Verifica Contraseña hash")]
        public Task VerifyPaswordHashExcepcionTest()
        {

            Assert.Throws<UsuarioExepcion>(() => _dao!.VerifyPasswordHash(null!,null!, null!));
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Obtiene tipo Usuario")]
        public Task ObtieneTipoUsuarioTest()
        {
            var resp = _dao.GetTipoUsuario(1);
            Assert.NotNull(resp);
            return Task.CompletedTask;
        }
    }
}
