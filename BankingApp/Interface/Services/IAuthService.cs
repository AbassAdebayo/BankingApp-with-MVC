using BankingApp.Models.DTOs;
using BankingApp.Models.DTOs.Auth;

namespace BankingApp.Interface.Services
{
    public interface IAuthService
    {
        public Task<BaseResponse<LoginResponseModel>> LoginAsync(LoginRequestModel request, CancellationToken cancellationToken);
    }
}
