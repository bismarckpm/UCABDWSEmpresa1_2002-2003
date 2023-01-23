using System.Data;
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

    /*Agrega objeto a la Entidad de base de datos ModeloParalelo y retorna un objeto ModeloParaleloDTO que sera usado por el controlador*/
    public ModeloParaleloCreateDTO AgregarModeloParaleloDAO(ModeloParalelo modeloParalelo)
    {
        try
        {
            context.ModeloParalelos.Add(modeloParalelo);
            context.DbContext.SaveChanges();
            return mapper.Map<ModeloParaleloCreateDTO>(modeloParalelo);
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine(ex.InnerException!.Message);
            throw new ModeloParaleloException("Error al agregar el Modelo Paralelo", ex);
        }       
    }

    /*Consulta la entidad de base de datos ModeloParalelo y retorna una lista de objetos ModeloParaleloDTO que sera usado por el controlador*/
    public List<ModeloParaleloDTO> ConsultarModelosParalelosDAO()
    {
        try
        {
            var data = context.ModeloParalelos.Select(j => new ModeloParaleloDTO() 
                                                    {
                                                        Id = j.id,
                                                        nombre = j.nombre,
                                                        categoriaId = j.categoriaid,
                                                        cantidaddeaprobacion = j.cantidaddeaprobacion
                                                    });
            return data.ToList();
        }
        catch (Exception ex)
        {
            throw new ModeloParaleloException("Error al consultar los modelos paralelos", ex.InnerException!); 
        } 
    }

    /*Consulta la Entidad de base de datos ModeloParalelo y retorna un objeto ModeloParaleloDTO que sera usado por el controlador*/
    public ModeloParaleloDTO ObtenerModeloParaleloDAO(int id)
    {
        try
        {
            var data = context.ModeloParalelos.Select(j => new ModeloParaleloDTO()
                                                    {
                                                        Id = j.id,
                                                        nombre = j.nombre,
                                                        categoriaId = j.categoriaid,
                                                        cantidaddeaprobacion = j.cantidaddeaprobacion
                                                    }).Where(j => j.Id == id);    
            return data.First();            
        }                          
        catch (Exception ex)
        {
            throw new ModeloParaleloException("Error al consultar el modelo paralelo", ex.InnerException!);
        } 
    } 


    /*Actualiza un objeto de la Entidad de base de datos ModeloParalelo y retorna un objeto ModeloParaleloDTO que sera usado por el controlador*/
    public ModeloParaleloDTO ActualizarModeloParaleloDAO(ModeloParalelo modeloParalelo)
    {
        try
        {
            context.ModeloParalelos.Update(modeloParalelo);
            context.DbContext.SaveChanges();
            return mapper.Map<ModeloParaleloDTO>(modeloParalelo);
        }
        catch (Exception ex)
        {
            throw new ModeloParaleloException("Error al actualizar el modelo paralelo ", ex.InnerException!);
        }
    }

    /*Elimina un objeto de la Entidad de base de datos ModeloParalelo y retorna un objeto ModeloParaleloDTO que sera usado por el controlador*/
    public ModeloParaleloDTO EliminarModeloParaleloDAO(int id)
    {
        try
        {
            var modeloActual = context.ModeloParalelos
                                                .Where(mj => mj.id == id)
                                                .First();
            context.DbContext.Remove(modeloActual);  
            context.DbContext.SaveChanges();
            return mapper.Map<ModeloParaleloDTO>(modeloActual);                                 
        }
        catch (Exception ex)    
        {
            throw new ModeloParaleloException("Error al eliminar el modelo paralelo", ex.InnerException!);
        }
    }
}