namespace ServicesDeskUCABWS.Persistence.Entity;

public class ModeloParalelo

{
    public Guid id { get; set; }
    public Guid paraleloId {get; set;}
    public string nombre {get; set;}
    public int cantidadAprobaciones{get; set;}
    public virtual Categoria categoria {get; set;}
}