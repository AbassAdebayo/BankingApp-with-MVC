using BankingApp.Models.Entities;

namespace BankingApp.Contracts.Service
{
    public interface IIdentityService
    {
        public string GetPasswordHash(string password, string salt = null);
        Task<string> GetRoleAsync(User user);
        //bool CheckPasswordAsync(User user, string password)
    }
}
