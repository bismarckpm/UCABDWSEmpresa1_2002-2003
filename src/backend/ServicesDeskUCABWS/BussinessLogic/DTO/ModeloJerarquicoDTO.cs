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
        public string? Nombre {get; set;}
        
        public List<TipoCargo>? orden {get; set;}
        public Categoria? categoria {get; set;}
    }
 }
 