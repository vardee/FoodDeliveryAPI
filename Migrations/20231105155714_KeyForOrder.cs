using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backendTask.Migrations
{
    /// <inheritdoc />
    public partial class KeyForOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderedDishes_Orders_OrderId_OrderUserId",
                table: "OrderedDishes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderedDishes",
                table: "OrderedDishes");

            migrationBuilder.DropIndex(
                name: "IX_OrderedDishes_OrderId_OrderUserId",
                table: "OrderedDishes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrderedDishes");

            migrationBuilder.DropColumn(
                name: "OrderUserId",
                table: "OrderedDishes");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "OrderedDishes");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Orders",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "Orders",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "orderTime",
                table: "Orders",
                newName: "OrderTime");

            migrationBuilder.RenameColumn(
                name: "deliveryTime",
                table: "Orders",
                newName: "DeliveryTime");

            migrationBuilder.RenameColumn(
                name: "adress",
                table: "Orders",
                newName: "Address");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderedDishes",
                table: "OrderedDishes",
                columns: new[] { "OrderId", "DishId" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderedDishes_Orders_OrderId",
                table: "OrderedDishes",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderedDishes_Orders_OrderId",
                table: "OrderedDishes");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderedDishes",
                table: "OrderedDishes");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Orders",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Orders",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "OrderTime",
                table: "Orders",
                newName: "orderTime");

            migrationBuilder.RenameColumn(
                name: "DeliveryTime",
                table: "Orders",
                newName: "deliveryTime");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Orders",
                newName: "adress");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "OrderedDishes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OrderUserId",
                table: "OrderedDishes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "OrderedDishes",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                columns: new[] { "Id", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderedDishes",
                table: "OrderedDishes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderedDishes_OrderId_OrderUserId",
                table: "OrderedDishes",
                columns: new[] { "OrderId", "OrderUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderedDishes_Orders_OrderId_OrderUserId",
                table: "OrderedDishes",
                columns: new[] { "OrderId", "OrderUserId" },
                principalTable: "Orders",
                principalColumns: new[] { "Id", "UserId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
