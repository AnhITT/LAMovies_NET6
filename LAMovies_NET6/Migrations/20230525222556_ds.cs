using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LAMovies_NET6.Migrations
{
    /// <inheritdoc />
    public partial class ds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovieHistorys",
                columns: table => new
                {
                    idMovie = table.Column<int>(type: "int", nullable: false),
                    idUser = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    minutes = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieHistorys", x => new { x.idUser, x.idMovie });
                    table.ForeignKey(
                        name: "FK_MovieHistorys_Movies_idMovie",
                        column: x => x.idMovie,
                        principalTable: "Movies",
                        principalColumn: "idMovie",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieHistorys_Users_idUser",
                        column: x => x.idUser,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieHistorys_idMovie",
                table: "MovieHistorys",
                column: "idMovie");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieHistorys");
        }
    }
}
