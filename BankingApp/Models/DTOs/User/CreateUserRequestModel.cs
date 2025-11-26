using BankingApp.Models.Entities;
using BankingApp.Models.Enum;

namespace BankingApp.Models.DTOs.User
{
    public class CreateUserRequestModel
    {
        public Guid RoleId { get; set; }
        public Guid BankId { get; set; }
        public Gender Gender { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Address { get; set; }
        public required string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public required string PasswordHash { get; set; }
        public required string ConfirmPassword { get; set; }
        public required string PhoneNumber { get; set; }
        public AccountType AccountType { get; set; }
    }
}
