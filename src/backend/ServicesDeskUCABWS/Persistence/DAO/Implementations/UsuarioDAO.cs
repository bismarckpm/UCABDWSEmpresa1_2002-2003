using System.Security.Cryptography;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.BussinessLogic.Mapper;
using Microsoft.EntityFrameworkCore;
using ServicesDeskUCABWS.Persistence.Database;
using System;
using System.Text;

namespace ServicesDeskUCABWS.Persistence.DAO.Implementations
{
    
    public class UsuarioDAO : IUsuarioDao
    {
        private readonly IMigrationDbContext _context;
        public UsuarioDAO(IMigrationDbContext context ){
            this._context = context;

        }
        public ICollection<Usuario> GetUsuarios()
        {
            return _context.Usuario.OrderBy(p => p.id).ToList();
        }


        public Usuario GetUsuarioTrimToUpper(RegistroDTO administratorDTO)
        {
            return GetUsuarios().Where(c => c.email.Trim().ToUpper() == administratorDTO.Email.TrimEnd().ToUpper())
                .FirstOrDefault();
        }


        public Usuario ChangePassword(string email, string newpassword, string confirmationpassword){
            if (newpassword == confirmationpassword){
             return _context.Usuario.Where(p=>p.email == email).FirstOrDefault()!;
            }
            return null!;
        }   

        public bool CreateUsuario(Usuario usuario, 
            int cargoid, int Departamentoid){
            usuario.cargo = 
                _context.Cargos.Where(c => c.id == cargoid).FirstOrDefault();
            usuario.Departamento = _context.Departamentos.Where(c => c.id == Departamentoid).FirstOrDefault();
             _context.Usuario.Add(usuario);
             return Save();
        }

        public Usuario CreatePasswordHash(Usuario usuario,string clave )
        {
            using (var hash = new HMACSHA512())
            {
                usuario.passwordSalt = hash.Key;
                usuario.passwordHash = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(clave));
                return usuario;
            }
        }
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hash = new HMACSHA512(passwordSalt))
            {
                var ComputedHash = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return ComputedHash.SequenceEqual(passwordHash);
            }
        }

        public bool Save()
        {
            var saved = _context.DbContext.SaveChanges();
            return saved > 0 ? true : false;
        }


    } 
}