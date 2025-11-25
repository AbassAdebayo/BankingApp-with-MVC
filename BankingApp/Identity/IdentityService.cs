using BankingApp.Contracts.Service;
using BankingApp.Interface.Repositories;
using BankingApp.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace BankingApp.Identity
{
    public class IdentityService(IPasswordHasher<User> passwordHasher, IUserRepository userRepository) : IIdentityService
    {
        private readonly IPasswordHasher<User> _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

        public string GetPasswordHash(string password, string salt = null)
        {
            if (string.IsNullOrEmpty(salt))
            {
                return _passwordHasher.HashPassword(new User(), password);
            }
            return _passwordHasher.HashPassword(new User(), $"{password}{salt}");
        }

        public async Task<string> GetRoleAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var role = await _userRepository.GetUserAndRole(user.Id);

            return role.Role.Name != null ? new string(role.Role.Name) : string.Empty;
        }

    }
}
