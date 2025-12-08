using BankingApp.Models.Entities;

namespace BankingApp.Interface.Repositories
{
    public interface IRoleRepository
    {
        Task<Role> GetRoleByName(string roleName);
    }
}
