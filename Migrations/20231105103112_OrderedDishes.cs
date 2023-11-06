using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backendTask.Migrations
{
    /// <inheritdoc />
    public partial class OrderedDishes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderedDishes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    DishId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    OrderUserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderedDishes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderedDishes_Dishes_DishId",
                        column: x => x.DishId,
                        principalTable: "Dishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderedDishes_Orders_OrderId_OrderUserId",
                        columns: x => new { x.OrderId, x.OrderUserId },
                        principalTable: "Orders",
                        principalColumns: new[] { "Id", "UserId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderedDishes_DishId",
                table: "OrderedDishes",
                column: "DishId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderedDishes_OrderId_OrderUserId",
                table: "OrderedDishes",
                columns: new[] { "OrderId", "OrderUserId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderedDishes");
        }
    }
}
