namespace ServicesDeskUCABWS.Persistence.Entity;

public class ModeloParalelo : FlujoAprobacion
{
    public Guid paraleloId {get; set;}
    public string? nombre {get; set;}
    public int CantidadAprobaciones{get; set;}
}