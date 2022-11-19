using ServicesDeskUCABWS.BussinessLogic.Mapper;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using System.ComponentModel.DataAnnotations;


namespace ServicesDeskUCABWS.Controllers;

[Route("modeloaprobacion")]
public class ModeloAprobacionController : ControllerBase
{
    private readonly IModeloJerarquicoDAO _modeloJerarquicoDAO;
    private readonly IModeloParaleloDAO _modeloParaleloDAO;
    private readonly ILogger<ModeloAprobacionController> _log;

    public ModeloAprobacionController (ILogger<ModeloAprobacionController> logger, IModeloJerarquicoDAO modeloJerarquicoDAO, IModeloParaleloDAO modeloParaleloDAO)
    {
        _log = logger;
        _modeloJerarquicoDAO = modeloJerarquicoDAO;
        _modeloParaleloDAO = modeloParaleloDAO;
    }

    [HttpPost("/jerarquico/crear")]
    public ModeloJerarquicoDTO CreateModeloJerarquico([FromBody] ModeloJerarquicoDTO modeloJerarquicoDTO)
    {
        try
        {
            var data = _modeloJerarquicoDAO.AgregarModeloJerarquicoDAO(ModeloJerarquicoMapper.DtoToEntity(modeloJerarquicoDTO));
            return data;
        }catch(Exception ex)
        {
            _log.LogError(ex.ToString());
            throw ex.InnerException!;
        }
    }

    [HttpGet]
    [Route("/jerarquico/consultar")]
    public List<ModeloJerarquicoDTO> ConsultarModelosJerarquicosDAO()
    {
        try
        {
            var data = _modeloJerarquicoDAO.ConsultarModelosJerarquicosDAO();
            return data;
        }catch(Exception ex)
        {
            _log.LogError(ex.ToString());
            throw ex.InnerException!;
        }
    }

    [HttpGet]
    [Route("/jerarquico/consultar/{id}")]
    public ModeloJerarquicoDTO ConsultaModeloJerarquico([Required][FromRoute]string id)
    {
        try
        {
            var data = _modeloJerarquicoDAO.ConsultaModeloJerarquicoDAO(id);
            return data;
        }catch (Exception ex)
        {
            _log.LogError(ex.ToString());
            throw ex.InnerException!;
        }   
    }

    [HttpPut]
    [Route("/jerarquico/actualizar")]
    public ModeloJerarquicoDTO ActualizarCategoria([Required][FromBody] ModeloJerarquicoDTO dto)
    {
        try
        {
            return _modeloJerarquicoDAO.ActualizarModeloJerarquicoDAO(ModeloJerarquicoMapper.DtoToEntity(dto));

        }catch(Exception ex)
        {
            Console.WriteLine(ex.Message + " : " + ex.StackTrace);
            throw ex.InnerException!;
        }
    }

    [HttpDelete]
    [Route("/jerarquico/eliminar/{id}")]
    public ModeloJerarquicoDTO EliminarCategoria([Required][FromRoute] string id)
    {
        try
        {
            return _modeloJerarquicoDAO.EliminarModeloJerarquicoDAO(id);
        }catch(Exception ex)
        {
            Console.WriteLine(ex.Message + " : " + ex.StackTrace);
            throw ex.InnerException!;    
        }
    }
    [HttpPost("/paralelo/crear")]
    public ModeloParaleloDTO CreateModeloParalelo([FromBody] ModeloParaleloDTO modeloParaleloDTO)
    {
        try
        {
            var data = _modeloParaleloDAO.AgregarModeloParaleloDAO(ModeloParaleloMapper.DtoToEntity(modeloParaleloDTO));
            return data;
        }catch(Exception ex)
        {
            _log.LogError(ex.ToString());
            throw ex.InnerException!;
        }
    }

    [HttpGet]
    [Route("/paralelo/consultar")]
    public List<ModeloParaleloDTO> ConsultarModelosParalelosDAO()
    {
        try
        {
            var data = _modeloParaleloDAO.ConsultarModelosParalelosDAO();
            return data;
        }catch(Exception ex)
        {
            _log.LogError(ex.ToString());
            throw ex.InnerException!;
        }
    }

    [HttpGet]
    [Route("/paralelo/consultar/{id}")]
    public ModeloParaleloDTO ConsultaModeloParalelo([Required][FromRoute]string id)
    {
        try
        {
            var data = _modeloParaleloDAO.ConsultaModeloParaleloDAO(id);
            return data;
        }catch (Exception ex)
        {
            _log.LogError(ex.ToString());
            throw ex.InnerException!;
        }   
    }

    [HttpPut]
    [Route("/paralelo/actualizar")]
    public ModeloParaleloDTO ActualizarModeloParalelo([Required][FromBody] ModeloParaleloDTO dto)
    {
        try
        {
            return _modeloParaleloDAO.ActualizarModeloParaleloDAO(ModeloParaleloMapper.DtoToEntity(dto));

        }catch(Exception ex)
        {
            Console.WriteLine(ex.Message + " : " + ex.StackTrace);
            throw ex.InnerException!;
        }
    }

    [HttpDelete]
    [Route("/paralelo/eliminar/{id}")]
    public ModeloParaleloDTO EliminarModeloParalelo([Required][FromRoute] string id)
    {
        try
        {
            return _modeloParaleloDAO.EliminarModeloParaleloDAO(id);
        }catch(Exception ex)
        {
            Console.WriteLine(ex.Message + " : " + ex.StackTrace);
            throw ex.InnerException!;    
        }
    }
}