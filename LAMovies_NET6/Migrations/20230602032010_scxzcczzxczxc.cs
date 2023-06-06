using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LAMovies_NET6.Migrations
{
    /// <inheritdoc />
    public partial class scxzcczzxczxc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeriesMovies_Movies_MovieidMovie",
                table: "SeriesMovies");

            migrationBuilder.DropIndex(
                name: "IX_SeriesMovies_MovieidMovie",
                table: "SeriesMovies");

            migrationBuilder.DropColumn(
                name: "MovieidMovie",
                table: "SeriesMovies");

            migrationBuilder.CreateIndex(
                name: "IX_SeriesMovies_idMovie",
                table: "SeriesMovies",
                column: "idMovie");

            migrationBuilder.AddForeignKey(
                name: "FK_SeriesMovies_Movies_idMovie",
                table: "SeriesMovies",
                column: "idMovie",
                principalTable: "Movies",
                principalColumn: "idMovie",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeriesMovies_Movies_idMovie",
                table: "SeriesMovies");

            migrationBuilder.DropIndex(
                name: "IX_SeriesMovies_idMovie",
                table: "SeriesMovies");

            migrationBuilder.AddColumn<int>(
                name: "MovieidMovie",
                table: "SeriesMovies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SeriesMovies_MovieidMovie",
                table: "SeriesMovies",
                column: "MovieidMovie");

            migrationBuilder.AddForeignKey(
                name: "FK_SeriesMovies_Movies_MovieidMovie",
                table: "SeriesMovies",
                column: "MovieidMovie",
                principalTable: "Movies",
                principalColumn: "idMovie",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
