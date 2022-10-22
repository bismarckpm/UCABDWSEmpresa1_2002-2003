using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface
{
    public interface ITipoCargoDAO
    {
        public List<TipoCargoDTO> ConsultarTipoCargoDAO();

        public TipoCargoDTO AgregarTipoCargoDAO(TipoCargo tipo);

        public TipoCargoDTO ActualizarTipoCargoDAO(TipoCargo tipoCargo);

        public TipoCargoDTO EliminarTipoCargoDAO(int id);
    }
}