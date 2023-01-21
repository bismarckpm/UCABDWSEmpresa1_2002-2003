namespace ServicesDeskUCABWS.Persistence.Entity
{
    public class ModeloJerarquicoCargos
    {
        public int Id { get; set; }
        public int orden {get;set;}
        public int modelojerarquicoid { get; set; } 
        public virtual ModeloJerarquico? jerarquico {get;set;}
        public int TipoCargoid { get; set; } 
        public virtual TipoCargo? TipoCargo { get; set; }
    }
}