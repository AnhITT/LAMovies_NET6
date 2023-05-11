using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LAMovies_NET6.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    idMovie = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nameMovie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    descriptionMovie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    uriMovie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    uriMovieTrailer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    uriImg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    uriImgCover = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    subLanguageMovie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    minAgeMovie = table.Column<int>(type: "int", nullable: false),
                    qualityMovie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    timeMovie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    yearCreateMovie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    viewMovie = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.idMovie);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
