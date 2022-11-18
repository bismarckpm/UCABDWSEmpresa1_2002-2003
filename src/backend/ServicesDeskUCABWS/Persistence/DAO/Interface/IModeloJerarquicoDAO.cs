using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Entity;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface;

public interface IModeloJerarquicoDAO
{
    ModeloJerarquicoDTO AgregarModeloJerarquicoDAO(ModeloJerarquico modeloJerarquico);

    List<ModeloJerarquicoDTO> ConsultarModelosJerarquicosDAO();

    ModeloJerarquicoDTO ActualizarModeloJerarquicoDAO(ModeloJerarquico modeloJerarquico);

    ModeloJerarquicoDTO EliminarModeloJerarquicoDAO(string id);

    ModeloJerarquicoDTO ConsultaModeloJerarquicoDAO(string id);
}