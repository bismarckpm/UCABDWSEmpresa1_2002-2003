using ServicesDeskUCABWS.BussinessLogic.Mapper;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using System.ComponentModel.DataAnnotations;
using ServicesDeskUCABWS.Exceptions;
using static ServicesDeskUCABWS.Reponses.AplicationResponse;
using System.Net;


namespace ServicesDeskUCABWS.Controllers
{
    [ApiController]
    [Route("Departamento")]
    public class DepartamentoController : Controller
    {
        //DECLARACION DE VARIABLES
        private readonly IDepartamentoDAO _dao;
        private readonly ILogger<DepartamentoController> _log;

        //MENSAJE SOLITUD EXITOSA
        static string MSG_SOL_EXITOSA = "Solicitud exitosa";

        //CONSTRUCTOR
        public DepartamentoController(ILogger<DepartamentoController> log, IDepartamentoDAO dao)
        {
            this._log = log;
            this._dao = dao;
        }

        //CREAR DEPARTAMENTO
        [HttpPost]
        [Route("CreateDepartamento/")]
        public ApplicationResponse<DepartamentoDTO> CreateDepartamento([FromBody] DepartamentoDTO dto)
        {
            var response = new ApplicationResponse<DepartamentoDTO>();
            try
            {
                response.Data = _dao.AgregarDepartamentoDAO(DepartamentoMapper.DtoToEntity(dto));
                response.Message = MSG_SOL_EXITOSA;
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                throw new PlantillaException("Error al crear departamento", ex, _log);
            }
            return response;
        }

        //CONSULTAR LISTA DEPARTAMENTOS
        [HttpGet]
        [Route("ConsultaDepartamentos/")]
        public ApplicationResponse<List<DepartamentoDTO>> ConsultaDepartamentos()
        {
            var response = new ApplicationResponse<List<DepartamentoDTO>>();
            try
            {
                response.Data = _dao.ConsultarDepartamentosDAO();
                if (!response.Data.Any())
                {
                    response.Message = "No existe ningun departamento en el sistema";
                }
                else
                {
                    response.Message = MSG_SOL_EXITOSA;
                }
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                throw new PlantillaException("Error al consultar departamentos", ex, _log);
            }
            return response;
        }

        //CONSULTAR UN DEPARTAMENTO MEDIANTE SU ID
        [HttpGet]
        [Route("ConsultaDepartamento/{id}")]
        public ApplicationResponse<DepartamentoDTO> ConsultaDepartamento([Required][FromRoute] int id)
        {
            var response = new ApplicationResponse<DepartamentoDTO>();
            try
            {
                response.Data = _dao.ConsultaUnDepartamentoDAO(id);
                response.Message = MSG_SOL_EXITOSA;
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                throw new PlantillaException("Error al buscar el departamento de id: " + id, ex, _log);
            }
            return response;
        }

        //ACTUALIZAR O MODIFICAR UN DEPARTAMENTO
        [HttpPut]
        [Route("Actualizar/")]
        public ApplicationResponse<DepartamentoDTO> ActualizarDepartamento([Required][FromBody] DepartamentoDTO dto)
        {
            var response = new ApplicationResponse<DepartamentoDTO>();
            try
            {
                response.Data = _dao.ModificarDepartamentoDAO(DepartamentoMapper.DtoToEntity(dto));
                response.Message = MSG_SOL_EXITOSA;
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                throw new PlantillaException("Error al actualizar el departamento de id: " + dto.Id, ex, _log);
            }
            return response;
        }

        //ELIMINAR UN DEPARTAMENTO
        [HttpDelete]
        [Route("Eliminar/{id}")]
        public ApplicationResponse<DepartamentoDTO> EliminarDepartamento([Required][FromRoute] int id)
        {
            var response = new ApplicationResponse<DepartamentoDTO>();
            try
            {
                response.Data = _dao.EliminarDepartamentoDAO(id);
                response.Message = MSG_SOL_EXITOSA;
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                throw new PlantillaException("Error al eliminar el departamento de id: " + id, ex, _log);
            }
            return response;
        }
    }
}

