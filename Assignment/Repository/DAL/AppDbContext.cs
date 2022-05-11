using DomainModels.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace Repository.DAL
{
    public class AppDbContext : DbContext
    {

        public AppDbContext
            (DbContextOptions<AppDbContext> options): base(options) {}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Ignore<Volunteer>();
            base.OnModelCreating(builder);
        }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<JobTitle> JobTitles { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<RoleOffer> RoleOffers { get; set; }
        public DbSet<FunctionalArea> FunctionalAreas { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
    }
}
