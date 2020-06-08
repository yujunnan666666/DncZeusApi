using Microsoft.EntityFrameworkCore.Migrations;

namespace DncZeus.Api.Migrations
{
    public partial class MyMigration123 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "worDate",
                table: "ManWorkshopOutLine",
                newName: "workDate");

            migrationBuilder.AlterColumn<string>(
                name: "remark",
                table: "ManWorkshopOutLine",
                type: "varchar(200)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "workDate",
                table: "ManWorkshopOutLine",
                newName: "worDate");

            migrationBuilder.AlterColumn<string>(
                name: "remark",
                table: "ManWorkshopOutLine",
                type: "varchar(200)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldNullable: true);
        }
    }
}
