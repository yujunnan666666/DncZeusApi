using Microsoft.EntityFrameworkCore.Migrations;

namespace DncZeus.Api.Migrations
{
    public partial class MyMigration68 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "pathUrl",
                table: "WeiXinNews",
                type: "varchar(200)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)");

            migrationBuilder.AlterColumn<string>(
                name: "isRoll",
                table: "WeiXinNews",
                type: "char(1)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "pathUrl",
                table: "WeiXinNews",
                type: "varchar(200)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "isRoll",
                table: "WeiXinNews",
                type: "char(1)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(1)",
                oldNullable: true);
        }
    }
}
