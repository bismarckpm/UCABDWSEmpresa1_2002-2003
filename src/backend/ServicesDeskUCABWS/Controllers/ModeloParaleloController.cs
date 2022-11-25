using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using ServicesDeskUCABWS.Persistence.Entity;

namespace ServicesDeskUCABWS.Controllers;

[Route("modeloparalelo")]

public class ModeloParaleloController : Controller
{
    private readonly IModeloParaleloDAO modeloParaleloDAO;
    private readonly IMapper mapper;
    
    public ModeloParaleloController(IModeloParaleloDAO dao, IMapper map)
    {
        modeloParaleloDAO = dao;
        mapper = map;
    }

    [HttpGet ("consultar/{id:int}")]
    public async Task<ActionResult<ModeloParaleloDTO>> Consultar([Required][FromRoute] int id)
    {
        if (id <= 0)
        {
            return BadRequest("El id debe ser mayor a 0");
        }
        var consulta = await modeloParaleloDAO.ConsultaModeloParaleloDAO(id);
        if (consulta.Value?.paraid == id)
        {
            return Ok(mapper.Map<ModeloParaleloDTO>(consulta.Value));
            
        }
            return NotFound(" ModeloParalelo no encontrado");
        
    }

    [HttpGet ("consultar")]
    public async Task<ActionResult<List<ModeloParaleloDTO>>> ConsultarTodos()
    {
        var consulta = await modeloParaleloDAO.ConsultarModelosParalelosDAO();
        if (consulta == null)
        {
            return BadRequest("No se encontraron los modelos paralelos");
        }
        return Ok(mapper.Map<List<ModeloParaleloDTO>>(consulta));        
    }

    [HttpPost ("crear")]
    public async Task<ActionResult> Crear([Required][FromBody] ModeloParaleloCreateDTO dto)
    {
        var modeloParalelo = mapper.Map<ModeloParalelo>(dto);
        var result = await modeloParaleloDAO.AgregarModeloParaleloDAO(modeloParalelo);
        return Ok(result);
    }

    [HttpPut ("actualizar/{id}")]
    public async Task<ActionResult> Actualizar([Required][FromRoute] int id, [Required][FromBody] ModeloParaleloDTO dto)
    {
        if (id <= 0)
        {
            return BadRequest("El id debe ser mayor a 0");
        }
        var modeloParalelo = mapper.Map<ModeloParalelo>(dto);
        var result = await modeloParaleloDAO.ActualizarModeloParaleloDAO(id, modeloParalelo);
        if (result.Value!.paraid == id)
        {
            return Ok(result);
        }
        else
        {
            return NotFound("No se encontro el modelo paralelo");
        }        
    }

    [HttpDelete ("eliminar/{id}")]
    public async Task<ActionResult> Eliminar([Required][FromRoute] int id)
    {
        if (id <= 0)
        {
            return BadRequest("El id debe ser mayor a 0");
        }
        var result = await modeloParaleloDAO.EliminarModeloParaleloDAO(id);
        return Ok(result);
    }
}