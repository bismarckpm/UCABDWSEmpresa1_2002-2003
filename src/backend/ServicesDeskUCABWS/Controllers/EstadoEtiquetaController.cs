using ServicesDeskUCABWS.BussinessLogic.Mapper;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Database;
using ServicesDeskUCABWS.Persistence.Entity;
using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;


namespace ServicesDeskUCABWS.Controllers
{
    [ApiController]
    [Tags("Estado")]
    [Route("api/etiquetas/{etiquetaId:int}/estados")]
    public class EstadoEtiquetaController : Controller
    {
        private readonly IEstadoDAO _dao_Estado;
        private readonly IEtiquetaDAO _dao_Etiqueta;
        private readonly ILogger<EstadoEtiquetaController> _log;
        private readonly IMapper _mapper;

        public EstadoEtiquetaController(ILogger<EstadoEtiquetaController> logger, IEstadoDAO dao_Estado, IEtiquetaDAO dao_Etiqueta, IMapper mapper)
        {
            _log = logger;
            _dao_Estado = dao_Estado;
            _dao_Etiqueta = dao_Etiqueta;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EstadoEtiquetaDTO dto, [FromRoute][Required] int etiquetaId)
        {
            if (etiquetaId <= 0)
            {
                return BadRequest("El id de la etiqueta debe ser mayor a 0");
            }
            var etiqueta = await _dao_Etiqueta.ObtenerEtiquetaDAO(etiquetaId);
            if (etiqueta.Value!.id != etiquetaId)
            {
                return BadRequest("La etiqueta no existe");
            }

            var estado = _mapper.Map<Estado>(dto);
            estado.EtiquetaId = etiquetaId;
            var result = await _dao_Estado.AgregarEstadoDAO(estado);
            _log.LogInformation("Estado agregado exitosamente");
            return Ok(result);


        }

        [HttpGet]
        public async Task<ActionResult<List<EstadoDTO>>> Get([FromRoute][Required] int etiquetaId)
        {


            var result = await _dao_Estado.GetEstadosEtiquetaDAO(etiquetaId);
            _log.LogInformation("Estados obtenidos exitosamente");
            return Ok(result);


        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] EstadoEtiquetaUpdateDTO dto_update, [FromRoute][Required] int etiquetaId)
        {
            try
            {

                var etiquetaOld = await _dao_Etiqueta.ObtenerEtiquetaDAO(etiquetaId);

                if (etiquetaOld.Value!.id != etiquetaId)
                {
                    return BadRequest("La etiqueta no existe");
                }

                var etiquetaNew = await _dao_Etiqueta.ObtenerEtiquetaDAO(dto_update.New_EtiquetaId);

                if (etiquetaNew.Value!.id != dto_update.New_EtiquetaId)
                {
                    return BadRequest("La nueva etiqueta no existe");
                }

                var result = await _dao_Estado.ActualizarEstadoEtiquetaDAO(dto_update);
                _log.LogInformation("Cambio de etiqueta al estado exitoso");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                throw ex.InnerException!;
            }
        }

    }

}