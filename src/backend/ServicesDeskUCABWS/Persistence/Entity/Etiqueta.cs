namespace ServicesDeskUCABWS.Persistence.Entity
{
    public class Etiqueta
    {
        public int id { get; set; }
        public string? nombre { get; set; }

        public string? descripcion { get; set; }

        public List<Estado>? estados { get; set; }

    }
}