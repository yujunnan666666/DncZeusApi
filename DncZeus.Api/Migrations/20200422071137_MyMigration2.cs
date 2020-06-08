using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DncZeus.Api.Migrations
{
    public partial class MyMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PmcDutiesecond",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    dutiesGuid = table.Column<Guid>(nullable: true),
                    creuser = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    credate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PmcDutiesecond", x => x.Guid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PmcDutiesecond");
        }
    }
}
