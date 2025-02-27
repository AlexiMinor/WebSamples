using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using WebApp.Core.DTOs;
using WebApp.MVC.Models;
using WebApp.Services.Abstract;

namespace WebApp.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            //todo : authenticate user
            if (ModelState.IsValid)
            {
                var loginData = await _accountService.TryToLogin(model.Email, model.Password);

                if (loginData != null)
                {
                    //todo authorize user
                   
                    
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("","Incorrect login or password");
            }
            return View();

        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationModel model)
        {
            //todo : authenticate user
            if (ModelState.IsValid)
            {
                var loginDto = await _accountService.TryToRegister(model.Email, model.Password);

                if (loginDto != null)
                {
                    //todo authorize user
                    await SignIn(loginDto);

                    return RedirectToAction("Index","Home");
                }
                ModelState.AddModelError("", "Incorrect login or password");
            }
            return View();
        }


        [HttpGet]
        public IActionResult Logout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Logout(LogOutModel model)
        {
            return View();
        }

        private async Task SignIn(LoginDto dto)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, dto.Email),
                new(ClaimTypes.Role, dto.Role),
                new(ClaimTypes.Name, dto.Nickname)
            };
            var claimsIdentity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
        }
    }
}
