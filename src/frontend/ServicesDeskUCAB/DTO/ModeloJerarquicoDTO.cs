using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCAB.DTO
{
    public class ModeloJerarquicoDTO
    {
        public int id {get; set;}
        public string nombre {get; set;}
        public int categoriaId {get; set;}
        public List<JerarquicoTipoCargoDTO> orden {get; set;}
    }
}