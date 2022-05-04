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
                name: "FunctionalAreas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FunctionalAreas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobTitles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTitles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Picture = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Nationality = table.Column<byte>(type: "smallint", nullable: false),
                    CountryIssued = table.Column<byte>(type: "smallint", nullable: false),
                    GenderForAccreditation = table.Column<string>(type: "text", nullable: true),
                    Dob = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CurrentOccupation = table.Column<string>(type: "text", nullable: true),
                    IdDocumentType = table.Column<string>(type: "text", nullable: true),
                    QidNumber = table.Column<string>(type: "text", nullable: true),
                    IdDocumentExpiryDateQ22 = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PassportNumber = table.Column<string>(type: "text", nullable: true),
                    IdDocumentExpiryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IdDocumentCountryOfIssue = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PassportExpiryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PassportType = table.Column<string>(type: "text", nullable: true),
                    QatariDrivingLicense = table.Column<bool>(type: "boolean", nullable: false),
                    DrivingLicenseType = table.Column<string>(type: "text", nullable: true),
                    Country = table.Column<byte>(type: "smallint", nullable: false),
                    InternationalAccommodationPreference = table.Column<string>(type: "text", nullable: true),
                    MedicalConditions = table.Column<string>(type: "text", nullable: true),
                    DisabilityYesNo = table.Column<bool>(type: "boolean", nullable: false),
                    DisabilityType = table.Column<string>(type: "text", nullable: true),
                    DisabilityOthers = table.Column<string>(type: "text", nullable: true),
                    SocialWorkerCaregiverSupport = table.Column<string>(type: "text", nullable: true),
                    DietaryRequirementIdentification = table.Column<string>(type: "text", nullable: true),
                    SpecialDietaryOptions = table.Column<string>(type: "text", nullable: true),
                    AlergiesOther = table.Column<string>(type: "text", nullable: true),
                    Covid19Vaccinated = table.Column<string>(type: "text", nullable: true),
                    VaccineTypeMultiSelect = table.Column<string>(type: "text", nullable: true),
                    FinalVaccineDoseDate = table.Column<string>(type: "text", nullable: true),
                    EducationOnechoice = table.Column<string>(type: "text", nullable: true),
                    AreaOfStudy = table.Column<string>(type: "text", nullable: true),
                    EnglishFluencyLevel = table.Column<string>(type: "text", nullable: true),
                    ArabicFluencyLevel = table.Column<string>(type: "text", nullable: true),
                    AdditionalLanguage1 = table.Column<string>(type: "text", nullable: true),
                    AdditionalLanguage1FluencyLevel = table.Column<string>(type: "text", nullable: true),
                    AdditionalLanguage2 = table.Column<string>(type: "text", nullable: true),
                    AdditionalLanguage2FluencyLevel = table.Column<string>(type: "text", nullable: true),
                    AdditionalLanguage3 = table.Column<string>(type: "text", nullable: true),
                    AdditionalLanguage3FluencyLevel = table.Column<string>(type: "text", nullable: true),
                    AdditionalLanguage4 = table.Column<string>(type: "text", nullable: true),
                    AdditionalLanguage4FluencyLevel = table.Column<string>(type: "text", nullable: true),
                    LanguagesOther = table.Column<string>(type: "text", nullable: true),
                    InterpretationAndTranslationExperience = table.Column<string>(type: "text", nullable: true),
                    CertifiedTranslatorLanguage = table.Column<string>(type: "text", nullable: true),
                    DescribeYourItSkills = table.Column<string>(type: "text", nullable: true),
                    Skill1 = table.Column<string>(type: "text", nullable: true),
                    Skill2 = table.Column<string>(type: "text", nullable: true),
                    Skill3 = table.Column<string>(type: "text", nullable: true),
                    Skill4 = table.Column<string>(type: "text", nullable: true),
                    Skill5 = table.Column<string>(type: "text", nullable: true),
                    Skill6 = table.Column<string>(type: "text", nullable: true),
                    PreviousVolunteeringExperience = table.Column<string>(type: "text", nullable: true),
                    VolunteerExperience = table.Column<string>(type: "text", nullable: true),
                    OtherRole = table.Column<string>(type: "text", nullable: true),
                    EventsTypeParticipations = table.Column<string>(type: "text", nullable: true),
                    OtherEventType = table.Column<string>(type: "text", nullable: true),
                    VolunteerHoursYearly = table.Column<string>(type: "text", nullable: true),
                    PreferredFunctionalArea = table.Column<string>(type: "text", nullable: true),
                    FwcAreYouInterestedInALeadershipRole = table.Column<string>(type: "text", nullable: true),
                    FwcLeadershipExperience = table.Column<string>(type: "text", nullable: true),
                    WorkExperience = table.Column<string>(type: "text", nullable: true),
                    CeremoniesYesNo = table.Column<bool>(type: "boolean", nullable: false),
                    CastYesNo = table.Column<bool>(type: "boolean", nullable: false),
                    CastOptions = table.Column<string>(type: "text", nullable: true),
                    MotivationToVolunteerAtFwc = table.Column<string>(type: "text", nullable: true),
                    LeaveOrArrange = table.Column<string>(type: "text", nullable: true),
                    LocalInternationalVolunteer = table.Column<string>(type: "text", nullable: true),
                    LanguagePathEnglishArabic = table.Column<string>(type: "text", nullable: true),
                    FwcAvailabilityPreTournamentStageOne = table.Column<string>(type: "text", nullable: true),
                    FwcAvailabilityPreTournamentStageTwo = table.Column<string>(type: "text", nullable: true),
                    AvailabilityDuringTournament = table.Column<string>(type: "text", nullable: true),
                    daily_availability_shift_morning = table.Column<string>(type: "text", nullable: true),
                    DailyAvailabilityShiftAfternoon = table.Column<string>(type: "text", nullable: true),
                    DailyAvailabilityShiftNight = table.Column<string>(type: "text", nullable: true),
                    DailyAvailabilityShiftOvernight = table.Column<string>(type: "text", nullable: true),
                    CandidateUnder18 = table.Column<string>(type: "text", nullable: true),
                    SpecialGroupsInternational = table.Column<string>(type: "text", nullable: true),
                    MunicipalityAddress = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Venues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleOffers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VenueId = table.Column<int>(type: "integer", nullable: true),
                    FunctionalAreaId = table.Column<int>(type: "integer", nullable: true),
                    LocationId = table.Column<int>(type: "integer", nullable: true),
                    JobTitleId = table.Column<int>(type: "integer", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleOffers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleOffers_FunctionalAreas_FunctionalAreaId",
                        column: x => x.FunctionalAreaId,
                        principalTable: "FunctionalAreas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RoleOffers_JobTitles_JobTitleId",
                        column: x => x.JobTitleId,
                        principalTable: "JobTitles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RoleOffers_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RoleOffers_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleOffers_FunctionalAreaId",
                table: "RoleOffers",
                column: "FunctionalAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleOffers_JobTitleId",
                table: "RoleOffers",
                column: "JobTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleOffers_LocationId",
                table: "RoleOffers",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleOffers_VenueId",
                table: "RoleOffers",
                column: "VenueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "RoleOffers");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "FunctionalAreas");

            migrationBuilder.DropTable(
                name: "JobTitles");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Venues");
        }
    }
}
