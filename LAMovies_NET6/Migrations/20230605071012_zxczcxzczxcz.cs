using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LAMovies_NET6.Migrations
{
    /// <inheritdoc />
    public partial class zxczcxzczxcz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "remainingTime",
                table: "MovieHistorys");

            migrationBuilder.AddColumn<int>(
                name: "episodes",
                table: "MovieHistorys",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "episodes",
                table: "MovieHistorys");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "remainingTime",
                table: "MovieHistorys",
                type: "time",
                nullable: true);
        }
    }
}
