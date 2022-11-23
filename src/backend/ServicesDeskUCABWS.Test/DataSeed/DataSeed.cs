using ServicesDeskUCABWS.Persistence.Database;
using ServicesDeskUCABWS.Persistence.Entity;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using System.Security.Cryptography;

namespace ServicesDeskUCABWS.Test.DataSeed
{
    public static class DataSeed
    {
        public static Mock<DbSet<TipoCargo>> mockSetTCargo = new Mock<DbSet<TipoCargo>>();
        public static Mock<DbSet<Categoria>> mockSetCategorias = new Mock<DbSet<Categoria>>();
        public static Mock<DbSet<Prioridad>> mockSetPrioridades = new Mock<DbSet<Prioridad>>();
        public static Mock<DbSet<Departamento>> mockSetDepartamentos = new Mock<DbSet<Departamento>>();
        public static Mock<DbSet<Usuario>> mockSetUsuarios = new Mock<DbSet<Usuario>>();
        public static Mock<DbSet<Cargo>> mockSetCargos = new Mock<DbSet<Cargo>>();
        public static Mock<DbSet<Etiqueta>> mockSetEtiquetas = new Mock<DbSet<Etiqueta>>();
        public static void SetupDbContextData(this Mock<IMigrationDbContext> _mockContext)
        {
            var hash = new HMACSHA512();
            //ListTipoCargo
            var requests = new List<TipoCargo>
            {
                new TipoCargo
                {
                    id = 1,
                    nombre = "Prueba"
                }, new TipoCargo
                {
                    id = 2,
                    nombre = "prueba"
                }, new TipoCargo
                {
                    id = 3,
                    nombre = "Junior"
                }
            };
            //ListCategoria
            var requestsCategorias = new List<Categoria>
            {
                new Categoria
                {
                    id = 1,
                    nombre = "Prueba"
                }, new Categoria
                {
                    id = 2,
                    nombre = "pruebacateg"
                }, new Categoria
                {
                    id = 3,
                    nombre = "probado"
                }
            };
            //Prioridad
            var requestsPrioridades = new List<Prioridad>
            {
                new Prioridad
                {
                    id = 1,
                    nombre = "Alta"
                }, new Prioridad
                {
                    id = 2,
                    nombre = "Media"
                }, new Prioridad
                {
                    id = 3,
                    nombre = "Baja"
                }
            };
            //Departamento
            var requestsDepartamentos = new List<Departamento>
            {
                new Departamento
                {
                    id = 1,
                    nombre = "departamento1",
                    grupos = new List<Grupo>(),
                    Usuarios = new List<Usuario>()
                }, new Departamento
                {
                    id = 2,
                    nombre = "departamento2",
                    grupos = new List<Grupo>(),
                    Usuarios = new List<Usuario>()
                }, new Departamento
                {
                    id = 3,
                    nombre = "departamento3",
                    grupos = new List<Grupo>(),
                    Usuarios = new List<Usuario>()
                }
            };
            //Usuario
            var requestsUsuarios = new List<Usuario>
            {
                new Cliente
                {
                    id=1,
                    email="prueba@gmail.com",
                    cargo= new Cargo{ id=1 },
                    Departamento = new Departamento { id=1 },
                    VerificationToken = "prueba",
                    VerifiedAt = new DateTime(),
                    PasswordResetToken=Guid.NewGuid().ToString(),
                    ResetTokenExpires= new DateTime(),
                    ticketsasignados = new List<Ticket>(),
                    ticketscreados= new List<Ticket>(),
                    Flujo = new List<FlujoAprobacion>()
                }
            };
            //Cargos
            var requestsCargos = new List<Cargo>
            {
                new Cargo
                {
                    id=1,
                    nombre="CEO",
                    tipoCargoId = 1,
                    tipoCargo = new TipoCargo(),
                    Usuarios = new List<Usuario>()
                },
                new Cargo
                {
                    id=2,
                    nombre="Gerente",
                    tipoCargoId = 2,
                    tipoCargo = new TipoCargo(),
                    Usuarios = new List<Usuario>()
                }
            };
            //Etiquetas
            var requestsEtiquetas = new List<Etiqueta>
            {
                new Etiqueta
                {
                    id=1,
                    nombre="Etiqueta1",
                    descripcion="Descripcion1",
                    estados = new List<Estado>()
                },
                new Etiqueta
                {
                    id=2,
                    nombre="Etiqueta2",
                    descripcion = "Descripcion2",
                    estados = new List<Estado>()
                }
            };
            //TipoCargo DataSeed
            _mockContext.Setup(t => t.TipoCargos).Returns(mockSetTCargo.Object);
            _mockContext.Setup(t => t.DbContext.SaveChanges()).Returns(1);
            _mockContext.Setup(c => c.TipoCargos).Returns(requests.AsQueryable().BuildMockDbSet().Object);
            //Categoria DataSeed
            _mockContext.Setup(t => t.Categorias).Returns(mockSetCategorias.Object);
            _mockContext.Setup(t => t.DbContext.SaveChanges()).Returns(1);
            _mockContext.Setup(c => c.Categorias).Returns(requestsCategorias.AsQueryable().BuildMockDbSet().Object);
            //Prioridad DataSeed
            _mockContext.Setup(t => t.Prioridades).Returns(mockSetPrioridades.Object);
            _mockContext.Setup(t => t.DbContext.SaveChanges()).Returns(1);
            _mockContext.Setup(c => c.Prioridades).Returns(requestsPrioridades.AsQueryable().BuildMockDbSet().Object);
            //Departamento DataSeed
            _mockContext.Setup(t => t.Departamentos).Returns(mockSetDepartamentos.Object);
            _mockContext.Setup(t => t.DbContext.SaveChanges()).Returns(1);
            _mockContext.Setup(c => c.Departamentos).Returns(requestsDepartamentos.AsQueryable().BuildMockDbSet().Object);
            //Usuario DataSeed
            _mockContext.Setup(t => t.Usuario).Returns(mockSetUsuarios.Object);
            _mockContext.Setup(t => t.DbContext.SaveChanges()).Returns(1);
            _mockContext.Setup(c => c.Usuario).Returns(requestsUsuarios.AsQueryable().BuildMockDbSet().Object);
            //Cargos DataSeed
            _mockContext.Setup(t => t.Cargos).Returns(mockSetCargos.Object);
            _mockContext.Setup(t => t.DbContext.SaveChanges()).Returns(1);
            _mockContext.Setup(c => c.Cargos).Returns(requestsCargos.AsQueryable().BuildMockDbSet().Object);
            //Etiquetas DataSeed	
            _mockContext.Setup(t => t.Etiquetas).Returns(mockSetEtiquetas.Object);
            _mockContext.Setup(t => t.DbContext.SaveChanges()).Returns(1);
            _mockContext.Setup(c => c.Etiquetas).Returns(requestsEtiquetas.AsQueryable().BuildMockDbSet().Object);
            _mockContext.Setup(e => e.Etiquetas.FindAsync(It.IsAny<int>())).ReturnsAsync((int i) => requestsEtiquetas.Where(x => x.id == i).Single());
        }
    }
}