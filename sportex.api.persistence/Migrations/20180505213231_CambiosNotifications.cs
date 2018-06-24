using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace sportex.api.persistance.Migrations
{
    public partial class CambiosNotifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_StandardProfiles_ProfileID",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_ProfileID",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "ProfileID",
                table: "Notifications");

            migrationBuilder.AddColumn<int>(
                name: "StandardProfileID",
                table: "Notifications",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Notifications",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_StandardProfileID",
                table: "Notifications",
                column: "StandardProfileID");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_StandardProfiles_StandardProfileID",
                table: "Notifications",
                column: "StandardProfileID",
                principalTable: "StandardProfiles",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_StandardProfiles_StandardProfileID",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_StandardProfileID",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "StandardProfileID",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Notifications");

            migrationBuilder.AddColumn<int>(
                name: "ProfileID",
                table: "Notifications",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ProfileID",
                table: "Notifications",
                column: "ProfileID");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_StandardProfiles_ProfileID",
                table: "Notifications",
                column: "ProfileID",
                principalTable: "StandardProfiles",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
