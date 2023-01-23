using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface
{
    public interface IGrupoDAO
    {
        public Task<List<GrupoResponseDTO>> ObtenerGruposDAO();
        public Task<GrupoResponseDTO> ObtenerGrupoByIdDAO(int id);
        public Task<GrupoDTO> AgregarGrupoDAO(Grupo Grupo);
        public Task<GrupoDTO> ActualizarGrupoDAO(Grupo Grupo, int id);
        public Task<Boolean> EliminarGrupoDAO(int id);



    }
}
