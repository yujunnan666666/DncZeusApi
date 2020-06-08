using Microsoft.EntityFrameworkCore.Migrations;

namespace DncZeus.Api.Migrations
{
    public partial class MyMigration35 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "userno",
                table: "WjgcPlanfllowuser",
                nullable: true,
                oldClrType: typeof(byte),
                oldType: "tinyint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "userno",
                table: "WjgcPlanfllowuser",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
