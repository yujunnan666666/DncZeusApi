using Microsoft.EntityFrameworkCore.Migrations;

namespace DncZeus.Api.Migrations
{
    public partial class MyMigration170 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "workerReste",
                table: "ManFactoryWorkdate");

            migrationBuilder.AddColumn<short>(
                name: "workerRest",
                table: "ManFactoryWorkdate",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "workerRest",
                table: "ManFactoryWorkdate");

            migrationBuilder.AddColumn<short>(
                name: "workerReste",
                table: "ManFactoryWorkdate",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }
    }
}
