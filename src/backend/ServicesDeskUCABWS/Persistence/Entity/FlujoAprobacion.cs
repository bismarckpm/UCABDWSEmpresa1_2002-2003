using MimeKit.Cryptography;

namespace ServicesDeskUCABWS.Persistence.Entity
{
    public class FlujoAprobacion
    {
        public int id { get; set; }
        public virtual Ticket? ticket {get; set;}
        public virtual ModeloJerarquico? modeloJerarquico {get; set; }
        public int modelojerarquicoid
        {
            get; set;
        }
        public virtual ModeloParalelo? modeloParalelo {get; set;}
        public int paraleloid {get; set;}
        public virtual Usuario? usuario {get;set;}
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