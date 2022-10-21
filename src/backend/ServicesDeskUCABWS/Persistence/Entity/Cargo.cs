namespace ServicesDeskUCABWS.Persistence.Entity
{
    public class Cargo
    {
        public int id {get; set;}

        public string? nombre {get; set;}

        public int tipoCargoId {get; set;}

        public TipoCargo? tipoCargo {get; set;}

        public int UserId { get; set;}
        public Usuario? user { get; set; }
    }
}