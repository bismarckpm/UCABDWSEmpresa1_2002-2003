using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.BussinessLogic.Mapper;
using ServicesDeskUCABWS.Persistence.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Collections.Generic;


namespace ServicesDeskUCABWS.Persistence.DAO.Implementations
{
    public class ModeloJerarquicoDAO : IModeloJerarquicoDAO
    {
        private readonly IMigrationDbContext _context;

        public ModeloJerarquicoDAO( IMigrationDbContext context)
        {
            _context = context;
        }



        public async Task<ActionResult> AgregarModeloJerarquicoDAO(ModeloJerarquico modeloJerarquico)
        {
            try
            {
                // Validar categoria 
                var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.id == modeloJerarquico.CategoriaId);
                if (categoria == null)
                {
                    return new NotFoundResult();
                }
                modeloJerarquico.categoria = categoria;
                // Validar orden
                if (modeloJerarquico.orden == null)
                {
                    return new BadRequestResult();
                }
                var listCargos = new List<TipoCargo>();
                // modeloJerarquico.orden.ForEach(async o =>
                // {
                //     var tipoCargo = await _context.TipoCargos.FirstOrDefaultAsync(tc => tc.id == o.id);
                //     if (tipoCargo == null)
                //     {
                //         throw new Exception("Tipo de cargo no encontrado");
                //     }
                //     listCargos.Add(tipoCargo);
                // });
                foreach (var cargo in modeloJerarquico.orden)
                {
                    var tipoCargo = await _context.TipoCargos.FirstOrDefaultAsync(tc => tc.id == cargo.id);
                    if (tipoCargo == null)
                    {
                        return new NotFoundResult();
                    }
                    listCargos.Add(tipoCargo);
                }
                modeloJerarquico.orden = listCargos;
                // Guardar modelo
                _context.ModeloJerarquicos.Add(modeloJerarquico);
                await _context.DbContext.SaveChangesAsync();
                return new OkResult();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.InnerException!.Message);
                throw new Exception("Error al agregar el Modelo Jerarquico");
            }

        }

        public Task<List<ModeloJerarquico>> ConsultarModeloJerarquicosDAO()
        {
            try
            {
                return _context.ModeloJerarquicos.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException!.Message);
                throw new Exception("Error al consultar los Modelos Jerarquicos");
            }
        }

        public async Task<ActionResult<ModeloJerarquico>> ObtenerModeloJerarquicoDAO(int id)
        {
            try
            {
                var ModeloJerarquico = await _context.ModeloJerarquicos
                                                     .Include(mj => mj.categoria)
                                                     .FirstOrDefaultAsync(mj => mj.Id == id);
                //var listCargos = await _context.TipoCargos.Where(tc => tc.ModeloJerarquicoId == id).ToListAsync();
                
                
                if (ModeloJerarquico == null)
                {
                    return new ModeloJerarquico();
                }
                //ModeloJerarquico.orden = listCargos;
                return ModeloJerarquico;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException!.Message);
                throw new Exception("Error al obtener el Modelo Jerarquico");
            }
        }

        public async Task<ActionResult<ModeloJerarquico>> ActualizarModeloJerarquicoDAO(ModeloJerarquico ModeloJerarquico, int id)
        {

            try
            {
                var ModeloJerarquicoDB = await _context.ModeloJerarquicos.FindAsync(id);
                if (ModeloJerarquicoDB == null)
                {
                    return new NotFoundResult();
                }
                // Validar categoria 
                var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.id == ModeloJerarquico.CategoriaId);
                if (categoria == null)
                {
                    return new NotFoundResult();
                }
                // Validar orden
                if (ModeloJerarquico.orden == null)
                {
                    return new BadRequestResult();
                }
                foreach (var cargo in ModeloJerarquico.orden)
                {
                    var cargoDB = await _context.TipoCargos.FirstOrDefaultAsync(c => c.id == cargo.id);
                    if (cargoDB == null)
                    {
                        return new NotFoundResult();
                    }

                }
                // Actualizar modelo
                ModeloJerarquicoDB.CategoriaId = ModeloJerarquico.CategoriaId;
                ModeloJerarquicoDB.orden = ModeloJerarquico.orden;
                await _context.DbContext.SaveChangesAsync();
                return ModeloJerarquicoDB;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.InnerException!.Message);
                throw new Exception("Error al actualizar el Modelo Jerarquico");
            }

        }

        public async Task<ActionResult> EliminarModeloJerarquicoDAO(int id)
        {
            try
            {
                var existe = await ObtenerModeloJerarquicoDAO(id);

                _context.ModeloJerarquicos.Remove(existe.Value!);
                await _context.DbContext.SaveChangesAsync();

                return new OkResult();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.InnerException!.Message);
                throw new Exception("Error al eliminar el Modelo Jerarquico");
            }
        }

    }
}