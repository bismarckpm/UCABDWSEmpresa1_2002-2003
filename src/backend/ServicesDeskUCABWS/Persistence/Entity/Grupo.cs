namespace ServicesDeskUCABWS.Persistence.Entity
{
    public class Grupo
    {
        public int id { get; set; }
        public string? nombre { get; set;}

        public ICollection<Usuario>? usuarios { get; set;}
    }
}