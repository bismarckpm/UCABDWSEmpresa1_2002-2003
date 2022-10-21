namespace ServicesDeskUCABWS.Persistence.Entity
{
    public class Usuario
    {
        public int id {get; set;}
        public string? username {get; set;}

        public string? password {get; set;}

        public string? email {get; set;}

        public Cargo? cargoUser { get; set; }
    }   
}