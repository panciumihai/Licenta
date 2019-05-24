﻿// <auto-generated />
using System;
using CampusManagement.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CampusManagement.Persistance.Migrations
{
    [DbContext(typeof(CampusManagementContext))]
    [Migration("20190523084224_StudentDetails")]
    partial class StudentDetails
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CampusManagement.Domain.Entities.Admin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Available");

                    b.Property<Guid>("PersonId");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("CampusManagement.Domain.Entities.Application", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AccommodationRequest");

                    b.Property<bool>("Available");

                    b.Property<bool>("ChildOfTeacher");

                    b.Property<string>("LastYearLocation");

                    b.Property<DateTimeOffset>("PostedDateTime");

                    b.Property<bool>("Scholarship");

                    b.Property<string>("SpecialCases");

                    b.Property<Guid>("StudentId");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("CampusManagement.Domain.Entities.Article", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AdminId");

                    b.Property<bool>("Available");

                    b.Property<string>("Content");

                    b.Property<string>("Image");

                    b.Property<DateTimeOffset>("PostedDateTime");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("CampusManagement.Domain.Entities.Hostel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<bool>("Available");

                    b.Property<string>("HostelAdminFullName");

                    b.Property<string>("MapLocation");

                    b.Property<string>("Name");

                    b.Property<int>("TotalCapacity");

                    b.HasKey("Id");

                    b.ToTable("Hostels");
                });

            modelBuilder.Entity("CampusManagement.Domain.Entities.HostelPreference", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ApplicationId");

                    b.Property<bool>("Available");

                    b.Property<Guid>("HostelId");

                    b.Property<int>("PreferenceNumber");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("HostelId");

                    b.ToTable("HostelPreferences");
                });

            modelBuilder.Entity("CampusManagement.Domain.Entities.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Available");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("Gender");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("CampusManagement.Domain.Entities.PersonRole", b =>
                {
                    b.Property<Guid>("PersonId");

                    b.Property<Guid>("RoleId");

                    b.Property<bool>("Available");

                    b.Property<Guid>("Id");

                    b.HasKey("PersonId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("PersonRoles");
                });

            modelBuilder.Entity("CampusManagement.Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Available");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("CampusManagement.Domain.Entities.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Available");

                    b.Property<int>("BeforeLicenseScore");

                    b.Property<int>("BeforeMasterScore");

                    b.Property<string>("Cnp");

                    b.Property<string>("Nationality");

                    b.Property<Guid>("PersonId");

                    b.Property<short>("Year");

                    b.Property<int>("YearOneMasterScore");

                    b.Property<int>("YearOneScore");

                    b.Property<int>("YearTwoScore");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("CampusManagement.Domain.Entities.Admin", b =>
                {
                    b.HasOne("CampusManagement.Domain.Entities.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CampusManagement.Domain.Entities.Application", b =>
                {
                    b.HasOne("CampusManagement.Domain.Entities.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CampusManagement.Domain.Entities.Article", b =>
                {
                    b.HasOne("CampusManagement.Domain.Entities.Admin", "Admin")
                        .WithMany("Articles")
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CampusManagement.Domain.Entities.HostelPreference", b =>
                {
                    b.HasOne("CampusManagement.Domain.Entities.Application", "Application")
                        .WithMany("HostelPreferences")
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CampusManagement.Domain.Entities.Hostel", "Hostel")
                        .WithMany()
                        .HasForeignKey("HostelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CampusManagement.Domain.Entities.PersonRole", b =>
                {
                    b.HasOne("CampusManagement.Domain.Entities.Person", "Person")
                        .WithMany("PersonRoles")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CampusManagement.Domain.Entities.Role", "Role")
                        .WithMany("PersonRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CampusManagement.Domain.Entities.Student", b =>
                {
                    b.HasOne("CampusManagement.Domain.Entities.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
