using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnePrice.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FavouritePostUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FavoriteUserPost_UserId",
                table: "FavoriteUserPost");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteUserPost_UserId_PostId",
                table: "FavoriteUserPost",
                columns: new[] { "UserId", "PostId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FavoriteUserPost_UserId_PostId",
                table: "FavoriteUserPost");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteUserPost_UserId",
                table: "FavoriteUserPost",
                column: "UserId");
        }
    }
}
