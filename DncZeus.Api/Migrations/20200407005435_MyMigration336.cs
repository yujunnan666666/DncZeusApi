using Microsoft.EntityFrameworkCore.Migrations;

namespace DncZeus.Api.Migrations
{
    public partial class MyMigration336 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "wxAccount",
                table: "Secuser",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "wxId",
                table: "Secuser",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "wxMail",
                table: "Secuser",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "wxName",
                table: "Secuser",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "wxPhone",
                table: "Secuser",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "wxPwd",
                table: "Secuser",
                type: "varchar(100)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "wxAccount",
                table: "Secuser");

            migrationBuilder.DropColumn(
                name: "wxId",
                table: "Secuser");

            migrationBuilder.DropColumn(
                name: "wxMail",
                table: "Secuser");

            migrationBuilder.DropColumn(
                name: "wxName",
                table: "Secuser");

            migrationBuilder.DropColumn(
                name: "wxPhone",
                table: "Secuser");

            migrationBuilder.DropColumn(
                name: "wxPwd",
                table: "Secuser");
        }
    }
}
