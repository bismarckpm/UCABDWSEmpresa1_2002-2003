using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Entity;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface;

public interface IModeloParaleloDAO
{
    ModeloParaleloDTO AgregarModeloParaleloDAO(ModeloParalelo ModeloParalelo);

    List<ModeloParaleloDTO> ConsultarModelosParalelosDAO();

    ModeloParaleloDTO ActualizarModeloParaleloDAO(ModeloParalelo ModeloParalelo);

    ModeloParaleloDTO EliminarModeloParaleloDAO(string id);

    ModeloParaleloDTO ConsultaModeloParaleloDAO(string id);
}