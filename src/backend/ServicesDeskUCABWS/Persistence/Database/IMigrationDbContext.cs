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
        DbSet<Notification> Notifications
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
    }
}