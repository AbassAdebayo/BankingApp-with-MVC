using BankingApp.Models.DTOs;
using BankingApp.Models.DTOs.User;

namespace BankingApp.Interface.Services
{
    public interface IUserService
    {
        Task<BaseResponse<bool>> CreateAsync(CreateUserRequestModel request);
        //public Task<BaseResponse<LoginResponseModel>> LoginAsync(LoginRequestModel request, CancellationToken cancellationToken);
        public Task<BaseResponse<UserDto>> GetUserByEmail(string email, CancellationToken cancellationToken);
        public Task<BaseResponse<UserDto>> GetUserProfileByUserId(Guid userId, CancellationToken cancellationToken);
        
}
