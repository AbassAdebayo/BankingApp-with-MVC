using BankingApp.Contracts.Entities;
using MassTransit;

namespace BankingApp.Models.Entities
{
    public class Bank : BaseEntity
    {
        public string Name { get; set; }
        public BankBranch BankBranch { get; set; }
        public ICollection<User> Users { get; set; } = [];


        public Bank(string name, BankBranch bankBranch)
        {
            Name = name;
            BankBranch = bankBranch;
        }

        public void UpdateBankName(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("Name cannot be empty");
             Name = name;


        }
    }
}
