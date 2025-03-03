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
    [Migration("20250104113705_AddedCreatedAtToUser")]
    partial class AddedCreatedAtToUser
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
                            Id = new Guid("01943117-fb31-746c-8c46-3e4656c887ba"),
                            IsActive = true,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = new Guid("01943118-4440-7b9e-8960-efa609825355"),
                            IsActive = true,
                            Name = "Recruiter"
                        },
                        new
                        {
                            Id = new Guid("01943118-53b4-7ca3-84fc-43f969fde248"),
                            IsActive = true,
                            Name = "Candidate"
                        });
                });

            modelBuilder.Entity("IdentityService.Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

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
                            Id = new Guid("01943117-825a-7c86-b126-2e2839404e47"),
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "admin@mail.com",
                            FirstName = "Admin",
                            LastName = "Admin",
                            PasswordHash = "7e490b39c1f46c3db6aa0a5d0150990b29b7e4420fe42a1f1632e4d5c41e480e",
                            PasswordSalt = "zLKOph3JrlA4WjOpdms/wQ==",
                            PhoneNumber = "123456789",
                            RoleId = new Guid("01943117-fb31-746c-8c46-3e4656c887ba")
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
