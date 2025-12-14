using BankingApp.Models.DTOs;
using BankingApp.Models.DTOs.User;

namespace BankingApp.Interface.Services
{
    public interface IUserService
    {
        Task<BaseResponse<bool>> CreateAsync(CreateUserRequestModel request);
        //public Task<BaseResponse<LoginResponseModel>> LoginAsync(LoginRequestModel request, CancellationToken cancellationToken);
        public Task<BaseResponse<UserDto>> GetCustomerUserProfileByUserIdAsync(Guid userId, CancellationToken cancellationToken);

        public Task<BaseResponse<IReadOnlyList<UserDto>>> ListOfCustomerUsersByBankAsync(string bankName, CancellationToken cancellationToken);
        public Task<BaseResponse<IReadOnlyList<UserDto>>> GetAllCustomerUsersAsync(CancellationToken cancellationToken);

        public Task<BaseResponse<CardInformationDto>> GetUserATMCardAsync(Guid userId, CancellationToken cancellationToken);
        public Task<int> TotalCountOfCustomersAsync();

    }
}
