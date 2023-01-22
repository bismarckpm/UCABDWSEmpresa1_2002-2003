using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Entity;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface;

public interface IModeloParaleloDAO
{
    public ModeloParaleloCreateDTO AgregarModeloParaleloDAO(ModeloParalelo modeloParalelo);
    public List<ModeloParaleloDTO> ConsultarModelosParalelosDAO();
    public ModeloParaleloDTO ObtenerModeloParaleloDAO(int id);
    public ModeloParaleloDTO ActualizarModeloParaleloDAO(ModeloParalelo modeloParalelo);
    public ModeloParaleloDTO EliminarModeloParaleloDAO(int id);    
}