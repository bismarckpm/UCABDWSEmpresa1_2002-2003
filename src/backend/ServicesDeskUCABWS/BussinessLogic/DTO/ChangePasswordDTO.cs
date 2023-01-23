using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCABWS.BussinessLogic.DTO
{
    public class ChangePasswordDTO
    {
        [Required]
        public string email {get; set;} = string.Empty;
        [Required,MinLength(8)]
        public string newpassword {get; set;} = string.Empty;
        [Required,Compare("newpassword")]
         public string confirmationpassword {get; set;} =string.Empty;
    }
}