using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Repository.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "driving_license_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deleted_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_driving_license_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "functional_areas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    code = table.Column<string>(type: "text", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deleted_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_functional_areas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "id_document_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deleted_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_id_document_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "international_accommodation_preferences",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deleted_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_international_accommodation_preferences", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "job_titles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    code = table.Column<string>(type: "text", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deleted_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
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
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deleted_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_locations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "passport_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deleted_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_passport_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "venues",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    code = table.Column<string>(type: "text", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deleted_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_venues", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "text", nullable: true),
                    last_name = table.Column<string>(type: "text", nullable: true),
                    picture = table.Column<string>(type: "text", nullable: true),
                    phone = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    nationality = table.Column<byte>(type: "smallint", nullable: false),
                    _country_issued = table.Column<byte>(type: "smallint", nullable: false),
                    gender_for_accreditation = table.Column<string>(type: "text", nullable: true),
                    dob = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    current_occupation = table.Column<string>(type: "text", nullable: true),
                    IdDocumentTypeId = table.Column<int>(type: "integer", nullable: true),
                    qid_number = table.Column<string>(type: "text", nullable: true),
                    id_document_expiry_date_q22 = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    passport_number = table.Column<string>(type: "text", nullable: true),
                    id_document_expiry_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    id_document_country_of_issue = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    passport_expiry_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PassportTypeId = table.Column<int>(type: "integer", nullable: true),
                    qatari_driving_license = table.Column<bool>(type: "boolean", nullable: false),
                    DrivingLicenseTypeId = table.Column<int>(type: "integer", nullable: true),
                    country = table.Column<byte>(type: "smallint", nullable: false),
                    InternationalAccommodationPreferenceId = table.Column<int>(type: "integer", nullable: true),
                    medical_conditions = table.Column<string>(type: "text", nullable: true),
                    disability_yes_no = table.Column<bool>(type: "boolean", nullable: false),
                    disability_type = table.Column<string>(type: "text", nullable: true),
                    disability_others = table.Column<string>(type: "text", nullable: true),
                    social_worker_caregiver_support = table.Column<string>(type: "text", nullable: true),
                    dietary_requirement_identification = table.Column<string>(type: "text", nullable: true),
                    special_dietary_options = table.Column<string>(type: "text", nullable: true),
                    alergies_other = table.Column<string>(type: "text", nullable: true),
                    covid19_vaccinated = table.Column<string>(name: "covid-19_vaccinated", type: "text", nullable: true),
                    vaccine_type_multi = table.Column<string>(type: "text", nullable: true),
                    final_vaccine_dose_date = table.Column<string>(type: "text", nullable: true),
                    education_onechoice = table.Column<string>(type: "text", nullable: true),
                    area_of_study = table.Column<string>(type: "text", nullable: true),
                    english_fluency_level = table.Column<string>(type: "text", nullable: true),
                    arabic_fluency_level = table.Column<string>(type: "text", nullable: true),
                    additional_language_1 = table.Column<string>(type: "text", nullable: true),
                    additional_language_1_fluency_level = table.Column<string>(type: "text", nullable: true),
                    additional_language_2 = table.Column<string>(type: "text", nullable: true),
                    additional_language_2_fluency_level = table.Column<string>(type: "text", nullable: true),
                    additional_language_3 = table.Column<string>(type: "text", nullable: true),
                    additional_language_3_fluency_level = table.Column<string>(type: "text", nullable: true),
                    additional_language_4 = table.Column<string>(type: "text", nullable: true),
                    additional_language_4_fluency_level = table.Column<string>(type: "text", nullable: true),
                    languages_other = table.Column<string>(type: "text", nullable: true),
                    interpretation_and_translation_experience = table.Column<string>(type: "text", nullable: true),
                    certified_translator_language = table.Column<string>(type: "text", nullable: true),
                    describe_your_it_skills = table.Column<string>(type: "text", nullable: true),
                    skill_1 = table.Column<string>(type: "text", nullable: true),
                    skill_2 = table.Column<string>(type: "text", nullable: true),
                    skill_3 = table.Column<string>(type: "text", nullable: true),
                    skill_4 = table.Column<string>(type: "text", nullable: true),
                    skill_5 = table.Column<string>(type: "text", nullable: true),
                    skill_6 = table.Column<string>(type: "text", nullable: true),
                    previous_volunteering_experience = table.Column<string>(type: "text", nullable: true),
                    volunteer_experience = table.Column<string>(type: "text", nullable: true),
                    other_role = table.Column<string>(type: "text", nullable: true),
                    events_type_participations = table.Column<string>(type: "text", nullable: true),
                    other_event_type = table.Column<string>(type: "text", nullable: true),
                    volunteer_hours_yearly = table.Column<string>(type: "text", nullable: true),
                    preferred_functional_area = table.Column<string>(type: "text", nullable: true),
                    fwc_are_you_interested_in_a_leadership_role = table.Column<string>(type: "text", nullable: true),
                    fwc_leadership_experience = table.Column<string>(type: "text", nullable: true),
                    work_experience = table.Column<string>(type: "text", nullable: true),
                    ceremonies_yes_no = table.Column<bool>(type: "boolean", nullable: false),
                    cast_yes_no = table.Column<bool>(type: "boolean", nullable: false),
                    cast_options = table.Column<string>(type: "text", nullable: true),
                    motivation_to_volunteer_at_fwc = table.Column<string>(type: "text", nullable: true),
                    leave_or_arrange = table.Column<string>(type: "text", nullable: true),
                    local__international_volunteer = table.Column<string>(type: "text", nullable: true),
                    language_path__english__arabic = table.Column<string>(name: "language_path_-_english__arabic", type: "text", nullable: true),
                    fwc_availability_pre_tournament_stage_one = table.Column<string>(type: "text", nullable: true),
                    fwc_availability_pre_tournament_stage_two = table.Column<string>(type: "text", nullable: true),
                    availability_during_tournament = table.Column<string>(type: "text", nullable: true),
                    daily_availability_shift_morning = table.Column<string>(type: "text", nullable: true),
                    daily_availability_shift_night = table.Column<string>(type: "text", nullable: true),
                    daily_availability_shift_overnight = table.Column<string>(type: "text", nullable: true),
                    candidate_under_18 = table.Column<string>(type: "text", nullable: true),
                    special_groups_international = table.Column<string>(type: "text", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deleted_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                    table.ForeignKey(
                        name: "FK_users_driving_license_types_DrivingLicenseTypeId",
                        column: x => x.DrivingLicenseTypeId,
                        principalTable: "driving_license_types",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_users_id_document_types_IdDocumentTypeId",
                        column: x => x.IdDocumentTypeId,
                        principalTable: "id_document_types",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_users_international_accommodation_preferences_International~",
                        column: x => x.InternationalAccommodationPreferenceId,
                        principalTable: "international_accommodation_preferences",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_users_passport_types_PassportTypeId",
                        column: x => x.PassportTypeId,
                        principalTable: "passport_types",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "role_offers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    venue_id = table.Column<int>(type: "integer", nullable: true),
                    functional_area_id = table.Column<int>(type: "integer", nullable: true),
                    location_id = table.Column<int>(type: "integer", nullable: true),
                    job_title_id = table.Column<int>(type: "integer", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deleted_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_offers", x => x.id);
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
                    table.ForeignKey(
                        name: "FK_role_offers_venues_venue_id",
                        column: x => x.venue_id,
                        principalTable: "venues",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "FK_user_roles_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_roles_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_role_offers_functional_area_id",
                table: "role_offers",
                column: "functional_area_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_offers_job_title_id",
                table: "role_offers",
                column: "job_title_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_offers_location_id",
                table: "role_offers",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_offers_venue_id",
                table: "role_offers",
                column: "venue_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_roles_role_id",
                table: "user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_DrivingLicenseTypeId",
                table: "users",
                column: "DrivingLicenseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_users_IdDocumentTypeId",
                table: "users",
                column: "IdDocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_users_InternationalAccommodationPreferenceId",
                table: "users",
                column: "InternationalAccommodationPreferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_users_PassportTypeId",
                table: "users",
                column: "PassportTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "role_offers");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropTable(
                name: "functional_areas");

            migrationBuilder.DropTable(
                name: "job_titles");

            migrationBuilder.DropTable(
                name: "locations");

            migrationBuilder.DropTable(
                name: "venues");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "driving_license_types");

            migrationBuilder.DropTable(
                name: "id_document_types");

            migrationBuilder.DropTable(
                name: "international_accommodation_preferences");

            migrationBuilder.DropTable(
                name: "passport_types");
        }
    }
}
