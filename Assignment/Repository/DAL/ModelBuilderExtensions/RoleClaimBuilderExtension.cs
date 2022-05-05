using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Repository.DAL.ModelBuilderExtensions
{
    public static class RoleClaimBuilderExtension
    {
        public static ModelBuilder BuildRoleClaimsTable(this ModelBuilder builder)
        {
            builder.Entity<IdentityRoleClaim<int>>().ToTable("user_claims")
                .Property(ut => ut.Id).HasColumnName("id");
            builder.Entity<IdentityRoleClaim<int>>().ToTable("role_claims")
                .Property(ut => ut.ClaimType).HasColumnName("claim_type");
            builder.Entity<IdentityRoleClaim<int>>()
                .Property(ut => ut.ClaimValue).HasColumnName("claim_value");
            builder.Entity<IdentityRoleClaim<int>>()
               .Property(ut => ut.RoleId).HasColumnName("role_id");
            return builder;
        }
    }
}
