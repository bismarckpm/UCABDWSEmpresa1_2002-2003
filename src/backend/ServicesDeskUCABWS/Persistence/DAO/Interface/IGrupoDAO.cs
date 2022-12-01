using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using Microsoft.EntityFrameworkCore.Storage;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface
{
    public interface IGrupoDAO
    {
        public GrupoDTO AgregarGrupoDAO(Grupo grupo);

        public List<GrupoDTO> ConsultarGrupoDAO();

        public GrupoDTO ActualizarGrupoDAO(Grupo grupo);

        public GrupoDTO EliminarGrupoDAO(int id);

        public GrupoDTO ConsultaGrupoIdDAO(int id);

    }
}
