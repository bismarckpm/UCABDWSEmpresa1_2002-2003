using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface
{
    public interface IEstadoDAO
    {
        public Task<ActionResult<List<EstadoDTO>>> GetEstadosDAO();
        public Task<ActionResult<EstadoDTO>> GetEstadoDAO(int id);
        public Task<ActionResult<List<EstadoDTO>>> GetEstadosEtiquetaDAO(int idEtiqueta);
        public Task<ActionResult<EstadoEtiquetaDTO>> AgregarEstadoDAO(Estado Estado);
        public Task<ActionResult> ActualizarEstadoDAO(Estado Estado, int id);
        public Task<ActionResult> EliminarEstadoDAO(int id);

        public Task<ActionResult> ActualizarEstadoEtiquetaDAO(EstadoEtiquetaUpdateDTO estadoEtiquetaUpdateDTO);
    }
}