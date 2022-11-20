namespace ServicesDeskUCABWS.Persistence.Entity;
public class ModeloJerarquico
{
    public int Id { get; set; }

    public string? Nombre {get; set;}
    
    public List<TipoCargo>? orden {get; set;}
    public FlujoAprobacion? flujoAprobacion{get; set;}
    public int CategoriaId { get; set; }
    public Categoria? categoria {get; set;}
}