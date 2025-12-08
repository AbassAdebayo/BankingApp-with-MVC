using BankingApp.Interface.Repositories;
using BankingApp.Interface.Services;
using BankingApp.Models.DTOs;
using BankingApp.Models.DTOs.Auth;
using BankingApp.Models.DTOs.Role;
using BankingApp.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace BankingApp.Implementation.Services
{
    public class AuthService(IUserRepository userRepository, 
        UserManager<User> userManager, ILogger<UserService> logger) : IAuthService
    {
        private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        private readonly UserManager<User> _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        private readonly ILogger<UserService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        public async Task<BaseResponse<LoginResponseModel>> LoginAsync(LoginRequestModel request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                _logger.LogError("Invalid credentials");
                return new BaseResponse<LoginResponseModel>
                {
                    Message = "Invalid credentials",
                    Status = false
                };
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordValid)
            {
                _logger.LogError("Invalid credentials");
                return new BaseResponse<LoginResponseModel>
                {
                    Message = "Invalid credentials",
                    Status = false
                };
            }
            var roles = await _userManager.GetRolesAsync(user);
            return new BaseResponse<LoginResponseModel>
            {
                Message = "Login successful",
                Status = true,
                Data = new LoginResponseModel
                {

                    UserId = user.Id,
                    Email = user.Email,
                    Role = roles.Select(r => new RoleDto { Name = r }).FirstOrDefault() ?? new RoleDto(),
                    FirstName =  $"{user.FirstName}" ?? string.Empty,
                    FullName = $"{user.FirstName} {user.LastName}" ?? string.Empty


                }
            };
        }
    }
}
