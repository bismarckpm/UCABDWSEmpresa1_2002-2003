using ServicesDeskUCABWS.Persistence.Database;
using ServicesDeskUCABWS.Persistence.Entity;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ServicesDeskUCABWS.Test.DataSeed
{
    public static class DataSeed
    {
        public static Mock<DbSet<TipoCargo>> mockSetTCargo = new Mock<DbSet<TipoCargo>>();
        public static Mock<DbSet<Categoria>> mockSetCategorias = new Mock<DbSet<Categoria>>();
        public static Mock<DbSet<Prioridad>> mockSetPrioridades = new Mock<DbSet<Prioridad>>();
        public static Mock<DbSet<Departamento>> mockSetDepartamentos = new Mock<DbSet<Departamento>>();
        public static Mock<DbSet<Grupo>> mockSetGrupo = new Mock<DbSet<Grupo>>();
        public static Mock<DbSet<Usuario>> mockSetUsuarios = new Mock<DbSet<Usuario>>();
        public static Mock<DbSet<Cargo>> mockSetCargos = new Mock<DbSet<Cargo>>();
        public static Mock<DbSet<Etiqueta>> mockSetEtiquetas = new Mock<DbSet<Etiqueta>>();
        public static Mock<DbSet<Plantilla>> mockSetPlantillas = new Mock<DbSet<Plantilla>>();
        public static Mock<DbSet<ModeloParalelo>> mockSetModeloParalelo = new Mock<DbSet<ModeloParalelo>>();
        public static Mock<DbSet<Ticket>> mockSetTicket = new Mock<DbSet<Ticket>>();
        public static Mock<DbSet<ModeloJerarquico>> mockSetModeloJerarquico = new Mock<DbSet<ModeloJerarquico>>();
        public static Mock<DbSet<FlujoAprobacion>> mockSetFlujoAprobacion = new Mock<DbSet<FlujoAprobacion>>();
        public static Mock<DbSet<Estado>> mockSetEstados = new Mock<DbSet<Estado>>();
        public static Mock<DbSet<TickectsRelacionados>> mockSetRelacionados = new Mock<DbSet<TickectsRelacionados>>();

        public static void SetupDbContextData(this Mock<IMigrationDbContext> _mockContext)
        {
            var hash = new HMACSHA512();
            //ListTipoCargo
            var requests = new List<TipoCargo>
            {
                new TipoCargo
                {
                    id = 1,
                    nombre = "Prueba",
                    Jeraruia = new List<ModeloJerarquicoCargos>()
                }, new TipoCargo
                {
                    id = 2,
                    nombre = "prueba",
                    Jeraruia = new List<ModeloJerarquicoCargos>()
                }, new TipoCargo
                {
                    id = 3,
                    nombre = "Junior",
                    Jeraruia = new List<ModeloJerarquicoCargos>()
                }
            };
            //ListCategoria
            var requestsCategorias = new List<Categoria>
            {
                new Categoria
                {
                    id = 1,
                    nombre = "Prueba",
                    modelosjerruicos = new List<ModeloJerarquico>(),
                    ModeloParalelos = new List<ModeloParalelo>()
                }, new Categoria
                {
                    id = 2,
                    nombre = "pruebacateg",
                    modelosjerruicos = new List<ModeloJerarquico>(),
                    ModeloParalelos = new List<ModeloParalelo>()
                }, new Categoria
                {
                    id = 3,
                    nombre = "probado",
                    modelosjerruicos = new List<ModeloJerarquico>(),
                    ModeloParalelos = new List<ModeloParalelo>()
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
                     Tickets = new List<Ticket>(),
                     Grupos = new List<Grupo>()
                 }, new Departamento
                 {
                     id = 2,
                     nombre = "departamento2",
                     Tickets = new List<Ticket>(),
                     Grupos = new List<Grupo>()
                 }, new Departamento
                 {
                     id = 3,
                     nombre = "departamento3",
                     Tickets = new List<Ticket>(),
                     Grupos = new List<Grupo>()
                 }
             };
            //Grupo
            var requestsGrupo = new List<Grupo>
             {
                 new Grupo
                 {
                     id=1,
                     nombre = "grupo1",
                     departamentoid = 1,
                     departamento = requestsDepartamentos[0],
                     usuarios = new List<Usuario>()
                 },
                 new Grupo
                 {
                     id=2,
                     nombre = "grupo2",
                     departamentoid = 2,
                     departamento = new Departamento(),
                     usuarios = new List<Usuario>()
                 },
                 new Grupo
                 {
                     id=3,
                     nombre = "grupo3",
                     departamentoid = 3,
                     departamento = new Departamento(),
                     usuarios = new List<Usuario>()
                 }
             };
            //usuario
            var requestsusuarios = new List<Usuario>
             {
                 new Cliente
                 {
                     id=1,
                     email="prueba@gmail.com",
                     cargo= new Cargo{ id=1 },
                     ticketsasignados = new List<Ticket>(),
                     ticketscreados= new List<Ticket>(),
                     Grupo = requestsGrupo[0],
                 }
             };
            //empleados
            var requestsempleados = new List<Empleado>
             {
                 new Empleado
                 {
                     id=1,
                     email="prueba@gmail.com",
                     cargo= new Cargo{ id=1 },
                     ticketsasignados = new List<Ticket>(),
                     ticketscreados= new List<Ticket>(),
                 },
                 new Empleado
                 {
                     id=2,
                     email="prueba@gmail.com",
                     cargo= new Cargo{ id=1 },
                     ticketsasignados = new List<Ticket>(),
                     ticketscreados= new List<Ticket>(),
                 }
             };
            //administradores
            var requestsadmins = new List<administrador>
             {
                 new administrador
                 {
                     id=1,
                     email="prueba@gmail.com",
                     cargo= new Cargo{ id=1 },
                     ticketsasignados = new List<Ticket>(),
                     ticketscreados= new List<Ticket>(),
                 }
             };
            //clientes
            var requestsclientes = new List<Cliente>
             {
                 new Cliente
                 {
                     id=1,
                     email="prueba@gmail.com",
                     cargo= new Cargo{ id=1 },
                     ticketsasignados = new List<Ticket>(),
                     ticketscreados= new List<Ticket>(),
                     nombre="Prueba",
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
            //Plantillas
            var requestsPlantillas = new List<Plantilla>
            {
                new Plantilla
                {
                    id = 1,
                    titulo = "Plantilla1",
                    cuerpo = "Cuerpo1",
                    EstadoId = 1,
                    estado = new Estado()
                    //notifications = new List<Notification>()
                },
                new Plantilla
                {
                    id = 2,
                    titulo = "Plantilla2",
                    cuerpo = "Cuerpo2",
                    EstadoId = 2,
                    estado = new Estado()
                    //notifications = new List<Notification>()
                }
            };
            //ModeloParalelo
            var requestsModeloParalelo = new List<ModeloParalelo>
            {
                new ModeloParalelo
                {
                    id = 1,
                    nombre = "Prueba Modelo",
                    categoriaid = 1,
                    categoria = new Categoria()
                    {
                        id = 1,
                        nombre = "Guardado"
                    },
                    cantidaddeaprobacion = 5
                },
                new ModeloParalelo
                {
                    id = 2,
                    nombre = "Prueba Modelo",
                    categoriaid = 3,
                    categoria = new Categoria()
                    {
                        id = 4,
                        nombre = "Rechazado"
                    },
                    cantidaddeaprobacion = 5
                }
            };
            //Estado
            var requestsEstado = new List<Estado>
            {
                new Estado
                {
                    id=1,
                    nombre="Estado1",
                    EtiquetaId=1,
                    etiqueta= new Etiqueta(),
                    plantillas = new List<Plantilla>(),
                    tickets = new List<Ticket>()
                },
                new Estado
                {
                    id=2,
                    nombre="Estado2",
                    EtiquetaId=1,
                    etiqueta= new Etiqueta(),
                    plantillas = new List<Plantilla>(),
                    tickets = new List<Ticket>()
                }
            };
            //Tickets
            var requestsTickets = new List<Ticket>
            {
                new Ticket
                {
                    id = 1,
                    nombre = "nombre",
                    asginadoa = new Empleado(),
                    creadopor = new Empleado(),
                    descripcion = "descripcion",
                    fecha = It.IsAny<DateTime>(),
                    Estado = new Estado(),
                    prioridad = new Prioridad(),
                    categoria = new Categoria(),
                    FlujoAprobacion = new FlujoAprobacion(),
                    TickectsRelacionadosPadre = new List<TickectsRelacionados>(),
                    TickectsRelacionadosHijos = new List<TickectsRelacionados>()
                },
                new Ticket
                {
                    id = 2,
                    nombre = "nombre2",
                    asginadoa = new Empleado(),
                    creadopor = new Empleado(),
                    descripcion = "descripcion2",
                    fecha = It.IsAny<DateTime>(),
                    Estado = new Estado(),
                    prioridad = new Prioridad(),
                    categoria = new Categoria(),
                    FlujoAprobacion = new FlujoAprobacion(),
                    TickectsRelacionadosPadre = new List<TickectsRelacionados>(),
                    TickectsRelacionadosHijos = new List<TickectsRelacionados>()
                }
            };

            //TicketsRelacionados
            var requestsRelacionados = new List<TickectsRelacionados>
            {
                new TickectsRelacionados
                {
                    Ticketid = It.IsAny<int>(),
                    TicketRelacionadoid = It.IsAny<int>()
                }, new TickectsRelacionados
                {
                    Ticketid = It.IsAny<int>(),
                    TicketRelacionadoid = It.IsAny<int>()
                }
            };

            //ModeloJerarquico
            var requestsModeloJerarquico = new List<ModeloJerarquico>
            {
                new ModeloJerarquico
                {
                   id = 1,
                   nombre = "Prueba Modelo",
                   categoriaid = 1,
                   categoria = new Categoria()
                   {
                     id = 1,
                     nombre = "Guardado"
                   },
                   Jeraruia = new List<ModeloJerarquicoCargos>()
                },
                new ModeloJerarquico
                {
                   id = 2,
                   nombre = "Prueba Modelo",
                   categoriaid = 3,
                   categoria = new Categoria()
                   {
                     id = 4,
                     nombre = "Rechazado"
                   },
                   Jeraruia = new List<ModeloJerarquicoCargos>()
                   {
                       new ModeloJerarquicoCargos
                       {
                           Id = It.IsAny<int>(),
                           orden = It.IsAny<int>(),
                           modelojerarquicoid = It.IsAny<int>(),
                           jerarquico = new ModeloJerarquico(),
                           TipoCargoid = It.IsAny<int>(),
                           TipoCargo = new TipoCargo()
                       }

                   }
                }
            };
            // //ModeloJerarquico
            // var requestsFlujoAprobacion = new List<FlujoAprobacion>
            // {
            //     new FlujoAprobacion
            //     {
            //         id = 1,
            //         ticketid = 1,
            //         modelojerarquicoid = 1,
            //         paraleloid = 1,
            //         usuario = It.IsAny<Usuario>(),
            //         secuencia = 1,
            //         status = Status.Pendiente,
            //         modeloJerarquico = new ModeloJerarquico(),
            //         modeloParalelo = new ModeloParalelo(),
            //         ticket = new Ticket()
            //     },
            //     new FlujoAprobacion
            //     {
            //         id = 2,
            //         ticketid = 2,
            //         modelojerarquicoid = 2,
            //         paraleloid = 2,
            //         usuario = It.IsAny<Usuario>(),
            //         secuencia = 1,
            //         status = Status.Aprobado,
            //         modeloJerarquico = new ModeloJerarquico(),
            //         modeloParalelo = new ModeloParalelo(),
            //         ticket = new Ticket()
            //     }
            // };

            //     //TipoCargo DataSeed
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
            // Grupo Dataseed
            _mockContext.Setup(t => t.Grupo).Returns(mockSetGrupo.Object);
            _mockContext.Setup(t => t.DbContext.SaveChanges()).Returns(1);
            _mockContext.Setup(c => c.Grupo).Returns(requestsGrupo.AsQueryable().BuildMockDbSet().Object);
            //Usuario DataSeed
            _mockContext.Setup(T => T.Usuario).Returns(mockSetUsuarios.Object);
            _mockContext.Setup(T => T.DbContext.SaveChanges()).Returns(1);
            _mockContext.Setup(C => C.Usuario).Returns(requestsusuarios.AsQueryable().BuildMockDbSet().Object);
            _mockContext.Setup(C => C.Empleados).Returns(requestsempleados.AsQueryable().BuildMockDbSet().Object);
            _mockContext.Setup(C => C.Administradores).Returns(requestsadmins.AsQueryable().BuildMockDbSet().Object);
            _mockContext.Setup(c => c.clientes).Returns(requestsclientes.AsQueryable().BuildMockDbSet().Object);
            //Cargos DataSeed
            _mockContext.Setup(t => t.Cargos).Returns(mockSetCargos.Object);
            _mockContext.Setup(t => t.DbContext.SaveChanges()).Returns(1);
            _mockContext.Setup(c => c.Cargos).Returns(requestsCargos.AsQueryable().BuildMockDbSet().Object);
            //Etiquetas DataSeed	
            _mockContext.Setup(t => t.Etiquetas).Returns(mockSetEtiquetas.Object);
            _mockContext.Setup(t => t.DbContext.SaveChanges()).Returns(1);
            _mockContext.Setup(c => c.Etiquetas).Returns(requestsEtiquetas.AsQueryable().BuildMockDbSet().Object);
            _mockContext.Setup(e => e.Etiquetas.FindAsync(It.IsAny<int>())).ReturnsAsync((int i) => requestsEtiquetas.Where(x => x.id == i).Single());
            //Plantillas DataSeed
            _mockContext.Setup(t => t.Plantillas).Returns(mockSetPlantillas.Object);
            _mockContext.Setup(t => t.DbContext.SaveChanges()).Returns(1);
            _mockContext.Setup(c => c.Plantillas).Returns(requestsPlantillas.AsQueryable().BuildMockDbSet().Object);
            _mockContext.Setup(p => p.Plantillas.FindAsync(It.IsAny<int>())).ReturnsAsync((int i) => requestsPlantillas.Where(x => x.id == i).Single());
            //ModeloParalelo DataSeed
            _mockContext.Setup(t => t.ModeloParalelos).Returns(mockSetModeloParalelo.Object);
            _mockContext.Setup(t => t.DbContext.SaveChanges()).Returns(1);
            _mockContext.Setup(c => c.ModeloParalelos).Returns(requestsModeloParalelo.AsQueryable().BuildMockDbSet().Object);
            _mockContext.Setup(e => e.ModeloParalelos.Find(It.IsAny<int>())).Returns((int i) => requestsModeloParalelo.Where(x => x.id == i).Single());
            //Estados DataSeed
            _mockContext.Setup(t => t.Estados).Returns(mockSetEstados.Object);
            _mockContext.Setup(t => t.DbContext.SaveChanges()).Returns(1);
            _mockContext.Setup(c => c.Estados).Returns(requestsEstado.AsQueryable().BuildMockDbSet().Object);
            _mockContext.Setup(e => e.Estados.FindAsync(It.IsAny<int>())).ReturnsAsync((int i) => requestsEstado.Where(x => x.id == i).Single());
            //Tickets DataSeed
            _mockContext.Setup(t => t.Tickets).Returns(mockSetTicket.Object);
            _mockContext.Setup(t => t.DbContext.SaveChanges()).Returns(1);
            _mockContext.Setup(c => c.Tickets).Returns(requestsTickets.AsQueryable().BuildMockDbSet().Object);
            _mockContext.Setup(e => e.Tickets.FindAsync(It.IsAny<int>())).ReturnsAsync((int i) => requestsTickets.Where(x => x.id == i).Single());
            //TicketsRelacionados DataSeed
            _mockContext.Setup(t => t.TickectsRelacionados).Returns(mockSetRelacionados.Object);
            _mockContext.Setup(t => t.DbContext.SaveChanges()).Returns(1);
            _mockContext.Setup(c => c.TickectsRelacionados).Returns(requestsRelacionados.AsQueryable().BuildMockDbSet().Object);
            _mockContext.Setup(e => e.TickectsRelacionados.FindAsync(It.IsAny<int>())).ReturnsAsync((int i) => requestsRelacionados.Where(x => x.Ticketid == i).Single());

            //ModeloJerarquico DataSeed
            _mockContext.Setup(t => t.ModeloJerarquicos).Returns(mockSetModeloJerarquico.Object);
            _mockContext.Setup(t => t.DbContext.SaveChanges()).Returns(1);
            _mockContext.Setup(c => c.ModeloJerarquicos).Returns(requestsModeloJerarquico.AsQueryable().BuildMockDbSet().Object);
            _mockContext.Setup(e => e.ModeloJerarquicos.FindAsync(It.IsAny<int>())).ReturnsAsync((int i) => requestsModeloJerarquico.Where(x => x.id == i).Single());

            //     _mockContext.Setup(t => t.FlujoAprobaciones).Returns(mockSetFlujoAprobacion.Object);
            //     _mockContext.Setup(t => t.DbContext.SaveChanges()).Returns(1);
            //     _mockContext.Setup(c => c.FlujoAprobaciones).Returns(requestsFlujoAprobacion.AsQueryable().BuildMockDbSet().Object);
            //     _mockContext.Setup(e => e.FlujoAprobaciones.FindAsync(It.IsAny<int>())).ReturnsAsync((int i) => requestsFlujoAprobacion.Where(x => x.id == i).Single());

            // }
        }
    }
}