using BankingApp.Contracts.Service;
using BankingApp.Interface.Repositories;
using BankingApp.Interface.Services;
using BankingApp.Models.DTOs;
using BankingApp.Models.DTOs.Bank;
using BankingApp.Models.DTOs.User;
using BankingApp.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace BankingApp.Implementation.Services
{
    public class UserService(IUserRepository userRepository, ILogger<UserService> logger,
        UserManager<User> userManager,
            IIdentityService identityService,
            IRoleRepository roleRepository, IBankRepository bankRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        private readonly ILogger<UserService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        private readonly UserManager<User> _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        private readonly IIdentityService _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        private readonly IBankRepository _bankRepository = bankRepository ?? throw new ArgumentNullException(nameof(bankRepository));
        private readonly IRoleRepository _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));

        public async Task<BaseResponse<bool>> CreateAsync(CreateUserRequestModel request)
        {
            var bank = await _bankRepository.GetById(request.BankId);
            var userExists = await _userRepository.ExistsByBank(request.Email, bank.Name);

            var customerRole = await _roleRepository.GetRoleByName("Customer");
            if (customerRole is null)
            {
                _logger.LogError($"Role doesn't exist.");
                return new BaseResponse<bool>
                {
                    Message = $"Role doesn't exist.",
                    Status = false
                };
            }
            if (userExists)
            {
                _logger.LogError($"User with email already has account with {bank.Name}");
                return new BaseResponse<bool>
                {
                    Message = $"User with email already has account with {bank.Name}",
                    Status = false
                };
            }

            if (request.PasswordHash != request.ConfirmPassword) return new BaseResponse<bool>
            {
                Message = "Password doesnt match!",
                Status = false,
            };

            (var passwordResult, var message) = ValidatePassword(request.PasswordHash);
            if (!passwordResult) return new BaseResponse<bool> { Message = message, Status = false };

            var hashPassword = _identityService.GetPasswordHash(request.PasswordHash);

            var user = new User
                (
                    request.FirstName,
                    request.LastName,
                    request.Address,
                    request.Email,
                    request.DateOfBirth,
                    hashPassword,
                    request.PhoneNumber,
                    request.Gender,
                    request.BankId,
                    customerRole.Id


                );
            var accountNumber = GenerateAccountNumber();
            user.AccountDetails = new AccountDetails(accountNumber, request.AccountType)
            {
                AccountBalance = 500m,
            };

            var cardHolder = $"{user.FirstName} {user.LastName}";
            var cardNumber = GenerateCardNumber();
            var cardCVV = GenerateCVV();
            var expiry = GenerateExpiry();
            user.CardInformation = new CardInformation(cardHolder, cardNumber, cardCVV, expiry, bank.Name);

            var adduserAccountResult = await _userManager.CreateAsync(user);

            if (!adduserAccountResult.Succeeded)
            {
                _logger.LogError("User creation failed");
                throw new Exception("User creation failed: " + string.Join(", ", adduserAccountResult.Errors.Select(e => e.Description)));
            }


            return new BaseResponse<bool>
            {
                Message = "User created successfully",
                Status = true
            };

        }


        public async Task<BaseResponse<UserDto>> GetCustomerUserProfileByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            var customerProfile = await _userRepository.GetUserProfile(userId);
            if (customerProfile == null)
            {
                _logger.LogError($"Customer user with ID: {userId} not found.");
                return new BaseResponse<UserDto>
                {
                    Message = $"Customer user with ID: {userId} not found.",
                    Status = false
                };
            }
            _logger.LogInformation($"Customer user profile retrieved successfully for ID: {userId}.");
            return new BaseResponse<UserDto>
            {
                Message = $"Customer user profile retrieved successfully for ID: {userId}.",
                Status = true,
                Data = new UserDto
                {
                    Id = customerProfile.Id,
                    FirstName = customerProfile.FirstName,
                    LastName = customerProfile.LastName,
                    PhoneNumber = customerProfile.PhoneNumber,
                    Email = customerProfile.Email,
                    DateOfBirth = customerProfile.DateOfBirth,
                    Address = customerProfile.Address,
                    Bank = new BankDto
                    {
                        Id = customerProfile.Bank.Id,
                        Name = customerProfile.Bank.Name,
                        BankBranch = customerProfile.Bank.BankBranch,
                        Description = customerProfile.Bank.Description,

                    },
                    AccountDetails = new AccountDetailsDto
                    {
                        Id = customerProfile.AccountDetails.Id,
                        AccountNumber = customerProfile.AccountDetails.AccountNumber,
                        AccountType = customerProfile.AccountDetails.AccountType,
                        Balance = customerProfile.AccountDetails.AccountBalance,
                    }

                }
            };
        }

        public async Task<BaseResponse<IReadOnlyList<UserDto>>> ListOfCustomerUsersByBankAsync(string bankName, CancellationToken cancellationToken)
        {
            var customers = await _userRepository.ListOfUsersByBank(bankName);
            if (customers == null || !customers.Any())
            {
                _logger.LogError($"No customer users found for bank: {bankName}.");
                return new BaseResponse<IReadOnlyList<UserDto>>
                {
                    Message = $"No customer users found for bank: {bankName}.",
                    Status = false
                };
            }
            _logger.LogInformation($"Customer users retrieved successfully for bank: {bankName}.");
            return new BaseResponse<IReadOnlyList<UserDto>>
            {
                Message = $"Customer users retrieved successfully for bank: {bankName}.",
                Status = true,
                Data = customers.Select(c => new UserDto
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    PhoneNumber = c.PhoneNumber,
                }).ToList(),

            };
        }

        public async Task<BaseResponse<IReadOnlyList<UserDto>>> GetAllCustomerUsersAsync(CancellationToken cancellationToken)
        {
            var customers = await _userRepository.ListOfUsers();
            if (customers == null || !customers.Any())
            {
                _logger.LogError($"No customer users found.");
                return new BaseResponse<IReadOnlyList<UserDto>>
                {
                    Message = $"No customer users found.",
                    Status = false
                };
            }
            _logger.LogInformation($"Customer users retrieved successfully.");
            return new BaseResponse<IReadOnlyList<UserDto>>
            {
                Message = $"Customer users retrieved successfully.",
                Status = true,
                Data = customers.Select(c => new UserDto
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    PhoneNumber = c.PhoneNumber,
                }).ToList(),

            };
        }

        public async Task<BaseResponse<CardInformationDto>> GetUserATMCardAsync(Guid userId, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(userId);
            if (user == null)
            {
                return new BaseResponse<CardInformationDto>
                {
                    Message = "User cannot be found",
                    Status = false
                };
            }

            var userCard = await _userRepository.GetUserATMCard(user.Id);

            if (userCard == null)
            {
                return new BaseResponse<CardInformationDto>
                {
                    Message = "ATM Card for this user cannot be found",
                    Status = false
                };
            }

            return new BaseResponse<CardInformationDto>
            {
                Message = "ATM card for user retrieved",
                Status = true,
                Data = new CardInformationDto
                {
                    BankName = userCard.BankName,
                    CardCVV = userCard.CardCVV,
                    CardHolder = userCard.CardHolder.ToUpper(),
                    CardNumber = userCard.CardNumber,
                    Expiry = userCard.Expiry
                }
            };
        }

        private static string GenerateCardNumber()
        {
            string cardNumber = "";
            Random random = new Random();
            for (int i = 0; i < 16; i++)
            {
                cardNumber += random.Next(0, 9);
                if ((i + 1) % 4 == 0 && i != 15) cardNumber += " ";
            }
            return cardNumber;
        }

        private static string GenerateExpiry()
        {
            DateTime now = DateTime.UtcNow;
            int month = now.Month;
            int expYear = now.Year + 4;

            return $"{month:00}/{expYear % 100:00}";
        }

        private static string GenerateCVV()
        {
            Random random = new Random();
            return random.Next(100, 999).ToString();
        }
        private static string GenerateAccountNumber()
        {
            Random random = new Random();
            string accountNumber = string.Empty;
            for (int i = 0; i < 10; i++)
            {
                accountNumber += random.Next(0, 10).ToString();
            }
            return accountNumber;
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
