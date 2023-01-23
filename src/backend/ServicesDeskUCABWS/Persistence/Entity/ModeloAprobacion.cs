namespace ServicesDeskUCABWS.Persistence.Entity
{
    public abstract class ModeloAprobacion
    {
        public int id {get;set;}
        public string? nombre {get;set;}
        public int categoriaid {get; set;}
        public Categoria? categoria {get;set;}
        public string? Discriminator {get; set;}
    }
}