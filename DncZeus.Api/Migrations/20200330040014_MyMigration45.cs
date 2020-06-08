using Microsoft.EntityFrameworkCore.Migrations;

namespace DncZeus.Api.Migrations
{
    public partial class MyMigration45 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "PmcProjects",
                type: "varchar(300)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "area",
                table: "PmcProjects",
                type: "varchar(300)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "address",
                table: "PmcProjects");

            migrationBuilder.DropColumn(
                name: "area",
                table: "PmcProjects");
        }
    }
}
