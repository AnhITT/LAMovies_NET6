using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LAMovies_NET6.Migrations
{
    /// <inheritdoc />
    public partial class dxc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "endTime",
                table: "UserPricings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "startTime",
                table: "UserPricings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "endTime",
                table: "UserPricings");

            migrationBuilder.DropColumn(
                name: "startTime",
                table: "UserPricings");
        }
    }
}
