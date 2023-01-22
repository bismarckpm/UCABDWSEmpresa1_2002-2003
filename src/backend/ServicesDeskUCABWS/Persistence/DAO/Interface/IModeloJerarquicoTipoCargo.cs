using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Entity;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface
{
    public interface IModeloJerarquicoTipoCargo
    {
        public JerarquicoTipoCargoDTO CreateJerarquicoTipoCargoDAO(ModeloJerarquicoCargos jc);
        public List<JerarquicoTipoCargoDTO> ListadoJerarquicoTipoCargoDAO();
        public JerarquicoTipoCargoDTO ObtenerJerarquicoTipoCargoDAO(int id);
        public JerarquicoTipoCargoDTO ActualizarJerarquicoTipoCargoDAO(ModeloJerarquicoCargos entity);
        public JerarquicoTipoCargoDTO EliminarJerarquicoTipoCargoDAO(int id);
    }
}