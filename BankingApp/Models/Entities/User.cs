using BankingApp.Contracts.Entities;
using MassTransit;

namespace BankingApp.Models.Entities
{
    public class User(string firstName, string lastName, string email, string passwordHash,
        string phoneNumber, Guid bankId) : BaseEntity
    {
        public required Guid BankId = bankId;
        public AccountDetails AccountDetails { get; set; }
        public required string FirstName = firstName;
        public required string LastName = lastName;
        public required string Email = email;
        public required string PasswordHash = passwordHash;
        public required string PhoneNumber = phoneNumber;
        public string PasswordSalt { get; set; }
    }
}
