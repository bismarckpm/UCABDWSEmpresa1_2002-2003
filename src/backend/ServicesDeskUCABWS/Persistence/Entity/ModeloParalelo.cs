using System.ComponentModel.DataAnnotations;
namespace ServicesDeskUCABWS.Persistence.Entity;

public class ModeloParalelo

{
    [Key]
    public int paraid {get; set; }
    public string? nombre {get; set;}
    public int? cantidadAprobaciones{get; set;}
    public int categoriaId {get; set;}
    public virtual Categoria? categoria {get; set;}
}