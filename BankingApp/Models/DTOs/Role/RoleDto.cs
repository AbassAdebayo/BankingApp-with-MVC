using MassTransit;

namespace BankingApp.Models.DTOs.Role
{
    public class RoleDto
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
