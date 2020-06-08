using Microsoft.EntityFrameworkCore.Migrations;

namespace DncZeus.Api.Migrations
{
    public partial class MyMigration22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "icon",
                table: "Secsys",
                type: "varchar(128)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "icon",
                table: "Secprg",
                type: "varchar(128)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "icon",
                table: "Secfun",
                type: "varchar(128)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(50)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "icon",
                table: "Secsys",
                type: "char(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(128)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "icon",
                table: "Secprg",
                type: "char(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(128)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "icon",
                table: "Secfun",
                type: "char(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(128)",
                oldNullable: true);
        }
    }
}
