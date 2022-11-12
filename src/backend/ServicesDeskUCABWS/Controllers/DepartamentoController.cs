using ServicesDeskUCABWS.BussinessLogic.Mapper;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Entity;
using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;


namespace ServicesDeskUCABWS.Controllers
{
    [ApiController]
    [Route("Departamento")]
    public class DepartamentoController : Controller
    {
        private readonly IDepartamentoDAO _dao;
        private readonly ILogger<DepartamentoController> _log;

        public DepartamentoController(ILogger<DepartamentoController> logger, IDepartamentoDAO dao)
        {
            _log = logger;
            _dao = dao;
        }

        [HttpPost]
        [Route("CreateDepartamento/")]
        public DepartamentoDTO CreateDepartamento([FromBody] DepartamentoDTO dto)
        {
            try
            {
                var data = _dao.AgregarDepartamentoDAO(DepartamentoMapper.DtoToEntity(dto));
                return data;

            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                throw ex.InnerException!;
            }
        }

        [HttpGet]
        [Route("ConsultaDepartamentos/")]
        public List<DepartamentoDTO> ConsultaDepartamentos()
        {
            try
            {
                var data = _dao.ConsultarDepartamentosDAO();
                return data;

            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                throw ex.InnerException!;
            }
        }

        [HttpPut]
        [Route("Actualizar/")]
        public DepartamentoDTO ActualizarDepartamento([Required][FromBody] DepartamentoDTO dto)
        {
            try
            {
                return _dao.ModificarDepartamentoDAO(DepartamentoMapper.DtoToEntity(dto));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw ex.InnerException!;
            }
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public DepartamentoDTO EliminarDepartamento([Required][FromRoute] int id)
        {
            try
            {

                return _dao.EliminarDepartamentoDAO(id);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw ex.InnerException!;
            }
        }
    }
}

