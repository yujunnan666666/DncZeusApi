using Microsoft.EntityFrameworkCore.Migrations;

namespace DncZeus.Api.Migrations
{
    public partial class MyMigration444 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "otherImages",
                table: "PmcProjectline",
                type: "varchar(200)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "otherImgSrc",
                table: "PmcProjectline",
                type: "varchar(300)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "otherImages",
                table: "PmcProjectline");

            migrationBuilder.DropColumn(
                name: "otherImgSrc",
                table: "PmcProjectline");
        }
    }
}
