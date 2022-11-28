namespace ServicesDeskUCABWS.Persistence.Entity
{
    public class Categoria
    {
        public int id {get; set;}
        public string? nombre { get; set;}

        public ICollection<ModeloJerarquico>? modelosjerruicos {get;set;}
        public ICollection<ModeloParalelo>? ModeloParalelos {get;set;}

    }
}