using System.Security.Cryptography;
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

        public Usuario GetUsuario(string username){
            return _context.Usuario.Where(p => p.username == username).FirstOrDefault();
        }

        public bool UsuarioExists(string usuarname, string password){
            return _context.Usuario.Any(p=>p.username == usuarname );
        }

       

        public Usuario ChangePassword(string usuarname, string newpassword, string confirmationpassword){
            if (newpassword == confirmationpassword){
             return _context.Usuario.Where(p=>p.username == usuarname).FirstOrDefault();
            }
            return null;
        }   

        public bool CreateUsuario(Usuario usuario){
        
             _context.Usuario.Add(usuario);
             return Save();
        }
        
      

        public bool Save()
{
    var saved =_context.DbContext.SaveChanges();
    return saved > 0 ? true:false;
}
    } 
}