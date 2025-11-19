using BankingApp.Contracts.Entities;

namespace BankingApp.Models.Entities
{
 
    public class AccountDetails : BaseEntity
    {

        public Guid UserId { get; set; }
        public User? User { get; set; }
        public string AccountNumber { get; set; }
        public decimal? AccountBalance { get; set; }

        public AccountDetails(Guid userId, string accountNumber)
        {
            UserId = userId;
            AccountNumber = accountNumber;
        }
    }
}
