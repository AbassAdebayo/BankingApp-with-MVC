using BankingApp.Interface.Services;
using BankingApp.Models.DTOs.Bank;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Controllers
{
    public class BankController(IBankService bankService) : Controller
    {
        private readonly IBankService _bankService = bankService ?? throw new ArgumentNullException(nameof(bankService));
        public async Task<IActionResult> Index()
        {
            var banks = await _bankService.GetAllBanksAsync();
            return View(banks);
        }

        [HttpGet]
        public IActionResult AddBank()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBank(CreateBankRequestModel model)
        {
            var bank = await _bankService.CreateBankAsync(model);
            if (bank.Status)
            {
                ViewBag.Success = bank.Status;
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = bank.Status;
                return View();
            }

        }
    }
}
