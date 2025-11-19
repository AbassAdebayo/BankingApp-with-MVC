using MassTransit;

namespace BankingApp.Contracts.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = NewId.Next().ToGuid();
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
