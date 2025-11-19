using BankingApp.Models;
using BankingApp.Models.Entities;

namespace BankingApp.Interface.Repositories
{
    public interface IBankRepository
    {
        Task<Bank> Add(Bank bank);
        Task<Bank> Update(Bank bank);
        Task<bool> Delete(Bank bank);
        Task<Bank> GetById(Guid id);
        Task<List<Bank>> ListOfBanks();
        Task<List<Bank>> ListOfBanksByBranchName(BankBranch bankBranch);
    }
}

