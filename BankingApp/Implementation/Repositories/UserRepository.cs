using BankingApp.Interface.Repositories;
using BankingApp.Models.Entities;
using BankingApp.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Implementation.Repositories
{
    public class UserRepository(BankContext bankContext) : IUserRepository
    {
        private readonly BankContext _bankContext = bankContext ?? throw new ArgumentNullException(nameof(bankContext));
        public async Task<User> Add(User user)
        {
            await _bankContext.Set<User>().AddAsync(user);
            await _bankContext.SaveChangesAsync();
            return user;
        }

        public async Task<bool> Any(Func<User, bool> expression)
        {
           return await _bankContext.Set<User>().AnyAsync(u => expression(u));
        }

        public async Task<bool> Delete(User user)
        {
            _bankContext.Set<User>().Remove(user);
            int result = await _bankContext.SaveChangesAsync();
            return result > 0;

        }

        public async Task<User> GetById(Guid id)
        {
            return await _bankContext.Set<User>().FindAsync(id);
        }

        public async Task<User> GetUserAndRole(Guid userId)
        {
            return await _bankContext.Set<User>()
                .Where(u => u.Id == userId)
                .Include(u => u.Role)
                .AsNoTracking()
                .SingleOrDefaultAsync();
        }

        public async Task<List<User>> ListOfUsers()
        {
            return await _bankContext.Set<User>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<User>> ListOfUsersByBank(string bankName)
        {
            var banksInUser = await _bankContext.Set<User>()
                .Include(u => u.Bank)
                .ThenInclude(b => b.Name)
                .AsNoTracking()
                .ToListAsync();

            return banksInUser
                .Where(b => b.Bank.Name == bankName)
                .ToList();

        }

        public async Task<User> Update(User user)
        {
            _bankContext.Set<User>().Update(user);
            await _bankContext.SaveChangesAsync();
            return user;

        }
    }
}
