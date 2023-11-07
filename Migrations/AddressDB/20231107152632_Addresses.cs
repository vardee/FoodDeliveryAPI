using System;
using System.Numerics;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backendTask.Migrations.AddressDB
{
    /// <inheritdoc />
    public partial class Addresses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "as_addr_obj",
                columns: table => new
                {
                    id = table.Column<BigInteger>(type: "numeric", nullable: false),
                    objectid = table.Column<BigInteger>(type: "numeric", nullable: false),
                    objectguid = table.Column<Guid>(type: "uuid", nullable: false),
                    changeid = table.Column<BigInteger>(type: "numeric", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    typename = table.Column<string>(type: "text", nullable: false),
                    level = table.Column<string>(type: "text", nullable: false),
                    opertypeid = table.Column<int>(type: "integer", nullable: false),
                    previd = table.Column<BigInteger>(type: "numeric", nullable: false),
                    nextid = table.Column<BigInteger>(type: "numeric", nullable: false),
                    updatedate = table.Column<DateOnly>(type: "date", nullable: false),
                    startdate = table.Column<DateOnly>(type: "date", nullable: false),
                    enddate = table.Column<DateOnly>(type: "date", nullable: false),
                    isactual = table.Column<int>(type: "integer", nullable: false),
                    isactive = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_as_addr_obj", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "as_adm_hierachy",
                columns: table => new
                {
                    id = table.Column<BigInteger>(type: "numeric", nullable: false),
                    objectid = table.Column<BigInteger>(type: "numeric", nullable: false),
                    parentobjid = table.Column<BigInteger>(type: "numeric", nullable: false),
                    changeid = table.Column<BigInteger>(type: "numeric", nullable: false),
                    regioncode = table.Column<string>(type: "text", nullable: false),
                    areacode = table.Column<string>(type: "text", nullable: false),
                    citycode = table.Column<string>(type: "text", nullable: false),
                    placecode = table.Column<int>(type: "integer", nullable: false),
                    plancode = table.Column<BigInteger>(type: "numeric", nullable: false),
                    streetcode = table.Column<BigInteger>(type: "numeric", nullable: false),
                    previd = table.Column<DateOnly>(type: "date", nullable: false),
                    updatedate = table.Column<DateOnly>(type: "date", nullable: false),
                    startdate = table.Column<DateOnly>(type: "date", nullable: false),
                    enddate = table.Column<DateOnly>(type: "date", nullable: false),
                    isactive = table.Column<int>(type: "integer", nullable: false),
                    path = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_as_adm_hierachy", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "as_houses",
                columns: table => new
                {
                    id = table.Column<BigInteger>(type: "numeric", nullable: false),
                    objectid = table.Column<BigInteger>(type: "numeric", nullable: false),
                    objectguid = table.Column<Guid>(type: "uuid", nullable: false),
                    changeid = table.Column<BigInteger>(type: "numeric", nullable: false),
                    housenum = table.Column<string>(type: "text", nullable: false),
                    addnum1 = table.Column<string>(type: "text", nullable: false),
                    addnum2 = table.Column<string>(type: "text", nullable: false),
                    housetype = table.Column<string>(type: "text", nullable: false),
                    addtype1 = table.Column<string>(type: "text", nullable: false),
                    addtype2 = table.Column<string>(type: "text", nullable: false),
                    opertypeid = table.Column<int>(type: "integer", nullable: false),
                    previd = table.Column<BigInteger>(type: "numeric", nullable: false),
                    nextid = table.Column<BigInteger>(type: "numeric", nullable: false),
                    updatedate = table.Column<DateOnly>(type: "date", nullable: false),
                    startdate = table.Column<DateOnly>(type: "date", nullable: false),
                    enddate = table.Column<DateOnly>(type: "date", nullable: false),
                    isactual = table.Column<int>(type: "integer", nullable: false),
                    isactive = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_as_houses", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "as_addr_obj");

            migrationBuilder.DropTable(
                name: "as_adm_hierachy");

            migrationBuilder.DropTable(
                name: "as_houses");
        }
    }
}
