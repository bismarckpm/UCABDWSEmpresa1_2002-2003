namespace ServicesDeskUCAB.DTO
{
    public class ErrotsDTO
    {
         public Errors errors { get; set; }
         public string type { get; set; }
         public string title { get; set; }
         public int status { get; set; }
         public string traceId { get; set; }
     }
    
     public class Errors
     {
         public string[] _ { get; set; }
     }
    
}