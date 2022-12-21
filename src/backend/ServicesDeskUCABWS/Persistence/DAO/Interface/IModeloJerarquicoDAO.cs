using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface
{
    public interface IModeloJerarquicoDAO
    {
        public List<ModeloJerarquicoDTO> ConsultarModeloJerarquicosDAO();
        public ModeloJerarquicoDTO AgregarModeloJerarquicoDAO(ModeloJerarquico modeloJerarquico);
        public ModeloJerarquicoDTO ObtenerModeloJerarquicoDAO(int id);
        public ModeloJerarquicoDTO ActualizarModeloJerarquicoDAO(ModeloJerarquico modeloJerarquico);
        public ModeloJerarquicoDTO EliminarModeloJerarquicoDAO(int id);
    }
}