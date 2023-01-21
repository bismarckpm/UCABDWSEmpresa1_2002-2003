using System.Security.Cryptography;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.BussinessLogic.Mapper;
using Microsoft.EntityFrameworkCore;
using ServicesDeskUCABWS.Persistence.Database;
using System;
using System.Text;
using ServicesDeskUCABWS.Exceptions;

namespace ServicesDeskUCABWS.Persistence.DAO.Implementations
{
    
    public class UsuarioDAO : IUsuarioDao
    {
        private readonly IMigrationDbContext _context;
        public UsuarioDAO(IMigrationDbContext context ){
            this._context = context;

        }
         public ICollection<Usuario> GetUsuario(){
            try
            {
               return _context.Usuario.OrderBy(c=>c.id).ToList();
            }
            catch (Exception ex)
            {
                throw new UsuarioExepcion("Ha ocurrido un error al buscar el usuario ", ex.Message, ex);
            }
        }

        
    

         public ICollection<UsuarioDTO> GetUsuariosPorDepartamento(int departamentoid){
             try
            {
            ;
            if (departamentoid==0){
                var q = (from usua in _context.Usuario
                     join dep in _context.Departamentos on usua.Grupo.departamento equals dep
                     select new UsuarioDTO()
                     {
                        id = usua.id,
                        Email = usua.email,
                        iddept = dep.id,
                        dept = dep.nombre
                      }).ToList();
                      return q;
            }else{
             var q = (from usua in _context.Usuario
                     join dep in _context.Departamentos on usua.Grupo.departamento equals dep
                     where dep.id == departamentoid
                     select new UsuarioDTO()
                     {
                        id = usua.id,
                        Email = usua.email
                      }).ToList();
                       return q;
           
            }
            }
            catch (Exception ex)
            {
                throw new UsuarioExepcion("Ha ocurrido un error al buscar usuario ", ex.Message, ex);
            }
        }

        public Usuario GetTipoUsuario(int id)
        {
            var usuario = _context.Usuario.Where(u => u.id == id).First();
            return usuario;
        }

        public string CreateUsuario(Usuario usuario, int cargoid, int Grupoid){
            try
            {
            usuario.cargo = _context.Cargos.Where(c => c.id == cargoid).FirstOrDefault();
            usuario.Grupo = _context.Grupo.Where(c => c.id == Grupoid).FirstOrDefault();
             _context.Usuario.Add(usuario);
             _context.DbContext.SaveChanges();
            return "Usuario Creado";
            }
            catch (Exception ex)
            {
                throw new UsuarioExepcion("Ha ocurrido un error al crear el usuario ", ex.Message, ex);
            }
        }
        public string UpdateU(Usuario usuario){
            try
            {
             _context.Usuario.Update(usuario);
             _context.DbContext.SaveChanges();
            return "Usuario Actualizado";
            }
            catch (Exception ex)
            {
                throw new UsuarioExepcion("Ha ocurrido un error al Actualizar el usuario ", ex.Message, ex);
            }
        }

        public Usuario CreatePasswordHash(Usuario usuario,string clave )
        {
             try{
            using (var hash = new HMACSHA512())
            {
                usuario.passwordSalt = hash.Key;
                usuario.passwordHash = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(clave));
                return usuario;
            }
              }
             catch (Exception ex)
            {
                throw new UsuarioExepcion("Ha ocurrido un error al crear el contrasena ", ex.Message, ex);
            }
        }
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            try{

            
            using (var hash = new HMACSHA512(passwordSalt))
            {
                var ComputedHash = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return ComputedHash.SequenceEqual(passwordHash);
            }
            }
             catch (Exception ex)
            {
                throw new UsuarioExepcion("Ha ocurrido un error al verificar contrasena ", ex.Message, ex);
            }
        }

        public UsuarioDTO GetUsuarioPorEmail(string email)
        {
            var usuario = _context.Usuario.Where(u => u.email == email)
                .Select(a => new UsuarioDTO
                        {
                            id = a.id,
                            Email = a.email,
                            Discriminator = a.Discriminator
                        }
                    );;
            return usuario.First();
        }
    } 
}