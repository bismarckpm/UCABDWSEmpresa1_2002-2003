using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCABWS.Exceptions;
using AutoMapper;

namespace ServicesDeskUCABWS.Persistence.DAO.Implementations;

public class ModeloParaleloDAO : IModeloParaleloDAO
{   
    private readonly IMigrationDbContext context;
    private readonly IMapper mapper;
    public ModeloParaleloDAO(IMigrationDbContext Dbcontext, IMapper map)
    {
        this.context = Dbcontext;
        this.mapper = map;
    }

    public async Task<ActionResult<ModeloParaleloDTO>> AgregarModeloParaleloDAO(ModeloParalelo modeloParalelo)
    {
        try
        {
            var categoria = await context.Categorias.FirstOrDefaultAsync(c => c.id == modeloParalelo.categoriaId);
            if (categoria == null)
            {
                throw new Exception("No existe el registro de la categoria para el modelo paralelo");
            }
            modeloParalelo.categoria = categoria;
            context.ModeloParalelos.Add(modeloParalelo);
            await context.DbContext.SaveChangesAsync();
            return mapper.Map<ModeloParaleloDTO>(modeloParalelo);
        }
        catch (DbUpdateException ex)
        {
            throw new ModeloParaleloException("Error al agregar el Modelo Paralelo", ex);
        }       
    }

    public Task<List<ModeloParalelo>> ConsultarModelosParalelosDAO()
    {
        try
        {
            return context.ModeloParalelos.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new ModeloParaleloException("Error al consultar los modelos paralelos", ex); 
        } 
    }

    public async Task<ActionResult<ModeloParalelo>> ConsultaModeloParaleloDAO(int id)
    {
        try
        {
            var consulta = await context.ModeloParalelos
                                        .Include(cat => cat.categoria)
                                        .FirstOrDefaultAsync(p => p.paraid == id);
            if (consulta == null)
            {
            throw new Exception("Error al consultar el modelo paralelo");
            }
            return consulta;                
        }                          
        catch (Exception ex)
        {
            throw new ModeloParaleloException("Error al consultar el modelo paralelo", ex);
        } 
    } 

    public async Task<ActionResult<ModeloParalelo>> ActualizarModeloParaleloDAO(int id, ModeloParaleloCreateDTO modeloParalelo)
    {
        try
        {
            var modeloActual = await context.ModeloParalelos.FindAsync(id);
            if (modeloActual == null)
            {
                throw new NullReferenceException("No existe el modelo paralelo a actualizar");
            }
            // Validar categoria 
            var categoria = await context.Categorias.FirstOrDefaultAsync(c => c.id == modeloParalelo.categoriaId);
            if (categoria == null)
            {
                throw new NullReferenceException("No existe en el modelo paralelo la categoria a actualizar");
            }
            modeloActual.nombre = modeloParalelo.nombre;
            modeloActual.cantidadAprobaciones = modeloParalelo.cantidadAprobaciones;
            modeloActual.categoriaId = modeloParalelo.categoriaId;
            await context.DbContext.SaveChangesAsync();
            return modeloActual;
        }
        catch (Exception ex)
        {
            throw new ModeloParaleloException("Error al actualizar el modelo paralelo", ex);
        }
    }

    public async Task<ActionResult> EliminarModeloParaleloDAO(int id)
    {
        try
        {
            var modeloActual = await context.ModeloParalelos.FindAsync(id);
            if(modeloActual == null)
            {
                return new NotFoundResult();    
            }
            context.ModeloParalelos.Remove(modeloActual);
            await context.DbContext.SaveChangesAsync(); 
            return new OkResult();
        }
        catch (Exception ex)
        {
            throw new ModeloParaleloException("Error al actualizar el modelo paralelo", ex);
        }
    }
}