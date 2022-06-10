using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class RemovedFRIdFromFR : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "functional_requirement_id",
                table: "functional_requirements");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "functional_requirement_id",
                table: "functional_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
