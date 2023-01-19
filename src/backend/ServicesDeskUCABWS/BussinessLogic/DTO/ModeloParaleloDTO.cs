using ServicesDeskUCABWS.Persistence.Entity;
namespace ServicesDeskUCABWS.BussinessLogic.DTO;

public class ModeloParaleloDTO
{
    public int paraid {get; set;}
    public string? nombre {get; set;}        
    public int? cantidaddeaprobacion {get; set;}
    public int categoriaId {get; set;}
}