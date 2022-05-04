using DomainModels.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Repository.DAL.ModelBuilderExtensions;

namespace Repository.DAL
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
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
            builder.BuildUsersTable();
            builder.BuildRolesTable();
            builder.BuildUserRolesTable();
            builder.Ignore<IdentityUserToken<int>>();
            builder.Ignore<IdentityUserLogin<int>>();
            builder.Ignore<IdentityUserClaim<int>>();
            builder.Ignore<IdentityRoleClaim<int>>();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options
            .UseNpgsql(_configuration.GetConnectionString("Default"), builder =>
            {
                builder.MigrationsAssembly(nameof(Repository));
                builder.MigrationsHistoryTable("__ef_migrations_history");
            }).ReplaceService<IHistoryRepository, EfMigrationsHistory>();
        public DbSet<PassportType> PassportTypes { get; set; }
        public DbSet<IdDocumentType> IdDocumentTypes { get; set; }

        public DbSet<InternationalAccommodationPreference> 
            InternationalAccommodationPreferences { get; set; }
        public DbSet<DrivingLicenseType> DrivingLicenseTypes { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<JobTitle> JobTitles { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<RoleOffer> RoleOffers { get; set; }
        public DbSet<FunctionalArea> FunctionalAreas { get; set; }
    }
}
