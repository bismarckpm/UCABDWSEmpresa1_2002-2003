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
    [Route("JerarquicoTipoCargo/")]
    public class JerarquicoTipoCargoController : Controller
    {
        private readonly IModeloJerarquicoTipoCargo _dao;
        private readonly ILogger<JerarquicoTipoCargoController> _log;     
        const string message = "Solicitud Exitosa";   
        public JerarquicoTipoCargoController(IModeloJerarquicoTipoCargo dao, ILogger<JerarquicoTipoCargoController> log)
        {
            this._dao = dao;
            this._log = log;
        }

        [HttpPost]
        [Route("JerarquicoTCargo/")]
        public ApplicationResponse<JerarquicoTipoCargoDTO> AgregarJerarquicoTipoCargo([FromBody] JerarquicoTipoCargoDTO dto)
        {
            ApplicationResponse<JerarquicoTipoCargoDTO> response = new ApplicationResponse<JerarquicoTipoCargoDTO>();
            try
            {
                    response.Data = _dao.CreateJerarquicoTipoCargoDAO(JerarquicoTipoCargoMapper.DtoToEntity(dto));
                    response.StatusCode = HttpStatusCode.OK;
                    response.Message = message;

            }catch(Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                response.Exception = ex.Message + ex.StackTrace;
            }
            return response;
        }

        [HttpGet]
        [Route("ListJerarquicoTCargo/")]
        public ApplicationResponse<List<JerarquicoTipoCargoDTO>> ObtenerListadoJerarquicoTCargo()
        {
            ApplicationResponse<List<JerarquicoTipoCargoDTO>> response = new ApplicationResponse<List<JerarquicoTipoCargoDTO>>();
            try
            {
                response.Data = _dao.ListadoJerarquicoTipoCargoDAO();
                response.StatusCode = HttpStatusCode.OK;
                response.Message = message;
            }catch(Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                response.Exception = ex.Message + ex.StackTrace;     
            }
            return response;
        }

        [HttpGet]
        [Route("JerarquicoTCargo/{id}")]
        public ApplicationResponse<JerarquicoTipoCargoDTO> ObtenerJerarquicoTCargo([FromRoute] int id)
        {
            ApplicationResponse<JerarquicoTipoCargoDTO> response = new ApplicationResponse<JerarquicoTipoCargoDTO>();
            try
            {
                response.Data = _dao.ObtenerJerarquicoTipoCargoDAO(id);
                response.StatusCode = HttpStatusCode.OK;
                response.Message = message;
            }catch(Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                response.Exception = ex.Message +" || "+ ex.StackTrace;
            }
            return response;
        }

        [HttpPut]
        [Route("UpdateJerarquicoTC/")]
        public ApplicationResponse<JerarquicoTipoCargoDTO> ActualizarJerarquicoTCargo([FromBody] JerarquicoTipoCargoDTO dto)
        {
            ApplicationResponse<JerarquicoTipoCargoDTO> response = new ApplicationResponse<JerarquicoTipoCargoDTO>();
            try
            {
                response.Data = _dao.ActualizarJerarquicoTipoCargoDAO(JerarquicoTipoCargoMapper.DtoToEntity(dto));
                response.StatusCode = HttpStatusCode.OK;
                response.Message = message;

            }catch(Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                response.Exception = ex.Message +" || "+ ex.StackTrace; 
            }
            return response;
        }

        [HttpDelete]
        [Route("DeleteJerarquicoTC/{id}")]
        public ApplicationResponse<JerarquicoTipoCargoDTO> EliminarJerarquicoTCargo([FromRoute] int id)
        {
            ApplicationResponse<JerarquicoTipoCargoDTO> response = new ApplicationResponse<JerarquicoTipoCargoDTO>();
            try
            {
                    response.Data = _dao.EliminarJerarquicoTipoCargoDAO(id);
                    response.StatusCode = HttpStatusCode.OK;
                    response.Message = message;
            }catch(Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                response.Exception = ex.Message +" || "+ ex.StackTrace; 
            }
            return response;
        }

    }
}