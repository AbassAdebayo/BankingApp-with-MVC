using BankingApp.Contracts.Entities;
using BankingApp.Models.Enum;

namespace BankingApp.Models.Entities
{

    public class User : BaseEntity
    {
        public Guid BankId { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        public Gender Gender { get; set; }
        public Bank Bank { get; set; }
        public AccountDetails AccountDetails { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public User() { }

        public User(string firstName, string lastName, string address, string email, DateTime dateOfBirth, string passwordHash,
        string phoneNumber, Gender gender, Guid bankId, Guid roleId)
        {
            BankId = bankId;
            RoleId = roleId;
            Gender = gender;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Email = email;
            DateOfBirth = dateOfBirth;
            PasswordHash = passwordHash;
            PhoneNumber = phoneNumber;
        }

        public User(string firstName, string lastName, string address, string email, DateTime dateOfBirth, string passwordHash,
        string phoneNumber, Gender gender, Guid bankId)
        {
            BankId = bankId;
            Gender = gender;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Email = email;
            DateOfBirth = dateOfBirth;
            PasswordHash = passwordHash;
            PhoneNumber = phoneNumber;
            DateCreated = DateTime.UtcNow;
        }


    }
}
