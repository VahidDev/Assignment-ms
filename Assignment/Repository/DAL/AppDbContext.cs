using DomainModels.Models.Entities;
using DomainModels.Models.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions.ModelBuilderExtensions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Repository.DAL
{
    public class AppDbContext : DbContext
    {

        public AppDbContext
            (DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // must always be on top
            base.OnModelCreating(builder);

            // UnComment this section when migrating

            //builder.Ignore<Volunteer>();
            //builder.Ignore<History>();

            builder.Entity<Volunteer>().Ignore(r => r.Id).HasKey(r => r.CandidateId);
            builder.Entity<History>()
                .Ignore(r => r.IsDeleted)
                .Ignore(r => r.UpdatedAt)
                .Ignore(r => r.DeletedAt);

            builder.ConfigureManyToManyRelationships();
        }
        public async override Task<int> SaveChangesAsync
            (bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            foreach (var entity in ChangeTracker.Entries<Entity>())
            {
                if (entity.State == EntityState.Modified && !entity.Entity.IsDeleted)
                {
                    entity.Entity.UpdatedAt = DateTime.Now;
                }
                else if (entity.State == EntityState.Added)
                {
                    entity.Entity.CreatedAt = DateTime.Now;
                }
                else if (entity.Entity.IsDeleted)
                {
                    entity.Entity.DeletedAt = DateTime.Now;
                }
            }
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        
        public DbSet<Location> Locations { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<JobTitle> JobTitles { get; set; }
        public DbSet<RoleOffer> RoleOffers { get; set; }
        public DbSet<FunctionalArea> FunctionalAreas { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<Filter> Filters { get; set; }
        public DbSet<FunctionalAreaType> FunctionalAreaTypes { get; set; }
        public DbSet<FunctionalRequirement> FunctionalRequirements { get; set; }
        public DbSet<Requirement> Requirements { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<History> Histories { get; set; }
    }
}
