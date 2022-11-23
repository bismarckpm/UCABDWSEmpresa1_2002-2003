using ServicesDeskUCABWS.Persistence.Database;
using ServicesDeskUCABWS.Persistence.Entity;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace ServicesDeskUCABWS.Test.DataSeed
{
    public static class DataSeed
    {
        public static Mock<DbSet<TipoCargo>> mockSetTCargo = new Mock<DbSet<TipoCargo>>();
        public static Mock<DbSet<Categoria>> mockSetCategorias = new Mock<DbSet<Categoria>>();
        public static Mock<DbSet<Prioridad>> mockSetPrioridades = new Mock<DbSet<Prioridad>>();
        public static Mock<DbSet<Departamento>> mockSetDepartamentos = new Mock<DbSet<Departamento>>();
        public static void SetupDbContextData(this Mock<IMigrationDbContext> _mockContext)
        {
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
                    nombre = "departamento1"
                }, new Departamento
                {
                    id = 2,
                    nombre = "departamento2"
                }, new Departamento
                {
                    id = 3,
                    nombre = "departamento3"
                }
            };
            //TipoCargo DataSeed
            _mockContext.Setup(t => t.TipoCargos).Returns(mockSetTCargo.Object);
            _mockContext.Setup(t=>t.DbContext.SaveChanges()).Returns(1);
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
        }
    }
}