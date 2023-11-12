using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnePrice.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class User_Changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AppUser_AuthorId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AppUser_AuthorId",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUser",
                table: "AppUser");

            migrationBuilder.RenameTable(
                name: "AppUser",
                newName: "Users");

            migrationBuilder.RenameIndex(
                name: "IX_AppUser_Email",
                table: "Users",
                newName: "IX_Users_Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Users_AuthorId",
                table: "Comment",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_AuthorId",
                table: "Posts",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Users_AuthorId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_AuthorId",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "AppUser");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Email",
                table: "AppUser",
                newName: "IX_AppUser_Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUser",
                table: "AppUser",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AppUser_AuthorId",
                table: "Comment",
                column: "AuthorId",
                principalTable: "AppUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AppUser_AuthorId",
                table: "Posts",
                column: "AuthorId",
                principalTable: "AppUser",
                principalColumn: "Id");
        }
    }
}
