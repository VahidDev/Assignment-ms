using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Repository.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "functional_area_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_functional_area_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "functional_areas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    code = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_functional_areas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "job_titles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    code = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_job_titles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "templates",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_templates", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "venues",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    RoleOfferLocationCode = table.Column<string>(name: "Role Offer - Location Code", type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_venues", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "functional_area_type_functional_areas",
                columns: table => new
                {
                    functional_area_id = table.Column<int>(type: "integer", nullable: false),
                    functional_area_type_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_functional_area_type_functional_areas", x => new { x.functional_area_type_id, x.functional_area_id });
                    table.ForeignKey(
                        name: "FK_functional_area_type_functional_areas_functional_area_types~",
                        column: x => x.functional_area_type_id,
                        principalTable: "functional_area_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_functional_area_type_functional_areas_functional_areas_func~",
                        column: x => x.functional_area_id,
                        principalTable: "functional_areas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "functional_area_job_titles",
                columns: table => new
                {
                    functional_area_id = table.Column<int>(type: "integer", nullable: false),
                    job_title_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_functional_area_job_titles", x => new { x.job_title_id, x.functional_area_id });
                    table.ForeignKey(
                        name: "FK_functional_area_job_titles_functional_areas_functional_area~",
                        column: x => x.functional_area_id,
                        principalTable: "functional_areas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_functional_area_job_titles_job_titles_job_title_id",
                        column: x => x.job_title_id,
                        principalTable: "job_titles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "filters",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    requirement = table.Column<string>(type: "text", nullable: true),
                    @operator = table.Column<string>(name: "operator", type: "text", nullable: true),
                    value = table.Column<string>(type: "text", nullable: true),
                    template_id = table.Column<int>(type: "integer", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_filters", x => x.id);
                    table.ForeignKey(
                        name: "FK_filters_templates_template_id",
                        column: x => x.template_id,
                        principalTable: "templates",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "job_title_venues",
                columns: table => new
                {
                    job_title_id = table.Column<int>(type: "integer", nullable: false),
                    location_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_job_title_venues", x => new { x.job_title_id, x.location_id });
                    table.ForeignKey(
                        name: "FK_job_title_venues_job_titles_job_title_id",
                        column: x => x.job_title_id,
                        principalTable: "job_titles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_job_title_venues_venues_location_id",
                        column: x => x.location_id,
                        principalTable: "venues",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "role_offers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    functional_area_type_id = table.Column<int>(type: "integer", nullable: true),
                    functional_area_id = table.Column<int>(type: "integer", nullable: true),
                    job_title_id = table.Column<int>(type: "integer", nullable: true),
                    location_id = table.Column<int>(type: "integer", nullable: true),
                    role_offer_id = table.Column<int>(type: "integer", nullable: false),
                    total_demand = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_offers", x => x.id);
                    table.ForeignKey(
                        name: "FK_role_offers_functional_area_types_functional_area_type_id",
                        column: x => x.functional_area_type_id,
                        principalTable: "functional_area_types",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_role_offers_functional_areas_functional_area_id",
                        column: x => x.functional_area_id,
                        principalTable: "functional_areas",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_role_offers_job_titles_job_title_id",
                        column: x => x.job_title_id,
                        principalTable: "job_titles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_role_offers_venues_location_id",
                        column: x => x.location_id,
                        principalTable: "venues",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_filters_template_id",
                table: "filters",
                column: "template_id");

            migrationBuilder.CreateIndex(
                name: "IX_functional_area_job_titles_functional_area_id",
                table: "functional_area_job_titles",
                column: "functional_area_id");

            migrationBuilder.CreateIndex(
                name: "IX_functional_area_type_functional_areas_functional_area_id",
                table: "functional_area_type_functional_areas",
                column: "functional_area_id");

            migrationBuilder.CreateIndex(
                name: "IX_job_title_venues_location_id",
                table: "job_title_venues",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_offers_functional_area_id",
                table: "role_offers",
                column: "functional_area_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_offers_functional_area_type_id",
                table: "role_offers",
                column: "functional_area_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_offers_job_title_id",
                table: "role_offers",
                column: "job_title_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_offers_location_id",
                table: "role_offers",
                column: "location_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "filters");

            migrationBuilder.DropTable(
                name: "functional_area_job_titles");

            migrationBuilder.DropTable(
                name: "functional_area_type_functional_areas");

            migrationBuilder.DropTable(
                name: "job_title_venues");

            migrationBuilder.DropTable(
                name: "role_offers");

            migrationBuilder.DropTable(
                name: "templates");

            migrationBuilder.DropTable(
                name: "functional_area_types");

            migrationBuilder.DropTable(
                name: "functional_areas");

            migrationBuilder.DropTable(
                name: "job_titles");

            migrationBuilder.DropTable(
                name: "venues");
        }
    }
}
