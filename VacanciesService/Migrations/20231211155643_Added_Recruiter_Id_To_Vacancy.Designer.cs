﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using VacanciesService.Database;

#nullable disable

namespace VacanciesService.Migrations
{
    [DbContext(typeof(VacanciesDbContext))]
    [Migration("20231211155643_Added_Recruiter_Id_To_Vacancy")]
    partial class Added_Recruiter_Id_To_Vacancy
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("VacanciesService.Domain.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("VacanciesService.Domain.Models.Company", b =>
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

            modelBuilder.Entity("VacanciesService.Domain.Models.Location", b =>
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

                    b.HasData(
                        new
                        {
                            Id = new Guid("52faf63e-62a4-40f3-b21e-3118535bff3e"),
                            City = "Kyiv",
                            Country = "Ukraine"
                        },
                        new
                        {
                            Id = new Guid("120a6313-946c-4189-9672-269d5f702e36"),
                            City = "Lviv",
                            Country = "Ukraine"
                        },
                        new
                        {
                            Id = new Guid("97711ed9-52d5-4690-80cb-c93206f3efb2"),
                            City = "Kharkiv",
                            Country = "Ukraine"
                        },
                        new
                        {
                            Id = new Guid("525660ec-4a96-4059-8217-0c2e438f2e1e"),
                            City = "Dnipro",
                            Country = "Ukraine"
                        },
                        new
                        {
                            Id = new Guid("eb6745e9-f29d-4640-a88f-c91057a79e28"),
                            City = "Odesa",
                            Country = "Ukraine"
                        },
                        new
                        {
                            Id = new Guid("21b34254-569c-4783-a94a-4a032997807f"),
                            City = "Zaporizhzhia",
                            Country = "Ukraine"
                        },
                        new
                        {
                            Id = new Guid("c1bb0135-93d7-4a8e-a34d-6238af0676c8"),
                            City = "Vinnytsia",
                            Country = "Ukraine"
                        },
                        new
                        {
                            Id = new Guid("8c9458ed-cffe-4e6f-a7e7-f449f35c491a"),
                            City = "Khmelnytskyi",
                            Country = "Ukraine"
                        });
                });

            modelBuilder.Entity("VacanciesService.Domain.Models.LocationVacancy", b =>
                {
                    b.Property<Guid>("LocationId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("VacancyId")
                        .HasColumnType("uuid");

                    b.HasKey("LocationId", "VacancyId");

                    b.HasIndex("VacancyId");

                    b.ToTable("LocationVacancy");
                });

            modelBuilder.Entity("VacanciesService.Domain.Models.Skill", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Skill");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e026f57a-cbfd-44fb-b868-9d3c58c727dc"),
                            Name = "C#"
                        },
                        new
                        {
                            Id = new Guid("bf962382-9bd2-4fb8-8afd-4203422abacc"),
                            Name = "Java"
                        },
                        new
                        {
                            Id = new Guid("8bef3018-9417-424e-970b-4f8029ecadbd"),
                            Name = "Python"
                        },
                        new
                        {
                            Id = new Guid("078efeb8-129a-4c87-a4b2-85a151af7c14"),
                            Name = "JavaScript"
                        },
                        new
                        {
                            Id = new Guid("792299fe-0394-4a52-a0f8-72114a764976"),
                            Name = "C++"
                        },
                        new
                        {
                            Id = new Guid("b5d51f7d-5352-4ded-af8c-484b9b8f3d60"),
                            Name = "PHP"
                        },
                        new
                        {
                            Id = new Guid("b713e7e3-ad4e-41d8-8f3a-5401da80f889"),
                            Name = "Ruby"
                        },
                        new
                        {
                            Id = new Guid("c8494c38-55a9-4784-b727-6ed4d3b53ceb"),
                            Name = "Swift"
                        },
                        new
                        {
                            Id = new Guid("8e1b3732-4039-44f5-9431-a128173d4a7f"),
                            Name = "Go"
                        },
                        new
                        {
                            Id = new Guid("1fa78f35-918e-4244-a24f-25029b997c03"),
                            Name = "Kotlin"
                        },
                        new
                        {
                            Id = new Guid("df8ed035-ed2e-41eb-907a-2f3d1f1e7abf"),
                            Name = "TypeScript"
                        },
                        new
                        {
                            Id = new Guid("9dd0d536-a120-4f2e-bc37-f65e08f436be"),
                            Name = "Scala"
                        });
                });

            modelBuilder.Entity("VacanciesService.Domain.Models.Vacancy", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AttendanceMode")
                        .HasColumnType("integer");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Experience")
                        .HasColumnType("integer");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("PositionTitle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("RecruiterId")
                        .HasColumnType("uuid");

                    b.Property<double>("Salary")
                        .HasColumnType("double precision");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CompanyId");

                    b.ToTable("Vacancy");
                });

            modelBuilder.Entity("VacanciesService.Domain.Models.VacancySkill", b =>
                {
                    b.Property<Guid>("SkillId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("VacancyId")
                        .HasColumnType("uuid");

                    b.HasKey("SkillId", "VacancyId");

                    b.HasIndex("VacancyId");

                    b.ToTable("VacancySkill");
                });

            modelBuilder.Entity("VacanciesService.Domain.Models.LocationVacancy", b =>
                {
                    b.HasOne("VacanciesService.Domain.Models.Location", "Location")
                        .WithMany("LocationVacancy")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VacanciesService.Domain.Models.Vacancy", "Vacancy")
                        .WithMany("LocationVacancy")
                        .HasForeignKey("VacancyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("Vacancy");
                });

            modelBuilder.Entity("VacanciesService.Domain.Models.Vacancy", b =>
                {
                    b.HasOne("VacanciesService.Domain.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VacanciesService.Domain.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("VacanciesService.Domain.Models.VacancySkill", b =>
                {
                    b.HasOne("VacanciesService.Domain.Models.Skill", "Skill")
                        .WithMany("VacancySkill")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VacanciesService.Domain.Models.Vacancy", "Vacancy")
                        .WithMany("VacancySkill")
                        .HasForeignKey("VacancyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Skill");

                    b.Navigation("Vacancy");
                });

            modelBuilder.Entity("VacanciesService.Domain.Models.Location", b =>
                {
                    b.Navigation("LocationVacancy");
                });

            modelBuilder.Entity("VacanciesService.Domain.Models.Skill", b =>
                {
                    b.Navigation("VacancySkill");
                });

            modelBuilder.Entity("VacanciesService.Domain.Models.Vacancy", b =>
                {
                    b.Navigation("LocationVacancy");

                    b.Navigation("VacancySkill");
                });
#pragma warning restore 612, 618
        }
    }
}
