using DomainModels.Models.Entities;
using DomainModels.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Repository.DAL
{
    public class AppDbContext : DbContext
    {

        public AppDbContext
            (DbContextOptions<AppDbContext> options): base(options) {}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // UnComment this section when migrating
            //builder.Ignore<Volunteer>();

            //when creating custom enums
            //builder.HasPostgresEnum<Statusenum>();
            base.OnModelCreating(builder);
        }
        static AppDbContext()
            => NpgsqlConnection.GlobalTypeMapper.MapEnum<Statusenum>();
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<JobTitle> JobTitles { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<RoleOffer> RoleOffers { get; set; }
        public DbSet<FunctionalArea> FunctionalAreas { get; set; }
    }
}
