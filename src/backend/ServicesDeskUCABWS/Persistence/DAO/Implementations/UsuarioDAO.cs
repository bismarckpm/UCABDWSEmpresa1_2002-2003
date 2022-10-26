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
        public UsuarioDAO(MigrationDbContext context ){
            _context = context;

        }
         public  ICollection<Usuario> GetUsuarios()
        {
         return _context.Usuario.OrderBy(p=>p.id).ToList();
         }



    }
}