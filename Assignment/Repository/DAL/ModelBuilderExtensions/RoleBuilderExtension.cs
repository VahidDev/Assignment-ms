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
               .Ignore(r => r.NormalizedName);
            builder.Entity<IdentityRole<int>>(builder => builder.ToTable("Roles"));
            return builder;
        }
    }
}
