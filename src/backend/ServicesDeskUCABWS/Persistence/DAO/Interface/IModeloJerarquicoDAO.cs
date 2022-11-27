using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface
{
    public interface IModeloJerarquicoDAO
    {
        public Task<List<ModeloJerarquico>> ConsultarModeloJerarquicosDAO();

        public Task<ActionResult<ModeloJerarquicoDTO>> AgregarModeloJerarquicoDAO(ModeloJerarquico modeloJerarquico);

        public Task<ActionResult<ModeloJerarquico>> ObtenerModeloJerarquicoDAO(int id);
        public Task<ActionResult<ModeloJerarquico>> ActualizarModeloJerarquicoDAO(ModeloJerarquicoCreateDTO modeloJerarquico, int id);

        public Task<ActionResult> EliminarModeloJerarquicoDAO(int id);
    }
}