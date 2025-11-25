using BankingApp.Models.Entities;

namespace BankingApp.Interface.Repositories
{
    public interface IUserRepository
    {
        Task<User> Add(User user);
        Task<User> Update(User user);
        Task<bool> Delete(User user);
        Task<User> GetById(Guid id);
        Task<User> GetUserAndRole(Guid userId);
        Task<List<User>> ListOfUsers();
        Task<List<User>> ListOfUsersByBank(string bankName);
        Task<bool> Any(Func<User, bool> expression);
    }
}
