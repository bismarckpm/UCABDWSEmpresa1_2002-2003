namespace ServicesDeskUCABWS.Persistence.Entity;
public class ModeloJerarquico
{
    public Guid jerarquicoId {get; set;}
    public string nombre {get; set;}
    public List<string> orden {get; set;}
    public int tipoCargo{get; set;}
    public virtual FlujoAprobacion flujoAprobacion{get; set;}
    public virtual Categoria categoria {get; set;}
}