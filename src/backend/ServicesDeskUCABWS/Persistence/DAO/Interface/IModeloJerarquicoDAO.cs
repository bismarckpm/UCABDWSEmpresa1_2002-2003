using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface
{
    public interface IModeloJerarquicoDAO
    {
        public List<ModeloJCDTO> ConsultarModeloJerarquicosDAO();
        public ModeloJerarquicoDTO AgregarModeloJerarquicoDAO(ModeloJerarquico modeloJerarquico);
        public ModeloJCDTO ObtenerModeloJerarquicoDAO(int id);
        public ModeloJerarquicoDTO ActualizarModeloJerarquicoDAO(ModeloJerarquico modeloJerarquico);
        public ModeloJerarquicoDTO EliminarModeloJerarquicoDAO(int id);
    }
}