using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface
{
    public interface ICategoriaDAO
    {
        CategoriaDTO AgregarCategoriaDAO(Categoria p);

        List<CategoriaDTO> ConsultarTodosCategoriasDAO();

        CategoriaDTO ActualizarCategoriaDAO(Categoria p);

        CategoriaDTO EliminarCategoriaDAO(int id);

        CategoriaDTO ConsultaCategoriaDAO(int id);
    }
}