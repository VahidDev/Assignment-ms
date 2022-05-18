using DomainModels.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository.Extensions.ModelBuilderExtensions
{
    internal static class ManyToManyConfigurationExtensions
    {
        public static void ConfigureManyToManyRelationships(this ModelBuilder builder)
        {

            // Many to Many between FunctionalArea and ExcelEntity
            builder.Entity<ExcelEntity>()
            .HasMany(p => p.FunctionalAreas)
            .WithMany(p => p.ExcelEntities)
            .UsingEntity<ExcelEntityFunctionalAreas>(
                j => j
                    .HasOne(pt => pt.FunctionalArea)
                    .WithMany(t => t.ExcelEntityFunctionalAreas)
                    .HasForeignKey(pt => pt.FunctionalAreaId),
                j => j
                    .HasOne(pt => pt.ExcelEntity)
                    .WithMany(p => p.ExcelEntityFunctionalAreas)
                    .HasForeignKey(pt => pt.ExcelEntityId),
                j =>
                {
                    j.HasKey(t => new { t.ExcelEntityId, t.FunctionalAreaId });
                });

            // Many to Many between FunctionalArea and JobTitle
            builder.Entity<JobTitle>()
            .HasMany(p => p.FunctionalAreas)
            .WithMany(p => p.JobTitles)
            .UsingEntity<FunctionalAreaJobTitles>(
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
            builder.Entity<Venue>()
            .HasMany(p => p.JobTitles)
            .WithMany(p => p.Venues)
            .UsingEntity<JobTitleVenues>(
                j => j
                    .HasOne(pt => pt.JobTitle)
                    .WithMany(t => t.JobTitleVenues)
                    .HasForeignKey(pt => pt.JobTitleId),
                j => j
                    .HasOne(pt => pt.Venue)
                    .WithMany(p => p.JobTitleVenues)
                    .HasForeignKey(pt => pt.VenueId),
                j =>
                {
                    j.HasKey(t => new { t.JobTitleId, t.VenueId });
                });
        }
    }
}
