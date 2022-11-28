using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface
{
    public interface IEtiquetaDAO
    {
        public Task<List<Etiqueta>> ConsultarEtiquetasDAO();

        public Task<ActionResult<EtiquetaDTO>> AgregarEtiquetaDAO(Etiqueta etiqueta);

        public Task<ActionResult<Etiqueta>> ObtenerEtiquetaDAO(int id);
        public Task<ActionResult<Etiqueta>> ActualizarEtiquetaDAO(Etiqueta etiqueta, int id);

        public Task<ActionResult> EliminarEtiquetaDAO(int id);
    }
}