using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backendTask.Migrations
{
    /// <inheritdoc />
    public partial class Carts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_FullName",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    DishId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => new { x.DishId, x.UserId });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_Users_FullName",
                table: "Users",
                column: "FullName");
        }
    }
}
