using BankingApp.Contracts.Entities;
using MassTransit;

namespace BankingApp.Models.Entities
{
    public class User(string firstName, string lastName, string email, string passwordHash,
        string phoneNumber, Guid bankId) : BaseEntity
    {
        public required Guid BankId { get; init; } = bankId;
        public Bank Bank { get; set; }
        public required AccountDetails AccountDetails { get; set; }
        public required string FirstName { get; init; } = firstName;
        public required string LastName { get; init; } = lastName;
        public required string Email { get; init; } = email;
        public required string PasswordHash { get; init; } = passwordHash;
        public required string PhoneNumber { get; init; } = phoneNumber;
        public required string PasswordSalt { get; set; }
    }
}
