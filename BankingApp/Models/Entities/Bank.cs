using BankingApp.Contracts.Entities;
using MassTransit;

namespace BankingApp.Models.Entities
{
    public class Bank(string name, BankBranch bankBranch) : BaseEntity
    {
        public required string Name = name;
        public required BankBranch BankBranch = bankBranch;
        public ICollection<User> Users {  get; set; }


        public void UpdateBankName(string name)
        {
            if(string.IsNullOrEmpty(name)) throw new ArgumentNullException("Name cannot be empty");
            Name = name;
            
        }
    }
}
