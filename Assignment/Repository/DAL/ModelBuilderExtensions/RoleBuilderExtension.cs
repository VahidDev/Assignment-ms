using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace Repository.DAL.ModelBuilderExtensions
{
    public static class RoleBuilderExtension
    {
        public static ModelBuilder BuildRolesTable(this ModelBuilder builder)
        {
            builder.Entity<IdentityRole<int>>()
               .Ignore(r => r.ConcurrencyStamp)
               .Ignore(r => r.NormalizedName)
               .Property(r=>r.Id).HasColumnName("id");
            builder.Entity<IdentityRole<int>>()
                .Property(r => r.Name).HasColumnName("name");
            builder.Entity<IdentityRole<int>>(builder => builder.ToTable("roles"));
            return builder;
        }
    }
}
