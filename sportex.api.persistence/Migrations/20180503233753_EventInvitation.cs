using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace sportex.api.persistance.Migrations
{
    public partial class EventInvitation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventInvitations",
                columns: table => new
                {
                    EventID = table.Column<int>(nullable: false),
                    IdProfileInvites = table.Column<int>(nullable: false),
                    IdProfileInvited = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventInvitations", x => new { x.EventID, x.IdProfileInvites, x.IdProfileInvited });
                    table.ForeignKey(
                        name: "FK_EventInvitations_Events_EventID",
                        column: x => x.EventID,
                        principalTable: "Events",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventInvitations_StandardProfiles_IdProfileInvited",
                        column: x => x.IdProfileInvited,
                        principalTable: "StandardProfiles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventInvitations_StandardProfiles_IdProfileInvites",
                        column: x => x.IdProfileInvites,
                        principalTable: "StandardProfiles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    ProfileID = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Notifications_StandardProfiles_ProfileID",
                        column: x => x.ProfileID,
                        principalTable: "StandardProfiles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventInvitations_IdProfileInvited",
                table: "EventInvitations",
                column: "IdProfileInvited");

            migrationBuilder.CreateIndex(
                name: "IX_EventInvitations_IdProfileInvites",
                table: "EventInvitations",
                column: "IdProfileInvites");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ProfileID",
                table: "Notifications",
                column: "ProfileID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventInvitations");

            migrationBuilder.DropTable(
                name: "Notifications");
        }
    }
}
