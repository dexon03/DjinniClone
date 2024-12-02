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
    [Migration("20230828082858_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("IdentityService.Domain.Models.Claim", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Claim");
                });

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
                });

            modelBuilder.Entity("IdentityService.Domain.Models.RoleClaim", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ClaimId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ClaimId");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaim");
                });

            modelBuilder.Entity("IdentityService.Domain.Models.RoleClaim", b =>
                {
                    b.HasOne("IdentityService.Domain.Models.Claim", null)
                        .WithMany("RoleClaim")
                        .HasForeignKey("ClaimId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IdentityService.Domain.Models.Role", null)
                        .WithMany("RoleClaim")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IdentityService.Domain.Models.Claim", b =>
                {
                    b.Navigation("RoleClaim");
                });

            modelBuilder.Entity("IdentityService.Domain.Models.Role", b =>
                {
                    b.Navigation("RoleClaim");
                });
#pragma warning restore 612, 618
        }
    }
}