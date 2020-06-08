using Microsoft.EntityFrameworkCore.Migrations;

namespace DncZeus.Api.Migrations
{
    public partial class MyMigration667 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "prkeepdate",
                table: "PmcProjectplanday",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AlterColumn<short>(
                name: "prdodate",
                table: "PmcProjectplanday",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AlterColumn<short>(
                name: "keepdate",
                table: "PmcProjectplanday",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AlterColumn<short>(
                name: "dodate",
                table: "PmcProjectplanday",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "prkeepdate",
                table: "PmcProjectplanday",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<byte>(
                name: "prdodate",
                table: "PmcProjectplanday",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<byte>(
                name: "keepdate",
                table: "PmcProjectplanday",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<byte>(
                name: "dodate",
                table: "PmcProjectplanday",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");
        }
    }
}
