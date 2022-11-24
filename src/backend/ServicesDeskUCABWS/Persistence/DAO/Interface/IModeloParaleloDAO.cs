using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Entity;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface;

public interface IModeloParaleloDAO
{
    public Task<ActionResult> AgregarModeloParaleloDAO(ModeloParalelo ModeloParalelo);
    public IEnumerable<ModeloParalelo> ConsultarModelosParalelosDAO();
    public Task<ActionResult<ModeloParalelo>> ConsultaModeloParaleloDAO(int id);
    public Task ActualizarModeloParaleloDAO(int id, ModeloParalelo ModeloParalelo);
    public Task EliminarModeloParaleloDAO(int id);    
}