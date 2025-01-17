using Microsoft.EntityFrameworkCore;
using ServicesDeskUCABWS.Persistence.Entity;

namespace ServicesDeskUCABWS.Persistence.Database
{
    public interface IMigrationDbContext
    {
        DbContext DbContext
        {
            get;
        }

        DbSet<Usuario> Usuario
        {
            get; set;
        }
        DbSet<Prioridad> Prioridades
        {
            get; set;
        }
        DbSet<TipoCargo> TipoCargos
        {
            get; set;
        }
        DbSet<Ticket> Tickets
        {
            get; set;
        }
        DbSet<Estado> Estados
        {
            get; set;
        }

        DbSet<Plantilla> Plantillas
        {
            get; set;
        }

        DbSet<Etiqueta> Etiquetas
        {
            get; set;
        }
        DbSet<administrador> Administradores
        {
            get; set;
        }
        DbSet<Empleado> Empleados
        {
            get; set;
        }
        DbSet<Cliente> clientes
        {
            get; set;
        }
        DbSet<Cargo> Cargos
        {
            get; set;
        }
        DbSet<Departamento> Departamentos
        {
            get; set;
        }
        DbSet<Categoria> Categorias
        {
            get; set;
        }
        DbSet<Grupo> Grupo
        {
            get;set;
        }

        DbSet<ModeloJerarquico> ModeloJerarquicos
        {
            get; set;
        }
    
        DbSet<ModeloParalelo> ModeloParalelos
        {
            get; set;
        }
        DbSet<FlujoAprobacion> FlujoAprobaciones
        {
            get; set;
        }
         public  DbSet<TickectsRelacionados> TickectsRelacionados
        {
            get; set;
        }
        public DbSet<ModeloAprobacion> ModeloAprobacion
        {
            get; set;
        }
        public DbSet<ModeloJerarquicoCargos> ModeloJerarquicoCargos
        {
            get; set;
        }

    }
}