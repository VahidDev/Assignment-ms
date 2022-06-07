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
                name: "locations",
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
                    table.PrimaryKey("PK_locations", x => x.id);
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
                name: "job_title_locations",
                columns: table => new
                {
                    job_title_id = table.Column<int>(type: "integer", nullable: false),
                    location_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_job_title_locations", x => new { x.job_title_id, x.location_id });
                    table.ForeignKey(
                        name: "FK_job_title_locations_job_titles_job_title_id",
                        column: x => x.job_title_id,
                        principalTable: "job_titles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_job_title_locations_locations_location_id",
                        column: x => x.location_id,
                        principalTable: "locations",
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
                    assignee_demand = table.Column<int>(type: "integer", nullable: false),
                    waitlist_demand = table.Column<int>(type: "integer", nullable: false),
                    level_of_confidence = table.Column<int>(type: "integer", nullable: true),
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
                        name: "FK_role_offers_locations_location_id",
                        column: x => x.location_id,
                        principalTable: "locations",
                        principalColumn: "id");
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
                name: "reports",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    volunteer_columns = table.Column<string>(type: "text", nullable: true),
                    role_offer_columns = table.Column<string>(type: "text", nullable: true),
                    role_offer_template_id = table.Column<int>(type: "integer", nullable: true),
                    volunteer_template_id = table.Column<int>(type: "integer", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reports", x => x.id);
                    table.ForeignKey(
                        name: "FK_reports_templates_role_offer_template_id",
                        column: x => x.role_offer_template_id,
                        principalTable: "templates",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_reports_templates_volunteer_template_id",
                        column: x => x.volunteer_template_id,
                        principalTable: "templates",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "functional_requirements",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    functional_requirement_id = table.Column<int>(type: "integer", nullable: false),
                    role_offer_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_functional_requirements", x => x.id);
                    table.ForeignKey(
                        name: "FK_functional_requirements_role_offers_role_offer_id",
                        column: x => x.role_offer_id,
                        principalTable: "role_offers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "requirements",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    requirement_name = table.Column<string>(type: "text", nullable: true),
                    @operator = table.Column<string>(name: "operator", type: "text", nullable: true),
                    value = table.Column<string>(type: "text", nullable: true),
                    functional_requirement_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_requirements", x => x.id);
                    table.ForeignKey(
                        name: "FK_requirements_functional_requirements_functional_requirement~",
                        column: x => x.functional_requirement_id,
                        principalTable: "functional_requirements",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_functional_requirements_role_offer_id",
                table: "functional_requirements",
                column: "role_offer_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_job_title_locations_location_id",
                table: "job_title_locations",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "IX_reports_role_offer_template_id",
                table: "reports",
                column: "role_offer_template_id");

            migrationBuilder.CreateIndex(
                name: "IX_reports_volunteer_template_id",
                table: "reports",
                column: "volunteer_template_id");

            migrationBuilder.CreateIndex(
                name: "IX_requirements_functional_requirement_id",
                table: "requirements",
                column: "functional_requirement_id");

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
                name: "job_title_locations");

            migrationBuilder.DropTable(
                name: "reports");

            migrationBuilder.DropTable(
                name: "requirements");

            migrationBuilder.DropTable(
                name: "templates");

            migrationBuilder.DropTable(
                name: "functional_requirements");

            migrationBuilder.DropTable(
                name: "role_offers");

            migrationBuilder.DropTable(
                name: "functional_area_types");

            migrationBuilder.DropTable(
                name: "functional_areas");

            migrationBuilder.DropTable(
                name: "job_titles");

            migrationBuilder.DropTable(
                name: "locations");
        }
    }
}
