using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace sportex.api.persistance.Migrations
{
    public partial class PlayerReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayerReviews",
                columns: table => new
                {
                    EventID = table.Column<int>(nullable: false),
                    IdProfileReviews = table.Column<int>(nullable: false),
                    IdProfileReviewed = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    Rate = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerReviews", x => new { x.EventID, x.IdProfileReviews, x.IdProfileReviewed });
                    table.ForeignKey(
                        name: "FK_PlayerReviews_Events_EventID",
                        column: x => x.EventID,
                        principalTable: "Events",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayerReviews_StandardProfiles_IdProfileReviewed",
                        column: x => x.IdProfileReviewed,
                        principalTable: "StandardProfiles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayerReviews_StandardProfiles_IdProfileReviews",
                        column: x => x.IdProfileReviews,
                        principalTable: "StandardProfiles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerReviews_IdProfileReviewed",
                table: "PlayerReviews",
                column: "IdProfileReviewed");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerReviews_IdProfileReviews",
                table: "PlayerReviews",
                column: "IdProfileReviews");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerReviews");
        }
    }
}
