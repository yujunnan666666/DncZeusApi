using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DncZeus.Api.Migrations
{
    public partial class MyMigration400 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CatalogGuid",
                table: "WeiXinCases");

            migrationBuilder.AddColumn<int>(
                name: "CatalogId",
                table: "WeiXinCases",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CatalogId",
                table: "WeiXinCases");

            migrationBuilder.AddColumn<Guid>(
                name: "CatalogGuid",
                table: "WeiXinCases",
                nullable: true);
        }
    }
}
