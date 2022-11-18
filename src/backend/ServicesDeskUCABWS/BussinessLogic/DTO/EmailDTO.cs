using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCABWS.BussinessLogic.DTO
{
    public class EmailDTO
    {
        [Required,EmailAddress]
        public string? para {get; set;}
        [Required]
        public string? asunto {get; set;}
         [Required]
        public string? Cuerpo {get; set;}
    }
}