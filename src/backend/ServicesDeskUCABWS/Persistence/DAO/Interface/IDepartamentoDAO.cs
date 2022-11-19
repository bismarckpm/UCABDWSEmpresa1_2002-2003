using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface
{
    public interface IDepartamentoDAO
    {
        DepartamentoDTO AgregarDepartamentoDAO(Departamento d);

        List<DepartamentoDTO> ConsultarDepartamentosDAO();

        DepartamentoDTO ModificarDepartamentoDAO(Departamento d);

        DepartamentoDTO EliminarDepartamentoDAO(int id);
    }
}