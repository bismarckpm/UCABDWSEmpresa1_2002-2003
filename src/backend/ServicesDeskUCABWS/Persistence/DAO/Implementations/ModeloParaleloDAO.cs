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

    public ModeloParaleloDTO AgregarModeloParaleloDAO(ModeloParalelo modeloParalelo)
    {
        try
        {
            context.ModeloParalelos.Add(modeloParalelo);
            context.DbContext.SaveChanges();
            return mapper.Map<ModeloParaleloDTO>(modeloParalelo);
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine(ex.InnerException!.Message);
            throw new ModeloParaleloException("Error al agregar el Modelo Paralelo", ex);
        }       
    }

    public List<ModeloParaleloDTO> ConsultarModelosParalelosDAO()
    {
        try
        {
            var data = context.ModeloParalelos.Select(j => new ModeloParaleloDTO() 
                                                    {
                                                        paraid = j.id,
                                                        nombre = j.nombre,
                                                        categoriaId = j.categoriaid,
                                                        cantidaddeaprobacion = j.cantidaddeaprobacion
                                                    });
            return data.ToList();
        }
        catch (Exception ex)
        {
            throw new ModeloParaleloException("Error al consultar los modelos paralelos" + ex.Message, ex); 
        } 
    }

    public ModeloParaleloDTO ObtenerModeloParaleloDAO(int id)
    {
        try
        {
            var data = context.ModeloParalelos.Select(j => new ModeloParaleloDTO()
                                                    {
                                                        paraid = j.id,
                                                        nombre = j.nombre,
                                                        categoriaId = j.categoriaid,
                                                        cantidaddeaprobacion = j.cantidaddeaprobacion
                                                    }).Where(j => j.paraid == id);         
            return data.First();                
        }                          
        catch (Exception ex)
        {
            throw new ModeloParaleloException("Error al consultar el modelo paralelo", ex.InnerException!);
        } 
    } 

    public ModeloParaleloDTO ActualizarModeloParaleloDAO(ModeloParalelo modeloParalelo)
    {
        try
        {
            context.ModeloParalelos.Update(modeloParalelo);
            context.DbContext.SaveChanges();
            var data = context.ModeloParalelos.Where(j => j.id == modeloParalelo.id)
                                                .Select(j => new ModeloParaleloDTO()
                                                {
                                                    paraid = j.id,
                                                    nombre = j.nombre,
                                                    categoriaId = j.categoriaid,
                                                    cantidaddeaprobacion = j.cantidaddeaprobacion
                                                });
            return data.First();
        }
        catch (Exception ex)
        {
            throw new ModeloParaleloException("Error al actualizar el modelo paralelo"+ ex.Message, ex);
        }
    }

    public ModeloParaleloDTO EliminarModeloParaleloDAO(int id)
    {
        try
        {
            var modeloParalelo = context.ModeloParalelos
                                        .Where(j => j.id == id)
                                        .First();
            context.DbContext.Remove(modeloParalelo);  
            context.DbContext.SaveChanges();
            return mapper.Map<ModeloParaleloDTO>(modeloParalelo);                                 
        }
        catch (Exception ex)    
        {
            throw new ServicesDeskUcabWsException(ex.Message, ex.InnerException!);
        }
    }
}