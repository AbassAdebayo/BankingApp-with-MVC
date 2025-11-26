using BankingApp.Models.DTOs.Role;

namespace BankingApp.Models.DTOs.Auth
{
    public class LoginRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }

    public class LoginResponseModel : BaseResponse
    {
        public string FirstName { get; set; }
        public string FullName { get; set; }
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public RoleDto Role { get; set; } = new RoleDto();
    }
}
