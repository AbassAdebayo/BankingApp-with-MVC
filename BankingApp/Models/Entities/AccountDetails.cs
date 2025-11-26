using BankingApp.Contracts.Entities;
using BankingApp.Models.Enum;

namespace BankingApp.Models.Entities
{

    public class AccountDetails : BaseEntity
    {

        public Guid UserId { get; set; }
        public User? User { get; set; }
        public string AccountNumber { get; set; }

        public AccountType AccountType { get; set; }
        public decimal? AccountBalance { get; set; }

        public AccountDetails(Guid userId, string accountNumber, AccountType accountType)
        {
            UserId = userId;
            AccountNumber = accountNumber;
            AccountType = accountType;
            DateCreated = DateTime.UtcNow;
        }

        public AccountDetails(string accountNumber, AccountType accountType)
        {
            AccountNumber = accountNumber;
            AccountType = accountType;
            DateCreated = DateTime.UtcNow;
        }
    }
}
