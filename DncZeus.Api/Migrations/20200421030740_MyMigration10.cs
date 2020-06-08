using Microsoft.EntityFrameworkCore.Migrations;

namespace DncZeus.Api.Migrations
{
    public partial class MyMigration10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "isdiff",
                table: "PmcProjects",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "isfocus",
                table: "PmcProjects",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "dotype",
                table: "PmcDuties",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isdiff",
                table: "PmcProjects");

            migrationBuilder.DropColumn(
                name: "isfocus",
                table: "PmcProjects");

            migrationBuilder.DropColumn(
                name: "dotype",
                table: "PmcDuties");
        }
    }
}
