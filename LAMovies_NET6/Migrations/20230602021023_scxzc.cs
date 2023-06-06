using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LAMovies_NET6.Migrations
{
    /// <inheritdoc />
    public partial class scxzc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OddMovies_Movies_MovieidMovie",
                table: "OddMovies");

            migrationBuilder.DropIndex(
                name: "IX_OddMovies_MovieidMovie",
                table: "OddMovies");

            migrationBuilder.DropColumn(
                name: "MovieidMovie",
                table: "OddMovies");

            migrationBuilder.CreateIndex(
                name: "IX_OddMovies_idMovie",
                table: "OddMovies",
                column: "idMovie",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OddMovies_Movies_idMovie",
                table: "OddMovies",
                column: "idMovie",
                principalTable: "Movies",
                principalColumn: "idMovie",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OddMovies_Movies_idMovie",
                table: "OddMovies");

            migrationBuilder.DropIndex(
                name: "IX_OddMovies_idMovie",
                table: "OddMovies");

            migrationBuilder.AddColumn<int>(
                name: "MovieidMovie",
                table: "OddMovies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OddMovies_MovieidMovie",
                table: "OddMovies",
                column: "MovieidMovie");

            migrationBuilder.AddForeignKey(
                name: "FK_OddMovies_Movies_MovieidMovie",
                table: "OddMovies",
                column: "MovieidMovie",
                principalTable: "Movies",
                principalColumn: "idMovie",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
