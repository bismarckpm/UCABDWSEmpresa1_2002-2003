using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.BussinessLogic.Mapper;
using Microsoft.EntityFrameworkCore;
using ServicesDeskUCABWS.Persistence.Database;
using System;

namespace ServicesDeskUCABWS.Persistence.DAO.Implementations
{
    public class CategoriaDAO : ICategoriaDAO
    {
        private readonly IMigrationDbContext _context;

        public CategoriaDAO(IMigrationDbContext context)
        {
            this._context = context;
        }

        public CategoriaDTO AgregarCategoriaDAO(Categoria categ)
        {
            try
            {
                _context.Categorias.Add(categ);
                _context.DbContext.SaveChanges();

                var data = _context.Categorias.Where(a => a.id == categ.id)
                .Select(
                        a => new CategoriaDTO
                        {
                            Id = a.id,
                            Nombre = a.nombre
                            //FlujoAprobacion = a.flujoaprobacion
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

        public List<CategoriaDTO> ConsultarTodosCategoriasDAO()
        {
            try
            {
                var data = _context.Categorias.Select(
                    p => new CategoriaDTO
                    {
                        Id = p.id,
                        Nombre = p.nombre
                        //FlujoAprobacion = a.flujoaprobacion
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

        public CategoriaDTO ActualizarCategoriaDAO(Categoria categ)
        {
            try
            {
                _context.Categorias.Update(categ);
                _context.DbContext.SaveChanges();

                return CategoriaMapper.EntityToDto(categ);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw new Exception("Transaccion Fallo", ex)!;
            }
        }

        public CategoriaDTO EliminarCategoriaDAO(int id)
        {
            try
            {
                var categoria = (Categoria)_context.Categorias.Where(
                    p => p.id == id).First();
                _context.Categorias.Remove(categoria);
                _context.DbContext.SaveChanges();

                return CategoriaMapper.EntityToDto(categoria);

            }
            catch (Exception ex)
            {
                Console.WriteLine("[Mensaje]: " + ex.Message + " [Seguimiento]: " + ex.StackTrace);
                throw new Exception("Transaccion Fallo", ex)!;
            }
        }

        public CategoriaDTO ConsultaCategoriaDAO(int id)
        {
            try
            {
                var categoria = _context.Categorias.Where(
                p => p.id == id).First();
                return CategoriaMapper.EntityToDto(categoria); ;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex.InnerException!;
            }
        }
    }
}