using System.ComponentModel;
using System;
using System.Reflection;
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
        private readonly IMapper mapper;

        public ModeloJerarquicoDAO( IMigrationDbContext context, IMapper map)
        {
            this._context = context;
            this.mapper = map;
        }



        public async Task<ActionResult<ModeloJerarquicoDTO>> AgregarModeloJerarquicoDAO(ModeloJerarquico modeloJerarquico)
        {
            try
            {
                // Validar categoria 
                var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.id == modeloJerarquico.CategoriaId);
                if (categoria == null)
                {
                    throw new Exception("No existe el registro de la categoria para el modelo paralelo");

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
                return mapper.Map<ModeloJerarquicoDTO>(modeloJerarquico);
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
                    throw new Exception("Error al consultar el modelo jerarquico");

                }
                //ModeloJerarquico.orden = listCargos;
                return ModeloJerarquico;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el Modelo Jerarquico");
            }
        }

        public async Task<ActionResult<ModeloJerarquico>> ActualizarModeloJerarquicoDAO(ModeloJerarquicoCreateDTO modeloJerarquico, int id)
        {

            try
            {
                var ModeloJerarquicoDB = await _context.ModeloJerarquicos.FindAsync(id);
                if (ModeloJerarquicoDB == null)
                {
                    throw new NullReferenceException("No existe el modelo jerarquico a actualizar");
                }
                // Validar categoria 
                var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.id == modeloJerarquico.CategoriaId);
                if (categoria == null)
                {
                    throw new NullReferenceException("No existe en el modelo jerarquico la categoria a actualizar");
                }
                // Validar orden
                if (modeloJerarquico.orden == null)
                {
                    throw new NullReferenceException("No existe en el modelo jerarquico la lista de orden a actualizar");

                }
                foreach (var cargo in modeloJerarquico.orden)
                {
                    var cargoDB = await _context.TipoCargos.FirstOrDefaultAsync(c => c.id == cargo.id);
                    if (cargoDB == null)
                    {
                        throw new NullReferenceException("No existe en el modelo jerarquico el cargo a actualizar");

                    }

                }
                // Actualizar modelo
                ModeloJerarquicoDB.CategoriaId = modeloJerarquico.CategoriaId;
                ModeloJerarquicoDB.orden = modeloJerarquico.orden;
                await _context.DbContext.SaveChangesAsync();
                return ModeloJerarquicoDB;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el Modelo Jerarquico", ex);
            }
        }

        /*
        System.InvalidOperationException: The property 'ModeloJerarquico.Id' has a temporary value while attempting to 
        change the entity's state to 'Deleted'. Either set a permanent value explicitly, or ensure that the database is configured to generate values for this property.
        */
        public async Task<ActionResult> EliminarModeloJerarquicoDAO(int id)
        {
            try
            {
                var existe = await _context.ModeloJerarquicos.FindAsync(id);
                if(existe == null)
                {
                    return new NotFoundResult();
                }
                _context.ModeloJerarquicos.Remove(existe);
                await _context.DbContext.SaveChangesAsync();

                return new OkResult();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el Modelo Jerarquico", ex);
            }
        }
    }
}