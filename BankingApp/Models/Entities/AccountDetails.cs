using BankingApp.Contracts.Entities;

namespace BankingApp.Models.Entities
{
    public class AccountDetails(Guid userId) : BaseEntity
    {
        public Guid UserId = userId;
        public required string AccountNumber { get; set; }
        public required decimal AccounBalance { get; set; }
    }
}
