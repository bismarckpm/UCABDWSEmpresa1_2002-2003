using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCAB.DTO
{
    public class JerarquicoTipoCargoDTO
    {
        public int id {get; set;}
        public int orden {get; set;}
        public int modelojerarquicoid {get; set;}
        public int tipoCargoid {get; set;}
    }
}