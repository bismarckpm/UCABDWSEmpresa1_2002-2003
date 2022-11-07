using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.BussinessLogic.Mapper;
using Microsoft.EntityFrameworkCore;
using ServicesDeskUCABWS.Persistence.Database;
using System;
using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace ServicesDeskUCABWS.Persistence.DAO.Implementations
{
    public class GrupoDAO : IGrupoDAO
    {
        private readonly IMigrationDbContext _dbContext;

        public GrupoDAO(IMigrationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public GrupoDTO AgregarGrupo(Grupo Grupo)
        {
            try
            {
                _dbContext.Grupos.Add(Grupo);
                _dbContext.DbContext.SaveChanges();

                var data = _dbContext.Grupos.Where(a => a.id == Grupo.id)
                    .Select(a => new GrupoDTO
                    {
                        id = a.id,
                        nombre = a.nombre,
                    });
                return data.First();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message + "ex start trace ===" + ex.StackTrace);
                throw ex.InnerException!;
            }
                    
        }
        public GrupoDTO ActualizarGrupo(Grupo Grupo)
        {
            try
            {
                _dbContext.Grupos.Update(Grupo);
                _dbContext.DbContext.SaveChanges();

                var data = _dbContext.Grupos.Where(a => a.id == Grupo.id)
                    .Select(t => new GrupoDTO
                    {
                        nombre = t.nombre,
                        id = t.id,
                    });
                return data.First();
            
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Error al actualizar: " + Grupo.nombre, ex);
            }
        }
        public GrupoDTO EliminarGrupo(int id)
        {
            try
            {
                var data = _dbContext.Grupos.Where(a => a.id == id).First();

                _dbContext.Grupos.Remove(data);
                _dbContext.DbContext.SaveChanges();

                return GrupoMapper.EntityToDto(data);
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Error al eliminar por id: " + id, ex);
            }
                
        }
        public List<GrupoDTO> ConsultarGrupo() 
            {
                try
                {
                    var lista = _dbContext.Grupos
                        .Select(a => new GrupoDTO
                        {
                            id = a.id,
                            nombre = a.nombre,
                        });
                    return lista.ToList();

                }catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw new Exception("error al consultar los grupos");
                                    }
            }

        }
    }
       

