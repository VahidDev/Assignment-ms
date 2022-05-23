using DomainModels.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository.Extensions.ModelBuilderExtensions
{
    internal static class ManyToManyConfigurationExtensions
    {
        public static void ConfigureManyToManyRelationships(this ModelBuilder builder)
        {

            // Many to Many between FunctionalArea and ExcelEntity
            builder.Entity<FunctionalAreaType>()
            .HasMany(p => p.FunctionalAreas)
            .WithMany(p => p.FunctionalAreaTypes)
            .UsingEntity<FunctionalAreaTypeFunctionalArea>(
                j => j
                    .HasOne(pt => pt.FunctionalArea)
                    .WithMany(t => t.ExcelEntityFunctionalAreas)
                    .HasForeignKey(pt => pt.FunctionalAreaId),
                j => j
                    .HasOne(pt => pt.FunctionalAreaType)
                    .WithMany(p => p.FunctionalAreaTypeFunctionalAreas)
                    .HasForeignKey(pt => pt.FunctionalAreaTypeId),
                j =>
                {
                    j.HasKey(t => new { t.FunctionalAreaTypeId, t.FunctionalAreaId });
                });

            // Many to Many between FunctionalArea and JobTitle
            builder.Entity<JobTitle>()
            .HasMany(p => p.FunctionalAreas)
            .WithMany(p => p.JobTitles)
            .UsingEntity<FunctionalAreaJobTitle>(
                j => j
                    .HasOne(pt => pt.FunctionalArea)
                    .WithMany(t => t.FunctionalAreaJobTitles)
                    .HasForeignKey(pt => pt.FunctionalAreaId),
                j => j
                    .HasOne(pt => pt.JobTitle)
                    .WithMany(p => p.FunctionalAreaJobTitles)
                    .HasForeignKey(pt => pt.JobTitleId),
                j =>
                {
                    j.HasKey(t => new { t.JobTitleId, t.FunctionalAreaId });
                });

            // Many to Many between JobTitle and Venues
            builder.Entity<Location>()
            .HasMany(p => p.JobTitles)
            .WithMany(p => p.Locations)
            .UsingEntity<JobTitleLocation>(
                j => j
                    .HasOne(pt => pt.JobTitle)
                    .WithMany(t => t.JobTitleVenues)
                    .HasForeignKey(pt => pt.JobTitleId),
                j => j
                    .HasOne(pt => pt.Location)
                    .WithMany(p => p.JobTitleVenues)
                    .HasForeignKey(pt => pt.LocationId),
                j =>
                {
                    j.HasKey(t => new { t.JobTitleId, t.LocationId });
                });
        }
    }
}
