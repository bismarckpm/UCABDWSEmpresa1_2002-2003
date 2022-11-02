using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.BussinessLogic.Mapper;
using Microsoft.EntityFrameworkCore;
using ServicesDeskUCABWS.Persistence.Database;
using System;

namespace ServicesDeskUCABWS.Persistence.DAO.Implementations
{
    public class NotificacionDAO : INotificacionDAO
    {
        private readonly IMigrationDbContext _context;

        public NotificacionDAO(IMigrationDbContext context)
        {
            _context = context;
        }

        public NotificacionDTO AgregarNotificacionDAO(Notification ntf)
        {
            try
            {
                _context.Notifications.Add(ntf);
                _context.DbContext.SaveChanges();

                var data = _context.Notifications.Where(n => n.id == ntf.id).Select(
                    n => new NotificacionDTO
                    {
                        Id = n.id,
                        Titulo = n.titulo,
                        Fecha = n.fecha,
                        Descripcion = n.descripcion
                    }
                );
                return data.First();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex.InnerException!;
            }
        }

        public List<NotificacionDTO> ConsultarTodaNotificacionDAO()
        {
            try
            {
                var data = _context.Notifications.Select(
                    n => new NotificacionDTO
                    {
                        Id = n.id,
                        Titulo = n.titulo,
                        Fecha = n.fecha,
                        Descripcion = n.descripcion
                    }
                );

                return data.ToList();

            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex.InnerException!;
            }
        }

        public NotificacionDTO ActualizarNotificacionDAO(Notification ntf)
        {
            try
            {
                _context.Notifications.First<Notification>(n => n.id == ntf.id);
                _context.Notifications.Update(ntf);
                _context.DbContext.SaveChanges();

                NotificacionMapper mapper = new NotificacionMapper();
                return mapper.EntityToDto(ntf);

            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex.InnerException!;
            }
        }
    }
}