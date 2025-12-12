using BankingApp.Models;
using BankingApp.Models.Entities;
using BankingApp.Models.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Persistence.Context
{
    public class BankContext(DbContextOptions<BankContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasOne(u => u.AccountDetails)
                .WithOne(ac => ac.User)
                .HasForeignKey<AccountDetails>(ac => ac.UserId);

            builder.Entity<User>()
                .HasOne(u => u.CardInformation)
                .WithOne(ci => ci.User)
                .HasForeignKey<CardInformation>(ci => ci.UserId);

            SeedAdminData(builder);
            SeedRoleData(builder);


        }

        private static void SeedAdminData(ModelBuilder modelBuilder)
        {
            Guid adminUserId = new Guid("d2719e67-52f4-4f9c-bdb2-123456789abc");
            Guid bankId = new Guid("d2719e67-52f4-4f9c-bdb2-225456789abc");
            Guid adminRoleId = new Guid("c8f2e5ab-9f34-4b97-8b7c-1a5e86897e42");

            var role = new Role("Admin", "Has full permissions")
            {
                DateCreated = DateTime.UtcNow,
                Id = adminRoleId
            };


            string firstName = "Admin";
            string lastName = "Manager";
            string address = "123 Admin Street, City, Country";
            string email = "admin001@gmail.com";
            DateTime dob = DateTime.SpecifyKind(new DateTime(1990, 11, 10), DateTimeKind.Utc);
            string phoneNumber = "09055123478";
            Gender gender = Gender.Male;
            var hasher = new PasswordHasher<object>();
            var passwordHash = hasher.HashPassword(null, "Admin@001");

            var bankBranch = BankBranch.KwaraState;

            var bank = new Bank("GTB", bankBranch)
            {
                Id = bankId,
                DateCreated = DateTime.UtcNow
            };

            var adminAccountDetails = new AccountDetails(adminUserId, "0234032001", AccountType.Savings)
            {
                DateCreated = DateTime.UtcNow,
                Id = new Guid("c8f2e5ab-9f34-4b97-8b7c-1a5e98c77e42"),
                UserId = adminUserId,

            };

            var adminCardInformation = new CardInformation(adminUserId, $"{firstName} {lastName}", "2345 6780 0877 9997", "179", "12 /29", "GTB")
            {
                DateCreated = DateTime.UtcNow,
                Id = new Guid("c8f2e5ab-9f34-4b97-8b7c-1a5e98c77e67")
            };

            var adminUser = new User
                (
                    firstName,
                    lastName,
                    address,
                    email,
                    dob,
                    passwordHash,
                    phoneNumber,
                    gender,
                    bankId,
                    adminRoleId





                )
            {
                Id = adminUserId,
                DateCreated = DateTime.UtcNow,

            };

            modelBuilder.Entity<Role>().HasData(role);
            modelBuilder.Entity<Bank>().HasData(bank);
            modelBuilder.Entity<User>().HasData(adminUser);
            modelBuilder.Entity<AccountDetails>().HasData(adminAccountDetails);
            modelBuilder.Entity<CardInformation>().HasData(adminCardInformation);

        }



        private void SeedRoleData(ModelBuilder modelBuilder)
        {

            var role = new Role("Customer")
            {
                Id = new Guid("c8f2e5ab-9f34-4b97-8b7c-1a5e86c77e76"),
                DateCreated = DateTime.SpecifyKind(new DateTime(2025, 11, 10), DateTimeKind.Utc),
            };

            modelBuilder.Entity<Role>().HasData(role);
        }

        DbSet<AccountDetails> AccountDetails => Set<AccountDetails>();
        DbSet<CardInformation> CardInformations => Set<CardInformation>();
        DbSet<Bank> Banks => Set<Bank>();
        DbSet<User> Users => Set<User>();
    }
}
