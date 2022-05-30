using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Repository.Migrations
{
    public partial class AddedReports : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "waitlist_count",
                table: "role_offers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "level_of_confidence",
                table: "role_offers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "report_id",
                table: "filters",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "reports",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    volunteer_columns = table.Column<string>(type: "text", nullable: true),
                    role_offer_columns = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reports", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_filters_report_id",
                table: "filters",
                column: "report_id");

            migrationBuilder.AddForeignKey(
                name: "FK_filters_reports_report_id",
                table: "filters",
                column: "report_id",
                principalTable: "reports",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_filters_reports_report_id",
                table: "filters");

            migrationBuilder.DropTable(
                name: "reports");

            migrationBuilder.DropIndex(
                name: "IX_filters_report_id",
                table: "filters");

            migrationBuilder.DropColumn(
                name: "report_id",
                table: "filters");

            migrationBuilder.AlterColumn<int>(
                name: "waitlist_count",
                table: "role_offers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "level_of_confidence",
                table: "role_offers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
