namespace ServicesDeskUCABWS.Persistence.Entity
{
    public class Departamento
    {
        public int id { get; set; }
        public string? nombre { get; set; }

        public ICollection<Grupo>? grupos { get; set;}
        public ICollection<Usuario> Usuarios {get;set;}
    }
}