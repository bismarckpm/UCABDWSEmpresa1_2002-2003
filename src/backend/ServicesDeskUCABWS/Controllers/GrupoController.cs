using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.BussinessLogic.Mapper;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Entity;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;


namespace ServicesDeskUCABWS.Controllers
{
    [ApiController]
    [Route("Grupo")]
    public class GrupoController : ControllerBase
    {
        private readonly ILogger<GrupoController> _log;
        private readonly IGrupoDAO _dao;
        public GrupoController(IGrupoDAO dao, ILogger<GrupoController> log)
        {
            this._dao = dao;
            this._log = log;
        }
        [HttpPost]
        [Route("CrearGrupo")]

        public ActionResult<GrupoDTO> AgregarGrupo([FromBody] GrupoDTO dTo)
        {
            try
            {
                var dao = _dao.AgregarGrupo(GrupoMapper.DtoToEntity(dTo));
                return dao;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "ex start trace ===" + ex.StackTrace);
                throw ex.InnerException!;
            }
        }
        [HttpDelete]
        [Route("EliminarGrupo")]
        public ActionResult<GrupoDTO> EliminarGrupo([FromRoute] int id)
        {
            try
            {
                return _dao.EliminarGrupo(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex.InnerException!;
            }
        }

        [HttpPut]
        [Route("ActualizarGrupo")]
        public ActionResult<GrupoDTO > ActualizarGrupo([FromBody]GrupoDTO grupo)
        {
            try
            {
                return _dao.ActualizarGrupo(GrupoMapper.DtoToEntity(grupo));
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex.InnerException!;
            }
        }

        [HttpGet]
        [Route("ConnsultarGrupo")]
        public ActionResult<List<GrupoDTO>> ConsultarGrupo()
        {
            try
            {
                return _dao.ConsultarGrupo();
            }catch(Exception ex)
            {
                throw ex.InnerException!;
            }
        }

    }   


}
