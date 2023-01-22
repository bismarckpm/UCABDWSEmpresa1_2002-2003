using ServicesDeskUCABWS.Persistence.Entity;
namespace ServicesDeskUCABWS.BussinessLogic.DTO;

/*Clase encargada de convertir los datos de la Entidad de base de datos ModeloParalelo */
public class ModeloParaleloDTO
{
    public int Id {get; set;}
    public string? nombre {get; set;}        
    public int? cantidaddeaprobacion {get; set;}
    public int categoriaId {get; set;}
}

/*Clase encargada de convertir los datos para crear una nueva Entidad de base de datos ModeloParalelo */
public class ModeloParaleloCreateDTO
{
    public string? nombre {get; set;}        
    public int? cantidaddeaprobacion {get; set;}
    public int categoriaId {get; set;}
}