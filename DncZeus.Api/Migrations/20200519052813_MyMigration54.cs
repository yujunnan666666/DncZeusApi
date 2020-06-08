using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DncZeus.Api.Migrations
{
    public partial class MyMigration54 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ManFactory",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    OrgID = table.Column<int>(nullable: false),
                    code = table.Column<string>(type: "varchar(20)", nullable: true),
                    name = table.Column<string>(type: "varchar(20)", nullable: true),
                    countType = table.Column<byte>(type: "tinyint", nullable: false),
                    cadre = table.Column<short>(type: "smallint", nullable: false),
                    logistic = table.Column<short>(type: "smallint", nullable: false),
                    worker = table.Column<short>(type: "smallint", nullable: false),
                    pertargetOut = table.Column<decimal>(type: "decimal(12,4)", nullable: false),
                    creuser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    credate = table.Column<DateTime>(nullable: true),
                    moduser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    moddate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManFactory", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "ManFactoryMonPlan",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    OrgID = table.Column<int>(nullable: false),
                    factoryGuid = table.Column<Guid>(nullable: true),
                    YY = table.Column<string>(type: "varchar(10)", nullable: true),
                    MM = table.Column<string>(type: "varchar(10)", nullable: true),
                    outqty = table.Column<decimal>(type: "decimal(12,4)", nullable: false),
                    creuser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    credate = table.Column<DateTime>(nullable: true),
                    moduser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    moddate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManFactoryMonPlan", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "ManFactoryWorkdate",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    OrgID = table.Column<int>(nullable: false),
                    factoryGuid = table.Column<Guid>(nullable: true),
                    wrokDate = table.Column<DateTime>(nullable: true),
                    cadre = table.Column<short>(type: "smallint", nullable: false),
                    logistic = table.Column<short>(type: "smallint", nullable: false),
                    worker = table.Column<short>(type: "smallint", nullable: false),
                    cadreLeave = table.Column<short>(type: "smallint", nullable: false),
                    logisticLeave = table.Column<short>(type: "smallint", nullable: false),
                    workerLeave = table.Column<short>(type: "smallint", nullable: false),
                    cadreRest = table.Column<short>(type: "smallint", nullable: false),
                    logisticRest = table.Column<short>(type: "smallint", nullable: false),
                    workerReste = table.Column<short>(type: "smallint", nullable: false),
                    creuser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    credate = table.Column<DateTime>(nullable: true),
                    moduser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    moddate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManFactoryWorkdate", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "ManFactoryWorkout",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    OrgID = table.Column<int>(nullable: false),
                    factoryGuid = table.Column<Guid>(nullable: true),
                    wrokDate = table.Column<DateTime>(nullable: true),
                    targetOut = table.Column<decimal>(type: "decimal(12,4)", nullable: false),
                    realityOut = table.Column<decimal>(type: "decimal(12,4)", nullable: false),
                    sumTime = table.Column<decimal>(type: "decimal(12,4)", nullable: false),
                    status = table.Column<byte>(type: "tinyint", nullable: false),
                    creuser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    credate = table.Column<DateTime>(nullable: true),
                    moduser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    moddate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManFactoryWorkout", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "ManOutSplit",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    OrgID = table.Column<int>(nullable: false),
                    factoryGuid = table.Column<Guid>(nullable: true),
                    countType = table.Column<byte>(type: "tinyint", nullable: false),
                    ordCode = table.Column<string>(type: "varchar(20)", nullable: true),
                    itemCode = table.Column<string>(type: "varchar(20)", nullable: true),
                    itemName = table.Column<string>(type: "varchar(200)", nullable: true),
                    status = table.Column<byte>(type: "tinyint", nullable: false),
                    creuser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    credate = table.Column<DateTime>(nullable: true),
                    moduser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    moddate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManOutSplit", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "ManOutSplitLine",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    workProcessGuid = table.Column<Guid>(nullable: true),
                    ProcessGuid = table.Column<Guid>(nullable: true),
                    rate = table.Column<decimal>(type: "decimal(12,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManOutSplitLine", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "ManWorkProcess",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    OrgID = table.Column<int>(nullable: false),
                    factoryGuid = table.Column<Guid>(nullable: true),
                    worCode = table.Column<string>(type: "varchar(10)", nullable: true),
                    rate = table.Column<decimal>(type: "decimal(12,4)", nullable: false),
                    isvalid = table.Column<byte>(type: "tinyint", nullable: false),
                    creuser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    credate = table.Column<DateTime>(nullable: true),
                    moduser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    moddate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManWorkProcess", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "ManWorkshopOut",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    OrgID = table.Column<int>(nullable: false),
                    factoryGuid = table.Column<Guid>(nullable: true),
                    workProcessGuid = table.Column<Guid>(nullable: true),
                    countType = table.Column<byte>(type: "tinyint", nullable: false),
                    ordCode = table.Column<string>(type: "varchar(20)", nullable: true),
                    itemCode = table.Column<string>(type: "varchar(20)", nullable: true),
                    itemName = table.Column<string>(type: "varchar(200)", nullable: true),
                    factoryPrice = table.Column<decimal>(type: "decimal(12,4)", nullable: false),
                    ordQty = table.Column<int>(nullable: false),
                    sumQty = table.Column<int>(nullable: false),
                    creuser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    credate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManWorkshopOut", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "ManWorkshopOutLine",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    workshopOutGuid = table.Column<Guid>(nullable: true),
                    worDate = table.Column<DateTime>(nullable: true),
                    qty = table.Column<decimal>(type: "decimal(12,4)", nullable: false),
                    remark = table.Column<string>(type: "varchar(200)", nullable: false),
                    creuser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    credate = table.Column<DateTime>(nullable: true),
                    moduser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    moddate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManWorkshopOutLine", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "ReportSaleplan",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    DeptName = table.Column<string>(type: "varchar(20)", nullable: true),
                    SalesMan = table.Column<string>(type: "varchar(20)", nullable: true),
                    YY = table.Column<string>(type: "varchar(10)", nullable: true),
                    MM = table.Column<string>(type: "varchar(10)", nullable: true),
                    amount = table.Column<decimal>(type: "decimal(14,2)", nullable: false),
                    creuser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    credate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportSaleplan", x => x.Guid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ManFactory");

            migrationBuilder.DropTable(
                name: "ManFactoryMonPlan");

            migrationBuilder.DropTable(
                name: "ManFactoryWorkdate");

            migrationBuilder.DropTable(
                name: "ManFactoryWorkout");

            migrationBuilder.DropTable(
                name: "ManOutSplit");

            migrationBuilder.DropTable(
                name: "ManOutSplitLine");

            migrationBuilder.DropTable(
                name: "ManWorkProcess");

            migrationBuilder.DropTable(
                name: "ManWorkshopOut");

            migrationBuilder.DropTable(
                name: "ManWorkshopOutLine");

            migrationBuilder.DropTable(
                name: "ReportSaleplan");
        }
    }
}
