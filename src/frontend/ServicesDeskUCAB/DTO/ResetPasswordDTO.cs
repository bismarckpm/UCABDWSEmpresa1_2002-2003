using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCAB.DTO
{
    public class ResetPasswordDTO
    {
              [Required]
        public string token {get; set;} = string.Empty;
        [Required,MinLength(8)]
        public string Password {get; set;} = string.Empty;
        [Required,Compare("Password")]
         public string confirmationpassword {get; set;} =string.Empty;
    }
    }
