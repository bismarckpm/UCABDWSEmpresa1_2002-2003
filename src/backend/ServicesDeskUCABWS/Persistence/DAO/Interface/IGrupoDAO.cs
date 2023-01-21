using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using Microsoft.EntityFrameworkCore.Storage;


namespace ServicesDeskUCABWS.Persistence.DAO.Interface
{
    public interface IGrupoDAO
    {
        /*public List<GrupoDTO> ConsultarGrupo();

        public GrupoDTO ActualizarGrupo(Grupo grupo);


        public GrupoDTO EliminarGrupo(int id);

        public GrupoDTO AgregarGrupo(Grupo grupo);*/

        
        public Task<List<Grupo>> ConsultarGrupoDAO();

        public Task<ActionResult<Grupo>> ConsultarGrupoByIdDAO(int id);

        public Task<ActionResult<GrupoDTO>> AgregarGrupoDAO(Grupo grupo);

        public Task<ActionResult<Grupo>> ActualizarGrupoDAO(Grupo grupo, int id);

        public Task<ActionResult> EliminarGrupoDAO(int id);
         
    }
}
