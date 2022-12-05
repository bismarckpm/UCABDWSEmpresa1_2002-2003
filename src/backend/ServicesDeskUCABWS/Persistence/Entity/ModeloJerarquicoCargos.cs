namespace ServicesDeskUCABWS.Persistence.Entity
{
    public class ModeloJerarquicoCargos
    {
          public int Id { get; set; }
          public int orden {get;set;}
         public int modelojerauicoid { get; set; } 
         public virtual ModeloAprobacion? ModeloAprobacion {get;set;}

        public int? TipoCargoid { get; set; } 
        public virtual TipoCargo? TipoCargo { get; set; }
    }
}