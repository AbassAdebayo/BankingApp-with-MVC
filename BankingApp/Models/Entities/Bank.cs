using BankingApp.Contracts.Entities;
using MassTransit;

namespace BankingApp.Models.Entities
{
    public class Bank(string name, BankBranch bankBranch) : BaseEntity
    {
        public required string Name { get; init; } = name;
        public required BankBranch BankBranch { get; init; } = bankBranch;
        public ICollection<User> Users { get; set; } = [];

        //public void UpdateBankName(string name)
        //{
        //    if(string.IsNullOrEmpty(name)) throw new ArgumentNullException("Name cannot be empty");
        //     = name;
            
        //}
    }
}
