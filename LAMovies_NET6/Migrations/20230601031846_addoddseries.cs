using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LAMovies_NET6.Migrations
{
    /// <inheritdoc />
    public partial class addoddseries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OddMovies",
                columns: table => new
                {
                    idOddMovie = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idMovie = table.Column<int>(type: "int", nullable: false),
                    urlMovie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MovieidMovie = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OddMovies", x => x.idOddMovie);
                    table.ForeignKey(
                        name: "FK_OddMovies_Movies_MovieidMovie",
                        column: x => x.MovieidMovie,
                        principalTable: "Movies",
                        principalColumn: "idMovie",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeriesMovies",
                columns: table => new
                {
                    idSeries = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idMovie = table.Column<int>(type: "int", nullable: false),
                    episodes = table.Column<int>(type: "int", nullable: false),
                    practice = table.Column<int>(type: "int", nullable: false),
                    MovieidMovie = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeriesMovies", x => x.idSeries);
                    table.ForeignKey(
                        name: "FK_SeriesMovies_Movies_MovieidMovie",
                        column: x => x.MovieidMovie,
                        principalTable: "Movies",
                        principalColumn: "idMovie",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OddMovies_MovieidMovie",
                table: "OddMovies",
                column: "MovieidMovie");

            migrationBuilder.CreateIndex(
                name: "IX_SeriesMovies_MovieidMovie",
                table: "SeriesMovies",
                column: "MovieidMovie");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OddMovies");

            migrationBuilder.DropTable(
                name: "SeriesMovies");
        }
    }
}
