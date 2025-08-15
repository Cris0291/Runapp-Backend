using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RunApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProductIndexMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.CreateIndex(
                name: "IX_Products_ActualPrice",
                table: "Products",
                column: "ActualPrice");

            migrationBuilder.CreateIndex(
                name: "IX_Products_AverageRatings",
                table: "Products",
                column: "AverageRatings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_ActualPrice",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_AverageRatings",
                table: "Products");

        }
    }
}
