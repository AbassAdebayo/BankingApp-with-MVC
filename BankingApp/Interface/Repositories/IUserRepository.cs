using BankingApp.Models;
using BankingApp.Models.Entities;

namespace BankingApp.Interface.Repositories
{
    public interface IUserRepository
    {
        Task<User> Add(User user);
        Task<User> Update(User user);
        Task<bool> Delete(User user);
        Task<User> GetById(Guid id);
        Task<List<User>> ListOfBanks();
        Task<List<User>> ListOfUsersBank(string bankName);
    }
}
