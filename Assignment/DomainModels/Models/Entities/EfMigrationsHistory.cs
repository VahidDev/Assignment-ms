using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Migrations.Internal;
namespace DomainModels.Models.Entities
{
    public class EfMigrationsHistory : NpgsqlHistoryRepository
    {
        public EfMigrationsHistory(HistoryRepositoryDependencies dependencies)
            : base(dependencies) { }

        protected override void ConfigureTable
            (EntityTypeBuilder<HistoryRow> history)
        {
            base.ConfigureTable(history);

            history.Property(h => h.MigrationId).HasColumnName("migration_id");
            history.Property(h => h.ProductVersion).HasColumnName("product_version");
        }
    }
}
