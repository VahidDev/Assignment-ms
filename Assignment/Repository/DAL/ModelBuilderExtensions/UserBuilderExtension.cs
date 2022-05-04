using DomainModels.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace Repository.DAL.ModelBuilderExtensions
{
    public static class UserBuilderExtension
    {
        public static ModelBuilder BuildUsersTable(this ModelBuilder builder)
        {
            builder.Entity<User>()
                .Ignore(c => c.AccessFailedCount)
                .Ignore(u => u.LockoutEnabled)
                .Ignore(u => u.TwoFactorEnabled)
                .Ignore(u => u.PhoneNumber)
                .Ignore(u => u.UserName)
                .Ignore(u => u.EmailConfirmed)
                .Ignore(u => u.AccessFailedCount)
                .Ignore(u => u.ConcurrencyStamp)
                .Ignore(u => u.SecurityStamp)
                .Ignore(u => u.TwoFactorEnabled)
                .Ignore(u => u.LockoutEnd)
                .Ignore(u => u.NormalizedEmail)
                .Ignore(u => u.NormalizedUserName)
                .Ignore(u => u.PhoneNumberConfirmed);
            builder.Entity<User>(builder => builder.ToTable("users"));
            return builder;
        }
    }
}
