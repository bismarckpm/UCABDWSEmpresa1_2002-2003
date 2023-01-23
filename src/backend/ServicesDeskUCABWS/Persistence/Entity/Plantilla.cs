namespace ServicesDeskUCABWS.Persistence.Entity
{
    public class Plantilla
    {
        public int id { get; set; }
        public string? titulo { get; set; }

        public string? cuerpo { get; set; }

        public int EstadoId { get; set; }
        public Estado? estado { get; set; }

    }
}