using BankingApp.Models.DTOs.Bank;
using BankingApp.Models.Entities;
using BankingApp.Models.Enum;

namespace BankingApp.Models.DTOs.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public Guid BankId { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        public string Address { get; set; }
        public Gender Gender { get; set; }
        public BankDto Bank { get; set; }
        public AccountDetailsDto AccountDetails { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class AccountDetailsDto
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public Guid UserId { get; set; }
        public string AccountNumber { get; set; }
        public AccountType AccountType { get; set; }
        public decimal? Balance { get; set; }
    }
}
