using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class RemovedFRForeignKeyFromRoleOffer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_functional_requirements_role_offers_role_offer_id",
                table: "functional_requirements");

            migrationBuilder.DropIndex(
                name: "IX_functional_requirements_role_offer_id",
                table: "functional_requirements");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_functional_requirements_role_offer_id",
                table: "functional_requirements",
                column: "role_offer_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_functional_requirements_role_offers_role_offer_id",
                table: "functional_requirements",
                column: "role_offer_id",
                principalTable: "role_offers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
