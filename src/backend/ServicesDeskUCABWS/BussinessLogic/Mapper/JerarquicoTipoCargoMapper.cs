using ServicesDeskUCABWS.BussinessLogic.DTO;
using AutoMapper;
using ServicesDeskUCABWS.Persistence.Entity;

namespace ServicesDeskUCABWS.BussinessLogic.Mapper
{
    public static class JerarquicoTipoCargoMapper
    {
        public static JerarquicoTipoCargoDTO EntityToDTO(ModeloJerarquicoCargos jc)
        {
            return new JerarquicoTipoCargoDTO()
            {
                Id = jc.Id,
                tipoCargoid = jc.TipoCargoid,
                modelojerarquicoid = jc.modelojerarquicoid,
                orden = jc.orden
            };
        }

        public static ModeloJerarquicoCargos DtoToEntity(JerarquicoTipoCargoDTO dto)
        {
            return new ModeloJerarquicoCargos()
            {
                Id =dto.Id,
                TipoCargoid = dto.tipoCargoid,
                modelojerarquicoid = dto.modelojerarquicoid,
                orden = dto.orden
            };
        }
    }
}