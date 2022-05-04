using DomainModels.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.DAL.ModelBuilderExtensions;

namespace Repository.DAL
{
    public class AppDbContext : IdentityDbContext<User,IdentityRole<int>,int> 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.BuildUsersTable();
            builder.BuildRolesTable();
        }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<JobTitle> JobTitles { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<RoleOffer> RoleOffers { get; set; }
        public DbSet<FunctionalArea> FunctionalAreas { get; set; }
    }
}
