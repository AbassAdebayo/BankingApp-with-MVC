using BankingApp.Contracts.Service;
using BankingApp.Interface.Repositories;
using BankingApp.Interface.Services;
using BankingApp.Models.DTOs;
using BankingApp.Models.DTOs.User;
using BankingApp.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace BankingApp.Implementation.Services
{
    public class UserService(IUserRepository userRepository, ILogger<UserService> logger,
        UserManager<User> userManager,
            IIdentityService identityService, IBankRepository bankRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        private readonly ILogger<UserService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        private readonly UserManager<User> _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        private readonly IIdentityService _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        private readonly IBankRepository _bankRepository = bankRepository ?? throw new ArgumentNullException(nameof(bankRepository));

        public async Task<BaseResponse<bool>> CreateAsync(CreateUserRequestModel request)
        {
            var bank = await _bankRepository.GetById(request.BankId);
            var userExists = await _userRepository.Any(u => u.Email == request.Email && u.Bank.Name == bank.Name);
            if (userExists)
            {
                _logger.LogError("User with email already exist");
                return new BaseResponse<bool>
                {
                    Message = "User with email already exist",
                    Status = false
                };
            }

            if (request.PasswordHash != request.ConfirmPassword) return new BaseResponse<bool>
            {
                Message = "Password doesnt match!",
                Status = false,
            };
        }

        public Task<BaseResponse<UserDto>> GetUserByEmail(string email, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<UserDto>> GetUserProfileByUserId(Guid userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private static (bool, string?) ValidatePassword(string password)
        {
            // Minimum length of password
            int minLength = 8;

            // Maximum length of password
            int maxLength = 50;

            // Check for null or empty password
            if (string.IsNullOrEmpty(password))
            {
                return (false, "Password cannot be null or empty.");
            }

            // Check length of password
            if (password.Length < minLength || password.Length > maxLength)
            {
                return (false, $"Password must be between {minLength} and {maxLength} characters long.");
            }

            // Check for at least one uppercase letter, one lowercase letter, and one digit
            bool hasUppercase = false;
            bool hasLowercase = false;
            bool hasDigit = false;

            foreach (char c in password)
            {
                if (char.IsUpper(c))
                {
                    hasUppercase = true;
                }
                else if (char.IsLower(c))
                {
                    hasLowercase = true;
                }
                else if (char.IsDigit(c))
                {
                    hasDigit = true;
                }
            }

            if (!hasUppercase || !hasLowercase || !hasDigit)
            {
                return (false, "Password must contain at least one uppercase letter, one lowercase letter, and one digit.");
            }

            // Check for any characters
            string invalidCharacters = @" !""#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";
            if (password.IndexOfAny(invalidCharacters.ToCharArray()) == -1)
            {
                return (false, "Password must contain one or more characters.");
            }

            // Password is valid
            return (true, null);
        }
    }
}
