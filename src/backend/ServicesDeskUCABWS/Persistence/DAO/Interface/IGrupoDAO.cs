using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface
{
       public interface IGrupoDAO
        {
            public List<GrupoDTO> ConsultarGrupo();

            public GrupoDTO ActualizarGrupo(Grupo grupo);

            public GrupoDTO EliminarGrupo(int id);

            public GrupoDTO AgregarGrupo(Grupo grupo);

        }
    
}
