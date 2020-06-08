using Microsoft.EntityFrameworkCore.Migrations;

namespace DncZeus.Api.Migrations
{
    public partial class MyMigration71 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "prgno",
                table: "Secprg",
                type: "varchar(16)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(8)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "prgno",
                table: "Secprg",
                type: "varchar(8)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(16)",
                oldNullable: true);
        }
    }
}
