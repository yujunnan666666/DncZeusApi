using Microsoft.EntityFrameworkCore.Migrations;

namespace DncZeus.Api.Migrations
{
    public partial class MyMigration77 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "butorder",
                table: "Secunit");

            migrationBuilder.RenameColumn(
                name: "butno",
                table: "Secunit",
                newName: "unitno");

            migrationBuilder.RenameColumn(
                name: "butname",
                table: "Secunit",
                newName: "unitname");

            migrationBuilder.AddColumn<short>(
                name: "unitorder",
                table: "Secunit",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "unitorder",
                table: "Secunit");

            migrationBuilder.RenameColumn(
                name: "unitno",
                table: "Secunit",
                newName: "butno");

            migrationBuilder.RenameColumn(
                name: "unitname",
                table: "Secunit",
                newName: "butname");

            migrationBuilder.AddColumn<short>(
                name: "butorder",
                table: "Secunit",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }
    }
}
