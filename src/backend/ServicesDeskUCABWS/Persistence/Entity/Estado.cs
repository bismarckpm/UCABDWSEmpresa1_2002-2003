namespace ServicesDeskUCABWS.Persistence.Entity
{
    public class Estado
    {
        public int id { get; set; }

        public string? nombre { get; set; }

        public int EtiquetaId { get; set; }

        public Etiqueta? etiqueta { get; set; }

        //public Notification? notification { get; set; }

        public ICollection<Ticket> tickets {get;set;}
    }
}