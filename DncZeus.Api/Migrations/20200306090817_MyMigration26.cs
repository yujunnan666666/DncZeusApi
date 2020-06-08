using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DncZeus.Api.Migrations
{
    public partial class MyMigration26 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mismessage",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    Org = table.Column<int>(nullable: false),
                    mtype = table.Column<byte>(type: "tinyint", nullable: false),
                    SourceID = table.Column<int>(nullable: false),
                    sendUserno = table.Column<string>(type: "varchar(20)", nullable: true),
                    sendUsername = table.Column<string>(type: "varchar(20)", nullable: true),
                    receiveUserno = table.Column<string>(type: "varchar(20)", nullable: true),
                    receiveUsername = table.Column<string>(type: "varchar(20)", nullable: true),
                    message = table.Column<string>(type: "varchar(250)", nullable: true),
                    receiveTime = table.Column<DateTime>(nullable: true),
                    readStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    readTime = table.Column<DateTime>(nullable: true),
                    status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mismessage", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "PmcDeporderday",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    OrgID = table.Column<int>(nullable: false),
                    dtype = table.Column<byte>(type: "tinyint", nullable: false),
                    department = table.Column<string>(type: "varchar(20)", nullable: true),
                    day = table.Column<DateTime>(type: "date", nullable: false),
                    isrest = table.Column<byte>(type: "tinyint", nullable: false),
                    creuser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    credate = table.Column<DateTime>(nullable: true),
                    moduser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    moddate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PmcDeporderday", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "PmcDuties",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    OrgID = table.Column<int>(nullable: false),
                    Code = table.Column<string>(type: "varchar(20)", nullable: true),
                    Name = table.Column<string>(type: "varchar(300)", nullable: true),
                    department = table.Column<string>(type: "varchar(20)", nullable: true),
                    iskeep = table.Column<byte>(type: "tinyint", nullable: false),
                    keepdate = table.Column<short>(type: "smallint", nullable: false),
                    dutiesGuid = table.Column<Guid>(nullable: true),
                    dodate = table.Column<short>(type: "smallint", nullable: false),
                    status = table.Column<byte>(type: "tinyint", nullable: false),
                    remark = table.Column<string>(type: "varchar(200)", nullable: true),
                    creuser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    credate = table.Column<DateTime>(nullable: true),
                    moduser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    moddate = table.Column<DateTime>(nullable: true),
                    cfmuser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    cfmdate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PmcDuties", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "PmcJobuser",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    OrgID = table.Column<int>(nullable: false),
                    jobGuid = table.Column<Guid>(nullable: true),
                    userGuid = table.Column<Guid>(nullable: true),
                    isvalid = table.Column<byte>(type: "tinyint", nullable: false),
                    creuser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    credate = table.Column<DateTime>(nullable: true),
                    moduser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    moddate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PmcJobuser", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "PmcOrderday",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    OrgID = table.Column<int>(nullable: false),
                    dtype = table.Column<byte>(type: "tinyint", nullable: false),
                    day = table.Column<DateTime>(type: "date", nullable: false),
                    isrest = table.Column<byte>(type: "tinyint", nullable: false),
                    creuser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    credate = table.Column<DateTime>(nullable: true),
                    moduser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    moddate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PmcOrderday", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "PmcPlannote",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    projectplanGuid = table.Column<Guid>(nullable: true),
                    isfactory = table.Column<byte>(type: "tinyint", nullable: false),
                    mono = table.Column<string>(type: "varchar(20)", nullable: true),
                    factory = table.Column<string>(type: "varchar(20)", nullable: true),
                    workdate = table.Column<DateTime>(nullable: true),
                    workstatus = table.Column<byte>(type: "tinyint", nullable: false),
                    workqty = table.Column<short>(type: "smallint", nullable: false),
                    remark = table.Column<string>(type: "varchar(200)", nullable: true),
                    creuser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    credate = table.Column<DateTime>(nullable: true),
                    moduser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    moddate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PmcPlannote", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "PmcProjectAttachment",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    parentType = table.Column<byte>(type: "tinyint", nullable: false),
                    parentGuid = table.Column<Guid>(nullable: true),
                    attType = table.Column<byte>(type: "tinyint", nullable: false),
                    fileName = table.Column<string>(type: "varchar(200)", nullable: true),
                    attGuid = table.Column<Guid>(nullable: true),
                    credate = table.Column<DateTime>(nullable: true),
                    creuser = table.Column<string>(type: "nvarchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PmcProjectAttachment", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "PmcProjectitem",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    projectlineGuid = table.Column<Guid>(nullable: false),
                    itemcode = table.Column<string>(type: "varchar(20)", nullable: true),
                    itemname = table.Column<string>(type: "varchar(200)", nullable: true),
                    qty = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    reqDate = table.Column<DateTime>(nullable: true),
                    reqqty = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    purqty = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    inqty = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    status = table.Column<byte>(type: "tinyint", nullable: false),
                    remark = table.Column<string>(type: "varchar(200)", nullable: true),
                    creuser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    credate = table.Column<DateTime>(nullable: true),
                    moduser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    moddate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PmcProjectitem", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "PmcProjectline",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    projectsGuid = table.Column<Guid>(nullable: true),
                    custitemcode = table.Column<string>(type: "varchar(20)", nullable: true),
                    custitemname = table.Column<string>(type: "varchar(200)", nullable: true),
                    styleNO = table.Column<string>(type: "varchar(20)", nullable: true),
                    imageNO = table.Column<string>(type: "varchar(20)", nullable: true),
                    factory = table.Column<string>(type: "varchar(20)", nullable: true),
                    desc = table.Column<string>(type: "varchar(200)", nullable: true),
                    qty = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    price = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    reqDate = table.Column<DateTime>(nullable: true),
                    remark = table.Column<string>(type: "varchar(200)", nullable: true),
                    creuser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    credate = table.Column<DateTime>(nullable: true),
                    moduser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    moddate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PmcProjectline", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "PmcProjectplan",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    projectsGuid = table.Column<Guid>(nullable: true),
                    ptype = table.Column<byte>(type: "tinyint", nullable: false),
                    dutiesGuid = table.Column<Guid>(nullable: true),
                    curProgress = table.Column<string>(type: "varchar(300)", nullable: true),
                    iskeep = table.Column<byte>(type: "tinyint", nullable: false),
                    dodate = table.Column<short>(type: "smallint", nullable: false),
                    pbegindate = table.Column<DateTime>(nullable: true),
                    penddate = table.Column<DateTime>(nullable: true),
                    abegindate = table.Column<DateTime>(nullable: true),
                    aenddate = table.Column<DateTime>(nullable: true),
                    totalqty = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    status = table.Column<byte>(type: "tinyint", nullable: false),
                    remark = table.Column<string>(type: "varchar(200)", nullable: true),
                    isdel = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PmcProjectplan", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "PmcProjects",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    OrgID = table.Column<int>(nullable: false),
                    Code = table.Column<string>(type: "varchar(20)", nullable: true),
                    Name = table.Column<string>(type: "varchar(300)", nullable: true),
                    custcode = table.Column<string>(type: "varchar(50)", nullable: true),
                    custname = table.Column<string>(type: "varchar(200)", nullable: true),
                    qty = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    totalmoney = table.Column<decimal>(type: "decimal(14,2)", nullable: false),
                    isbatch = table.Column<byte>(type: "tinyint", nullable: false),
                    custDate = table.Column<DateTime>(nullable: true),
                    factoryDate1 = table.Column<DateTime>(nullable: true),
                    factoryDate2 = table.Column<DateTime>(nullable: true),
                    followgrade = table.Column<byte>(type: "tinyint", nullable: false),
                    status = table.Column<byte>(type: "tinyint", nullable: false),
                    diffStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    focusStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    remark = table.Column<string>(type: "varchar(200)", nullable: true),
                    creuser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    credate = table.Column<DateTime>(nullable: true),
                    moduser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    moddate = table.Column<DateTime>(nullable: true),
                    cfmuser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    cfmdate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PmcProjects", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "PmcWorknote",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    projectsGuid = table.Column<Guid>(nullable: true),
                    wtype = table.Column<byte>(type: "tinyint", nullable: false),
                    followRemark = table.Column<string>(type: "varchar(200)", nullable: true),
                    credate = table.Column<DateTime>(nullable: true),
                    creuser = table.Column<string>(type: "nvarchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PmcWorknote", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "WjgcPlanfllow",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    projectplanGuid = table.Column<Guid>(nullable: true),
                    fllowDesc = table.Column<string>(type: "varchar(200)", nullable: true),
                    isnote = table.Column<byte>(type: "tinyint", nullable: false),
                    nodes = table.Column<string>(type: "varchar(200)", nullable: true),
                    Supremo = table.Column<byte>(type: "tinyint", nullable: false),
                    workmaster = table.Column<byte>(type: "tinyint", nullable: false),
                    pmc = table.Column<byte>(type: "tinyint", nullable: false),
                    status = table.Column<byte>(type: "tinyint", nullable: false),
                    creuser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    credate = table.Column<DateTime>(nullable: true),
                    moduser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    moddate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WjgcPlanfllow", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "WjgcPlanfllowuser",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    planfllowGuid = table.Column<Guid>(nullable: true),
                    isnotice = table.Column<byte>(type: "tinyint", nullable: false),
                    userno = table.Column<byte>(type: "tinyint", nullable: false),
                    status = table.Column<byte>(type: "tinyint", nullable: false),
                    senddate = table.Column<DateTime>(nullable: true),
                    creuser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    credate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WjgcPlanfllowuser", x => x.Guid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mismessage");

            migrationBuilder.DropTable(
                name: "PmcDeporderday");

            migrationBuilder.DropTable(
                name: "PmcDuties");

            migrationBuilder.DropTable(
                name: "PmcJobuser");

            migrationBuilder.DropTable(
                name: "PmcOrderday");

            migrationBuilder.DropTable(
                name: "PmcPlannote");

            migrationBuilder.DropTable(
                name: "PmcProjectAttachment");

            migrationBuilder.DropTable(
                name: "PmcProjectitem");

            migrationBuilder.DropTable(
                name: "PmcProjectline");

            migrationBuilder.DropTable(
                name: "PmcProjectplan");

            migrationBuilder.DropTable(
                name: "PmcProjects");

            migrationBuilder.DropTable(
                name: "PmcWorknote");

            migrationBuilder.DropTable(
                name: "WjgcPlanfllow");

            migrationBuilder.DropTable(
                name: "WjgcPlanfllowuser");
        }
    }
}
