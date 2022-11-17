namespace ServicesDeskUCABWS.Persistence.Entity
{
    public class Plantilla
    {
        public int id { get; set; }
        public string? titulo { get; set; }

        public string? cuerpo { get; set; }

        public string? tipo { get; set; }


        public List<Notification>? notifications { get; set; }

    }
}