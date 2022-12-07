namespace ServicesDeskUCABWS.Persistence.Entity
{
    public class Departamento
    {
        public int id { get; set; }
        public string? nombre { get; set; }

        
        public ICollection<Ticket> Tickets {get;set;}
        public ICollection<Grupo> Grupos {get;set;}
    }
}