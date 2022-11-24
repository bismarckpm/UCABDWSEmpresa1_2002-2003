using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface
{
    public interface IDepartamentoDAO
    {
        public DepartamentoDTO AgregarDepartamentoDAO(Departamento d);

        public List<DepartamentoDTO> ConsultarDepartamentosDAO();

        public DepartamentoDTO ModificarDepartamentoDAO(Departamento d);

        public DepartamentoDTO EliminarDepartamentoDAO(int id);

        public DepartamentoDTO ConsultaUnDepartamentoDAO(int id);
    }
}