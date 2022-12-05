namespace ServicesDeskUCABWS.Persistence.Entity;
public class ModeloJerarquico : ModeloAprobacion
{
     public ICollection<ModeloJerarquicoCargos>? Jeraruia { get; set; }
}