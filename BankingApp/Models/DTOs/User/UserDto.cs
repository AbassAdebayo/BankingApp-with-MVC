using BankingApp.Models.Entities;
using BankingApp.Models.Enum;

namespace BankingApp.Models.DTOs.User
{
    public class UserDto
    {
        public Guid ID { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
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
    }
}
