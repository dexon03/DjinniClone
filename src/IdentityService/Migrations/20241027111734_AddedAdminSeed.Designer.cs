﻿// <auto-generated />
using System;
using IdentityService.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IdentityService.Migrations
{
    [DbContext(typeof(IdentityDbContext))]
    [Migration("20241027111734_AddedAdminSeed")]
    partial class AddedAdminSeed
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("IdentityService.Domain.Models.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            Id = new Guid("978d3f9e-89d0-4061-8716-a45c3e75d5c0"),
                            IsActive = true,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = new Guid("f9a4f1d5-b7c8-4501-b30f-c1bbb59a3fb6"),
                            IsActive = true,
                            Name = "Recruiter"
                        },
                        new
                        {
                            Id = new Guid("44d04cb4-8c75-4125-b53e-d29fabafe372"),
                            IsActive = true,
                            Name = "Candidate"
                        });
                });

            modelBuilder.Entity("IdentityService.Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a7d84623-a276-443e-8f3a-8009d71f5221"),
                            Email = "admin@mail.com",
                            FirstName = "Admin",
                            LastName = "Admin",
                            PasswordHash = "e31600e95eb220a2977ce41943e87acb4ba82e7bb80e30c9f7c9cd256a87c6d0",
                            PasswordSalt = "2FogXOBmztSYqisKP7F8zg==",
                            PhoneNumber = "123456789",
                            RoleId = new Guid("978d3f9e-89d0-4061-8716-a45c3e75d5c0")
                        });
                });

            modelBuilder.Entity("IdentityService.Domain.Models.User", b =>
                {
                    b.HasOne("IdentityService.Domain.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });
#pragma warning restore 612, 618
        }
    }
}