using Microsoft.EntityFrameworkCore.Migrations;

namespace DncZeus.Api.Migrations
{
    public partial class MyMigration51 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "utype",
                table: "Secuser",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(1)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "utype",
                table: "Secuser",
                type: "char(1)",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
