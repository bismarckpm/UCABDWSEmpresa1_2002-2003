using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;

namespace ServicesDeskUCABWS.BussinessLogic.Mapper
{
    public class DepartamentoMapper
    {
        public static DepartamentoDTO EntityToDto(Departamento depa)
        {
            return new DepartamentoDTO()
            {
                Id = depa.id,
                Nombre = depa.nombre
            };
        }

        public static Departamento DtoToEntity(DepartamentoDTO dto)
        {
            return new Departamento()
            {
                id = dto.Id,
                nombre = dto.Nombre
            };
        }
    }
}