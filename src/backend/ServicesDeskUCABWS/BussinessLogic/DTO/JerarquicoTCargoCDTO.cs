namespace ServicesDeskUCABWS.BussinessLogic.DTO
{
    public class JerarquicoTCargoCDTO
    {
        public int Id { get; set; }
        public int orden {get;set;}
        public int modelojerarquicoid { get; set; } 
        public string? modelo { get; set; } 
        public int tipoCargoid {get; set;}        
        public string? tipocargo {get; set;}
        
    }
}