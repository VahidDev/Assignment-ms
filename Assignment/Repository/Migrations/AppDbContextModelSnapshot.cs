﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Repository.DAL;

#nullable disable

namespace Repository.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DomainModels.Models.Entities.Filter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("deleted_at");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Operator")
                        .HasColumnType("text")
                        .HasColumnName("operator");

                    b.Property<string>("Requirement")
                        .HasColumnType("text")
                        .HasColumnName("requirement");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("updated_at");

                    b.Property<string>("Value")
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.Property<int?>("template_id")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("template_id");

                    b.ToTable("filters");
                });

            modelBuilder.Entity("DomainModels.Models.Entities.FunctionalArea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .HasColumnType("text")
                        .HasColumnName("code");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("deleted_at");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.ToTable("functional_areas");
                });

            modelBuilder.Entity("DomainModels.Models.Entities.FunctionalAreaJobTitle", b =>
                {
                    b.Property<int>("JobTitleId")
                        .HasColumnType("integer")
                        .HasColumnName("job_title_id");

                    b.Property<int>("FunctionalAreaId")
                        .HasColumnType("integer")
                        .HasColumnName("functional_area_id");

                    b.HasKey("JobTitleId", "FunctionalAreaId");

                    b.HasIndex("FunctionalAreaId");

                    b.ToTable("functional_area_job_titles");
                });

            modelBuilder.Entity("DomainModels.Models.Entities.FunctionalAreaType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("deleted_at");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.ToTable("functional_area_types");
                });

            modelBuilder.Entity("DomainModels.Models.Entities.FunctionalAreaTypeFunctionalArea", b =>
                {
                    b.Property<int>("FunctionalAreaTypeId")
                        .HasColumnType("integer")
                        .HasColumnName("functional_area_type_id");

                    b.Property<int>("FunctionalAreaId")
                        .HasColumnType("integer")
                        .HasColumnName("functional_area_id");

                    b.HasKey("FunctionalAreaTypeId", "FunctionalAreaId");

                    b.HasIndex("FunctionalAreaId");

                    b.ToTable("functional_area_type_functional_areas");
                });

            modelBuilder.Entity("DomainModels.Models.Entities.JobTitle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .HasColumnType("text")
                        .HasColumnName("code");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("deleted_at");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.ToTable("job_titles");
                });

            modelBuilder.Entity("DomainModels.Models.Entities.JobTitleLocation", b =>
                {
                    b.Property<int>("JobTitleId")
                        .HasColumnType("integer")
                        .HasColumnName("job_title_id");

                    b.Property<int>("LocationId")
                        .HasColumnType("integer")
                        .HasColumnName("location_id");

                    b.HasKey("JobTitleId", "LocationId");

                    b.HasIndex("LocationId");

                    b.ToTable("job_title_venues");
                });

            modelBuilder.Entity("DomainModels.Models.Entities.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .HasColumnType("text")
                        .HasColumnName("Role Offer - Location Code");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("deleted_at");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.ToTable("venues");
                });

            modelBuilder.Entity("DomainModels.Models.Entities.RoleOffer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("deleted_at");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<int>("RoleOfferId")
                        .HasColumnType("integer")
                        .HasColumnName("role_offer_id");

                    b.Property<int>("TotalDemand")
                        .HasColumnType("integer")
                        .HasColumnName("total_demand");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("updated_at");

                    b.Property<int?>("functional_area_id")
                        .HasColumnType("integer");

                    b.Property<int?>("functional_area_type_id")
                        .HasColumnType("integer");

                    b.Property<int?>("job_title_id")
                        .HasColumnType("integer");

                    b.Property<int?>("location_id")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("functional_area_id");

                    b.HasIndex("functional_area_type_id");

                    b.HasIndex("job_title_id");

                    b.HasIndex("location_id");

                    b.ToTable("role_offers");
                });

            modelBuilder.Entity("DomainModels.Models.Entities.Template", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("deleted_at");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.ToTable("templates");
                });

            modelBuilder.Entity("DomainModels.Models.Entities.Filter", b =>
                {
                    b.HasOne("DomainModels.Models.Entities.Template", "Template")
                        .WithMany("Filters")
                        .HasForeignKey("template_id");

                    b.Navigation("Template");
                });

            modelBuilder.Entity("DomainModels.Models.Entities.FunctionalAreaJobTitle", b =>
                {
                    b.HasOne("DomainModels.Models.Entities.FunctionalArea", "FunctionalArea")
                        .WithMany("FunctionalAreaJobTitles")
                        .HasForeignKey("FunctionalAreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DomainModels.Models.Entities.JobTitle", "JobTitle")
                        .WithMany("FunctionalAreaJobTitles")
                        .HasForeignKey("JobTitleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FunctionalArea");

                    b.Navigation("JobTitle");
                });

            modelBuilder.Entity("DomainModels.Models.Entities.FunctionalAreaTypeFunctionalArea", b =>
                {
                    b.HasOne("DomainModels.Models.Entities.FunctionalArea", "FunctionalArea")
                        .WithMany("ExcelEntityFunctionalAreas")
                        .HasForeignKey("FunctionalAreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DomainModels.Models.Entities.FunctionalAreaType", "FunctionalAreaType")
                        .WithMany("FunctionalAreaTypeFunctionalAreas")
                        .HasForeignKey("FunctionalAreaTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FunctionalArea");

                    b.Navigation("FunctionalAreaType");
                });

            modelBuilder.Entity("DomainModels.Models.Entities.JobTitleLocation", b =>
                {
                    b.HasOne("DomainModels.Models.Entities.JobTitle", "JobTitle")
                        .WithMany("JobTitleVenues")
                        .HasForeignKey("JobTitleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DomainModels.Models.Entities.Location", "Location")
                        .WithMany("JobTitleVenues")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("JobTitle");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("DomainModels.Models.Entities.RoleOffer", b =>
                {
                    b.HasOne("DomainModels.Models.Entities.FunctionalArea", "FunctionalArea")
                        .WithMany("RoleOffers")
                        .HasForeignKey("functional_area_id");

                    b.HasOne("DomainModels.Models.Entities.FunctionalAreaType", "FunctionalAreaType")
                        .WithMany("RoleOffers")
                        .HasForeignKey("functional_area_type_id");

                    b.HasOne("DomainModels.Models.Entities.JobTitle", "JobTitle")
                        .WithMany("RoleOffers")
                        .HasForeignKey("job_title_id");

                    b.HasOne("DomainModels.Models.Entities.Location", "Location")
                        .WithMany("RoleOffers")
                        .HasForeignKey("location_id");

                    b.Navigation("FunctionalArea");

                    b.Navigation("FunctionalAreaType");

                    b.Navigation("JobTitle");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("DomainModels.Models.Entities.FunctionalArea", b =>
                {
                    b.Navigation("ExcelEntityFunctionalAreas");

                    b.Navigation("FunctionalAreaJobTitles");

                    b.Navigation("RoleOffers");
                });

            modelBuilder.Entity("DomainModels.Models.Entities.FunctionalAreaType", b =>
                {
                    b.Navigation("FunctionalAreaTypeFunctionalAreas");

                    b.Navigation("RoleOffers");
                });

            modelBuilder.Entity("DomainModels.Models.Entities.JobTitle", b =>
                {
                    b.Navigation("FunctionalAreaJobTitles");

                    b.Navigation("JobTitleVenues");

                    b.Navigation("RoleOffers");
                });

            modelBuilder.Entity("DomainModels.Models.Entities.Location", b =>
                {
                    b.Navigation("JobTitleVenues");

                    b.Navigation("RoleOffers");
                });

            modelBuilder.Entity("DomainModels.Models.Entities.Template", b =>
                {
                    b.Navigation("Filters");
                });
#pragma warning restore 612, 618
        }
    }
}
