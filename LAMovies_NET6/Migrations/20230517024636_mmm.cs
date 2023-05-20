using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LAMovies_NET6.Migrations
{
    /// <inheritdoc />
    public partial class mmm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPricings_Pricings_PricingidPricing",
                table: "UserPricings");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPricings_Users_UserId",
                table: "UserPricings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPricings",
                table: "UserPricings");

            migrationBuilder.DropIndex(
                name: "IX_UserPricings_PricingidPricing",
                table: "UserPricings");

            migrationBuilder.DropIndex(
                name: "IX_UserPricings_UserId",
                table: "UserPricings");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserPricings");

            migrationBuilder.DropColumn(
                name: "PricingidPricing",
                table: "UserPricings");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserPricings");

            migrationBuilder.AlterColumn<string>(
                name: "idUser",
                table: "UserPricings",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPricings",
                table: "UserPricings",
                columns: new[] { "idUser", "idPricing" });

            migrationBuilder.CreateIndex(
                name: "IX_UserPricings_idPricing",
                table: "UserPricings",
                column: "idPricing");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPricings_Pricings_idPricing",
                table: "UserPricings",
                column: "idPricing",
                principalTable: "Pricings",
                principalColumn: "idPricing",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPricings_Users_idUser",
                table: "UserPricings",
                column: "idUser",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPricings_Pricings_idPricing",
                table: "UserPricings");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPricings_Users_idUser",
                table: "UserPricings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPricings",
                table: "UserPricings");

            migrationBuilder.DropIndex(
                name: "IX_UserPricings_idPricing",
                table: "UserPricings");

            migrationBuilder.AlterColumn<string>(
                name: "idUser",
                table: "UserPricings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserPricings",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "PricingidPricing",
                table: "UserPricings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "UserPricings",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPricings",
                table: "UserPricings",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserPricings_PricingidPricing",
                table: "UserPricings",
                column: "PricingidPricing");

            migrationBuilder.CreateIndex(
                name: "IX_UserPricings_UserId",
                table: "UserPricings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPricings_Pricings_PricingidPricing",
                table: "UserPricings",
                column: "PricingidPricing",
                principalTable: "Pricings",
                principalColumn: "idPricing",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPricings_Users_UserId",
                table: "UserPricings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
