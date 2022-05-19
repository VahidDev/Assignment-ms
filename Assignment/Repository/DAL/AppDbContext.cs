using DomainModels.Models.Entities;
using DomainModels.Models.Entities.Base;
using DomainModels.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Npgsql;
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
            // UnComment this section when migrating
            builder.Ignore<Volunteer>();
            //when creating custom enums
            //builder.HasPostgresEnum<Statusenum>();
            base.OnModelCreating(builder);
            builder.ConfigureManyToManyRelationships();
        }
        public async override Task<int> SaveChangesAsync
            (bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {

            foreach (var entity in ChangeTracker.Entries<Entity>())
            {
                if (entity.Entity.Id > 0)
                {
                    entity.State = EntityState.Modified;
                }
            }
            //foreach (var entity in ChangeTracker.Entries<Entity>())
            //{
            //    if (entity.State == EntityState.Modified && !entity.Entity.IsDeleted)
            //    {
            //        entity.Entity.UpdatedAt = DateTime.Now;
            //    }else if (entity.State == EntityState.Added)
            //    {
            //        entity.Entity.CreatedAt = DateTime.Now;
            //    }
            //    // if the entity is not added (which means it is only modified)
            //    // then ignore changes on CreatedAt prop
            //    if (entity.State != EntityState.Added)
            //    {
            //        entity.Property<DateTime?>(nameof(Entity.CreatedAt)).IsModified = false;
            //    }
            //    if (entity.Entity.IsDeleted)
            //    {
            //        entity.Entity.DeletedAt = DateTime.Now;
            //    }
            //}
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        static AppDbContext()
            => NpgsqlConnection.GlobalTypeMapper.MapEnum<Statusenum>();
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<JobTitle> JobTitles { get; set; }
        public DbSet<RoleOffer> RoleOffers { get; set; }
        public DbSet<FunctionalArea> FunctionalAreas { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<Filter> Filters { get; set; }
        public DbSet<ExcelEntity> ExcelEntities { get; set; }
    }
}
