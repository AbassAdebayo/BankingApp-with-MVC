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

        public async Task<bool> ExistsByBank(string email, string bankName)
        {
            return await _bankContext.Set<User>()
                .AnyAsync(u => u.Email == email && u.Bank.Name == bankName);
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

        public async Task<User> GetUserByEmail(string email)
        {
            return await _bankContext.Set<User>()
               .Where(u => u.Email == email)
               .AsNoTracking()
               .Include(u => u.Role)
               .Include(u => u.Bank)
                .Include(u => u.AccountDetails)
               .SingleOrDefaultAsync();
        }

        public async Task<User> GetUserProfile(Guid id)
        {
            return await _bankContext.Set<User>()
                .Where(u => u.Id == id)
                .Include(u => u.Bank)
                .Include(u => u.AccountDetails)
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
            return await _bankContext.Set<User>()
                .Where(u => u.Role.Name == "Customer" && u.Bank.Name == bankName)
                .Include(u => u.Bank)
                .ThenInclude(b => b.Name)
                .AsNoTracking()
                .ToListAsync();
               

        }

        public async Task<User> Update(User user)
        {
            _bankContext.Set<User>().Update(user);
            await _bankContext.SaveChangesAsync();
            return user;

        }
    }
}
