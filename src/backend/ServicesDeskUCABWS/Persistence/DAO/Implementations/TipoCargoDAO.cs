using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.BussinessLogic.Mapper;
using Microsoft.EntityFrameworkCore;
using ServicesDeskUCABWS.Persistence.Database;
using System;

namespace ServicesDeskUCABWS.Persistence.DAO.Implementations
{
    public class TipoCargoDAO : ITipoCargoDAO
    {
        private readonly IMigrationDbContext _context;

        public TipoCargoDAO(IMigrationDbContext context)
        {
            this._context = context;
        }
        public TipoCargoDTO AgregarTipoCargoDAO(TipoCargo tipo)
        {
            try
            {
                _context.TipoCargos.Add(tipo);
                _context.DbContext.SaveChanges();

                var data = _context.TipoCargos.Where(t => t.id == tipo.id)
                            .Select(t => new TipoCargoDTO
                            {
                                Id = t.id,
                                Nombre = t.nombre
                            });

                return data.First();            

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw new Exception("Error al Crear, detalles:", ex);
            }
        }

        public List<TipoCargoDTO> ConsultarTipoCargoDAO()
        {
            try
            {
                var lista = _context.TipoCargos.Select(
                    t=> new TipoCargoDTO
                    {
                        Id = t.id,
                        Nombre = t.nombre                        
                    }
                );

                return lista.ToList();

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw new Exception("Error al Consultar: "+ex.Message,ex);
            }
        }

        public TipoCargoDTO ActualizarTipoCargoDAO(TipoCargo tipoCargo)
        {
            try
            {
                _context.TipoCargos.Update(tipoCargo);
                _context.DbContext.SaveChanges();

                var data = _context.TipoCargos.Where(t => t.id == tipoCargo.id).Select(
                    t=> new TipoCargoDTO
                    {
                        Id = t.id,
                        Nombre = t.nombre
                    }
                );
                return data.First();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message +" || "+ ex.StackTrace);
                throw new Exception("Fallo al actualizar: " + tipoCargo.nombre, ex);
            }
        } 

        public TipoCargoDTO EliminarTipoCargoDAO(int id)
        {
            try
            {
                    var tCargo = _context.TipoCargos
                    .Where(t => t.id == id).First();
                    
                    _context.TipoCargos.Remove(tCargo);
                    _context.DbContext.SaveChanges();

                    return TipoCargoMapper.EntityToDTO(tCargo);

            }catch(Exception ex)
            {
                    Console.WriteLine(ex.Message +" || "+ ex.StackTrace);
                    throw new Exception("Fallo al Eliminar por id: " + id, ex);
            }
        }               
    }
}