using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCAB.DTO
{
    public class ModeloJerarquicoDTO
    {


        public int id {get; set;}
        public string nombre {get; set;}

        [Required(ErrorMessage = "Seleccione una categoria")]
        public int categoriaId {get; set;}
        public List<JerarquicoTipoCargoDTO> orden {get; set;}
    }
}