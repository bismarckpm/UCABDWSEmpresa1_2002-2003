using ServicesDeskUCABWS.BussinessLogic.DTO;
using AutoMapper;
using ServicesDeskUCABWS.Persistence.Entity;

namespace ServicesDeskUCABWS.BussinessLogic.Mapper
{
    public class ModeloJerarquicoMapper: Profile
    {

        public ModeloJerarquicoMapper()
        {
            CreateMap<ModeloJerarquico, ModeloJerarquicoDTO>();
            CreateMap<ModeloJerarquicoDTO, ModeloJerarquico>();        
        }

        public static ModeloJerarquicoDTO EntityToDto(ModeloJerarquico modeloJerarquico)
        {
            return new ModeloJerarquicoDTO()
            {
                id = modeloJerarquico.id,
                Nombre = modeloJerarquico.nombre,
                CategoriaId = modeloJerarquico.categoriaid
            };
        }

        public static ModeloJerarquico DtoToEntity(ModeloJerarquicoDTO dto)
        {
            return new ModeloJerarquico()
            {
                id = dto.id,
                nombre = dto.Nombre,
                categoriaid = dto.CategoriaId,
                Jeraruia = DtoToEntityList(dto.orden!)
            };
        }

        private static List<ModeloJerarquicoCargos> DtoToEntityList(List<JerarquicoTipoCargoDTO> dTO)
        {
            var lista = new List<ModeloJerarquicoCargos>();
            foreach (var item in dTO)
            {
                var jerarquicoTipo = new ModeloJerarquicoCargos()
                {
                    Id = item.Id,
                    modelojerarquicoid= item.modelojerarquicoid,
                    TipoCargoid = item.tipoCargoid,
                    orden = item.orden
                };

                lista.Add(jerarquicoTipo);
            }
            return lista;
        }

        public static List<JerarquicoTipoCargoDTO> EntityToDtoList(ICollection<ModeloJerarquicoCargos> entity)
        {
            var listaDTO = new List<JerarquicoTipoCargoDTO>();
            foreach (var item in entity)
            {
                var jerarquicoCargo = new JerarquicoTipoCargoDTO()
                {
                    orden = item.orden,
                    modelojerarquicoid = item.modelojerarquicoid,
                    tipoCargoid = item.TipoCargoid
                };

                listaDTO.Add(jerarquicoCargo);
            }
            return listaDTO;
        }
    }
}