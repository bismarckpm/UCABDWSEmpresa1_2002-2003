namespace ServicesDeskUCAB.DTO
{
    public class EmpleadosDTO
    {
        public int? id {get;set;}
        public string email {get; set;}
   public string Discriminator { get; set; }
        public int iddept {get;set;}
        public string dept {get;set;}    }
}