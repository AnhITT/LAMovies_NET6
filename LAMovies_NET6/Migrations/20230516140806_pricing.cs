﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LAMovies_NET6.Migrations
{
    /// <inheritdoc />
    public partial class pricing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pricing",
                columns: table => new
                {
                    idPricing = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    namePricing = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pricePricing = table.Column<long>(type: "bigint", nullable: false),
                    timePricing = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pricing", x => x.idPricing);
                });

            migrationBuilder.CreateTable(
                name: "UserPricing",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idUser = table.Column<int>(type: "int", nullable: false),
                    idPricing = table.Column<int>(type: "int", nullable: false),
                    PricingidPricing = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPricing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPricing_Pricing_PricingidPricing",
                        column: x => x.PricingidPricing,
                        principalTable: "Pricing",
                        principalColumn: "idPricing",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPricing_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPricing_PricingidPricing",
                table: "UserPricing",
                column: "PricingidPricing");

            migrationBuilder.CreateIndex(
                name: "IX_UserPricing_UserId",
                table: "UserPricing",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPricing");

            migrationBuilder.DropTable(
                name: "Pricing");
        }
    }
}