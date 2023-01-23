using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.BussinessLogic.Mapper;
using Microsoft.EntityFrameworkCore;
using ServicesDeskUCABWS.Persistence.Database;
using System;
using ServicesDeskUCABWS.Exceptions;

namespace ServicesDeskUCABWS.Persistence.DAO.Implementations
{
    public class TipoCargoDAO : ITipoCargoDAO
    {
        private readonly IMigrationDbContext _context;

        public TipoCargoDAO(IMigrationDbContext context)
        {
            this._context = context;
        }

        // SE AGREGA UN TIPO  DE CARGO 
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
                throw new ServicesDeskUcabWsException("Error al Crear, detalles: " + ex.Message, ex);
            }
        }

        // SE CONSULTAN LOS TIPOS DE CARGO QUE EXISTEN EN EL SISTEMA
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
                throw new ServicesDeskUcabWsException("Error al Consultar: "+ex.Message, ex);
            }
        }

        // SE ACTUALIZA EL TIPO DE CARGO 
        public TipoCargoDTO ActualizarTipoCargoDAO(TipoCargo tipoCargo)
        {
            try
            {
                _context.TipoCargos.Update(tipoCargo);
                _context.DbContext.SaveChanges();

                return TipoCargoMapper.EntityToDTO(tipoCargo);
            }catch(Exception ex)
            {
                throw new Exception("Fallo al actualizar: " + tipoCargo.nombre + ", " + ex.Message, ex);
            } 
        } 

        // SE ELIMINA EL TIPO DE CARGO MEDIANTE SU ID
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
                    throw new ServicesDeskUcabWsException("Fallo al Eliminar por id: " + id +", "+ex.Message, ex);
            }
        }               
    }
}