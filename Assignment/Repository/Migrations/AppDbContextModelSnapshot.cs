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

            modelBuilder.Entity("DomainModels.Models.Entities.Assignment", b =>
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

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("updated_at");

                    b.Property<int>("VolunteerId")
                        .HasColumnType("integer")
                        .HasColumnName("volunteer_id");

                    b.Property<int?>("role_offer_id")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("role_offer_id");

                    b.ToTable("assignment");
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

            modelBuilder.Entity("DomainModels.Models.Entities.Location", b =>
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

                    b.ToTable("locations");
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

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("updated_at");

                    b.Property<int?>("functional_area_id")
                        .HasColumnType("integer");

                    b.Property<int?>("job_title_id")
                        .HasColumnType("integer");

                    b.Property<int?>("location_id")
                        .HasColumnType("integer");

                    b.Property<int?>("venue_id")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("functional_area_id");

                    b.HasIndex("job_title_id");

                    b.HasIndex("location_id");

                    b.HasIndex("venue_id");

                    b.ToTable("role_offers");
                });

            modelBuilder.Entity("DomainModels.Models.Entities.Venue", b =>
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

                    b.ToTable("venues");
                });

            modelBuilder.Entity("DomainModels.Models.Entities.Assignment", b =>
                {
                    b.HasOne("DomainModels.Models.Entities.RoleOffer", "RoleOffer")
                        .WithMany("Assignments")
                        .HasForeignKey("role_offer_id");

                    b.Navigation("RoleOffer");
                });

            modelBuilder.Entity("DomainModels.Models.Entities.RoleOffer", b =>
                {
                    b.HasOne("DomainModels.Models.Entities.FunctionalArea", "FunctionalArea")
                        .WithMany()
                        .HasForeignKey("functional_area_id");

                    b.HasOne("DomainModels.Models.Entities.JobTitle", "JobTitle")
                        .WithMany("RoleOffers")
                        .HasForeignKey("job_title_id");

                    b.HasOne("DomainModels.Models.Entities.Location", "Location")
                        .WithMany("RoleOffers")
                        .HasForeignKey("location_id");

                    b.HasOne("DomainModels.Models.Entities.Venue", "Venue")
                        .WithMany("RoleOffers")
                        .HasForeignKey("venue_id");

                    b.Navigation("FunctionalArea");

                    b.Navigation("JobTitle");

                    b.Navigation("Location");

                    b.Navigation("Venue");
                });

            modelBuilder.Entity("DomainModels.Models.Entities.JobTitle", b =>
                {
                    b.Navigation("RoleOffers");
                });

            modelBuilder.Entity("DomainModels.Models.Entities.Location", b =>
                {
                    b.Navigation("RoleOffers");
                });

            modelBuilder.Entity("DomainModels.Models.Entities.RoleOffer", b =>
                {
                    b.Navigation("Assignments");
                });

            modelBuilder.Entity("DomainModels.Models.Entities.Venue", b =>
                {
                    b.Navigation("RoleOffers");
                });
#pragma warning restore 612, 618
        }
    }
}
