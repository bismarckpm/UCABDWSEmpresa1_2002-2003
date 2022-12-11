namespace ServicesDeskUCABWS.Persistence.Entity
{
    public class ModeloAprobacion
    {
        public int id {get;set;}
        public string nombre {get;set;}
        public Categoria? categoria {get;set;}
    }
}