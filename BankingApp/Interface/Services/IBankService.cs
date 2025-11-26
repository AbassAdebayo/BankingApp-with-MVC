using BankingApp.Models;
using BankingApp.Models.DTOs;
using BankingApp.Models.DTOs.Bank;

namespace BankingApp.Interface.Services
{
    public interface IBankService
    {
        Task<BaseResponse<bool>> CreateBankAsync(CreateBankRequestModel request);
        //Task<BaseResponse<bool> Update(Bank bank);
        Task<BaseResponse<bool>> Delete(Guid id);
        Task<BaseResponse<BankDto>> GetBankByIdAsync(Guid id);
        Task<BaseResponse<List<BankDto>>> GetAllBanksAsync();
        Task<BaseResponse<List<BankDto>>> ListOfBanksByBranchNameAsync(BankBranch bankBranch);
    }
}
