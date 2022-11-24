using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.BussinessLogic.Mapper;
using ServicesDeskUCABWS.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace ServicesDeskUCABWS.Persistence.DAO.Implementations;

public class ModeloParaleloDAO : IModeloParaleloDAO
{   
    private readonly IMigrationDbContext context;
    public ModeloParaleloDAO(IMigrationDbContext Dbcontext)
    {
        this.context = Dbcontext;
    }

    public async Task<ActionResult> AgregarModeloParaleloDAO(ModeloParalelo modeloParalelo)
    {
        try
        {
            var categoria = await context.Categorias.FirstOrDefaultAsync(c => c.id == modeloParalelo.categoriaId);
            if (categoria == null)
            {
                return new NotFoundResult();
            }
            modeloParalelo.categoria = categoria;
            context.ModeloParalelos.Add(modeloParalelo);
            await context.DbContext.SaveChangesAsync();
            return new OkResult();
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine(ex.InnerException!.Message);
            throw new Exception("Error al agregar el Modelo Paralelo");
        }       
    }

    public IEnumerable<ModeloParalelo> ConsultarModelosParalelosDAO()
    {
        try
        {
            IEnumerable<ModeloParalelo> datos = context.ModeloParalelos;
            if (datos == null)
            {
                return null;
            }
            return datos;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerException!.Message);
            throw new Exception("Error al consultar los modelos paralelos", ex); 
        } 
    }

    public async Task<ActionResult<ModeloParalelo>> ConsultaModeloParaleloDAO(int id)
    {
        try
        {
            var consulta = await context.ModeloParalelos.Include(cat => cat.categoria).FirstOrDefaultAsync(p => p.paraid == id);
            if (consulta == null)
            {
                return new ModeloParalelo(); 
            }
            return consulta;                
        }                          
        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerException!.Message);
            throw new Exception("Error al obtener el Modelo Paralelo"); 
        } 
    } 

    public async Task ActualizarModeloParaleloDAO(int id, ModeloParalelo modeloParalelo)
    {
        try
        {
            var modeloActual = context.ModeloParalelos.Find(id);
            if (modeloActual != null)
            {
                modeloActual.paraid = modeloParalelo.paraid;
                modeloActual.nombre = modeloParalelo.nombre;
                modeloActual.cantidadAprobaciones = modeloParalelo.cantidadAprobaciones;
                modeloActual.categoriaId = modeloParalelo.categoriaId;
                await context.DbContext.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message + " : " + ex.StackTrace);
            throw new Exception("Transaccion Fallo", ex)!;
        }
    }

    public async Task EliminarModeloParaleloDAO(int id)
        {
        try
        {
            var modeloActual = context.ModeloParalelos.Find(id);
            if(modeloActual != null)
            {
                context.DbContext.Remove(modeloActual);
                await context.DbContext.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("[Mensaje]: " + ex.Message + " [Seguimiento]: " + ex.StackTrace);
            throw new Exception("Transaccion Fallo", ex)!;
        }
    }
}