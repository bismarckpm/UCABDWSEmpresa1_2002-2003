using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ServicesDeskUCABWS.Controllers
{
    public class RegisroController : Controller 
    {
        public readonly UserManager<IdentityUser> _userManager;
        public readonly SignInManager<IdentityUser> _signInManager;
        public RegisroController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)

        {
            this._signInManager = signInManager;
            this._userManager = userManager;
        }

        
    }
}