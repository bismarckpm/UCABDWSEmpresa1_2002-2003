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
using AutoMapper;

namespace ServicesDeskUCABWS
{
      public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration {get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<MigrationDbContext>(options => 
            options.UseSqlServer(Configuration["ConnectionString"]));    
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo{ Title="ServicesDeskUcabWs", Version= "v1"});
            });  
                services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
                services.AddTransient<IMigrationDbContext, MigrationDbContext>();
                services.AddScoped<IUsuarioDao,UsuarioDAO>();
                services.AddScoped<ICargoDAO,CargoDao>();
                services.AddTransient<IPrioridadDAO,PrioridadDAO>();
                services.AddTransient<ITipoCargoDAO,TipoCargoDAO>();
                services.AddTransient<IDepartamentoDAO, DepartamentoDAO>();
                services.AddTransient<ICategoriaDAO, CategoriaDAO>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using(var scope = app.ApplicationServices.CreateScope())
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });   
        }
    }  
}