namespace ServicesDeskUCABWS.Persistence.Entity;
public class ModeloJerarquico
{
    public Guid jerarquicoId {get; set;}
    public List<string>? orden {get; set;}
    public int tipoCargo{get; set;}
}