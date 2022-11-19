 namespace ServicesDeskUCABWS.BussinessLogic.DTO;
 
 public class ModeloJerarquicoDTO
 {  
    public Guid id {get; set;}
    public string nombre {get; set;}
    public List<string> orden {get; set;}
    public int tipoCargo{get; set;}
    public CategoriaDTO categoria {get; set;}
}