using ServicesDeskUCABWS.BussinessLogic.Mapper;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Entity;
using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using ServicesDeskUCABWS.Exceptions;
using static ServicesDeskUCABWS.Reponses.AplicationResponse;
using ServicesDeskUCABWS.Reponses;
using System.Net;

namespace ServicesDeskUCABWS.Controllers
{
    [ApiController]
    [Route("Categoria")]
    public class CategoriaController : Controller
    {
        //DECLARACION DE VARIABLES
        private readonly ICategoriaDAO _dao;
        private readonly ILogger<CategoriaController> _log;

        //CONSTANTE DE MENSAJE SOLITUD EXITOSA
        static string MSG_SOL_EXITOSA = "Solicitud exitosa";

        //CONSTRUCTOR
        public CategoriaController(ICategoriaDAO dao, ILogger<CategoriaController> logger)
        {
            _log = logger;
            _dao = dao;
        }


        //ENDPOINT PARA CREAR UNA CATEGORIA
        [HttpPost]
        [Route("CreateCategoria/")]
        public ApplicationResponse<CategoriaDTO> CreateCategoria([FromBody] CategoriaDTO dto1)
        {
            var response = new ApplicationResponse<CategoriaDTO>();
            try
            {               
                response.Data =  _dao.AgregarCategoriaDAO(CategoriaMapper.DtoToEntity(dto1));
                response.Message = MSG_SOL_EXITOSA;
                response.StatusCode = HttpStatusCode.OK;

            }
            catch(ServicesDeskUcabWsException ex)
            {
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }


        //ENDPOINT PARA CONSULTAR TODAS LAS CATEGORIAS
        [HttpGet]
        [Route("ConsultaCategorias/")]
        public ApplicationResponse<List<CategoriaDTO>> ConsultaCategorias()
        {
            var response = new ApplicationResponse<List<CategoriaDTO>>();
            try
            {
                response.Data = _dao.ConsultarTodosCategoriasDAO();
                response.Message = MSG_SOL_EXITOSA;
                response.StatusCode = HttpStatusCode.OK;

            }
            catch(ServicesDeskUcabWsException ex)
            {
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }


        //ENDPOINT PARA CONSULTAR CATEGORIA ESPECIFICA
        [HttpGet]
        [Route("ConsultaCategoria/{id}")]
        public ApplicationResponse<CategoriaDTO> ConsultaCategoria([Required][FromRoute] int id)
        {
            var response = new ApplicationResponse<CategoriaDTO>();
            try
            {
                response.Data = _dao.ConsultaCategoriaDAO(id);
                response.Message = MSG_SOL_EXITOSA;
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (ServicesDeskUcabWsException ex)
            {
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion.ToString();
            } 
            return response;
        }


        //ENDPOINT PARA ACTUALIZAR CATEGORIA
        [HttpPut]
        [Route("Actualizar/")]
        public ApplicationResponse<CategoriaDTO> ActualizarCategoria([Required][FromBody] CategoriaDTO dto)
        {
            var response = new ApplicationResponse<CategoriaDTO>();
            try
            {
                response.Data = _dao.ActualizarCategoriaDAO(CategoriaMapper.DtoToEntity(dto));
                response.Message = MSG_SOL_EXITOSA;
                response.StatusCode = HttpStatusCode.OK;

            }
            catch(ServicesDeskUcabWsException ex)
            {
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }


        //ENDPOINT PARA ELIMINAR CATEGORIA
        [HttpDelete]
        [Route("Eliminar/{id}")]
        public ApplicationResponse<CategoriaDTO> EliminarCategoria([Required][FromRoute] int id)
        {
            var response = new ApplicationResponse<CategoriaDTO>();
            try
            {

                response.Data = _dao.EliminarCategoriaDAO(id);
                response.Message = MSG_SOL_EXITOSA;
                response.StatusCode = HttpStatusCode.OK;

            }
            catch(ServicesDeskUcabWsException ex)
            {
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }
    }

}