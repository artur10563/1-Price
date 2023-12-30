using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnePrice.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ComplaintType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Complaints",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ComplaintTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_TypeId",
                table: "Complaints",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_ComplaintTypes_TypeId",
                table: "Complaints",
                column: "TypeId",
                principalTable: "ComplaintTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_ComplaintTypes_TypeId",
                table: "Complaints");

            migrationBuilder.DropTable(
                name: "ComplaintTypes");

            migrationBuilder.DropIndex(
                name: "IX_Complaints_TypeId",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Complaints");
        }
    }
}
