using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DncZeus.Api.Migrations
{
    public partial class MyMigration28 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "procreDate",
                table: "PmcProjects",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "itype",
                table: "PmcDuties",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "procreDate",
                table: "PmcProjects");

            migrationBuilder.DropColumn(
                name: "itype",
                table: "PmcDuties");
        }
    }
}
