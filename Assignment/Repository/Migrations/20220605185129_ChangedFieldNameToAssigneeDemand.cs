using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class ChangedFieldNameToAssigneeDemand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "waitlist_count",
                table: "role_offers");

            migrationBuilder.RenameColumn(
                name: "waitlist_fulfillment",
                table: "role_offers",
                newName: "waitlist_demand");

            migrationBuilder.RenameColumn(
                name: "role_offer_fulfillment",
                table: "role_offers",
                newName: "assignee_demand");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "waitlist_demand",
                table: "role_offers",
                newName: "waitlist_fulfillment");

            migrationBuilder.RenameColumn(
                name: "assignee_demand",
                table: "role_offers",
                newName: "role_offer_fulfillment");

            migrationBuilder.AddColumn<int>(
                name: "waitlist_count",
                table: "role_offers",
                type: "integer",
                nullable: true);
        }
    }
}
