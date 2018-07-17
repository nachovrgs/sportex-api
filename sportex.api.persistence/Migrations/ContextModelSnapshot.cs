﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using sportex.api.domain.notification;
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

                    b.Property<string>("Token");

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

            modelBuilder.Entity("sportex.api.domain.Event", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CountStarters");

                    b.Property<int>("CountSubs");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Description");

                    b.Property<string>("EventName");

                    b.Property<int>("EventType");

                    b.Property<bool>("IsPublic");

                    b.Property<DateTime>("LastUpdate");

                    b.Property<int>("LocationID");

                    b.Property<int>("MaxStarters");

                    b.Property<int>("MaxSubs");

                    b.Property<int>("StandardProfileID");

                    b.Property<DateTime?>("StartingTime");

                    b.Property<int>("Status");

                    b.HasKey("ID");

                    b.HasIndex("LocationID");

                    b.HasIndex("StandardProfileID");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("sportex.api.domain.EventInvitation", b =>
                {
                    b.Property<int>("EventID");

                    b.Property<int>("IdProfileInvites");

                    b.Property<int>("IdProfileInvited");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("LastUpdate");

                    b.Property<string>("Message");

                    b.Property<int>("Status");

                    b.Property<int>("Type");

                    b.HasKey("EventID", "IdProfileInvites", "IdProfileInvited");

                    b.HasIndex("IdProfileInvited");

                    b.HasIndex("IdProfileInvites");

                    b.ToTable("EventInvitations");
                });

            modelBuilder.Entity("sportex.api.domain.EventParticipant", b =>
                {
                    b.Property<int>("EventID");

                    b.Property<int>("StandardProfileID");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("LastUpdate");

                    b.Property<int>("Order");

                    b.Property<int>("Status");

                    b.Property<int>("Type");

                    b.HasKey("EventID", "StandardProfileID");

                    b.HasIndex("StandardProfileID");

                    b.ToTable("EventParticipants");
                });

            modelBuilder.Entity("sportex.api.domain.Group", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("GroupDescription");

                    b.Property<string>("GroupName");

                    b.Property<DateTime>("LastUpdate");

                    b.Property<int>("MemberCount");

                    b.Property<string>("PicturePath");

                    b.Property<int>("StandardProfileID");

                    b.Property<int>("Status");

                    b.HasKey("ID");

                    b.HasIndex("StandardProfileID");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("sportex.api.domain.GroupMember", b =>
                {
                    b.Property<int>("GroupID");

                    b.Property<int>("StandardProfileID");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("LastUpdate");

                    b.Property<int>("Status");

                    b.Property<int>("Type");

                    b.HasKey("GroupID", "StandardProfileID");

                    b.HasIndex("StandardProfileID");

                    b.ToTable("GroupMembers");
                });

            modelBuilder.Entity("sportex.api.domain.Location", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Description");

                    b.Property<DateTime>("LastUpdate");

                    b.Property<double?>("Latitude");

                    b.Property<double?>("Longitude");

                    b.Property<string>("Name");

                    b.Property<int>("Status");

                    b.HasKey("ID");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("sportex.api.domain.Log", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Details");

                    b.Property<string>("Message");

                    b.Property<int>("Type");

                    b.HasKey("ID");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("sportex.api.domain.notification.Notification", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Message");

                    b.Property<int>("StandardProfileID");

                    b.Property<int>("Status");

                    b.Property<int>("Type");

                    b.HasKey("ID");

                    b.HasIndex("StandardProfileID");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("sportex.api.domain.PlayerReview", b =>
                {
                    b.Property<int>("EventID");

                    b.Property<int>("IdProfileReviews");

                    b.Property<int>("IdProfileReviewed");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("LastUpdate");

                    b.Property<string>("Message");

                    b.Property<int>("Rate");

                    b.Property<int>("Status");

                    b.HasKey("EventID", "IdProfileReviews", "IdProfileReviewed");

                    b.HasIndex("IdProfileReviewed");

                    b.HasIndex("IdProfileReviews");

                    b.ToTable("PlayerReviews");
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

                    b.Property<double>("CountReviews");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<DateTime>("LastUpdate");

                    b.Property<string>("MailAddress");

                    b.Property<string>("PicturePath");

                    b.Property<int>("Sex");

                    b.Property<int>("Status");

                    b.Property<double>("TotalRate");

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

            modelBuilder.Entity("sportex.api.domain.Event", b =>
                {
                    b.HasOne("sportex.api.domain.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("sportex.api.domain.StandardProfile", "CreatorProfile")
                        .WithMany()
                        .HasForeignKey("StandardProfileID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("sportex.api.domain.EventInvitation", b =>
                {
                    b.HasOne("sportex.api.domain.Event", "EventInvited")
                        .WithMany("EventInvited")
                        .HasForeignKey("EventID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("sportex.api.domain.StandardProfile", "ProfileInvited")
                        .WithMany("ProfileInvited")
                        .HasForeignKey("IdProfileInvited")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("sportex.api.domain.StandardProfile", "ProfileInvites")
                        .WithMany("ProfileInvites")
                        .HasForeignKey("IdProfileInvites")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("sportex.api.domain.EventParticipant", b =>
                {
                    b.HasOne("sportex.api.domain.Event", "EventParticipates")
                        .WithMany("EventParticipates")
                        .HasForeignKey("EventID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("sportex.api.domain.StandardProfile", "ProfileParticipant")
                        .WithMany("ProfileParticipant")
                        .HasForeignKey("StandardProfileID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("sportex.api.domain.Group", b =>
                {
                    b.HasOne("sportex.api.domain.StandardProfile", "CreatorProfile")
                        .WithMany()
                        .HasForeignKey("StandardProfileID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("sportex.api.domain.GroupMember", b =>
                {
                    b.HasOne("sportex.api.domain.Group", "GroupIntegrates")
                        .WithMany("GroupIntegrates")
                        .HasForeignKey("GroupID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("sportex.api.domain.StandardProfile", "ProfileMember")
                        .WithMany("ProfileMember")
                        .HasForeignKey("StandardProfileID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("sportex.api.domain.notification.Notification", b =>
                {
                    b.HasOne("sportex.api.domain.StandardProfile", "Profile")
                        .WithMany()
                        .HasForeignKey("StandardProfileID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("sportex.api.domain.PlayerReview", b =>
                {
                    b.HasOne("sportex.api.domain.Event", "EventReviewed")
                        .WithMany("EventReviewed")
                        .HasForeignKey("EventID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("sportex.api.domain.StandardProfile", "ProfileReviewed")
                        .WithMany("ProfileReviewed")
                        .HasForeignKey("IdProfileReviewed")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("sportex.api.domain.StandardProfile", "ProfileReviews")
                        .WithMany("ProfileReviews")
                        .HasForeignKey("IdProfileReviews")
                        .OnDelete(DeleteBehavior.Restrict);
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
