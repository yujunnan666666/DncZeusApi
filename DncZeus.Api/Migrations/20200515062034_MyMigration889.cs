using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DncZeus.Api.Migrations
{
    public partial class MyMigration889 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "unitcode",
                table: "PmcProjectitem",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Secunit",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    butno = table.Column<string>(type: "varchar(20)", nullable: true),
                    butname = table.Column<string>(type: "varchar(20)", nullable: true),
                    butorder = table.Column<short>(type: "smallint", nullable: false),
                    enabled = table.Column<string>(type: "char(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Secunit", x => x.Guid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Secunit");

            migrationBuilder.DropColumn(
                name: "unitcode",
                table: "PmcProjectitem");
        }
    }
}
