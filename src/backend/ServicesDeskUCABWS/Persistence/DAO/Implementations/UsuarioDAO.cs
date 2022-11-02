using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.BussinessLogic.Mapper;
using Microsoft.EntityFrameworkCore;
using ServicesDeskUCABWS.Persistence.Database;
using System;

namespace ServicesDeskUCABWS.Persistence.DAO.Implementations
{
    
    public class UsuarioDAO : IUsuarioDao
    {
        private readonly IMigrationDbContext _context;
        public UsuarioDAO(IMigrationDbContext context)
        {
                _context = context;
        }

        public UsuarioDTO AgregarUsuario(Usuario user)
        {
            try
            {
                     _context.Usuario.Add(user);
                      _context.DbContext.SaveChanges();          
                    UsuarioMapper map = new UsuarioMapper();


                return map.EntityToDto(user); 
            }
            catch (Exception ex)
            { 
                throw new Exception("Transaccion fallo",ex);
            }
        } 
  
    }
}