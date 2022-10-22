using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ServicesDeskUCABWS.Persistence.Database;

namespace ServicesDeskUCABWS.Persistence
{
    public class DesignTimeDBContextFactory : IDesignTimeDbContextFactory<MigrationDbContext>
    {
        public MigrationDbContext CreateDbContext(string[]? agrs)
        {
            var builder = new DbContextOptionsBuilder<MigrationDbContext>();
            var builder0 = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
             
            IConfiguration _config = builder0.Build();

            Appsettings config = new Appsettings(_config);
            string connectionString = config.DbConnectionString();
            builder.UseSqlServer(connectionString);
            return new MigrationDbContext(builder.Options);
        }
    }
}