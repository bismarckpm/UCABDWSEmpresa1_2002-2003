namespace ServicesDeskUCABWS.Persistence.Entity
{

    public class Notification
    {
        public int id {get; set;}
        public string? titulo {get; set;}
        public string? fecha {get; set;}
        public string? descripcion {get; set;}
        public Usuario? usuario {get; set;}
    }
}

