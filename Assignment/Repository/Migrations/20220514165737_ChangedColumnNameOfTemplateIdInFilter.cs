using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class ChangedColumnNameOfTemplateIdInFilter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_filter_template_TemplateId",
                table: "filter");

            migrationBuilder.RenameColumn(
                name: "TemplateId",
                table: "filter",
                newName: "template_id");

            migrationBuilder.RenameIndex(
                name: "IX_filter_TemplateId",
                table: "filter",
                newName: "IX_filter_template_id");

            migrationBuilder.AddForeignKey(
                name: "FK_filter_template_template_id",
                table: "filter",
                column: "template_id",
                principalTable: "template",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_filter_template_template_id",
                table: "filter");

            migrationBuilder.RenameColumn(
                name: "template_id",
                table: "filter",
                newName: "TemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_filter_template_id",
                table: "filter",
                newName: "IX_filter_TemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_filter_template_TemplateId",
                table: "filter",
                column: "TemplateId",
                principalTable: "template",
                principalColumn: "id");
        }
    }
}
