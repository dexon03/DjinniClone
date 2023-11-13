﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ProfilesService.Database;

#nullable disable

namespace ProfilesService.Migrations
{
    [DbContext(typeof(ProfilesDbContext))]
    partial class ProfilesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ProfilesService.Domain.Models.CandidateProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("DateBirth")
                        .HasColumnType("date");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<double>("DesiredSalary")
                        .HasColumnType("double precision");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("GitHubUrl")
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("LinkedInUrl")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<string>("PositionTitle")
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<double>("WorkExperience")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("CandidateProfile");
                });

            modelBuilder.Entity("ProfilesService.Domain.Models.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("ProfilesService.Domain.Models.Location", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("ProfilesService.Domain.Models.LocationProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("LocationId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProfileId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("ProfileId");

                    b.ToTable("LocationProfile");
                });

            modelBuilder.Entity("ProfilesService.Domain.Models.ProfileSkills", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProfileId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SkillId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.HasIndex("SkillId");

                    b.ToTable("ProfileSkills");
                });

            modelBuilder.Entity("ProfilesService.Domain.Models.RecruiterProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("DateBirth")
                        .HasColumnType("date");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("GitHubUrl")
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("LinkedInUrl")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<string>("PositionTitle")
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("RecruiterProfile");
                });

            modelBuilder.Entity("ProfilesService.Domain.Models.Skill", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Skill");
                });

            modelBuilder.Entity("ProfilesService.Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserRole")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ProfilesService.Domain.Models.LocationProfile", b =>
                {
                    b.HasOne("ProfilesService.Domain.Models.Location", "Location")
                        .WithMany("LocationProfiles")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProfilesService.Domain.Models.CandidateProfile", "Profile")
                        .WithMany("LocationProfiles")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("ProfilesService.Domain.Models.ProfileSkills", b =>
                {
                    b.HasOne("ProfilesService.Domain.Models.CandidateProfile", "Profile")
                        .WithMany("ProfileSkills")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProfilesService.Domain.Models.Skill", "Skill")
                        .WithMany("ProfileSkills")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profile");

                    b.Navigation("Skill");
                });

            modelBuilder.Entity("ProfilesService.Domain.Models.RecruiterProfile", b =>
                {
                    b.HasOne("ProfilesService.Domain.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("ProfilesService.Domain.Models.CandidateProfile", b =>
                {
                    b.Navigation("LocationProfiles");

                    b.Navigation("ProfileSkills");
                });

            modelBuilder.Entity("ProfilesService.Domain.Models.Location", b =>
                {
                    b.Navigation("LocationProfiles");
                });

            modelBuilder.Entity("ProfilesService.Domain.Models.Skill", b =>
                {
                    b.Navigation("ProfileSkills");
                });
#pragma warning restore 612, 618
        }
    }
}
