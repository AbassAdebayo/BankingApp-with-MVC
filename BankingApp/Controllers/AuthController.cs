using BankingApp.Interface.Services;
using BankingApp.Models.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BankingApp.Controllers
{
    public class AuthController(IAuthService authService) : Controller
    {
        private readonly IAuthService _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CustomerDashBoard()
        {
            return View();
        }

        public IActionResult AdminDashBoard()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Models.DTOs.Auth.LoginRequestModel model)
        {
            var loginResponse = await _authService.LoginAsync(model, CancellationToken.None);
            var checkRole = "";

            if (loginResponse.Status)
            {
                var claims = new List<System.Security.Claims.Claim>
                {
                    new Claim(ClaimTypes.Name, loginResponse.Data.FirstName),
                    new Claim(ClaimTypes.GivenName, loginResponse.Data.FullName),
                    new Claim(ClaimTypes.Email, loginResponse.Data.Email),
                    new Claim(ClaimTypes.NameIdentifier, loginResponse.Data.UserId.ToString()),
                };

                claims.Add(new Claim(ClaimTypes.Role, loginResponse.Data.Role.Name));
                checkRole = loginResponse.Data.Role.Name;

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authenticationProperties = new AuthenticationProperties();
                var principal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, principal, authenticationProperties);

                if (checkRole == "Customer")
                {
                    return RedirectToAction("CustomerDashBoard", "Auth");
                }
                return RedirectToAction("AdminDashBoard", "Auth");

            }

            ModelState.AddModelError(string.Empty, loginResponse.Message);
            return View(model);
        }
    }
}
