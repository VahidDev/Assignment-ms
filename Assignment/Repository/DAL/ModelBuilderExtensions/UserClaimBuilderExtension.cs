using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Repository.DAL.ModelBuilderExtensions
{
    public static class UserClaimBuilderExtension
    {
        public static ModelBuilder BuildUserClaimsTable(this ModelBuilder builder)
        {
            builder.Entity<IdentityUserClaim<int>>().ToTable("user_claims")
                .Property(ut => ut.UserId).HasColumnName("user_id");
            builder.Entity<IdentityUserClaim<int>>()
                .Property(ut => ut.Id).HasColumnName("id");
            builder.Entity<IdentityUserClaim<int>>()
                .Property(ut => ut.ClaimType).HasColumnName("claim_type");
            builder.Entity<IdentityUserClaim<int>>()
                .Property(ut => ut.ClaimValue).HasColumnName("claim_value");
            return builder;
        }
    }
}
