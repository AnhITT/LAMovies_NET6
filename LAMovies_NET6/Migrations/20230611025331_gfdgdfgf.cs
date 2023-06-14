using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LAMovies_NET6.Migrations
{
    /// <inheritdoc />
    public partial class gfdgdfgf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "episodes",
                table: "SeriesMovies");

            migrationBuilder.DropColumn(
                name: "uriMovie",
                table: "Movies");

            migrationBuilder.AddColumn<double>(
                name: "totalAmount",
                table: "UserPricings",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "episodes",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "totalAmount",
                table: "UserPricings");

            migrationBuilder.DropColumn(
                name: "episodes",
                table: "Movies");

            migrationBuilder.AddColumn<int>(
                name: "episodes",
                table: "SeriesMovies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "uriMovie",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
