using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Identity.MVC.Data;
using WebApp.Identity.MVC.Models;

namespace WebApp.Identity.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<MyCustomUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<MyCustomUser> _signInManager;

        public AccountController(UserManager<MyCustomUser> userManager, 
            RoleManager<IdentityRole> roleManager, 
            SignInManager<MyCustomUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new MyCustomUser { UserName = model.Email, Email = model.Email };
                //to be careful with the password - allow any possible value
                var addUserResult = await _userManager.CreateAsync(user, model.Password); //request to the database
                if (addUserResult.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync("User"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("User"));
                    }
                    
                    await _userManager.AddToRoleAsync(user, "User");
                    await _signInManager.SignInAsync(user, false);
                }
            }
            return View();
        }

    }
}
