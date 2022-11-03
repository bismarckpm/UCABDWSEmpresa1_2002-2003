using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.BussinessLogic.Mapper;
using Microsoft.EntityFrameworkCore;
using ServicesDeskUCABWS.Persistence.Database;
using System;

namespace ServicesDeskUCABWS.Persistence.DAO.Implementations
{
    public class PrioridadDAO : IPrioridadDAO
    {
        private readonly IMigrationDbContext _context;

        public PrioridadDAO(IMigrationDbContext context)
        {
            this._context = context;
        }

        public PrioridadDTO AgregarPrioridadDAO(Prioridad prioridad)
        {
            try
            {
                _context.Prioridades.Add(prioridad);
                _context.DbContext.SaveChanges();

                var data = _context.Prioridades.Where(a => a.id == prioridad.id)
                .Select(
                        a => new PrioridadDTO
                        {
                            Id = a.id,
                            Nombre = a.nombre
                        }
                    );

                return data.First();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception("Transaccion Fallida", ex);
            }
        }

        public List<PrioridadDTO> ConsultarTodosPrioridadesDAO()
        {
            try
            {
                var data = _context.Prioridades.Select(
                    p => new PrioridadDTO
                    {
                        Id = p.id,
                        Nombre = p.nombre
                    }
                );

                return data.ToList();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex.InnerException!;
            }
        }

        public PrioridadDTO ActualizarPrioridadDAO(Prioridad prioridad)
        {
            try
            {

                _context.Prioridades.Update(prioridad);
                _context.DbContext.SaveChanges();

                return PrioridadMapper.EntityToDto(prioridad);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw new Exception("Transaccion Fallo", ex)!;
            }
        }

        public PrioridadDTO EliminarPrioridadDAO(int id)
        {
            try
            {
                var prioridad = (Prioridad)_context.Prioridades.Where(
                    p => p.id == id).First();
                _context.Prioridades.Remove(prioridad);
                _context.DbContext.SaveChanges();

                return PrioridadMapper.EntityToDto(prioridad);

            }
            catch (Exception ex)
            {
                Console.WriteLine("[Mensaje]: " + ex.Message + " [Seguimiento]: " + ex.StackTrace);
                PrioridadDTO errorDTO = new PrioridadDTO();
                errorDTO.Nombre = "Error al Eliminar Prioridad";
                return errorDTO;
            }
        }
    }
}