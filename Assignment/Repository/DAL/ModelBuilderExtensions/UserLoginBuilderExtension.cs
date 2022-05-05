using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Repository.DAL.ModelBuilderExtensions
{
    public static class UserLoginBuilderExtension
    {
        public static ModelBuilder BuildUserLoginsTable(this ModelBuilder builder)
        {
            builder.Entity<IdentityUserLogin<int>>().ToTable("user_logins")
               .Property(ut => ut.UserId).HasColumnName("user_id");
            builder.Entity<IdentityUserLogin<int>>()
                .Property(ut => ut.ProviderKey).HasColumnName("provider_key");
            builder.Entity<IdentityUserLogin<int>>()
                .Property(ut => ut.ProviderDisplayName).HasColumnName("provider_display_name");
            builder.Entity<IdentityUserLogin<int>>()
               .Property(ut => ut.LoginProvider).HasColumnName("login_provider");
            return builder;
        }
    }
}
