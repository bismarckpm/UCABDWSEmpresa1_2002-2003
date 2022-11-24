using ServicesDeskUCABWS.Persistence.Entity;
namespace ServicesDeskUCABWS.BussinessLogic.DTO;

public class ModeloParaleloCreateDTO
{
    public string? nombre { get; set;}        
    public int? cantidadAprobaciones { get; set;}
    public int categoriaId {get; set;}
    
}

public class ModeloParaleloDTO
{
    public string? nombre {get; set;}        
    public int? cantidadAprobaciones {get; set;}
    public Categoria? categoria {get; set;}
}