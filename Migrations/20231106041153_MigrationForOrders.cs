using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backendTask.Migrations
{
    /// <inheritdoc />
    public partial class MigrationForOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "OrderedDishes");

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Orders",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryTime",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Orders",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DeliveryTime",
                table: "Orders",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "OrderedDishes",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
