using BankingApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Persistence.Context
{
    public class BankContext(DbContextOptions<BankContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<AccountDetails>()
            //    .HasOne(a => a.User)
            //    .WithOne(u => u.AccountDetails)
            //    .HasForeignKey<AccountDetails>(a => a.UserId);

        }

        DbSet<AccountDetails> AccountDetails => Set<AccountDetails>();
        DbSet<Bank> Banks => Set<Bank>();
        DbSet<User> Users => Set<User>();
    }
}
