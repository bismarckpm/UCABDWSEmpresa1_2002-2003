namespace ServicesDeskUCABWS.BussinessLogic.DTO;

public class ModeloParaleloDTO
{
    public Guid id {get; set;}
    public string nombre {get; set;}
    public int cantidadAprobaciones{get; set;}
    public CategoriaDTO categoria {get; set;}
}