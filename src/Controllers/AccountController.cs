using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WikiCore.Configuration;
using WikiCore.DB;

namespace WikiCore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IOptions<ApplicationConfigurations> _options;
        private readonly IDBService _dbs;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IOptions<ApplicationConfigurations> options, IDBService dbs)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._options = options;
            this._dbs = dbs;
        }

        public IActionResult Register()
        {                                    
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string password, string repassword)
        {
            //Even if registration is disabled, if there is no other user one user can register
            if(!_options.Value.EnableRegistration && _dbs.SomeUserRegistered())
            {
                ModelState.AddModelError(string.Empty, "Registration is disabled.");
                return View();
            }

            if(string.IsNullOrEmpty(password))
            { 
                ModelState.AddModelError(string.Empty, "You need a password.");
                return View();
            }

            if (password != repassword)
            {
                ModelState.AddModelError(string.Empty, "Password don't match");
                return View();
            }

            var newUser = new IdentityUser 
            {
                UserName = username,
                EmailConfirmed = true,
            };

            var userCreationResult = await _userManager.CreateAsync(newUser, password);
            if (!userCreationResult.Succeeded)
            {
                foreach(var error in userCreationResult.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
                return View();
            }

            await _userManager.AddClaimAsync(newUser, new Claim(ClaimTypes.Role, "Administrator"));

            ViewData["SuccessMessage"] = "User created. You can now log in.";
            return View("Login");
        }

        public IActionResult Login()
        {
            return View();
        }        

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, bool rememberMe)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login");
                return View();
            }

            var passwordSignInResult = await _signInManager.PasswordSignInAsync(user, password, isPersistent: rememberMe, lockoutOnFailure: false);
            
            if (!passwordSignInResult.Succeeded)
            {                
                await _userManager.AccessFailedAsync(user);
                ModelState.AddModelError(string.Empty, "Invalid login");
                return View();                
            }

            return Redirect("/");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/");
        }                    
    }
}
