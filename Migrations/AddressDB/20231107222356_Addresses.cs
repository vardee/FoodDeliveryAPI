using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

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
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    objectid = table.Column<long>(type: "bigint", nullable: false),
                    objectguid = table.Column<Guid>(type: "uuid", nullable: false),
                    changeid = table.Column<long>(type: "bigint", nullable: true),
                    name = table.Column<string>(type: "text", nullable: false),
                    typename = table.Column<string>(type: "text", nullable: true),
                    level = table.Column<string>(type: "text", nullable: false),
                    opertypeid = table.Column<int>(type: "integer", nullable: true),
                    previd = table.Column<long>(type: "bigint", nullable: true),
                    nextid = table.Column<long>(type: "bigint", nullable: true),
                    updatedate = table.Column<DateOnly>(type: "date", nullable: true),
                    startdate = table.Column<DateOnly>(type: "date", nullable: true),
                    enddate = table.Column<DateOnly>(type: "date", nullable: true),
                    isactual = table.Column<int>(type: "integer", nullable: true),
                    sactive = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_as_addr_obj", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "as_adm_hierachy",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    objectid = table.Column<long>(type: "bigint", nullable: true),
                    parentobjid = table.Column<long>(type: "bigint", nullable: true),
                    changeid = table.Column<long>(type: "bigint", nullable: true),
                    regioncode = table.Column<string>(type: "text", nullable: true),
                    areacode = table.Column<string>(type: "text", nullable: true),
                    citycode = table.Column<string>(type: "text", nullable: true),
                    placecode = table.Column<string>(type: "text", nullable: true),
                    plancode = table.Column<string>(type: "text", nullable: true),
                    streetcode = table.Column<string>(type: "text", nullable: true),
                    previd = table.Column<long>(type: "bigint", nullable: true),
                    nextid = table.Column<long>(type: "bigint", nullable: true),
                    updatedate = table.Column<DateOnly>(type: "date", nullable: true),
                    startdate = table.Column<DateOnly>(type: "date", nullable: true),
                    enddate = table.Column<DateOnly>(type: "date", nullable: true),
                    isactive = table.Column<int>(type: "integer", nullable: true),
                    path = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_as_adm_hierachy", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "as_houses",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    objectid = table.Column<long>(type: "bigint", nullable: false),
                    objectguid = table.Column<Guid>(type: "uuid", nullable: false),
                    changeid = table.Column<long>(type: "bigint", nullable: true),
                    housenum = table.Column<string>(type: "text", nullable: true),
                    addnum1 = table.Column<string>(type: "text", nullable: true),
                    addnum2 = table.Column<string>(type: "text", nullable: true),
                    housetype = table.Column<int>(type: "integer", nullable: true),
                    addtype1 = table.Column<int>(type: "integer", nullable: true),
                    addtype2 = table.Column<int>(type: "integer", nullable: true),
                    opertypeid = table.Column<int>(type: "integer", nullable: true),
                    orevid = table.Column<long>(type: "bigint", nullable: true),
                    nextid = table.Column<long>(type: "bigint", nullable: true),
                    updatedate = table.Column<DateOnly>(type: "date", nullable: true),
                    startdate = table.Column<DateOnly>(type: "date", nullable: true),
                    enddate = table.Column<DateOnly>(type: "date", nullable: true),
                    isactual = table.Column<int>(type: "integer", nullable: true),
                    isactive = table.Column<int>(type: "integer", nullable: true)
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
