using BankingApp.Interface.Repositories;
using BankingApp.Models;
using BankingApp.Models.Entities;
using BankingApp.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Implementation.Repositories
{
    public class BankRepository(BankContext bankContext) : IBankRepository
    {
        private readonly BankContext _bankContext = bankContext ?? throw new ArgumentNullException(nameof(bankContext));

        public async Task<Bank> Add(Bank bank)
        {
            await _bankContext.Set<Bank>().AddAsync(bank);
            await _bankContext.SaveChangesAsync();
            return bank;
        }

        public async Task<bool> BankExistsByBranchName(BankBranch bankBranch)
        {
            return await _bankContext.Set<Bank>().AnyAsync(b => b.BankBranch == bankBranch);
        }

        public async Task<bool> Delete(Bank bank)
        {
            _bankContext.Set<Bank>().Remove(bank);
            int result = await _bankContext.SaveChangesAsync();
            return result > 0;

        }

        public async Task<Bank> GetById(Guid id)
        {
            return await _bankContext.Set<Bank>()
                .FindAsync(id);
        }

        public async Task<List<Bank>> ListOfBanks()
        {
            return await _bankContext.Set<Bank>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Bank>> ListOfBanksByBranchName(BankBranch bankBranch)
        {
            return await _bankContext.Set<Bank>()
                .Where(b => b.BankBranch == bankBranch)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Bank> Update(Bank bank)
        {
            _bankContext.Set<Bank>().Update(bank);
            await _bankContext.SaveChangesAsync();
            return bank;
        }
    }
}
