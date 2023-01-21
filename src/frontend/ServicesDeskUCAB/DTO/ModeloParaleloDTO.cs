using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCAB.DTO
{
    public class ModeloParaleloDTO
    {
        public int id {get; set;}
        public string nombre {get; set;}
        public int categoriaId {get; set;}
        public int cantidaddeaprobacion {get; set;}
    }
}