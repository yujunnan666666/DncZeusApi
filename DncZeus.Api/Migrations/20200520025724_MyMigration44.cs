using Microsoft.EntityFrameworkCore.Migrations;

namespace DncZeus.Api.Migrations
{
    public partial class MyMigration44 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "worCode",
                table: "ManWorkProcess",
                newName: "workCode");

            migrationBuilder.AddColumn<string>(
                name: "workName",
                table: "ManWorkProcess",
                type: "varchar(30)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "workName",
                table: "ManWorkProcess");

            migrationBuilder.RenameColumn(
                name: "workCode",
                table: "ManWorkProcess",
                newName: "worCode");
        }
    }
}
