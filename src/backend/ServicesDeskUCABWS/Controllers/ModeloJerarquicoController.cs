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
using static ServicesDeskUCABWS.Reponses.AplicationResponse;
using System.Net;

namespace ServicesDeskUCABWS.Controllers
{
    [ApiController]
    [Route("ModeloAprobacion/")]
    public class ModeloJerarquicoController : Controller
    {
        private readonly IModeloJerarquicoDAO _dao;
        private readonly ILogger<ModeloJerarquicoController> _log;

        private readonly IMapper _mapper;
        const string message = "Solicitud Exitosa";

        public ModeloJerarquicoController(ILogger<ModeloJerarquicoController> logger, IModeloJerarquicoDAO dao, IMapper mapper)
        {
            _log = logger;
            _dao = dao;
            _mapper = mapper;
        }

        /// <summary>
        /// Llama un servicio de ModeloJerarquicoDAO que agrega un objeto ModeloJerarquico
        /// </summary>
        /// <param name="dto">Un Objeto ModeloJerarquicoDTO. </param>
        /// <returns>Un ApplicationResponse que contiene el objeto creado
        /// satisfactoriamente. </returns>
        [HttpPost]
        [Route("Jerarquico/")]
        public ApplicationResponse<ModeloJerarquicoDTO> Post([FromBody] ModeloJerarquicoDTO dto)
        {
            var response = new ApplicationResponse<ModeloJerarquicoDTO>();
            try
            {
                var p = ModeloJerarquicoMapper.DtoToEntity(dto);
                response.Data = _dao.AgregarModeloJerarquicoDAO(p);
                response.StatusCode = HttpStatusCode.OK;
                response.Message = message;

            }catch(ServicesDeskUcabWsException ex)
            {
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion.ToString();

            }
            return response;
        }

        /// <summary>
        /// Llama un servicio de ModeloJerarquicoDAO que 
        ///obtiene un listado de objetos ModeloJerarquico
        /// </summary>
        /// <returns>Un ApplicationResponse que contiene un
        /// listado de objetos modelo jerarquico. </returns>
        [HttpGet]
        [Route("GetModeloJerarquico/")]
        public ApplicationResponse<List<ModeloJerarquicoDTO>> GetModeloJerarquico()
        {
            var response = new ApplicationResponse<List<ModeloJerarquicoDTO>>();
            try
            {
                response.Data= _dao.ConsultarModeloJerarquicosDAO();
                response.StatusCode = HttpStatusCode.OK;
                response.Message = message;

            }catch(ServicesDeskUcabWsException ex)
            {
                response.Message = ex.Mensaje;
                response.Success = false;
                response.Exception = ex.Excepcion.ToString();

            }
            return response;
        }        

        /// <summary>
        /// Llama un servicio de ModeloJerarquicoDAO que consulta un objeto ModeloJerarquico
        /// </summary>
        /// <param name="id">Un valor de tipo int32. </param>
        /// <returns>Un ApplicationResponse que contiene el objeto de
        /// la consulta. </returns>
        [HttpGet]
        [Route("Jerarquico/{id}")]
        public ApplicationResponse<ModeloJerarquicoDTO> ConsultaMJerarquicoPorId(int id)
        {
            var response = new ApplicationResponse<ModeloJerarquicoDTO>();
            try
            {
                response.Data = _dao.ObtenerModeloJerarquicoDAO(id);
                response.StatusCode = HttpStatusCode.OK;
                response.Message = message;

            }catch(ServicesDeskUcabWsException ex)
            {
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion.ToString();

            }
            return response;
        }

        /// <summary>
        /// Llama un servicio de ModeloJerarquicoDAO que actualiza 
        ///un objeto ModeloJerarquico
        /// </summary>
        /// <param name="dto">Un Objeto ModeloJerarquicoDTO. </param>
        /// <returns>Un ApplicationResponse que contiene el objeto actualizado
        /// satisfactoriamente. </returns>
         [HttpPut]
         [Route("ActualizaModeloJerarquico/")]
         public ApplicationResponse<ModeloJerarquicoDTO> ActualizarModeloJerarquico([FromBody] ModeloJerarquicoDTO dto)
         {
            var response = new ApplicationResponse<ModeloJerarquicoDTO>();
            try
            {                   
                    response.Data =_dao.ActualizarModeloJerarquicoDAO(ModeloJerarquicoMapper.DtoToEntity(dto));;
                    response.StatusCode = HttpStatusCode.OK;
                    response.Message = message;

            }catch(ServicesDeskUcabWsException ex)
            {
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion.ToString();

            }
             return  response;
         }

        /// <summary>
        /// Llama un servicio de ModeloJerarquicoDAO que agrega un objeto ModeloJerarquico
        /// </summary>
        /// <param name="id">Un valor de tipo int32. </param>
        /// <returns>Un ApplicationResponse que contiene el objeto eliminado
        /// satisfactoriamente. </returns>
        [HttpDelete]
        [Route("DeleteModeloJerarquico/{id}")]
        public ApplicationResponse<ModeloJerarquicoDTO> EliminarModeloJerarquico([FromRoute] int id)
        {
            var response = new ApplicationResponse<ModeloJerarquicoDTO>();
                try
                {
                    response.Data=  _dao.EliminarModeloJerarquicoDAO(id);
                    response.StatusCode = HttpStatusCode.OK;
                    response.Message = message;
                    
                }catch(ServicesDeskUcabWsException ex)
                {
                    response.Success = false;
                    response.Message = ex.Mensaje;
                    response.Exception = ex.Excepcion.ToString();
                    
                }
                return response;
        }
    }
}