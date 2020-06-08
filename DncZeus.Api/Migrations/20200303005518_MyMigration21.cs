using Microsoft.EntityFrameworkCore.Migrations;

namespace DncZeus.Api.Migrations
{
    public partial class MyMigration21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "icon",
                table: "Secsys",
                type: "char(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "icon",
                table: "Secprg",
                type: "char(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "icon",
                table: "Secfun",
                type: "char(50)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "icon",
                table: "Secsys");

            migrationBuilder.DropColumn(
                name: "icon",
                table: "Secprg");

            migrationBuilder.DropColumn(
                name: "icon",
                table: "Secfun");
        }
    }
}
