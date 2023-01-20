using ServicesDeskUCABWS.BussinessLogic.Mapper;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Entity;
using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using ServicesDeskUCABWS.Exceptions;
using static ServicesDeskUCABWS.Reponses.AplicationResponse;
using System.Net;

namespace ServicesDeskUCABWS.Controllers
{
    [ApiController]
    [Route("TipoCargo")]
    public class TipoCargoController : ControllerBase
    {
        private readonly ITipoCargoDAO _dao;
        private readonly ILogger<TipoCargoController> _log;
        const string message = "Solicitud Exitosa";
        public TipoCargoController(ITipoCargoDAO dao, ILogger<TipoCargoController> log)
        {
            this._dao = dao;
            this._log = log;
        }

        /// <summary>
        /// Llama un servicio de TipoCargoDAO que agrega un objeto TipoCargo
        /// </summary>
        /// <param name="dto1">Un Objeto TipoCargoDTO. </param>
        /// <returns>Un ApplicationResponse que contiene el objeto creado
        /// satisfactoriamente. </returns>
        [HttpPost]
        [Route("CreateTCargo/")]
        public ApplicationResponse<TipoCargoDTO> AgregarTipoCargo([FromBody] TipoCargoDTO dto1)
        {
            var response = new ApplicationResponse<TipoCargoDTO>();
            try
            {
                    response.Data = _dao.AgregarTipoCargoDAO(TipoCargoMapper.DtoToEntity(dto1));
                    response.StatusCode = HttpStatusCode.OK;
                    response.Message = message;                    
                                        
            }catch(ServicesDeskUcabWsException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
                _log.LogError("[Error al crear]: " + ex.Message + ", [Ubicado]: " + ex.StackTrace);
            }
            return response;
        }

        /// <summary>
        /// Llama un servicio del TipoCargoDAO que realiza una consulta
        /// </summary>
        /// <returns>Un ApplicationResponse que contiene el listado de todos
        /// los TipoCargo. </returns>
        [HttpGet]
        [Route("ConsultaTCargo/")]
        public ApplicationResponse<List<TipoCargoDTO>> ConsultaTipoCargo()
        {
            var response = new ApplicationResponse<List<TipoCargoDTO>>();
            try
            {
                response.Data= _dao.ConsultarTipoCargoDAO();
                response.StatusCode = HttpStatusCode.OK;
                response.Message = message;
    
            }catch(ServicesDeskUcabWsException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
                _log.LogError("[Error al Consultar]: " + ex.Message +", [Ubicado]: "+ ex.StackTrace);
            }
            return response;
        }

        /// <summary>
        /// Llama un Servicio del TipoCargoDAO para actualizar el objeto TipoCargo
        /// </summary>
        /// <param name="TipoCargoDTO"> es un Objeto DTO de TipoCargo</param>
        /// <returns>Un applicationResponse que contiene el objeto TipoCargoDTO</returns>
        [HttpPut]
        [Route("ActualizarTCargo/")]
        public ApplicationResponse<TipoCargoDTO> ActualizarTipoCargo([FromBody] TipoCargoDTO tipoCargo)
        {
            var response = new ApplicationResponse<TipoCargoDTO>();
            try
            {
                response.Data = _dao.ActualizarTipoCargoDAO(TipoCargoMapper.DtoToEntity(tipoCargo));
                response.StatusCode = HttpStatusCode.OK;
                response.Message = message;
                

            }catch(ServicesDeskUcabWsException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
                _log.LogError("[Error al crear]: " + ex.Message + ", [Ubicado]: " + ex.StackTrace);
            }
            return response;
        }

        /// <summary>
        /// Llama un servicio de TipoCargoDAO para borrar un objeto mediante su id
        /// </summary>
        /// <param name="id">codigo del objeto que sera borrado</param>
        /// <returns>Retorna un ApplicationResponse que contiene el TipoCargo que fue 
        /// borrado. </returns>
        [HttpDelete]
        [Route("EliminarTCargo/{id}")]
        public ApplicationResponse<TipoCargoDTO> EliminarTipoCargo([FromRoute] int id)
        {
            var response = new ApplicationResponse<TipoCargoDTO>();
            try
            {
                response.Data = _dao.EliminarTipoCargoDAO(id);
                response.StatusCode = HttpStatusCode.OK;
                response.Message = message;
                

            }catch(ServicesDeskUcabWsException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                
                _log.LogError("[Error al crear]: " + ex.Message + ", [Ubicado]: " + ex.StackTrace);
            }
            return response;
        }        
    }
}