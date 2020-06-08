using Microsoft.EntityFrameworkCore.Migrations;

namespace DncZeus.Api.Migrations
{
    public partial class MyMigration112 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MM",
                table: "ManFactoryMonPlan");

            migrationBuilder.RenameColumn(
                name: "outqty",
                table: "ManFactoryMonPlan",
                newName: "planput9");

            migrationBuilder.AddColumn<decimal>(
                name: "planput1",
                table: "ManFactoryMonPlan",
                type: "decimal(12,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "planput10",
                table: "ManFactoryMonPlan",
                type: "decimal(12,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "planput11",
                table: "ManFactoryMonPlan",
                type: "decimal(12,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "planput12",
                table: "ManFactoryMonPlan",
                type: "decimal(12,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "planput2",
                table: "ManFactoryMonPlan",
                type: "decimal(12,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "planput3",
                table: "ManFactoryMonPlan",
                type: "decimal(12,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "planput4",
                table: "ManFactoryMonPlan",
                type: "decimal(12,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "planput5",
                table: "ManFactoryMonPlan",
                type: "decimal(12,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "planput6",
                table: "ManFactoryMonPlan",
                type: "decimal(12,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "planput7",
                table: "ManFactoryMonPlan",
                type: "decimal(12,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "planput8",
                table: "ManFactoryMonPlan",
                type: "decimal(12,4)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "planput1",
                table: "ManFactoryMonPlan");

            migrationBuilder.DropColumn(
                name: "planput10",
                table: "ManFactoryMonPlan");

            migrationBuilder.DropColumn(
                name: "planput11",
                table: "ManFactoryMonPlan");

            migrationBuilder.DropColumn(
                name: "planput12",
                table: "ManFactoryMonPlan");

            migrationBuilder.DropColumn(
                name: "planput2",
                table: "ManFactoryMonPlan");

            migrationBuilder.DropColumn(
                name: "planput3",
                table: "ManFactoryMonPlan");

            migrationBuilder.DropColumn(
                name: "planput4",
                table: "ManFactoryMonPlan");

            migrationBuilder.DropColumn(
                name: "planput5",
                table: "ManFactoryMonPlan");

            migrationBuilder.DropColumn(
                name: "planput6",
                table: "ManFactoryMonPlan");

            migrationBuilder.DropColumn(
                name: "planput7",
                table: "ManFactoryMonPlan");

            migrationBuilder.DropColumn(
                name: "planput8",
                table: "ManFactoryMonPlan");

            migrationBuilder.RenameColumn(
                name: "planput9",
                table: "ManFactoryMonPlan",
                newName: "outqty");

            migrationBuilder.AddColumn<string>(
                name: "MM",
                table: "ManFactoryMonPlan",
                type: "varchar(10)",
                nullable: true);
        }
    }
}
