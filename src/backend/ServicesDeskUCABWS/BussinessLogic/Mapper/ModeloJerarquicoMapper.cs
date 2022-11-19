using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Entity;

namespace ServicesDeskUCABWS.BussinessLogic.Mapper;

public class ModeloJerarquicoMapper
{
    public static ModeloJerarquicoDTO EntityToDto(ModeloJerarquico modeloJerarquico)
    {
        return new ModeloJerarquicoDTO()
        {
            id = modeloJerarquico.jerarquicoId,
            nombre = modeloJerarquico.nombre,
            orden = modeloJerarquico.orden,
            tipoCargo = modeloJerarquico.tipoCargo,
            categoria = CategoriaMapper.EntityToDto(modeloJerarquico.categoria)
        };
    }
    
    public static ModeloJerarquico DtoToEntity(ModeloJerarquicoDTO modeloJerarquicoDTO)
    {
        return new ModeloJerarquico()
        {
           jerarquicoId = modeloJerarquicoDTO.id, 
           nombre = modeloJerarquicoDTO.nombre,
           orden = modeloJerarquicoDTO.orden,
           categoria = CategoriaMapper.DtoToEntity(modeloJerarquicoDTO.categoria)
        };
    }
}