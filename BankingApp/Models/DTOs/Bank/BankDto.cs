using BankingApp.Models.Entities;
using MassTransit;

namespace BankingApp.Models.DTOs.Bank
{
    public class BankDto
    {
        public Guid Id { get; set; } = NewId.Next().ToGuid();
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public BankBranch BankBranch { get; set; }
        public ICollection<User> Users { get; set; } = [];

    }
}
