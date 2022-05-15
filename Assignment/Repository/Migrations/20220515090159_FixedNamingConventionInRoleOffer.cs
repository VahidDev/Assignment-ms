using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class FixedNamingConventionInRoleOffer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Headcount",
                table: "role_offers",
                newName: "headcount");

            migrationBuilder.RenameColumn(
                name: "RoleOfferId",
                table: "role_offers",
                newName: "role_offer_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "headcount",
                table: "role_offers",
                newName: "Headcount");

            migrationBuilder.RenameColumn(
                name: "role_offer_id",
                table: "role_offers",
                newName: "RoleOfferId");
        }
    }
}
