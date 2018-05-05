using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace sportex.api.persistance.Migrations
{
    public partial class GroupAndGroupMembers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    GroupDescription = table.Column<string>(nullable: true),
                    GroupName = table.Column<string>(nullable: true),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    MemberCount = table.Column<int>(nullable: false),
                    PicturePath = table.Column<string>(nullable: true),
                    StandardProfileID = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Groups_StandardProfiles_StandardProfileID",
                        column: x => x.StandardProfileID,
                        principalTable: "StandardProfiles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupMembers",
                columns: table => new
                {
                    GroupID = table.Column<int>(nullable: false),
                    StandardProfileID = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMembers", x => new { x.GroupID, x.StandardProfileID });
                    table.ForeignKey(
                        name: "FK_GroupMembers_Groups_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupMembers_StandardProfiles_StandardProfileID",
                        column: x => x.StandardProfileID,
                        principalTable: "StandardProfiles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_StandardProfileID",
                table: "GroupMembers",
                column: "StandardProfileID");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_StandardProfileID",
                table: "Groups",
                column: "StandardProfileID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupMembers");

            migrationBuilder.DropTable(
                name: "Groups");
        }
    }
}
