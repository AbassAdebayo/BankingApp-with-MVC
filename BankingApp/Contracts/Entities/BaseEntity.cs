using MassTransit;

namespace BankingApp.Contracts.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = NewId.NextGuid();
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
