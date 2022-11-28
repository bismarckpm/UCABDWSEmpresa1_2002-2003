using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;

namespace ServicesDeskUCABWS.BussinessLogic.Mapper
{
    public class CategoriaMapper
    {
        public static CategoriaDTO EntityToDto(Categoria categ)
        {
            return new CategoriaDTO()
            {
                Id = categ.id,
                Nombre = categ.nombre
            };
        }

        public static Categoria DtoToEntity(CategoriaDTO dto)
        {
            return new Categoria()
            {
                id = dto.Id,
                nombre = dto.Nombre
            };
        }
    }
}