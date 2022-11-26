 using ServicesDeskUCABWS.Persistence.Entity;
 
 namespace ServicesDeskUCABWS.BussinessLogic.DTO
 {
    public class ModeloJerarquicoCreateDTO
    {  
    
        public string? Nombre {get; set;}
        
        public List<TipoCargo>? orden {get; set;}
        public int CategoriaId {get; set;}
    
    }

    public class ModeloJerarquicoDTO
    {
        public int id {get; set;}
        public string? Nombre {get; set;}        
        public List<TipoCargo>? orden {get; set;}
        public int CategoriaId {get; set;}
    }
 }
 