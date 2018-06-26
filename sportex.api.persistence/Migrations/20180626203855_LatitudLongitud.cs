using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace sportex.api.persistance.Migrations
{
    public partial class LatitudLongitud : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Latitude",
                table: "Location",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Longitude",
                table: "Location",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Location");
        }
    }
}
