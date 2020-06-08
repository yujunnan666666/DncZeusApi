using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DncZeus.Api.Migrations
{
    public partial class MyMigration67 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeiXinCases",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    OrgID = table.Column<int>(nullable: false),
                    CaseName = table.Column<string>(type: "varchar(50)", nullable: false),
                    CaseSketch = table.Column<string>(type: "varchar(100)", nullable: true),
                    pathUrl = table.Column<string>(type: "varchar(200)", nullable: false),
                    status = table.Column<string>(type: "char(1)", nullable: true),
                    HtmlContent = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    creuser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    credate = table.Column<DateTime>(nullable: true),
                    moduser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    moddate = table.Column<DateTime>(nullable: true),
                    cfmuser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    cfmdate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeiXinCases", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "WeiXinKnowledge",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    OrgID = table.Column<int>(nullable: false),
                    Catalog = table.Column<string>(type: "varchar(20)", nullable: true),
                    CaseName = table.Column<string>(type: "varchar(50)", nullable: true),
                    CaseSketch = table.Column<string>(type: "varchar(100)", nullable: true),
                    pathUrl = table.Column<string>(type: "varchar(200)", nullable: false),
                    status = table.Column<string>(type: "char(1)", nullable: true),
                    HtmlContent = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    creuser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    credate = table.Column<DateTime>(nullable: true),
                    moduser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    moddate = table.Column<DateTime>(nullable: true),
                    cfmuser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    cfmdate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeiXinKnowledge", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "WeiXinKnowledgePower",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    knowledgeGuid = table.Column<Guid>(nullable: true),
                    usetype = table.Column<string>(type: "char(1)", nullable: true),
                    userno = table.Column<string>(type: "varchar(10)", nullable: true),
                    candown = table.Column<string>(type: "char(1)", nullable: true),
                    creuser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    credate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeiXinKnowledgePower", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "WeiXinNews",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    OrgID = table.Column<int>(nullable: false),
                    NewsName = table.Column<string>(type: "varchar(50)", nullable: true),
                    NewsSketch = table.Column<string>(type: "varchar(100)", nullable: true),
                    pathUrl = table.Column<string>(type: "varchar(200)", nullable: false),
                    isRoll = table.Column<string>(type: "char(1)", nullable: false),
                    status = table.Column<string>(type: "char(1)", nullable: true),
                    type = table.Column<string>(type: "char(1)", nullable: true),
                    HtmlContent = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    creuser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    credate = table.Column<DateTime>(nullable: true),
                    moduser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    moddate = table.Column<DateTime>(nullable: true),
                    cfmuser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    cfmdate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeiXinNews", x => x.Guid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeiXinCases");

            migrationBuilder.DropTable(
                name: "WeiXinKnowledge");

            migrationBuilder.DropTable(
                name: "WeiXinKnowledgePower");

            migrationBuilder.DropTable(
                name: "WeiXinNews");
        }
    }
}
