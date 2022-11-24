using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.BussinessLogic.Mapper;
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

    [HttpGet ("consultar/{id}")]
    public async Task<ActionResult<ModeloParaleloDTO>> Get([Required][FromRoute] int id)
    {
        if (id <= 0)
        {
            return BadRequest("El id debe ser positivo");
        }
        var consulta = await modeloParaleloDAO.ConsultaModeloParaleloDAO(id);
        if (consulta.Value?.paraid == null)
        {
            return NotFound(" ModeloParalelo no encontrado");
        }
        return mapper.Map<ModeloParaleloDTO>(consulta.Value);
    }

    [HttpGet ("consultar")]
    public IActionResult ConsultarTodos()
    {
        if (modeloParaleloDAO.ConsultarModelosParalelosDAO() == null)
        {
            return BadRequest("No se encontraron los modelos paralelos");
        }
        return Ok(modeloParaleloDAO.ConsultarModelosParalelosDAO());        
    }

    [HttpPost ("crear")]
    public async Task<ActionResult> Post([FromBody] ModeloParaleloCreateDTO dto)
    {
        var modeloParalelo = mapper.Map<ModeloParalelo>(dto);
        var result = await modeloParaleloDAO.AgregarModeloParaleloDAO(modeloParalelo);
        return result;
    }

    [HttpPut ("actualizar/{id}")]
    public IActionResult Actualizar([Required][FromRoute] int id, [Required][FromBody] ModeloParaleloDTO dto)
    {
        var modeloParalelo = mapper.Map<ModeloParalelo>(dto);
        modeloParaleloDAO.ActualizarModeloParaleloDAO(id, modeloParalelo);
        return Ok();
    }

    [HttpDelete ("eliminar/{id}")]
    public IActionResult Eliminar([Required][FromRoute] int id)
    {
        modeloParaleloDAO.EliminarModeloParaleloDAO(id);
        return Ok();
    }
}