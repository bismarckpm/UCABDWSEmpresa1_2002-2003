using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Entity;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface;

public interface IModeloParaleloDAO
{
    public Task<ActionResult<ModeloParaleloDTO>> AgregarModeloParaleloDAO(ModeloParalelo ModeloParalelo);
    public Task<List<ModeloParalelo>> ConsultarModelosParalelosDAO();
    public Task<ActionResult<ModeloParalelo>> ConsultaModeloParaleloDAO(int id);
    public Task<ActionResult<ModeloParalelo>> ActualizarModeloParaleloDAO(int id, ModeloParaleloCreateDTO ModeloParalelo);
    public Task<ActionResult> EliminarModeloParaleloDAO(int id);    
}