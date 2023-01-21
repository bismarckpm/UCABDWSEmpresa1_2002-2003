using ServicesDeskUCABWS.Persistence.Database;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.DAO.Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using AutoMapper;
using ServicesDeskUCABWS.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;


namespace ServicesDeskUCABWS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<MigrationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("MyConn")));
            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ServicesDeskUcabWs", Version = "v1" });
            });
            services.AddAutoMapper(typeof(Startup));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddTransient<IMigrationDbContext, MigrationDbContext>();
            services.AddTransient<IUsuarioDao, UsuarioDAO>();
            services.AddScoped<ICargoDAO, CargoDAO>();
            services.AddTransient<IPrioridadDAO, PrioridadDAO>();
            services.AddTransient<ITipoCargoDAO, TipoCargoDAO>();
            services.AddTransient<IEtiquetaDAO, EtiquetaDAO>();
            services.AddTransient<IEstadoDAO, EstadoDAO>();
            services.AddTransient<IPlantillaDAO, PlantillaDAO>();
            services.AddTransient<ICargoDAO, CargoDAO>();
            services.AddScoped<IEmailDao, EmailDao>();
            services.AddScoped<IDepartamentoDAO, DepartamentoDAO>();
            services.AddTransient<ICategoriaDAO, CategoriaDAO>();
            services.AddTransient<IGrupoDAO, GrupoDAO>();
            services.AddTransient<ITicketDao, TicketDao>();
            services.AddTransient<IModeloJerarquicoDAO, ModeloJerarquicoDAO>();
            services.AddTransient<IModeloParaleloDAO, ModeloParaleloDAO>();
           services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(jwt => {
    var key = Encoding.ASCII    .GetBytes(Configuration["JwtConfig:Secret"]);

    jwt.SaveToken = true;
    jwt.TokenValidationParameters = new TokenValidationParameters{
        ValidateIssuerSigningKey= true, // this will validate the 3rd part of the jwt token using the secret that we added in the appsettings and verify we have generated the jwt token
        IssuerSigningKey = new SymmetricSecurityKey(key), // Add the secret key to our Jwt encryption
        ValidateIssuer = false, 
        ValidateAudience = false,
        RequireExpirationTime = false,
        ValidateLifetime = true
    }; 
});
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<MigrationDbContext>();
                context.Database.Migrate();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ServicesDeskUcabWs v1"));
            }
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}