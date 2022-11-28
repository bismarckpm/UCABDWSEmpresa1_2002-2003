namespace ServicesDeskUCABWS.Persistence.Entity
{
    public class Cargo
    {
        public int id {get; set;}

        public string? nombre {get; set;}
        public int tipoCargoId { get; set; }
        public TipoCargo? tipoCargo { get; set; }

        public ICollection<Usuario> Usuarios {get;set;}
    }
}