using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleStore.Web.Areas.Account.ViewModels;
using System.Collections.Generic;
using System.Security.Claims;

namespace SimpleStore.Web.Areas.Account
{
    [Area("Account")]
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
                return View("Index", loginViewModel);

            if (loginViewModel.Username != "admin" ||
                loginViewModel.Password != "admin")
                return View("Index", loginViewModel);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginViewModel.Username),
                new Claim(ClaimTypes.Role, "Administrator"),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Home", new { area = "Administration" });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Logout(string returnUrl)
        {
            HttpContext.SignOutAsync();

            return Redirect(returnUrl);
        }
    }
}
