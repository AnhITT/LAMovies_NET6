using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LAMovies_NET6.Migrations
{
    /// <inheritdoc />
    public partial class pricings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPricing_Pricing_PricingidPricing",
                table: "UserPricing");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPricing_Users_UserId",
                table: "UserPricing");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPricing",
                table: "UserPricing");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pricing",
                table: "Pricing");

            migrationBuilder.RenameTable(
                name: "UserPricing",
                newName: "UserPricings");

            migrationBuilder.RenameTable(
                name: "Pricing",
                newName: "Pricings");

            migrationBuilder.RenameIndex(
                name: "IX_UserPricing_UserId",
                table: "UserPricings",
                newName: "IX_UserPricings_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPricing_PricingidPricing",
                table: "UserPricings",
                newName: "IX_UserPricings_PricingidPricing");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserPricings",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPricings",
                table: "UserPricings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pricings",
                table: "Pricings",
                column: "idPricing");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pricings",
                table: "Pricings");

            migrationBuilder.RenameTable(
                name: "UserPricings",
                newName: "UserPricing");

            migrationBuilder.RenameTable(
                name: "Pricings",
                newName: "Pricing");

            migrationBuilder.RenameIndex(
                name: "IX_UserPricings_UserId",
                table: "UserPricing",
                newName: "IX_UserPricing_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPricings_PricingidPricing",
                table: "UserPricing",
                newName: "IX_UserPricing_PricingidPricing");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserPricing",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPricing",
                table: "UserPricing",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pricing",
                table: "Pricing",
                column: "idPricing");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPricing_Pricing_PricingidPricing",
                table: "UserPricing",
                column: "PricingidPricing",
                principalTable: "Pricing",
                principalColumn: "idPricing",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPricing_Users_UserId",
                table: "UserPricing",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
