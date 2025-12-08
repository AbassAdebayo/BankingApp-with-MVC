using BankingApp.Interface.Repositories;
using BankingApp.Models.Entities;
using BankingApp.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Implementation.Repositories
{
    public class RoleRepository(BankContext bankContext) : IRoleRepository
    {
        private readonly BankContext _bankContext = bankContext ?? throw new ArgumentNullException(nameof(bankContext));
        public async Task<Role> GetRoleByName(string roleName)
        {
            return await _bankContext.Set<Role>().SingleOrDefaultAsync(r => r.Name == roleName);
        }
    }
}
