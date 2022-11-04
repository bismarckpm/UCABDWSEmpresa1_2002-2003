namespace ServicesDeskUCABWS.Persistence.Entity
{
    public class Cargo
    {
        public int id {get; set;}

        public string? nombre {get; set;}
        public virtual TipoCargo? tipocargo { get; set; }
        public ICollection<Usuario>? Usuarios {get;set;}
    }
}