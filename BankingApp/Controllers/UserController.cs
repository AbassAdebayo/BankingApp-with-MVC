using BankingApp.Interface.Repositories;
using BankingApp.Interface.Services;
using BankingApp.Models.DTOs.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Numerics;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BankingApp.Controllers
{
    public class UserController(
        IUserService userService, IBankService bankService) : Controller
    {
        private readonly IUserService _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        private readonly IBankService _bankService = bankService ?? throw new ArgumentNullException(nameof(bankService));

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var allUsers = await _userService.GetAllCustomerUsersAsync(CancellationToken.None);
            return View(allUsers);
        }

        [HttpGet]
        public async Task<IActionResult> RegisterCustomer()
        {
            var banks = await _bankService.GetAllBanksAsync();
            ViewData["Banks"] = new MultiSelectList(banks.Data, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterCustomer(CreateUserRequestModel model)
        {
            var user = await _userService.CreateAsync(model);

            if (user.Status)
            {
                RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CustomerProfile()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(string.IsNullOrEmpty(userIdString))
            {
                return RedirectToAction("Login", "Auth");
            }
            if (!Guid.TryParse(userIdString, out var userId))
            {
                return BadRequest("Invalid user ID format.");
            }

            var userCustomer = await _userService.GetCustomerUserProfileByUserIdAsync(userId, CancellationToken.None);

            if (userCustomer == null || !userCustomer.Status) return NotFound(userCustomer?.Message);

            return View(userCustomer);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomersByBank(string bankName)
        {
            var users = await _userService.ListOfCustomerUsersByBankAsync(bankName, CancellationToken.None);
            return View(users);
        }
    }
}
