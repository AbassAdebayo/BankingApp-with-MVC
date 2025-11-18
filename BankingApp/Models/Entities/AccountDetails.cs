using BankingApp.Contracts.Entities;

namespace BankingApp.Models.Entities
{
 
    public class AccountDetails(Guid userId, string accountNumber) : BaseEntity
    {

        public Guid UserId { get; init; } = userId;
        public User? User { get; set; }
        public string AccountNumber { get; init; } = accountNumber;
        public decimal AccountBalance { get; set; }
    }
}
