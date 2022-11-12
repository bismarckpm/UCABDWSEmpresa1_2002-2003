namespace ServicesDeskUCABWS.Persistence.Entity
{
    public class FlujoAprobacion
    {
        public virtual Ticket? ticket {get; set;}
        public virtual ModeloJerarquico? modeloJerarquico {get; set;}
        public virtual ModeloParalelo? modeloParalelo {get; set;}
        public virtual Usuario? usuario {get;set;}
        public ICollection<Categoria> categorias {get;set;}
        public int secuencia {get;set;}
        public Status status {get; set;}
    }

    public enum Status
    {
        Pendiente,
        Aprobado,
        Rechazado
    }
}