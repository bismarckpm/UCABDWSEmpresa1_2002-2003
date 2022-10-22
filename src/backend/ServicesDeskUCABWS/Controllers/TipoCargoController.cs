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
    [Route("TipoCargo")]
    public class TipoCargoController : ControllerBase
    {
        private readonly ITipoCargoDAO _dao;
        private readonly ILogger<TipoCargoController> _log;

        public TipoCargoController(ITipoCargoDAO dao, ILogger<TipoCargoController> log)
        {
            this._dao = dao;
            this._log = log;
        }


        [HttpPost]
        [Route("CreateTCargo/")]
        public ActionResult<TipoCargoDTO> AgregarTipoCargo([Required][FromBody] TipoCargoDTO dto1)
        {
            try
            {
                    var dao0 = _dao.AgregarTipoCargoDAO(TipoCargoMapper.DtoToEntity(dto1));                    
                    return dao0;
                    
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message + " | [Separador] | " + ex.StackTrace);
                throw ex.InnerException!;
            }
        }

        [HttpGet]
        [Route("ConsultaTCargo/")]
        public ActionResult<List<TipoCargoDTO>> ConsultaTipoCargo()
        {
            try
            {
                return _dao.ConsultarTipoCargoDAO();
            }catch(Exception ex)
            {
                  Console.WriteLine(ex.Message + " : " + ex.StackTrace);  
                throw ex.InnerException!;
            }
        }

        [HttpPut]
        [Route("ActualizarTCargo/")]
        public ActionResult<TipoCargoDTO> ActualizarTipoCargo([FromBody] TipoCargoDTO tipoCargo)
        {
            try
            {
                return _dao.ActualizarTipoCargoDAO(TipoCargoMapper.DtoToEntity(tipoCargo));

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw ex.InnerException!;
            }
        }

        [HttpDelete]
        [Route("EliminarTCargo/{id}")]
        public ActionResult<TipoCargoDTO> EliminarTipoCargo([FromRoute] int id)
        {
            try
            {
                return _dao.EliminarTipoCargoDAO(id);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw ex.InnerException!;
            }
        }
    }
}