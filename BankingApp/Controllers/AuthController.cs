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

        [HttpGet]
        public IActionResult CustomerDashBoard()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdString == null) return RedirectToAction("Login");
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
                    new Claim(ClaimTypes.Gender, loginResponse.Data.Gender.ToString()),
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
                    return RedirectToAction("CustomerDashBoard");
                }

                return RedirectToAction("AdminDashBoard");

            }

            ModelState.AddModelError(string.Empty, loginResponse.Message);
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserId");

            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";
            return RedirectToAction("Login");
        }
    }
}
