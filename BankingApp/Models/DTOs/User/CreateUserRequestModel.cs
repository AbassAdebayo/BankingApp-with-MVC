using BankingApp.Models.Entities;
using BankingApp.Models.Enum;

namespace BankingApp.Models.DTOs.User
{
    public class CreateUserRequestModel
    {
        public Guid RoleId { get; set; }
        public Guid BankId { get; set; }
        public Gender Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PasswordHash { get; set; }
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }
    }
}
