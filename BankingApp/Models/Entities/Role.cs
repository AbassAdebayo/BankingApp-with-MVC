using BankingApp.Contracts.Entities;

namespace BankingApp.Models.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public Role(string name, string description = null)
        {
            Name = name;
            Description = description;
        }

    }
}
