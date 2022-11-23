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
        public static Mock<DbSet<Etiqueta>> mockSetEtiquetas = new Mock<DbSet<Etiqueta>>();
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
                    id = 1,
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
                    id = 1,
                    nombre = "probado"
                }
            };
            //ListEtiqueta
            var requestsEtiquetas = new List<Etiqueta>
            {
                new Etiqueta
                {
                    id = 1,
                    nombre = "Prueba",
                    descripcion = "Esta es una prueba"
                }, new Etiqueta
                {
                    id = 2,
                    nombre = "Importante",
                    descripcion = "Esta es una prueba"
                }, new Etiqueta
                {
                    id = 3,
                    nombre = "probado",
                    descripcion = "Esta es una prueba"
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
            //Etiqueta DataSeed
            _mockContext.Setup(t => t.Etiquetas).Returns(mockSetEtiquetas.Object);
            _mockContext.Setup(t => t.DbContext.SaveChanges()).Returns(1);
            _mockContext.Setup(c => c.Etiquetas).Returns(requestsEtiquetas.AsQueryable().BuildMockDbSet().Object);

        }
    }
}