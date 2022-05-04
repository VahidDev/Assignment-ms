using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Repository.DAL.ModelBuilderExtensions
{
    public static class UserRolesBuilderExtension
    {
        public static ModelBuilder BuildUserRolesTable(this ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<int>>()
                .Property(ur=>ur.UserId).HasColumnName("user_id");
            builder.Entity<IdentityUserRole<int>>()
               .Property(ur => ur.RoleId).HasColumnName("role_id");
            builder.Entity<IdentityUserRole<int>>().ToTable("user_roles");
            return builder;
        }
    }
}
