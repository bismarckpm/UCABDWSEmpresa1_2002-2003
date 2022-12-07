using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.BussinessLogic.Mapper;
using Microsoft.EntityFrameworkCore;
using ServicesDeskUCABWS.Persistence.Database;
using System;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;


namespace ServicesDeskUCABWS.Persistence.DAO.Implementations
{
    public class GrupoDAO : IGrupoDAO
    {
        private readonly IMigrationDbContext _dbContext;

        public GrupoDAO(IMigrationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public GrupoDTO AgregarGrupoDAO (Grupo grupo)
        {
            try
            {
                _dbContext.Grupo.Add(grupo);
                _dbContext.DbContext.SaveChanges();

                var variable = GrupoMapper.EntityToDto(grupo);

                //var data = _dbContext.Grupo.Where(a => a.id == grupo.id)
                //  .Select(a => new GrupoDTO
                //  {
                //      id = a.id,
                //      nombre = a.nombre,
                //      departamentoid = a.departamentoid,

                //  });

                return variable;

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message + "ex start trace ===" + ex.StackTrace);
                throw ex.InnerException!;
            }
        }
        public GrupoDTO ActualizarGrupoDAO (Grupo grupo)
        {
            try
            {
                _dbContext.Grupo.Update(grupo);
                _dbContext.DbContext.SaveChanges();

                var data = _dbContext.Grupo.Where(a => a.id == grupo.id)
                    .Select(a => new GrupoDTO
                    {
                        nombre = a.nombre,
                        id = a.id
                        
                    });
                return data.First();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Error al actualizar: " + grupo.nombre, ex);
            }
        }
        public GrupoDTO EliminarGrupoDAO(int id)
        {                 
            try
            {
                var data = _dbContext.Grupo.Where(a => a.id == id).FirstOrDefault();                                  
                _dbContext.Grupo.Remove(data);
                _dbContext.DbContext.SaveChanges();

                return GrupoMapper.EnityToDto(data);

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Error al Eliminar por id: " + id, ex);
            }

        }
        public List<GrupoDTO> ConsultarGrupoDAO()
        {
            try
            {
                var lista = _dbContext.Grupo.Select(a => new GrupoDTO
                {
                    id = a.id,
                    nombre = a.nombre
                   
                });
                return lista.ToList();

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Error al consultar los Grupos");
            }
        }

    }
}
