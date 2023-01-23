 using ServicesDeskUCABWS.Persistence.Entity;
 
 namespace ServicesDeskUCABWS.BussinessLogic.DTO
 {
    public class ModeloJerarquicoDTO
    {  
        public int id {get; set;}
        public string? Nombre {get; set;}
        public int CategoriaId {get; set;}
    
        public List<JerarquicoTipoCargoDTO>? orden{get; set;}
    }
 }
 