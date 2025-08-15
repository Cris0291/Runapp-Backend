using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RunApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NoIndexMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_ActualPrice",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ActualPrice_ProductId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_AverageRatings",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_AverageRatings_ProductId",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Products_ActualPrice",
                table: "Products",
                column: "ActualPrice");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ActualPrice_ProductId",
                table: "Products",
                columns: new[] { "ActualPrice", "ProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_AverageRatings",
                table: "Products",
                column: "AverageRatings");

            migrationBuilder.CreateIndex(
                name: "IX_Products_AverageRatings_ProductId",
                table: "Products",
                columns: new[] { "AverageRatings", "ProductId" });
        }
    }
}
