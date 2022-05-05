using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace Repository.DAL.ModelBuilderExtensions
{
    public static class UserTokenBuilderExtension
    {
        public static ModelBuilder BuildUserTokensTable(this ModelBuilder builder)
        {
            builder.Entity<IdentityUserToken<int>>().ToTable("user_tokens")
                .Property(ut => ut.UserId).HasColumnName("user_id");
            builder.Entity<IdentityUserToken<int>>()
                .Property(ut => ut.Value).HasColumnName("value");
            builder.Entity<IdentityUserToken<int>>()
                .Property(ut => ut.Name).HasColumnName("name");
            builder.Entity<IdentityUserToken<int>>()
                .Property(ut => ut.LoginProvider).HasColumnName("login_provider");
            return builder;
        }
    }
}
