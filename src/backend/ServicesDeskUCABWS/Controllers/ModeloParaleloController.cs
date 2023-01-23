using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.Exceptions;

namespace ServicesDeskUCABWS.Controllers;

[ApiController]
[Route("ModeloAprobacion/")]

public class ModeloParaleloController : Controller
{
    private readonly IModeloParaleloDAO modeloParaleloDAO;
    private readonly IMapper mapper;
    private readonly ILogger<ModeloParaleloController> log;
    
    public ModeloParaleloController(ILogger<ModeloParaleloController> _log, IModeloParaleloDAO dao, IMapper map)
    {
        modeloParaleloDAO = dao;
        mapper = map;
        log = _log;
    }

   /*Llama al servicio de ModeloParaleloDAO que retorna un objeto de ModeloParalelo*/
    [HttpGet ("Paralelo/{id}")]
    public ModeloParaleloDTO ConsultarMParaleloPorId([Required][FromRoute] int id)
    {
        try
        {
            return modeloParaleloDAO.ObtenerModeloParaleloDAO(id);
        }
        catch(ServicesDeskUcabWsException ex)
        {
            log.LogError("[Error]: "+ ex.Mensaje + " || " + ex.StackTrace);
            throw new ServicesDeskUcabWsException("[Error] : "+ ex.Mensaje, ex);
        }     
    }

    /*Llama al servicio de ModeloParaleloDAO que retorna una lista de objetos de ModeloParalelo*/
    [HttpGet ("GetModeloParalelo/")]
    public List<ModeloParaleloDTO> GetModeloParalelo()
    {
        try
        {
            return modeloParaleloDAO.ConsultarModelosParalelosDAO();
        }
        catch(ServicesDeskUcabWsException ex)
        {
            log.LogError("Error al consultar " + ex.Mensaje, ex.StackTrace);
            throw new ServicesDeskUcabWsException("Error al Consultar" + ex.Mensaje, ex);
        }              
    }

    /*Llama al servicio de ModeloParaleloDAO que agrega un objeto de ModeloParalelo*/
    [HttpPost ("Paralelo/")]
    public ModeloParaleloCreateDTO Post([Required][FromBody] ModeloParaleloCreateDTO dto)
    {
        try
        {
            var modeloParalelo = mapper.Map<ModeloParalelo>(dto);
            log.LogInformation("ModeloParalelo agregado con exito");
            return modeloParaleloDAO.AgregarModeloParaleloDAO(modeloParalelo);
        }
        catch(ServicesDeskUcabWsException ex)
        {
            log.LogError("Error al crear" + ex.Mensaje, ex.Excepcion);
            throw new ServicesDeskUcabWsException(ex.Mensaje,ex.Excepcion);
        }        
    }

    /*Llama al servicio de ModeloParaleloDAO que actualiza un objeto de ModeloParalelo*/
    [HttpPut ("ActualizarModeloParalelo/")]
    public ModeloParaleloDTO ActualizarModeloParalelo([Required][FromBody] ModeloParaleloDTO dto)
    {
        try
        {
            var data = modeloParaleloDAO.ActualizarModeloParaleloDAO(mapper.Map<ModeloParalelo>(dto));
            log.LogInformation("[Objeto Actualizado]: " + data.nombre + ", " + data.categoriaId + ", " + data.cantidaddeaprobacion);
            return data;
        }
        catch(ServicesDeskUcabWsException ex)
        {
            log.LogError(ex.Mensaje + " || " + ex.StackTrace);
            throw new ServicesDeskUcabWsException(ex.Mensaje, ex.Excepcion);
        }       
    }

    /*Llama al servicio de ModeloParaleloDAO que elimina un objeto de ModeloParalelo*/
    [HttpDelete ("DeleteModeloParalelo/{id}")]
    public ModeloParaleloDTO EliminarModeloParalelo([Required][FromRoute] int id)
    {
        try
        {
            return modeloParaleloDAO.EliminarModeloParaleloDAO(id);
        }
        catch(Exception ex)
        {
            log.LogError("("+DateTime.Now +") "+"- [ "+ex.Message +" ]");
            throw new ServicesDeskUcabWsException("Error al eliminar el Objeto: " + id, ex.Message, ex);
        }
    }
}