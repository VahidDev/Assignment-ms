using DomainModels.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
namespace Repository.DAL
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public AppDbContext
            (DbContextOptions<AppDbContext> options, IConfiguration configuration) 
            : base(options) 
        {
            _configuration = configuration;
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Ignore<Volunteer>();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options
            .UseNpgsql(_configuration.GetConnectionString("Default"), builder =>
            {
                builder.MigrationsAssembly(nameof(Repository));
                builder.MigrationsHistoryTable("__ef_assignment_migrations_history");
            }).ReplaceService<IHistoryRepository, EfMigrationsHistory>();
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<JobTitle> JobTitles { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<RoleOffer> RoleOffers { get; set; }
        public DbSet<FunctionalArea> FunctionalAreas { get; set; }
    }
}
