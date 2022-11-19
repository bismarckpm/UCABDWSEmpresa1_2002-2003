using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Entity;

namespace ServicesDeskUCABWS.BussinessLogic.Mapper;

public class ModeloParaleloMapper
{
    public static ModeloParaleloDTO EntityToDto(ModeloParalelo modeloParalelo)
    {
        return new ModeloParaleloDTO()
        {
            id = modeloParalelo.paraleloId,
            nombre = modeloParalelo.nombre,
            cantidadAprobaciones = modeloParalelo.cantidadAprobaciones,
            categoria = CategoriaMapper.EntityToDto(modeloParalelo.categoria)
        };
    }
    
    public static ModeloParalelo DtoToEntity(ModeloParaleloDTO modeloParaleloDTO)
    {
        return new ModeloParalelo()
        {
           paraleloId = modeloParaleloDTO.id, 
           nombre = modeloParaleloDTO.nombre,
           cantidadAprobaciones = modeloParaleloDTO.cantidadAprobaciones,
           categoria = CategoriaMapper.DtoToEntity(modeloParaleloDTO.categoria)
        };
    }
}