using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RatingService.Migrations
{
    /// <inheritdoc />
    public partial class RatingMigrationV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProducSizesId",
                table: "RatingProducts",
                newName: "ProductSizesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductSizesId",
                table: "RatingProducts",
                newName: "ProducSizesId");
        }
    }
}
