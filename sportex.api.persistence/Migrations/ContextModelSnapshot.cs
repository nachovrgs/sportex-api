﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using sportex.api.persistence;
using System;

namespace sportex.api.persistance.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("sportex.api.domain.Account", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("LastAccess");

                    b.Property<DateTime>("LastUpdate");

                    b.Property<string>("Password");

                    b.Property<int>("Status");

                    b.Property<string>("Username");

                    b.HasKey("ID");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("sportex.api.domain.AdminProfile", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccountID");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<DateTime>("LastUpdate");

                    b.Property<string>("MailAddress");

                    b.Property<string>("PicturePath");

                    b.Property<int>("Status");

                    b.HasKey("ID");

                    b.HasIndex("AccountID");

                    b.ToTable("AdminProfiles");
                });

            modelBuilder.Entity("sportex.api.domain.AdminRole", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AdminProfileID");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.HasIndex("AdminProfileID");

                    b.ToTable("AdminRoles");
                });

            modelBuilder.Entity("sportex.api.domain.Relationship", b =>
                {
                    b.Property<int>("IdProfile1");

                    b.Property<int>("IdProfile2");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("LastUpdate");

                    b.Property<int>("Status");

                    b.HasKey("IdProfile1", "IdProfile2");

                    b.HasIndex("IdProfile2");

                    b.ToTable("Relationships");
                });

            modelBuilder.Entity("sportex.api.domain.StandardProfile", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccountID");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<DateTime>("LastUpdate");

                    b.Property<string>("MailAddress");

                    b.Property<string>("PicturePath");

                    b.Property<int>("Sex");

                    b.Property<int>("Status");

                    b.HasKey("ID");

                    b.HasIndex("AccountID");

                    b.ToTable("StandardProfiles");
                });

            modelBuilder.Entity("sportex.api.domain.AdminProfile", b =>
                {
                    b.HasOne("sportex.api.domain.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("sportex.api.domain.AdminRole", b =>
                {
                    b.HasOne("sportex.api.domain.AdminProfile")
                        .WithMany("Roles")
                        .HasForeignKey("AdminProfileID");
                });

            modelBuilder.Entity("sportex.api.domain.Relationship", b =>
                {
                    b.HasOne("sportex.api.domain.StandardProfile", "Profile1")
                        .WithMany("Relationships1")
                        .HasForeignKey("IdProfile1")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("sportex.api.domain.StandardProfile", "Profile2")
                        .WithMany("Relationships2")
                        .HasForeignKey("IdProfile2")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("sportex.api.domain.StandardProfile", b =>
                {
                    b.HasOne("sportex.api.domain.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
