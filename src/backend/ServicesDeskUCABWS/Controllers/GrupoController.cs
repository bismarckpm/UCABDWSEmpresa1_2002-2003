using ServicesDeskUCABWS.BussinessLogic.Mapper;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Entity;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.DAO.Implementations;

namespace ServicesDeskUCABWS.Controllers
{
    [ApiController]
    [Route("Grupo")]
    public class GrupoController : ControllerBase
    {
        private readonly IGrupoDAO _dao;
        private readonly ILogger<GrupoController> _log;


        public GrupoController(ILogger<GrupoController> logger, IGrupoDAO dao)
        {
            this._log = logger;
            this._dao = dao;
        }
        [HttpPost]
        [Route("CreateGrupo/")]

        public ActionResult<GrupoDTO> AgregarGrupo([FromBody] GrupoDTO dTo)
        {
            try
            {
                var dao = _dao.AgregarGrupoDAO(GrupoMapper.DtoToEntity(dTo));
                return dao;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "ex start trace ===" + ex.StackTrace);
                throw ex.InnerException!;
            }
        }

        [HttpGet]
        [Route("ConsultarGrupos/")]

        public ActionResult<List<GrupoDTO>> ConsultarGrupo()
        {
            try
            {
                return _dao.ConsultarGrupoDAO();
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }

        [HttpGet]
        [Route("ConsultaGrupo/{id}")]
        public ActionResult<GrupoDTO> ConsultaGrupoId([Required][FromRoute] int id)
        {
            try
            {
                var data = _dao.ConsultaGrupoIdDAO(id);
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
        public ActionResult<GrupoDTO> ActualizarGrupo([FromBody] GrupoDTO grupo)
        {
            try
            {
                return _dao.ActualizarGrupoDAO(GrupoMapper.DtoToEntity(grupo));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex.InnerException!;
            }
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public ActionResult<GrupoDTO> EliminarGrupo([FromRoute] int id)
        {
            try
            {
                return _dao.EliminarGrupoDAO(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw ex.InnerException!;
            }

        }
    }
}
