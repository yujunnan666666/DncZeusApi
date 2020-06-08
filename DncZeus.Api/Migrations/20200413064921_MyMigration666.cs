using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DncZeus.Api.Migrations
{
    public partial class MyMigration666 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PmcProjectplanday",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    projectsGuid = table.Column<Guid>(nullable: true),
                    dutiesGuid = table.Column<Guid>(nullable: true),
                    prkeepdate = table.Column<byte>(type: "tinyint", nullable: false),
                    prdodate = table.Column<byte>(type: "tinyint", nullable: false),
                    keepdate = table.Column<byte>(type: "tinyint", nullable: false),
                    dodate = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PmcProjectplanday", x => x.Guid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PmcProjectplanday");
        }
    }
}
