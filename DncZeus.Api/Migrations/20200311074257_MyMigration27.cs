using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DncZeus.Api.Migrations
{
    public partial class MyMigration27 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "jobGuid",
                table: "PmcJobuser",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "jobId",
                table: "PmcJobuser",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "jobId",
                table: "PmcJobuser");

            migrationBuilder.AlterColumn<Guid>(
                name: "jobGuid",
                table: "PmcJobuser",
                nullable: true,
                oldClrType: typeof(Guid));
        }
    }
}
