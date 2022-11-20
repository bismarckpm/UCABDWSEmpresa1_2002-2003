namespace ServicesDeskUCABWS.Persistence.Entity
{
    public class TipoCargo
    {
        public int id {get; set;}

        public string? nombre {get; set;}

       //Fk ModeloJerarquico
        public int ModeloJerarquicoId {get; set;}
        public ModeloJerarquico? modeloJerarquico {get; set;}
    }
}