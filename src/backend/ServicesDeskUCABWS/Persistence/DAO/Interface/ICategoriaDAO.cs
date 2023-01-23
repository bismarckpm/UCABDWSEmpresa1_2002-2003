using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface
{
    public interface ICategoriaDAO
    {
        public CategoriaDTO AgregarCategoriaDAO(Categoria p);

        public List<CategoriaDTO> ConsultarTodosCategoriasDAO();

        public CategoriaDTO ActualizarCategoriaDAO(Categoria p);

        public CategoriaDTO EliminarCategoriaDAO(int id);

        public CategoriaDTO ConsultaCategoriaDAO(int id);
    }
}