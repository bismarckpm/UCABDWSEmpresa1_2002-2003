using ServicesDeskUCABWS.BussinessLogic.Mapper;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Database;
using ServicesDeskUCABWS.Persistence.Entity;
using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using ServicesDeskUCABWS.Exceptions;


namespace ServicesDeskUCABWS.Controllers
{
    [ApiController]
    [Route("ModeloAprobacion/")]
    public class ModeloJerarquicoController : Controller
    {
        private readonly IModeloJerarquicoDAO _dao;
        private readonly ILogger<ModeloJerarquicoController> _log;

        private readonly IMapper _mapper;


        public ModeloJerarquicoController(ILogger<ModeloJerarquicoController> logger, IModeloJerarquicoDAO dao, IMapper mapper)
        {
            _log = logger;
            _dao = dao;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Jerarquico/")]
        public ModeloJerarquicoDTO Post([FromBody] ModeloJerarquicoDTO dto)
        {
            try
            {
                var p = ModeloJerarquicoMapper.DtoToEntity(dto);

                _log.LogInformation("ModeloJerarquico agregada con exito");
                return _dao.AgregarModeloJerarquicoDAO(p);

            }catch(ServicesDeskUcabWsException ex)
            {
                _log.LogError("Error al crear" + ex.Mensaje, ex.Excepcion);
                throw new ServicesDeskUcabWsException(ex.Mensaje,ex.Excepcion);
            }
        }

        [HttpGet]
        [Route("GetModeloJerarquico/")]
        public List<ModeloJerarquicoDTO> GetModeloJerarquico()
        {
            try
            {
                return _dao.ConsultarModeloJerarquicosDAO();
            }catch(ServicesDeskUcabWsException ex)
            {
                _log.LogError("Error al consultar " + ex.Mensaje, ex.StackTrace);
                throw new ServicesDeskUcabWsException("Error al Consultar" + ex.Mensaje, ex);
            }
        }        

        [HttpGet]
        [Route("Jerarquico/{id}")]
        public ModeloJerarquicoDTO ConsultaMJerarquicoPorId(int id)
        {
            try
            {
                return _dao.ObtenerModeloJerarquicoDAO(id);

            }catch(ServicesDeskUcabWsException ex)
            {
                _log.LogError("[Error]: "+ ex.Mensaje + " || " + ex.StackTrace);
                throw new ServicesDeskUcabWsException("[Error] : "+ ex.Mensaje, ex);
            }
        }

         [HttpPut]
         [Route("ActualizaModeloJerarquico/")]
         public ModeloJerarquicoDTO ActualizarModeloJerarquico([FromBody] ModeloJerarquicoDTO dto)
         {
            try
            {
                   var data = _dao.ActualizarModeloJerarquicoDAO(ModeloJerarquicoMapper.DtoToEntity(dto));
                    _log.LogInformation("[Objeto Actualizado]: " + data.Nombre + ", " + data.CategoriaId + ", " + data.orden);
                    return data;

            }catch(ServicesDeskUcabWsException ex)
            {
                _log.LogError(ex.Mensaje + " || " + ex.StackTrace);
                throw new ServicesDeskUcabWsException(ex.Mensaje, ex.Excepcion);
            }
             
         }

        [HttpDelete]
        [Route("DeleteModeloJerarquico/{id}")]
        public ModeloJerarquicoDTO EliminarModeloJerarquico([FromRoute] int id)
        {
                try
                {
                    return  _dao.EliminarModeloJerarquicoDAO(id);
                    
                }catch(Exception ex)
                {
                    _log.LogError("("+DateTime.Now +") "+"- [ "+ex.Message +" ]");
                    throw new ServicesDeskUcabWsException("Error al eliminar el Objeto: " + id, ex.Message, ex);
                }
        }
    }
}